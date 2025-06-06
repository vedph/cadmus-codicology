﻿using Cadmus.Refs.Bricks;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// A single content unit in a manuscript.
/// </summary>
public class CodContent
{
    /// <summary>
    /// Gets or sets an arbitrary ID assigned to this content to uniquely
    /// identify it.
    /// </summary>
    public string? Eid { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the work from some authority list.
    /// </summary>
    public AssertedCompositeId? WorkId { get; set; }

    /// <summary>
    /// Gets or sets the range(s) covered by this content in the manuscript.
    /// </summary>
    public List<CodLocationRange> Ranges { get; set; }

    /// <summary>
    /// Gets or sets the content's material state(s).
    /// </summary>
    public List<string>? States { get; set; }

    /// <summary>
    /// Gets or sets the author. Usually this is specified when
    /// not setting <see cref="WorkId"/>."/>
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// Gets or sets the content's title. Usually this is specified when
    /// not setting <see cref="WorkId"/>."/>
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the location in the work, e.g. 12,34-78 for Iliad book
    /// 12 line 34-78.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the claimed author.
    /// </summary>
    public string? ClaimedAuthor { get; set; }

    /// <summary>
    /// Gets or sets the claimed author ranges.
    /// </summary>
    public List<CodLocationRange>? ClaimedAuthorRanges { get; set; }

    /// <summary>
    /// Gets or sets the claimed title ranges.
    /// </summary>
    public string? ClaimedTitle { get; set; }

    public List<CodLocationRange>? ClaimedTitleRanges { get; set; }

    /// <summary>
    /// Gets or sets a generic tag used to group or classify a content.
    /// </summary>
    public string? Tag { get; set; }

    /// <summary>
    /// Gets or sets a generic note.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Gets or sets the incipit.
    /// </summary>
    public string? Incipit { get; set; }

    /// <summary>
    /// Gets or sets the explicit.
    /// </summary>
    public string? Explicit { get; set; }

    /// <summary>
    /// Gets or sets annotations in this content.
    /// </summary>
    public List<CodContentAnnotation> Annotations { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodContent"/> class.
    /// </summary>
    public CodContent()
    {
        Ranges = [];
        States = [];
        Annotations = [];
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        StringBuilder sb = new();

        if (!string.IsNullOrEmpty(Eid)) sb.Append('#').Append(Eid);
        if (!string.IsNullOrEmpty(Title))
        {
            if (sb.Length > 0) sb.Append(' ');
            sb.Append(Title);
        }
        if (Ranges?.Count > 0) sb.Append(": ").AppendJoin(",", Ranges);

        return sb.ToString();
    }
}
