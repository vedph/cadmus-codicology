using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cadmus.Codicology.Parts
{
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
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder(
                DataPinHelper.DefaultFilter);

            builder.Set("tot", Decorations?.Count ?? 0, false);

            if (Decorations?.Count > 0)
            {
                foreach (CodDecoration decoration in Decorations)
                {
                    builder.Increase(
                        DataPinHelper.DefaultFilter.Apply(decoration.Type, true),
                        false, "type-");

                    if (decoration.Chronotope?.Date?.Value != null)
                    {
                        builder.AddValue("date-value",
                            decoration.Chronotope.Date.Value.GetSortValue());
                    }

                    foreach (CodDecorationElement element in decoration?.Elements)
                    {
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

                    if (decoration.ArtistIds?.Count > 0)
                    {
                        builder.AddValues("artist-id",
                            decoration.ArtistIds.Select(id => id.Value));
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
            StringBuilder sb = new StringBuilder();

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
}
