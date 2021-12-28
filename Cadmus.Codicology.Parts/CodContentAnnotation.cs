using System;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// An annotation in a <see cref="CodContent"/>.
    /// </summary>
    public class CodContentAnnotation
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the range covered by this annotation.
        /// </summary>
        public CodLocationRange Range { get; set; }

        /// <summary>
        /// Gets or sets this annotation's text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
