using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// Manuscript's watermarks part.
    /// <para>Tag: <c>it.vedph.codicology.watermarks</c>.</para>
    /// </summary>
    [Tag("it.vedph.codicology.watermarks")]
    public sealed class CodWatermarksPart : PartBase
    {
        /// <summary>
        /// Gets or sets the watermarks.
        /// </summary>
        public List<CodWatermark> Watermarks { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodWatermarksPart"/> class.
        /// </summary>
        public CodWatermarksPart()
        {
            Watermarks = new List<CodWatermark>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c> and a collection of pins with
        /// these keys: <c>id</c>, <c>place</c>, <c>date-value</c>.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem? item = null)
        {
            DataPinBuilder builder = new(
                DataPinHelper.DefaultFilter);

            builder.Set("tot", Watermarks?.Count ?? 0, false);

            if (Watermarks?.Count > 0)
            {
                foreach (CodWatermark watermark in Watermarks)
                {
                    builder.AddValue("name", watermark.Name, filter: true,
                        filterOptions: true);
                    if (watermark.Ids?.Count > 0)
                        builder.AddValues("id", watermark.Ids.Select(i => i.Value!));
                    if (!string.IsNullOrEmpty(watermark.Chronotope?.Place?.Value))
                        builder.AddValue("place", watermark.Chronotope.Place.Value);
                    if (watermark.Chronotope?.Date is not null)
                    {
                        builder.AddValue("date-value",
                            watermark.Chronotope.Date.GetSortValue());
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
                   "The total count of watermarks."),
                new DataPinDefinition(DataPinValueType.String,
                   "name",
                   "The watermarks names.",
                   "MF"),
                new DataPinDefinition(DataPinValueType.String,
                   "id",
                   "The watermarks IDs.",
                   "M"),
                new DataPinDefinition(DataPinValueType.String,
                   "place",
                   "The watermarks places.",
                   "M"),
                new DataPinDefinition(DataPinValueType.Decimal,
                   "date-value",
                   "The watermarks date values.",
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

            sb.Append("[CodWatermarks]");

            if (Watermarks?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (var entry in Watermarks)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(entry);
                }
                if (Watermarks.Count > 3)
                    sb.Append("...(").Append(Watermarks.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
