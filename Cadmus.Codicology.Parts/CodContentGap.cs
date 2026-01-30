using Cadmus.Refs.Bricks;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// A gap in the content of a manuscript. The gap is described with its start
/// and end references using the standard citation convention for the work
/// covered by the content. In each reference, the type is the gap type, and
/// tag can be used to further qualify the gap like e.g. partial vs. complete.
/// For instance, if a content for Dante's Commedia has a gap ranging from
/// If.I 30 to If.I 35 where the first portion of I 30 is not missing, then
/// the gap ranges from If.I 30 partial to If.I 35. In most cases the gap type
/// is the same for both start and end, but it might happen that they differ
/// e.g. for the damage type (e.g. burn vs. water).
/// </summary>
/// <remarks>In most cases, gap types and tags are fed by thesauri
/// <c>cod-content-gap-types</c> and <c>cod-content-gap-tags</c>.</remarks>
public class CodContentGap
{
    /// <summary>
    /// Gets or sets the starting reference point for the document range.
    /// </summary>
    public required DocReference Start { get; set; }

    /// <summary>
    /// Gets or sets the reference to the end of the documentation range.
    /// </summary>
    public required DocReference End { get; set; }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string in the format "Start - End".</returns>
    public override string ToString()
    {
        return $"{Start} - {End}";
    }
}

