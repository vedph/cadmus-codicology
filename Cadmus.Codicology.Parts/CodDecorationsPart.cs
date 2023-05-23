using Cadmus.Core;
using Cadmus.Refs.Bricks;
using Fusi.Tools.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// Manuscript's decorations description part.
/// <para>Tag: <c>it.vedph.codicology.decorations</c>.</para>
/// </summary>
[Tag("it.vedph.codicology.decorations")]
public sealed class CodDecorationsPart : PartBase
{
    /// <summary>
    /// Gets or sets the decorations.
    /// </summary>
    public List<CodDecoration> Decorations { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MsDecorationsPart"/>
    /// class.
    /// </summary>
    public CodDecorationsPart()
    {
        Decorations = new List<CodDecoration>();
    }

    /// <summary>
    /// Get all the key=value pairs (pins) exposed by the implementor.
    /// </summary>
    /// <param name="item">The optional item. The item with its parts
    /// can optionally be passed to this method for those parts requiring
    /// to access further data.</param>
    /// <returns>The pins: <c>tot-count</c> and a collection of pins with
    /// these keys: <c>type-X-count</c>, <c>subject-X-count</c>,
    /// <c>color-X-count</c>, (all the keys with X are filtered, with digits),
    /// <c>artist-id</c> (filtered, with digits).
    /// </returns>
    public override IEnumerable<DataPin> GetDataPins(IItem? item = null)
    {
        DataPinBuilder builder = new(
            DataPinHelper.DefaultFilter);

        builder.Set("tot", Decorations?.Count ?? 0, false);

        if (Decorations?.Count > 0)
        {
            foreach (CodDecoration decoration in Decorations)
            {
                // eid
                if (!string.IsNullOrEmpty(decoration.Eid))
                    builder.AddValue("eid", decoration.Eid);

                // chronotopes
                if (decoration.Chronotopes?.Count > 0)
                {
                    foreach (AssertedChronotope c in decoration.Chronotopes)
                    {
                        if (c.Date is not null)
                            builder.AddValue("date-value", c.Date.GetSortValue());
                        if (!string.IsNullOrEmpty(c.Place?.Value))
                        {
                            builder.AddValue("place", c.Place.Value,
                                filter: true, filterOptions: true);
                        }
                    }
                }

                if (decoration?.Elements?.Count > 0)
                {
                    foreach (CodDecorationElement element in decoration.Elements)
                    {
                        builder.Increase(
                            DataPinHelper.DefaultFilter.Apply(element.Type, true),
                            false, "type-");

                        builder.Increase(
                            DataPinHelper.DefaultFilter.Apply(element.Subject, true),
                            false,
                            "subject-");

                        foreach (string color in element.Colors)
                        {
                            builder.Increase(
                                DataPinHelper.DefaultFilter.Apply(color, true),
                                false,
                                "color-");
                        }
                    }
                }

                if (decoration?.Artists?.Count > 0)
                {
                    foreach (var artist in decoration.Artists)
                    {
                        builder.AddValue("artist-name", artist.Name,
                            filter: true, filterOptions: true);

                        if (artist.Ids != null)
                        {
                            builder.AddValues("artist-id",
                                artist.Ids.Where(id => id.Target?.Gid != null)
                                          .Select(id => id.Target!.Gid));
                        }
                    }
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
        return new List<DataPinDefinition>(new[]
        {
            new DataPinDefinition(DataPinValueType.Integer,
                "tot-count",
                "The total count of decorations."),
            new DataPinDefinition(DataPinValueType.Integer,
                "type-{TYPE}-count",
                "The count of each decoration's type."),
            new DataPinDefinition(DataPinValueType.Integer,
                "subject-{SUBJECT}-count",
                "The count of each decoration's subject."),
            new DataPinDefinition(DataPinValueType.Integer,
                "color-{COLOR}-count",
                "The count of each decoration's colors."),
            new DataPinDefinition(DataPinValueType.String,
               "eid",
               "The decorations IDs.",
               "M"),
            new DataPinDefinition(DataPinValueType.String,
                "artist-name",
                "The list of artists names.",
                "Mf"),
            new DataPinDefinition(DataPinValueType.String,
                "artist-id",
                "The list of decorations artists IDs.",
                "M"),
            new DataPinDefinition(DataPinValueType.Decimal,
                "date-value",
                "The list of decorations sortable date values",
                "M")
        });
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

        sb.Append("[CodDecorations]");

        if (Decorations?.Count > 0)
        {
            sb.Append(' ');
            int n = 0;
            foreach (var entry in Decorations)
            {
                if (++n > 3) break;
                if (n > 1) sb.Append("; ");
                sb.Append(entry);
            }
            if (Decorations.Count > 3)
                sb.Append("...(").Append(Decorations.Count).Append(')');
        }

        return sb.ToString();
    }
}
