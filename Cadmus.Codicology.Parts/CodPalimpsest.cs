using Cadmus.Refs.Bricks;
using System.Collections.Generic;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// Information about palimpsest sheets in a manuscript.
/// </summary>
public class CodPalimpsest
{
    /// <summary>
    /// Gets or sets the range.
    /// </summary>
    public List<CodLocationRange>? Ranges { get; set; }

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
        return Ranges != null ? string.Join(", ", Ranges) : base.ToString()!;
    }
}
