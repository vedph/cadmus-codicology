namespace Cadmus.Codicology.Parts;

/// <summary>
/// Quire register signatures on sheets in the table model of the sheet
/// labels part.
/// </summary>
/// <seealso cref="CodSheetColumnDefinition" />
public class CodSheetRColumnDefinition : CodSheetColumnDefinition
{
    /// <summary>
    /// Gets or sets the position of labels in the page.
    /// </summary>
    public string? Position { get; set; }

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
