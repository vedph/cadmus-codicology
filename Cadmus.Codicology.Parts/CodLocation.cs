using System.Text;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// Location in a manuscript's sheet.
    /// </summary>
    public class CodLocation
    {
        /// <summary>
        /// Gets or sets the reference system. This starts with a-z or A-Z and
        /// then contains only letters a-z or A-Z, underscores, or digits 0-9.
        /// If not specified, the default reference system is assumed, which
        /// is determined by the convention adopted in the project.
        /// </summary>
        public string S { get; set; }

        /// <summary>
        /// Gets or sets the ordinal sheet number (1-N).
        /// </summary>
        public int N { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="N"/> is to be
        /// displayed as a Roman number.
        /// </summary>
        public bool Rmn { get; set; }

        /// <summary>
        /// Gets or sets an optional suffix after N (e.g. "bis").
        /// </summary>
        public string Sfx { get; set; }

        /// <summary>
        /// Gets or sets recto/verso. If null, the location refers to both
        /// sides or we don't care about specifying one.
        /// </summary>
        public bool? V { get; set; }

        /// <summary>
        /// Gets or sets the column letter(s).
        /// </summary>
        public string C { get; set; }

        /// <summary>
        /// Gets or sets the line number (1-N, 0 if not specified).
        /// </summary>
        public int L { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(S)) sb.Append(S).Append(':');
            if (Rmn) sb.Append('^');
            sb.Append(N);
            if (!string.IsNullOrEmpty(Sfx))
                sb.Append('"').Append(Sfx).Append('"');
            if (V != null)
            {
                sb.Append(V == true ? 'v' : 'r');
                if (!string.IsNullOrEmpty(C)) sb.Append(C);
            }
            if (L != 0) sb.Append('.').Append(L);
            return sb.ToString();
        }
    }
}
