// ReSharper disable UnusedMember.Global

namespace HamedStack.Localization.MultiLingual;

/// <summary>
/// Represents a translation associated with an entity.
/// </summary>
public interface IMultiLingualEntityTranslation
{
    /// <summary>
    /// Gets or sets the language for this translation.
    /// </summary>
    string Language { get; set; }
}