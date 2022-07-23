using Cadmus.Refs.Bricks;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// Manuscript's codicological unit ("a sample of folios or queries whose
    /// production can be regarded a unic operation, realised in the same
    /// conditions (place, time, technics)", Muzerelle 1985).
    /// </summary>
    public class CodUnit
    {
        /// <summary>
        /// Gets or sets an arbitrary entity ID optionally assigned to this unit.
        /// </summary>
        public string? Eid { get; set; }

        /// <summary>
        /// Gets or sets the optional tag used to classify or groups units.
        /// </summary>
        public string? Tag { get; set; }

        /// <summary>
        /// Gets or sets the unit's material.
        /// </summary>
        public string? Material { get; set; }

        /// <summary>
        /// Gets or sets the format of this unit.
        /// </summary>
        public string? Format { get; set; }

        /// <summary>
        /// Gets or sets the material state of this unit.
        /// </summary>
        public string? State { get; set; }

        /// <summary>
        /// Gets or sets the range covered by this unit in the manuscript.
        /// </summary>
        public CodLocationRange? Range { get; set; }

        /// <summary>
        /// Gets or sets places/dates assigned to this unit.
        /// </summary>
        public List<AssertedChronotope> Chronotopes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this unit does not follow
        /// Gregory's law (which determines that during the creation of the
        /// parchment quires the pages with the same side -hair/flesh- always
        /// touch).
        /// </summary>
        public bool NoGregory { get; set; }

        /// <summary>
        /// Gets or sets an optional note.
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodUnit"/> class.
        /// </summary>
        public CodUnit()
        {
            Chronotopes = new();
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
            if (!string.IsNullOrEmpty(Eid)) sb.Append('#').Append(Eid);
            if (!string.IsNullOrEmpty(Material)) sb.Append(' ').Append(Material);
            if (!string.IsNullOrEmpty(Format))
                sb.Append(" (").Append(Format).Append(')');
            if (Range != null) sb.Append(": ").Append(Range);

            return sb.ToString();
        }
    }
}
