using Cadmus.Core;
using Fusi.Tools.Configuration;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// Manuscript's shelfmark(s). Among these, the shelfmark with an empty tag
/// is the default (current) shelfmark. Other shelfmarks may be added for
/// historical reasons, and should have a tag.
/// <para>Tag: <c>it.vedph.codicology.shelfmarks</c>.</para>
/// </summary>
[Tag("it.vedph.codicology.shelfmarks")]
public sealed class CodShelfmarksPart : PartBase
{
    /// <summary>
    /// Gets or sets the shelfmarks.
    /// </summary>
    public List<CodShelfmark> Shelfmarks { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodShelfmarksPart"/>
    /// class.
    /// </summary>
    public CodShelfmarksPart()
    {
        Shelfmarks = [];
    }

    /// <summary>
    /// Get all the key=value pairs (pins) exposed by the implementor.
    /// </summary>
    /// <param name="item">The optional item. The item with its parts
    /// can optionally be passed to this method for those parts requiring
    /// to access further data.</param>
    /// <returns>The pins: <c>tot-count</c>, and lists of pins with keys:
    /// <c>tag-TAG-count</c>, <c>library</c> (filtered, with digits),
    /// <c>city</c> (filtered).
    /// </returns>
    public override IEnumerable<DataPin> GetDataPins(IItem? item = null)
    {
        DataPinBuilder builder = new(
            DataPinHelper.DefaultFilter);

        builder.Set("tot", Shelfmarks?.Count ?? 0, false);

        if (Shelfmarks?.Count > 0)
        {
            foreach (CodShelfmark shelfmark in Shelfmarks)
            {
                builder.Increase(shelfmark.Tag, false, "tag-");

                if (!string.IsNullOrEmpty(shelfmark.Library))
                {
                    builder.AddValue("library",
                        shelfmark.Library, filter: true, filterOptions: true);
                }

                if (!string.IsNullOrEmpty(shelfmark.City))
                    builder.AddValue("city", shelfmark.City, filter: true);

                if (!string.IsNullOrEmpty(shelfmark.Location))
                {
                    builder.AddValue("location", shelfmark.Location);
                }
            }
        }

        return builder.Build(this);
    }

    /// <summary>
    /// Gets the definitions of data pins used by the implementor.
    /// </summary>
    /// <returns>Data pins definitions.</returns>
    public override IList<DataPinDefinition> GetDataPinDefinitions()
    {
        return [.. new[]
        {
            new DataPinDefinition(DataPinValueType.Integer,
                "tot-count",
                "The total count of shelfmarks."),
            new DataPinDefinition(DataPinValueType.Integer,
                "tag-{TAG}-count",
                "The counts for each shelfmark's tag."),
            new DataPinDefinition(DataPinValueType.String,
                "library",
                "The list of libraries from the shelfmarks.",
                "Mf"),
            new DataPinDefinition(DataPinValueType.String,
                "city",
                "The list of cities from the shelfmarks.",
                "MF"),
            new DataPinDefinition(DataPinValueType.String,
                "location",
                "The list of locations from the shelfmarks.",
                "M")
        }];
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

        sb.Append("[CodShelfmarks]");

        if (Shelfmarks?.Count > 0)
        {
            int n = 0;
            foreach (CodShelfmark shelfmark in Shelfmarks)
            {
                if (++n > 3) break;
                if (n > 1) sb.Append("; ");
                sb.Append(shelfmark);
            }
        }

        return sb.ToString();
    }
}
