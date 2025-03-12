namespace Cadmus.Codicology.Parts;

/// <summary>
/// The reuse of a color in an illumination instruction.
/// </summary>
public class CodIllumColorReuse
{
    /// <summary>
    /// Gets or sets the reused color. This refers to
    /// <see cref="CodIllumInstruction.Colors"/>.
    /// </summary>
    public string Color { get; set; } = "";

    /// <summary>
    /// Gets or sets a generic note about the reuse.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return $"{Color}" + (string.IsNullOrEmpty(Note) ? "" : ": " + Note);
    }
}
