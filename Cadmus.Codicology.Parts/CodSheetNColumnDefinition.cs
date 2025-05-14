using Fusi.Antiquity.Chronology;
using System.Collections.Generic;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// Numbering on sheets in the table model of the sheet labels part.
/// </summary>
/// <seealso cref="CodSheetColumnDefinition" />
public class CodSheetNColumnDefinition : CodSheetColumnDefinition
{
    /// <summary>
    /// Gets or sets a value indicating whether this numbering is a
    /// pagination, i.e. both sides of each sheet get a label.
    /// </summary>
    public bool IsPagination { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this numbering was provided
    /// by the scribe.
    /// </summary>
    public bool IsByScribe { get; set; }

    /// <summary>
    /// Gets or sets the numbering system (e.g. Arabic, Roman,
    /// Latin alphabetic, etc.).
    /// </summary>
    public string? System { get; set; }

    /// <summary>
    /// Gets or sets the technique.
    /// </summary>
    public string? Technique { get; set; }

    /// <summary>
    /// Gets or sets the position of labels in the page.
    /// </summary>
    public string? Position { get; set; }

    /// <summary>
    /// Gets or sets the color(s).
    /// </summary>
    public List<string> Colors { get; set; }

    /// <summary>
    /// Gets or sets the date.
    /// </summary>
    public HistoricalDate? Date { get; set; }

    /// <summary>
    /// Gets or sets the numbering ranges for which this column should be used
    /// as the canonical system to reference the pages in them. All the ranges
    /// not covered by canonical ranges of any of the N-columns will be assumed
    /// to be referenced via the default "natural" N-column.
    /// </summary>
    public List<CodLocationRange>? CanonicalRanges { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodSheetNColumnDefinition"/>
    /// class.
    /// </summary>
    public CodSheetNColumnDefinition()
    {
        Colors = [];
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return $"#{Id}: {System}@{Position}";
    }
}
