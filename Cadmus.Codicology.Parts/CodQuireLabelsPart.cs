using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// Manuscript's catchwords and signatures part.
    /// <para>Tag: <c>it.vedph.codicology.quire-labels</c>.</para>
    /// </summary>
    [Tag("it.vedph.codicology.quire-labels")]
    public sealed class CodQuireLabelsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the catchwords.
        /// </summary>
        public List<CodCatchword> Catchwords { get; set; }

        /// <summary>
        /// Gets or sets the quire signatures.
        /// </summary>
        public List<CodQuireSignature> QuireSignatures { get; set; }

        /// <summary>
        /// Gets or sets the quire registry signatures.
        /// </summary>
        public List<CodQuireRegSignature> QuireRegSignatures { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodQuireLabelsPart"/>
        /// class.
        /// </summary>
        public CodQuireLabelsPart()
        {
            Catchwords = new List<CodCatchword>();
            QuireSignatures = new List<CodQuireSignature>();
            QuireRegSignatures = new List<CodQuireRegSignature>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder(
                new StandardDataPinTextFilter());

            builder.Set("catchword", Catchwords?.Count ?? 0, false);
            builder.Set("signature", QuireSignatures?.Count ?? 0, false);
            builder.Set("reg-signature", QuireRegSignatures?.Count ?? 0, false);

            if (Catchwords?.Count > 0)
            {
                foreach (CodCatchword catchword in Catchwords)
                {
                    builder.AddValue("catchword-position", catchword.Position);
                    builder.AddValue("catchword-vertical", catchword.IsVertical);
                    if (!string.IsNullOrEmpty(catchword.Decoration))
                        builder.AddValue("catchword-has-dec", true);
                }
            }

            if (QuireSignatures?.Count > 0)
            {
                foreach (CodQuireSignature signature in QuireSignatures)
                {
                    builder.AddValue("signature-position", signature.Position);
                    builder.AddValue("signature-system", signature.System);
                }
            }

            if (QuireRegSignatures?.Count > 0)
            {
                builder.AddValues("reg-signature-position",
                    QuireRegSignatures.Select(s => s.Position));
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
                    "The total count of entries.")
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

            sb.Append("[CodCatchwords]");

            sb.Append("C=").Append(Catchwords.Count)
              .Append("S=").Append(QuireSignatures.Count)
              .Append("R=").Append(QuireRegSignatures.Count);

            return sb.ToString();
        }
    }
}
