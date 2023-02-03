using Cadmus.Refs.Bricks;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// Information about palimpsest sheets in a manuscript.
/// </summary>
public class CodPalimpsest
{
    /// <summary>
    /// Gets or sets the range.
    /// </summary>
    public CodLocationRange? Range { get; set; }

    /// <summary>
    /// Gets or sets the place/time of reuse.
    /// </summary>
    public AssertedChronotope? Chronotope { get; set; }

    /// <summary>
    /// Gets or sets an optional note.
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
        return Range != null ? Range.ToString() : base.ToString()!;
    }
}
