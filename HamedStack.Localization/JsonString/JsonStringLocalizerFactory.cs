// ReSharper disable IdentifierTypo
// ReSharper disable UnusedType.Global

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace HamedStack.Localization.JsonString;

/// <summary>
/// A factory responsible for producing instances of <see cref="JsonStringLocalizer"/>.
/// This factory utilizes distributed caching to efficiently manage localization strings.
/// </summary>
public class JsonStringLocalizerFactory : IStringLocalizerFactory
{
    private readonly IDistributedCache _cache;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonStringLocalizerFactory"/> class with the specified distributed cache.
    /// </summary>
    /// <param name="cache">The distributed cache to be used by the created <see cref="JsonStringLocalizer"/> instances.</param>
    public JsonStringLocalizerFactory(IDistributedCache cache)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    /// <summary>
    /// Creates an instance of the <see cref="JsonStringLocalizer"/> class using the specified resource source.
    /// </summary>
    /// <remarks>
    /// In this implementation, the <paramref name="resourceSource"/> is not utilized. 
    /// The function always produces a new instance of <see cref="JsonStringLocalizer"/> with the factory's cache.
    /// </remarks>
    /// <param name="resourceSource">The source of the resource data. Ignored in this implementation.</param>
    /// <returns>An instance of <see cref="JsonStringLocalizer"/>.</returns>
    public IStringLocalizer Create(Type resourceSource) => new JsonStringLocalizer(_cache);

    /// <summary>
    /// Creates an instance of the <see cref="JsonStringLocalizer"/> class using the specified base name and location.
    /// </summary>
    /// <remarks>
    /// In this implementation, both <paramref name="baseName"/> and <paramref name="location"/> are ignored. 
    /// The function always produces a new instance of <see cref="JsonStringLocalizer"/> with the factory's cache.
    /// </remarks>
    /// <param name="baseName">The base name of the resource. Ignored in this implementation.</param>
    /// <param name="location">The location of the resource. Ignored in this implementation.</param>
    /// <returns>An instance of <see cref="JsonStringLocalizer"/>.</returns>
    public IStringLocalizer Create(string baseName, string location) => new JsonStringLocalizer(_cache);
}