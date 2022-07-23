using Cadmus.Refs.Bricks;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// A style of a <see cref="CodDecorationArtist"/>.
    /// </summary>
    public class CodDecorationArtistStyle
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the chronotope.
        /// </summary>
        public AssertedChronotope? Chronotope { get; set; }

        /// <summary>
        /// Gets or sets the assertion.
        /// </summary>
        public Assertion? Assertion { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Name}: {Chronotope}";
        }
    }
}
