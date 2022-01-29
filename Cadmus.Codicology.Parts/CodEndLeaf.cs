using Cadmus.Refs.Bricks;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// Manuscript's endleaf.
    /// </summary>
    public class CodEndleaf
    {
        /// <summary>
        /// Gets or sets the endleaf location. See <see cref="CodSheetRow.Id"/>
        /// for the details.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        public string Material { get; set; }

        /// <summary>
        /// Gets or sets the optional place/date for this end-leaf.
        /// </summary>
        public AssertedChronotope Chronotope { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Location}: {Material}";
        }
    }
}
