﻿using System.Text;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// A signature of a manuscript's quire.
    /// </summary>
    public class CodQuireSignature
    {
        /// <summary>
        /// Gets or sets the range covered by this signature.
        /// </summary>
        public CodLocationRange Range { get; set; }

        /// <summary>
        /// Gets or sets the position of this signature.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets the system used for this signature.
        /// </summary>
        public string System { get; set; }

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
            if (!string.IsNullOrEmpty(System))
                sb.Append(" (").Append(System).Append(')');
            return base.ToString();
        }
    }
}