﻿using System.Collections.Generic;
using System.Text;

namespace Cadmus.Codicology.Parts
{
    public class CodContent
    {
        /// <summary>
        /// Gets or sets an arbitrary ID assigned to this content to uniquely
        /// identify it.
        /// </summary>
        public string Eid { get; set; }

        /// <summary>
        /// Gets or sets the range covered by this content in the manuscript.
        /// </summary>
        public CodLocationRange Range { get; set; }

        /// <summary>
        /// Gets or sets the content's material state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the content's standard title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the location in the work, e.g. 12,34-78 for Iliad book
        /// 12 line 34-78.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the claimed author.
        /// </summary>
        public string ClaimedAuthor { get; set; }

        /// <summary>
        /// Gets or sets the claimed title.
        /// </summary>
        public string ClaimedTitle { get; set; }

        /// <summary>
        /// Gets or sets a generic tag used to group or classify a content.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets a generic note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the incipit.
        /// </summary>
        public string Incipit { get; set; }

        /// <summary>
        /// Gets or sets the explicit.
        /// </summary>
        public string Explicit { get; set; }

        /// <summary>
        /// Gets or sets annotations in this content.
        /// </summary>
        public List<CodContentAnnotation> Annotations { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodContent"/> class.
        /// </summary>
        public CodContent()
        {
            Annotations = new List<CodContentAnnotation>();
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
            if (!string.IsNullOrEmpty(Title))
            {
                if (sb.Length > 0) sb.Append(' ');
                sb.Append(Title);
            }
            if (Range != null) sb.Append(": ").Append(Range);

            return sb.ToString();
        }
    }
}
