using Fusi.Antiquity.Chronology;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// A numbering system in a manuscript.
    /// </summary>
    public class CodNumbering
    {
        /// <summary>
        /// Gets or sets the optional entity ID for this numbering.
        /// </summary>
        public string Eid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is the main numbering
        /// system in the manuscript.
        /// </summary>
        public bool IsMain { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this numbering refers to
        /// pagination.
        /// </summary>
        public bool IsPagination { get; set; }

        /// <summary>
        /// Gets or sets the numbering system.
        /// </summary>
        public string System { get; set; }

        /// <summary>
        /// Gets or sets the numbering technique.
        /// </summary>
        public string Technique { get; set; }

        /// <summary>
        /// Gets or sets the numbering position in the sheet.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets the color(s) used for numberings.
        /// </summary>
        public IList<string> Colors { get; set; }

        /// <summary>
        /// Gets or sets the date when this numbering was applied to the
        /// manuscript.
        /// </summary>
        public HistoricalDate Date { get; set; }

        /// <summary>
        /// Gets or sets the manuscript's ranges this numbering is applied to.
        /// </summary>
        public IList<CodLocationRange> Ranges { get; set; }

        /// <summary>
        /// Gets or sets the spans for this numbering.
        /// </summary>
        public IList<CodNumberingSpan> Spans { get; set; }

        /// <summary>
        /// Gets or sets a human-readable description of issues found in this
        /// numbering.
        /// </summary>
        public string Issues { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodNumbering"/> class.
        /// </summary>
        public CodNumbering()
        {
            Colors = new List<string>();
            Ranges = new List<CodLocationRange>();
            Spans = new List<CodNumberingSpan>();
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
            if (IsMain) sb.Append('*');
            if (IsPagination) sb.Append('P');

            if (!string.IsNullOrEmpty(Technique)) sb.Append(' ').Append(Technique);
            if (!string.IsNullOrEmpty(Position)) sb.Append(" @").Append(Position);

            if (Ranges?.Count > 0) sb.Append(": ").Append(string.Join(" ", Ranges));

            return sb.ToString();
        }
    }
}
