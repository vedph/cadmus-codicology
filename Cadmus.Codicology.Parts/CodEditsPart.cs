using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Configuration;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// Manuscript's edit events part.
/// <para>Tag: <c>it.vedph.codicology.edits</c>.</para>
/// </summary>
[Tag("it.vedph.codicology.edits")]
public sealed class CodEditsPart : PartBase, IHasText
{
    /// <summary>
    /// Gets or sets the entries.
    /// </summary>
    public List<CodEdit> Edits { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodEditsPart"/> class.
    /// </summary>
    public CodEditsPart()
    {
        Edits = new List<CodEdit>();
    }

    /// <summary>
    /// Gets the text from all the edit operations.
    /// </summary>
    public string GetText()
    {
        return Edits?.Count > 0
            ? string.Join("\n", Edits.Select(e => e.Text))
            : string.Empty;
    }

    /// <summary>
    /// Get all the key=value pairs (pins) exposed by the implementor.
    /// </summary>
    /// <param name="item">The optional item. The item with its parts
    /// can optionally be passed to this method for those parts requiring
    /// to access further data.</param>
    /// <returns>The pins: <c>tot-count</c> and a collection of pins with
    /// these keys: <c>eid</c>, <c>type</c>, <c>technique</c>,
    /// <c>language</c>, <c>color</c>, <c>date-value</c>.</returns>
    public override IEnumerable<DataPin> GetDataPins(IItem? item = null)
    {
        DataPinBuilder builder = new();

        builder.Set("tot", Edits?.Count ?? 0, false);

        if (Edits?.Count > 0)
        {
            foreach (CodEdit edit in Edits)
            {
                builder.AddValue("eid", edit.Eid);
                builder.AddValue("type", edit.Type);
                if (edit.Techniques?.Count > 0)
                    builder.AddValues("technique", edit.Techniques);
                builder.AddValue("language", edit.Language);

                if (edit.Colors?.Count > 0)
                    builder.AddValues("color", edit.Colors);
                if (edit.Date is not null)
                    builder.AddValue("date-value", edit.Date.GetSortValue());
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
               "The total count of entries."),
            new DataPinDefinition(DataPinValueType.String,
                "eid",
                "The edits entity IDs.",
                "M"),
            new DataPinDefinition(DataPinValueType.String,
                "type",
                "The edits types.",
                "M"),
            new DataPinDefinition(DataPinValueType.String,
                "technique",
                "The edits techniques.",
                "M"),
            new DataPinDefinition(DataPinValueType.String,
                "language",
                "The edits languages.",
                "M"),
            new DataPinDefinition(DataPinValueType.String,
                "color",
                "The edits colors.",
                "M"),
            new DataPinDefinition(DataPinValueType.Decimal,
                "date-value",
                "The date values of edits.",
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

        sb.Append("[CodEdits]");

        if (Edits?.Count > 0)
        {
            sb.Append(' ');
            int n = 0;
            foreach (var entry in Edits)
            {
                if (++n > 3) break;
                if (n > 1) sb.Append("; ");
                sb.Append(entry);
            }
            if (Edits.Count > 3)
                sb.Append("...(").Append(Edits.Count).Append(')');
        }

        return sb.ToString();
    }
}
