using Cadmus.Refs.Bricks;
using Fusi.Antiquity.Chronology;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// A textual edit operation in a manuscript.
    /// </summary>
    public class CodEdit
    {
        /// <summary>
        /// Gets or sets the optional entity ID for this edit event.
        /// </summary>
        public string Eid { get; set; }

        /// <summary>
        /// Gets or sets the edit type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the edit technique(s).
        /// </summary>
        public List<string> Techniques { get; set; }

        /// <summary>
        /// Gets or sets the text's language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the color(s).
        /// </summary>
        public IList<string> Colors { get; set; }

        /// <summary>
        /// Gets or sets the ranges of locations affected by this edit event.
        /// </summary>
        public IList<CodLocationRange> Ranges { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public HistoricalDate Date { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the references.
        /// </summary>
        public IList<DocReference> References { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodEdit"/> class.
        /// </summary>
        public CodEdit()
        {
            Techniques = new List<string>();
            Colors = new List<string>();
            Ranges = new List<CodLocationRange>();
            References = new List<DocReference>();
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

            if (!string.IsNullOrEmpty(Eid)) sb.Append('#').Append(Eid);
            if (!string.IsNullOrEmpty(Type))
                sb.Append(" [").Append(Type).Append(']');
            if (!string.IsNullOrEmpty(Language))
                sb.Append(" (").Append(Language).Append(')');
            if (Ranges?.Count > 0)
                sb.Append(": ").Append(string.Join(" ", Ranges));

            return sb.ToString();
        }
    }
}
