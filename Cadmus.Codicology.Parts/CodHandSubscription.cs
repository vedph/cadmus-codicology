using System.Collections.Generic;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// A subscription in a manuscript's hand.
/// </summary>
public class CodHandSubscription
{
    /// <summary>
    /// Gets or sets the range covered by the subscription.
    /// </summary>
    public List<CodLocationRange> Ranges { get; set; }

    /// <summary>
    /// Gets or sets the language.
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// Gets or sets the text.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Gets or sets an optional note.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodHandSubscription"/> class.
    /// </summary>
    public CodHandSubscription()
    {
        Ranges = [];
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return $"[{Language}] {string.Join(", ", Ranges)}";
    }
}
