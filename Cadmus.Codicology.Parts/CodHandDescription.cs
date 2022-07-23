using System.Collections.Generic;
using System.Text;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// The description of a manuscript's hand.
    /// </summary>
    public class CodHandDescription
    {
        /// <summary>
        /// Gets or sets the key of this description. This is used to internally
        /// link hand instances to descriptions in the context of the same
        /// part.
        /// </summary>
        public string? Key { get; set; }

        /// <summary>
        /// Gets or sets the hand's description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the initials description.
        /// </summary>
        public string? Initials { get; set; }

        /// <summary>
        /// Gets or sets the corrections description.
        /// </summary>
        public string? Corrections { get; set; }

        /// <summary>
        /// Gets or sets the punctuation description.
        /// </summary>
        public string? Punctuation { get; set; }

        /// <summary>
        /// Gets or sets the abbreviations description.
        /// </summary>
        public string? Abbreviations { get; set; }

        /// <summary>
        /// Gets or sets the descriptions of single signs.
        /// </summary>
        public List<CodHandSign> Signs { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodHandDescription"/>
        /// class.
        /// </summary>
        public CodHandDescription()
        {
            Signs = new List<CodHandSign>();
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
            if (!string.IsNullOrEmpty(Key)) sb.Append(Key);
            if (!string.IsNullOrEmpty(Description))
            {
                sb.Append(": ")
                  .Append(Description.Length > 60
                    ? Description.Substring(0, 60) : Description);
            }

            return sb.ToString();
        }
    }
}
