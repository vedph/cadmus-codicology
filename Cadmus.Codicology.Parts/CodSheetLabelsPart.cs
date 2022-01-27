using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// Manuscript's sheet labels and quires part.
    /// <para>Tag: <c>it.vedph.codicology.sheet-labels</c>.</para>
    /// </summary>
    [Tag("it.vedph.codicology.sheet-labels")]
    public sealed class CodSheetLabelsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        public List<CodSheetRow> Rows { get; set; }

        /// <summary>
        /// Gets or sets the definitions of numbering columns.
        /// </summary>
        public List<CodSheetNColumnDefinition> NDefinitions { get; set; }

        /// <summary>
        /// Gets or sets the definitions of catchword columns.
        /// </summary>
        public List<CodSheetCColumnDefinition> CDefinitions { get; set; }

        /// <summary>
        /// Gets or sets the definitions of quire signature columns.
        /// </summary>
        public List<CodSheetSColumnDefinition> SDefinitions { get; set; }

        /// <summary>
        /// Gets or sets the definitions of quire register signature columns.
        /// </summary>
        public List<CodSheetRColumnDefinition> RDefinitions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodSheetLabelsPart"/>
        /// class.
        /// </summary>
        public CodSheetLabelsPart()
        {
            NDefinitions = new List<CodSheetNColumnDefinition>();
            CDefinitions = new List<CodSheetCColumnDefinition>();
            SDefinitions = new List<CodSheetSColumnDefinition>();
            RDefinitions = new List<CodSheetRColumnDefinition>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>row-count</c> and a collection of pins with
        /// these keys: <c>n-id</c>, <c>c-id</c>, <c>s-id</c>, <c>r-id</c>.
        /// </returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item = null)
        {
            DataPinBuilder builder = new DataPinBuilder();

            builder.Set("row", Rows?.Count ?? 0);

            if (NDefinitions?.Count > 0)
                builder.AddValues("n-id", NDefinitions.Select(d => d.Id));

            if (CDefinitions?.Count > 0)
                builder.AddValues("c-id", CDefinitions.Select(d => d.Id));

            if (SDefinitions?.Count > 0)
                builder.AddValues("s-id", SDefinitions.Select(d => d.Id));

            if (RDefinitions?.Count > 0)
                builder.AddValues("r-id", RDefinitions.Select(d => d.Id));

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
                   "row-count",
                   "The count of rows."),
                new DataPinDefinition(DataPinValueType.String,
                   "n-id",
                   "The numbering IDs.",
                   "M"),
                new DataPinDefinition(DataPinValueType.String,
                   "c-id",
                   "The catchword IDs.",
                   "M"),
                new DataPinDefinition(DataPinValueType.String,
                   "s-id",
                   "The quire signature IDs.",
                   "M"),
                new DataPinDefinition(DataPinValueType.String,
                   "r-id",
                   "The quire register signature IDs.",
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
            StringBuilder sb = new StringBuilder();

            sb.Append("[CodSheetLabels]: R=").Append(Rows?.Count ?? 0);

            sb.Append(" CN=").Append(NDefinitions?.Count ?? 0);
            sb.Append(" CC=").Append(CDefinitions?.Count ?? 0);
            sb.Append(" CS=").Append(SDefinitions?.Count ?? 0);
            sb.Append(" CR=").Append(RDefinitions?.Count ?? 0);

            return sb.ToString();
        }
    }
}
