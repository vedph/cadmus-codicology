using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// Manuscript's numbering system(s) part.
    /// Tag: <c>it.vedph.codicology.numberings</c>.
    /// </summary>
    /// <seealso cref="PartBase" />
    [Tag("it.vedph.codicology.numberings")]
    public sealed class CodNumberingsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the numberings.
        /// </summary>
        public List<CodNumbering> Numberings { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodNumberingPart"/> class.
        /// </summary>
        public CodNumberingsPart()
        {
            Numberings = new List<CodNumbering>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c> and a collection of pins with
        /// these keys: ....</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder();

            builder.Set("tot", Numberings?.Count ?? 0, false);

            if (Numberings?.Count > 0)
            {
                foreach (CodNumbering numbering in Numberings)
                {
                    builder.AddValue("eid", numbering.Eid);
                    builder.AddValue("system", numbering.System);
                    builder.AddValue("technique", numbering.Technique);
                    builder.AddValue("position", numbering.Position);
                    if (numbering.Colors?.Count > 0)
                        builder.AddValues("color", numbering.Colors);
                    if (numbering.Date != null)
                        builder.AddValue("date-value", numbering.Date.GetSortValue());
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
                    "The numbering entity IDs.",
                    "M"),
                new DataPinDefinition(DataPinValueType.String,
                    "system",
                    "The numbering systems.",
                    "M"),
                new DataPinDefinition(DataPinValueType.String,
                    "technique",
                    "The numbering techniques.",
                    "M"),
                new DataPinDefinition(DataPinValueType.String,
                    "position",
                    "The numbering positions.",
                    "M"),
                new DataPinDefinition(DataPinValueType.String,
                    "color",
                    "The numbering colors.",
                    "M"),
                new DataPinDefinition(DataPinValueType.Decimal,
                    "date-value",
                    "The date values of numbering systems.",
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

            sb.Append("[CodNumberings]");

            if (Numberings?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (CodNumbering numbering in Numberings)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(numbering);
                }
                if (Numberings.Count > 3)
                    sb.Append("...(").Append(Numberings.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
