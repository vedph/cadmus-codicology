using Cadmus.Core;
using Fusi.Tools.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// Codicological location ranges part. This part contains a list of location
/// ranges and a generic note.
/// <para>Tag: <c>it.vedph.codicology.location-ranges</c>.</para>
/// </summary>
[Tag("it.vedph.codicology.location-ranges")]
public sealed class CodLocationRangesPart : PartBase
{
    /// <summary>
    /// Gets or sets the ranges.
    /// </summary>
    public List<CodLocationRange> Ranges { get; set; } = [];

    /// <summary>
    /// Gets or sets a generic note.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Get all the key=value pairs (pins) exposed by the implementor.
    /// </summary>
    /// <param name="item">The optional item. The item with its parts
    /// can optionally be passed to this method for those parts requiring
    /// to access further data.</param>
    /// <returns>The pins: <c>tot-count</c> and a collection of pins with
    /// key <c>range</c> for each range.</returns>
    public override IEnumerable<DataPin> GetDataPins(IItem? item = null)
    {
        DataPinBuilder builder = new();

        builder.Set("tot", Ranges?.Count ?? 0, false);

        if (Ranges?.Count > 0)
            builder.AddValues("range", Ranges.Select(r => r.ToString()));

        return builder.Build(this);
    }

    /// <summary>
    /// Gets the definitions of data pins used by the implementor.
    /// </summary>
    /// <returns>Data pins definitions.</returns>
    public override IList<DataPinDefinition> GetDataPinDefinitions()
    {
        return new List<DataPinDefinition>(
        [
            new DataPinDefinition(DataPinValueType.Integer,
               "tot-count",
               "The total count of entries."),
            new DataPinDefinition(DataPinValueType.String,
                "range",
                "The string representation of each range.")
        ]);
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

        sb.Append("[CodLocationRanges]");

        if (Ranges?.Count > 0)
        {
            sb.Append(' ');
            int n = 0;
            foreach (CodLocationRange range in Ranges)
            {
                if (++n > 3) break;
                if (n > 1) sb.Append(' ');
                sb.Append(range);
            }
            if (Ranges.Count > 3)
                sb.Append("...(").Append(Ranges.Count).Append(')');
        }

        return sb.ToString();
    }
}
