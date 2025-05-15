using System.Collections.Generic;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// Additional data about quires in <see cref="CodSheetLabelsPart"/>.
/// While you can add features and notes to each column's page (=cell) in the
/// general table-like layout of the part, this class is meant to contain
/// quire-specific data (thus valid for multiple pages), like features,
/// notes for each quire, and a general note about all the quires.
/// </summary>
public class CodQuireDescription
{
    /// <summary>
    /// The features referred to all the quires. Usually derived from thesaurus
    /// <c>cod-quire-features</c>.
    /// </summary>
    public List<string>? Features { get; set; }

    /// <summary>
    /// A note about all the quires.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Notes scoped to each single quire. The key is the 1-based index of the
    /// quire, while the value is the note text.
    /// </summary>
    public Dictionary<int, string>? ScopedNotes { get; set; }
}
