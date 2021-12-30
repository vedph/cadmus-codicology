﻿using System.Text;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// A catchword in a manuscript.
    /// </summary>
    public class CodCatchword
    {
        /// <summary>
        /// Gets or sets the range covered by this catchword in the manuscript.
        /// </summary>
        public CodLocationRange Range { get; set; }

        /// <summary>
        /// Gets or sets the position of this catchword in the sheet.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this catchword is vertical
        /// rather than horizontal.
        /// </summary>
        public bool IsVertical { get; set; }

        /// <summary>
        /// Gets or sets the description of an optional decoration linked to
        /// this catchword.
        /// </summary>
        public string Decoration { get; set; }

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
            if (Range != null) sb.Append(Range);
            if (!string.IsNullOrEmpty(Position))
            {
                if (sb.Length > 0) sb.Append(": ");
                sb.Append(Position);
            }
            if (IsVertical) sb.Append('^');
            return base.ToString();
        }
    }
}