using Cadmus.Refs.Bricks;
using System.Text;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// Manuscript's endleaf.
    /// </summary>
    public class CodEndleaf
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        public string Material { get; set; }

        /// <summary>
        /// Gets or sets the range covered by this end-leaf.
        /// </summary>
        public CodLocationRange Range { get; set; }

        /// <summary>
        /// Gets or sets the optional date for this end-leaf.
        /// </summary>
        public AssertedDate Date { get; set; }

        /// <summary>
        /// Gets or sets an optional note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(Type)) sb.Append(Type);
            if (!string.IsNullOrEmpty(Material))
            {
                if (sb.Length > 0) sb.Append(' ');
                sb.Append(Material);
            }
            if (Range != null) sb.Append(": "); sb.Append(Range);

            return sb.ToString();
        }
    }
}
