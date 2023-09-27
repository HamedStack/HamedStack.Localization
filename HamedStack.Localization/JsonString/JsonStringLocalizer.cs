// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace HamedStack.Localization.JsonString;

/// <summary>
/// Provides localized strings based on JSON files, with caching capability using distributed cache.
/// </summary>
public class JsonStringLocalizer : IStringLocalizer
{
    private readonly IDistributedCache _cache;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonStringLocalizer"/> class.
    /// </summary>
    /// <param name="cache">The distributed cache used to store localized strings.</param>
    public JsonStringLocalizer(IDistributedCache cache)
    {
        _cache = cache;
    }

    /// <summary>
    /// Gets the localized string for the given name.
    /// </summary>
    /// <param name="name">The name of the localized string.</param>
    /// <returns>A localized string.</returns>
    public LocalizedString this[string name]
    {
        get
        {
            var value = GetString(name);
            return new LocalizedString(name, value ?? name, value == null);
        }
    }

    /// <summary>
    /// Gets the formatted localized string using the given name and arguments.
    /// </summary>
    /// <param name="name">The name of the localized string.</param>
    /// <param name="arguments">The arguments to format the localized string with.</param>
    /// <returns>A formatted localized string.</returns>
    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var actualValue = this[name];
            return !actualValue.ResourceNotFound
                ? new LocalizedString(name, string.Format(actualValue.Value, arguments), false)
                : actualValue;
        }
    }

    /// <summary>
    /// Retrieves all the localized strings for the current culture.
    /// </summary>
    /// <param name="includeParentCultures">This parameter is ignored in this implementation.</param>
    /// <returns>An enumeration that contains all the localized strings.</returns>
    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
        if (!File.Exists(filePath))
        {
            yield break;
        }

        var json = File.ReadAllText(filePath);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        if (dictionary == null) yield break;
        foreach (var pair in dictionary)
        {
            yield return new LocalizedString(pair.Key, pair.Value, false);
        }
    }

    /// <summary>
    /// Retrieves the localized string for the given key.
    /// </summary>
    /// <param name="key">The key of the localized string.</param>
    /// <returns>A localized string if found; otherwise, null.</returns>
    private string? GetString(string key)
    {
        var relativeFilePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
        var fullFilePath = Path.GetFullPath(relativeFilePath);
        if (!File.Exists(fullFilePath)) return default;
        var cacheKey = $"locale_{Thread.CurrentThread.CurrentCulture.Name}_{key}";
        var cacheValue = _cache.GetString(cacheKey);
        if (!string.IsNullOrEmpty(cacheValue)) return cacheValue;
        var result = GetValueFromJson(key, fullFilePath);
        if (!string.IsNullOrEmpty(result)) _cache.SetString(cacheKey, result);
        return result;
    }

    /// <summary>
    /// Retrieves the localized string value from the JSON file based on the given property name.
    /// </summary>
    /// <param name="propertyName">The property name of the localized string.</param>
    /// <param name="filePath">The path to the JSON file.</param>
    /// <returns>A localized string value if found; otherwise, null.</returns>
    private static string? GetValueFromJson(string propertyName, string filePath)
    {
        var json = File.ReadAllText(filePath);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        if (dictionary == null) return default;
        dictionary.TryGetValue(propertyName, out var value);
        return value;
    }
}