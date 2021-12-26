namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// A span of regularly numbered sheets in a manuscript. This is defined
    /// with reference to a range in the system used as base (usually corresponding
    /// to the current physical state of the manuscript, where the first sheet
    /// is 1, the second is 2, etc.), and has a start value and an end value.
    /// It is assumed that all the values in between can be calculated according
    /// to the numbering system the span refers to; so, whenever there is some
    /// incongruence we must start a new span.
    /// </summary>
    public class CodNumberingSpan
    {
        /// <summary>
        /// Gets or sets the range this span refers to.
        /// </summary>
        public CodLocationRange Range { get; set; }

        /// <summary>
        /// Gets or sets the start numbering value of this span.
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// Gets or sets the end numbering value of this span (included).
        /// </summary>
        public string End { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Range}: {Start}-{End}";
        }
    }
}
