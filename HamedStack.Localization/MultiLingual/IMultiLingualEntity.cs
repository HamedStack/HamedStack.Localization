// ReSharper disable UnusedMember.Global

namespace HamedStack.Localization.MultiLingual;

/// <summary>
/// Represents an entity that supports multiple translations.
/// </summary>
/// <typeparam name="TTranslation">The type of the translation.</typeparam>
public interface IMultiLingualEntity<TTranslation> where TTranslation : IMultiLingualEntityTranslation
{
    /// <summary>
    /// Gets or sets the translations for this entity.
    /// </summary>
    ICollection<TTranslation> Translations { get; set; }
}