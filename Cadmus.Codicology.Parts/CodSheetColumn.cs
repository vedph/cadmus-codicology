namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// A column in the table model of the sheet labels part.
    /// </summary>
    public class CodSheetColumn
    {
        /// <summary>
        /// Gets or sets the column identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the note.
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
            return $"#{Id}={Value}";
        }
    }
}
