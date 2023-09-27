// ReSharper disable UnusedMember.Global

namespace HamedStack.Localization.MultiLingual;

/// <summary>
/// Represents a localized entity with translations.
/// </summary>
/// <typeparam name="TTranslation">The type of the translation.</typeparam>
public class MultiLingualEntity<TTranslation> : IMultiLingualEntity<TTranslation> where TTranslation : IMultiLingualEntityTranslation
{
    /// <summary>
    /// Gets or sets the translations for this entity.
    /// </summary>
    public ICollection<TTranslation> Translations { get; set; } = null!;
}