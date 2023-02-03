namespace Cadmus.Codicology.Parts;

/// <summary>
/// A column in the table model of the sheet labels part.
/// </summary>
public class CodSheetColumn
{
    /// <summary>
    /// Gets or sets the column identifier.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the value. This depends on the column type, and for
    /// quires it has a fixed syntax, like <c>N.S/T</c> where N=quire ordinal
    /// number, S=sheet number, T=total count of sheets in the quire.
    /// So, a quire sequence is like <c>1.1/4 1.1/4 1.2/4 1.2/4 1.3/4 1.3/4</c>
    /// etc. Also, it can stop before reaching the total when sheets are
    /// missing, or go beyond it when sheets were added (e.g. <c>2.3/4</c>
    /// or <c>2.5/4</c>).
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// Gets or sets the note.
    /// </summary>
    public string? Note { get; set; }

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
