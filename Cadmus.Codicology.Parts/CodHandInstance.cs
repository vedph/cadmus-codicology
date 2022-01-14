using Cadmus.Refs.Bricks;
using System.Collections.Generic;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// The description of a manuscript's hand instance.
    /// </summary>
    public class CodHandInstance
    {
        /// <summary>
        /// Gets or sets the script type.
        /// </summary>
        public string Script { get; set; }

        /// <summary>
        /// Gets or sets the typologies.
        /// </summary>
        public List<string> Typologies { get; set; }

        /// <summary>
        /// Gets or sets the colors.
        /// </summary>
        public List<string> Colors { get; set; }

        /// <summary>
        /// Gets or sets the ranges covered by this instance.
        /// </summary>
        public List<CodLocationRange> Ranges { get; set; }

        /// <summary>
        /// Gets or sets the confidence rank for this instance identification.
        /// </summary>
        public short Rank { get; set; }

        /// <summary>
        /// Gets or sets the description identifier. This is a link to a
        /// <see cref="CodHandDescription"/>.
        /// </summary>
        public string DescriptionId { get; set; }

        /// <summary>
        /// Gets or sets the place/time of this instance.
        /// </summary>
        public AssertedChronotope Chronotope { get; set; }

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        public List<CodImage> Images { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodHandInstance"/> class.
        /// </summary>
        public CodHandInstance()
        {
            Typologies = new List<string>();
            Colors = new List<string>();
            Ranges = new List<CodLocationRange>();
            Images = new List<CodImage>();
        }

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
