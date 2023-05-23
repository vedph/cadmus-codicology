using Cadmus.Mat.Bricks;
using Cadmus.Refs.Bricks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// A manuscript's watermark.
/// </summary>
public class CodWatermark
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the range of sheets used as a sample for this watermark.
    /// </summary>
    public CodLocationRange? SampleRange { get; set; }

    /// <summary>
    /// Gets or sets the ranges covered by this watermark.
    /// </summary>
    public List<CodLocationRange> Ranges { get; set; }

    /// <summary>
    /// Gets or sets the watermark IDs, usually from repertories like
    /// Briquet (https://briquet-online.at) or Piccard
    /// (https://www.piccard-online.de/struktur.php).
    /// </summary>
    public List<AssertedCompositeId> Ids { get; set; }

    /// <summary>
    /// Gets or sets the size.
    /// </summary>
    public PhysicalSize? Size { get; set; }

    /// <summary>
    /// Gets or sets the dates and/or places for this watermark.
    /// </summary>
    public List<AssertedChronotope> Chronotopes { get; set; }

    /// <summary>
    /// Gets or sets a short description of this watermark.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodWatermark"/> class.
    /// </summary>
    public CodWatermark()
    {
        Ranges = new List<CodLocationRange>();
        Chronotopes = new List<AssertedChronotope>();
        Ids = new List<AssertedCompositeId>();
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        StringBuilder sb = new();

        if (!string.IsNullOrEmpty(Name)) sb.Append(Name);
        if (Ranges?.Count > 0)
            sb.Append('@').AppendJoin(" ", Ranges);
        if (Ids?.Count > 0)
            sb.Append(": ").AppendJoin(" ", Ids.Select(i => i.Target?.Gid));

        return sb.ToString().TrimEnd();
    }
}
