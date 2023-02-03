using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Cadmus.Core;
using Cadmus.Mat.Bricks;
using Fusi.Tools.Configuration;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// Manuscript's layouts part.
/// <para>Tag: <c>it.vedph.codicology.layouts</c>.</para>
/// </summary>
[Tag("it.vedph.codicology.layouts")]
public sealed class CodLayoutsPart : PartBase
{
    /// <summary>
    /// Gets or sets the layouts.
    /// </summary>
    public List<CodLayout> Layouts { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodLayoutsPart"/> class.
    /// </summary>
    public CodLayoutsPart()
    {
        Layouts = new List<CodLayout>();
    }

    /// <summary>
    /// Get all the key=value pairs (pins) exposed by the implementor.
    /// </summary>
    /// <param name="item">The optional item. The item with its parts
    /// can optionally be passed to this method for those parts requiring
    /// to access further data.</param>
    /// <returns>The pins: <c>tot-count</c> and a collection of pins with
    /// these keys: <c>cols</c>, <c>ruling</c>, <c>derolez</c>,
    /// <c>pricking</c>, <c>d.TAG</c> for dimensions.</returns>
    public override IEnumerable<DataPin> GetDataPins(IItem? item = null)
    {
        DataPinBuilder builder = new();

        builder.Set("tot", Layouts?.Count ?? 0, false);

        if (Layouts?.Count > 0)
        {
            foreach (CodLayout layout in Layouts)
            {
                builder.AddValue("cols", layout.ColumnCount);

                if (!string.IsNullOrEmpty(layout.RulingTechnique))
                    builder.AddValue("ruling", layout.RulingTechnique);

                if (!string.IsNullOrEmpty(layout.Derolez))
                    builder.AddValue("derolez", layout.Derolez);

                if (!string.IsNullOrEmpty(layout.Pricking))
                    builder.AddValue("pricking", layout.Pricking);

                if (layout.Dimensions?.Count > 0)
                {
                    foreach (PhysicalDimension dim in layout.Dimensions)
                    {
                        builder.AddValue($"d.{dim.Tag ?? "-"}",
                            dim.Value.ToString("00.0",
                            CultureInfo.InvariantCulture));
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
               "The total count of entries."),
            new DataPinDefinition(DataPinValueType.Integer,
                "cols",
                "The number of columns.",
                "M"),
            new DataPinDefinition(DataPinValueType.String,
                "ruling",
                "The ruling technique.",
                "M"),
            new DataPinDefinition(DataPinValueType.String,
                "derolez",
                "The Derolez classification.",
                "M"),
            new DataPinDefinition(DataPinValueType.String,
                "pricking",
                "The pricking type.",
                "M"),
            new DataPinDefinition(DataPinValueType.Decimal,
                "d.{TAG}",
                "The list of dimensions grouped by their tag, " +
                "with format 00.0.",
                "M"),
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

        sb.Append("[CodLayouts]");

        if (Layouts?.Count > 0)
        {
            sb.Append(' ');
            int n = 0;
            foreach (var entry in Layouts)
            {
                if (++n > 3) break;
                if (n > 1) sb.Append("; ");
                sb.Append(entry);
            }
            if (Layouts.Count > 3)
                sb.Append("...(").Append(Layouts.Count).Append(')');
        }

        return sb.ToString();
    }
}
