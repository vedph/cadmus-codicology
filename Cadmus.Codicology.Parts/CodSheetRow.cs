using System.Collections.Generic;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// A row in the table model of the sheet labels part.
    /// </summary>
    public class CodSheetRow
    {
        /// <summary>
        /// Gets or sets the row's identifier. This is the physical sheet
        /// number + <c>r</c>ecto or <c>v</c>erso, except for endleaves. These
        /// have number=0 and a suffix built with the reserved prefix
        /// <c>&lt;</c>=front or <c>&gt;</c>=back + the ordinal number of the
        /// endleaf, e.g. <c>0r"&lt;1"</c>=recto of the first front endleaf.
        /// When the prefix is not followed by a number, it's the endleaf
        /// internally attached to the book's cover (controguardia).
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the columns in this row.
        /// </summary>
        public List<CodSheetColumn> Columns { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodSheetRow"/> class.
        /// </summary>
        public CodSheetRow()
        {
            Columns = new List<CodSheetColumn>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"#{Id}: {Columns?.Count ?? 0}";
        }
    }
}
