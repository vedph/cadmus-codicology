namespace Cadmus.Codicology.Parts;

/// <summary>
/// The description of a single sign in a manuscript's hand.
/// </summary>
public class CodHandSign
{
    /// <summary>
    /// Gets or sets the entity ID for this sign.
    /// </summary>
    public string? Eid { get; set; }

    /// <summary>
    /// Gets or sets the MUFI character code for this sign.
    /// </summary>
    public int? Mufi { get; set; }

    /// <summary>
    /// Gets or sets the sign's type (e.g. letter, ligature, punctuation...).
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the location in the manuscript used to get a sample
    /// representative of this sign.
    /// </summary>
    public CodLocation? SampleLocation { get; set; }

    /// <summary>
    /// Gets or sets the sign's description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return $"#{Eid} [{Type}]";
    }
}
