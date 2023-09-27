// ReSharper disable UnusedMember.Global

namespace HamedStack.Localization.MultiLingual;

/// <summary>
/// Represents a base entity translation.
/// </summary>
public class MultiLingualEntityTranslation : IMultiLingualEntityTranslation
{
    /// <summary>
    /// Gets or sets the language for this translation.
    /// </summary>
    public string Language { get; set; } = null!;
}