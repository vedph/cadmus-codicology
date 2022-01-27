namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// Catchwords on sheets in the table model of the sheet labels part.
    /// </summary>
    /// <seealso cref="CodSheetColumnDefinition" />
    public class CodSheetCColumnDefinition : CodSheetColumnDefinition
    {
        /// <summary>
        /// Gets or sets the position of labels in the page.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this label is runs
        /// vertically.
        /// </summary>
        public bool IsVertical { get; set; }

        /// <summary>
        /// Gets or sets the decoration.
        /// </summary>
        public string Decoration { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"#{Id}: {Position}";
        }
    }
}
