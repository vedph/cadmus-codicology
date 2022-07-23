using System.Collections.Generic;
using System.Linq;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// Manuscript's material description part.
    /// <para>Tag: <c>it.vedph.codicology.material-dsc</c>.</para>
    /// </summary>
    [Tag("it.vedph.codicology.material-dsc")]
    public sealed class CodMaterialDscPart : PartBase
    {
        /// <summary>
        /// Gets or sets the codicological units in the manuscript.
        /// </summary>
        public List<CodUnit> Units { get; set; }

        /// <summary>
        /// Gets or sets the palimpsests sheets in the manuscript.
        /// </summary>
        public List<CodPalimpsest> Palimpsests { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodMaterialDscPart"/>
        /// class.
        /// </summary>
        public CodMaterialDscPart()
        {
            Units = new List<CodUnit>();
            Palimpsests = new List<CodPalimpsest>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item = null)
        {
            DataPinBuilder builder = new(
                new StandardDataPinTextFilter());

            // unit-count
            builder.Set("unit", Units?.Count ?? 0, false);
            // palimpsest-count
            builder.Set("palimpsest", Palimpsests?.Count ?? 0, false);
            // unit-eid
            if (Units?.Count > 0)
                builder.AddValues("unit-eid", Units.Select(u => u.Eid));

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
                    "unit-count",
                    "The total count of units."),
                 new DataPinDefinition(DataPinValueType.Integer,
                    "palimpsest-count",
                    "The total count of palimpsests."),
                 new DataPinDefinition(DataPinValueType.String,
                    "unit-eid",
                    "The unit IDs.",
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
            return $"[CodMaterialDsc] Units: {Units?.Count ?? 0} - " +
                $"Palimpsests: {Palimpsests?.Count ?? 0}";
        }
    }
}
