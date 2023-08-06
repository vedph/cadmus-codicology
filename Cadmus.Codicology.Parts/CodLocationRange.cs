namespace Cadmus.Codicology.Parts;

/// <summary>
/// A range of manuscript's locations.
/// </summary>
public class CodLocationRange
{
    /// <summary>
    /// Gets or sets the start location.
    /// </summary>
    public CodLocation? Start { get; set; }

    /// <summary>
    /// Gets or sets the end location (included). If the range refers to
    /// a single location, <see cref="Start"/> is equal to <see cref="End"/>.
    /// </summary>
    public CodLocation? End { get; set; }

    /// <summary>
    /// Converts to a parsable string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        if (Start == null || End == null) return "";

        string a = Start.ToString();
        string b = End.ToString();
        return a == b ? a : $"{a}-{b}";
    }
}
