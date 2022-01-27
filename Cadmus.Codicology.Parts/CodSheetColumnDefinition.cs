namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// Base class for sheet columns definitions.
    /// </summary>
    public abstract class CodSheetColumnDefinition
    {
        /// <summary>
        /// Gets or sets the identifier. This conventionally starts with a prefix
        /// defining the column's type: <c>q</c>=quires, <c>n</c>=numbering,
        /// <c>c</c>=catchwords, <c>s</c>=quire signatures, <c>r</c>=quire
        /// register signatures; this letter is typically followed by a name
        /// after a dot, unless the column is to be treated as the default
        /// for its type. For instance, <c>n.roman-endleaf</c> is a numbering
        /// system, while <c>q</c> is the (unique and thus default) quires
        /// description.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the rank. This is used to define the main column among
        /// columns of the same type, if any. For instance, we might have two
        /// different numbering systems, one treated as the main one and thus
        /// e.g. having rank=1, and another with rank=2. Should both systems
        /// have the same relevance, they would have the same rank.
        /// </summary>
        public short Rank { get; set; }

        /// <summary>
        /// Gets or sets a generic short note.
        /// </summary>
        public string Note { get; set; }
    }
}
