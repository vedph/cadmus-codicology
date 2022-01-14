namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// Generic metadata about an image depicting any relevant portion of
    /// a manuscript.
    /// </summary>
    public class CodImage
    {
        /// <summary>
        /// Gets or sets the image identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the image type. This usually is drawn from a thesaurus.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the source identifier.
        /// </summary>
        public string SourceId { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the copyright note.
        /// </summary>
        public string Copyright { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Id} [{Type}] {Label}";
        }
    }
}
