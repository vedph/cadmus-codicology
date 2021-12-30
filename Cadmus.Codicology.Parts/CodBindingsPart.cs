using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// Manuscript's bindings part.
    /// <para>Tag: <c>it.vedph.codicology.bindings</c>.</para>
    /// </summary>
    [Tag("it.vedph.codicology.bindings")]
    public sealed class CodBindingsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the entries.
        /// </summary>
        public List<CodBinding> Bindings { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodBindingsPart"/> class.
        /// </summary>
        public CodBindingsPart()
        {
            Bindings = new List<CodBinding>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c> and a collection of pins with
        /// these keys: <c>cover</c>, <c>support</c>, <c>place</c>,
        /// <c>date-value</c>.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder();

            builder.Set("tot", Bindings?.Count ?? 0, false);

            if (Bindings?.Count > 0)
            {
                foreach (CodBinding binding in Bindings)
                {
                    builder.AddValue("cover", binding.CoverMaterial);
                    builder.AddValue("support", binding.SupportMaterial);
                    if (binding.Chronotope != null)
                    {
                        builder.AddValue("place", binding.Chronotope.Place?.Value);
                        if (binding.Chronotope.Date?.Value != null)
                        {
                            builder.AddValue("date-value",
                                binding.Chronotope.Date.Value.GetSortValue());
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
                   "The total count of bindings."),
                new DataPinDefinition(DataPinValueType.String,
                   "cover",
                   "The cover material of each binding.",
                   "M"),
                new DataPinDefinition(DataPinValueType.String,
                   "support",
                   "The support material of each binding.",
                   "M"),
                new DataPinDefinition(DataPinValueType.String,
                   "place",
                   "The place of each binding.",
                   "M"),
                new DataPinDefinition(DataPinValueType.Decimal,
                   "date-value",
                   "The date value of each binding.",
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

            sb.Append("[CodBindings]");

            if (Bindings?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (var entry in Bindings)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(entry);
                }
                if (Bindings.Count > 3)
                    sb.Append("...(").Append(Bindings.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
