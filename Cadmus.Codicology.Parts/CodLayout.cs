﻿using Cadmus.Mat.Bricks;
using Cadmus.Refs.Bricks;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// Manuscript's layout.
/// </summary>
public class CodLayout
{
    /// <summary>
    /// Gets or sets the sheet used as a sample for this layout.
    /// </summary>
    public CodLocation? Sample { get; set; }

    /// <summary>
    /// Gets or sets the ranges covered by this layout.
    /// </summary>
    public List<CodLocationRange> Ranges { get; set; }

    /// <summary>
    /// The optional codicological layout formula
    /// (see https://github.com/vedph/cod-layout-view). This is usually
    /// also reflected by <see cref="Dimensions"/>.
    /// </summary>
    public string? Formula { get; set; }

    /// <summary>
    /// Gets or sets the dimensions.
    /// </summary>
    public List<PhysicalDimension> Dimensions { get; set; }

    /// <summary>
    /// Gets or sets the ruling technique(s).
    /// </summary>
    public List<string>? RulingTechniques { get; set; }

    /// <summary>
    /// Gets or sets the Derolez classification.
    /// </summary>
    public string? Derolez { get; set; }

    /// <summary>
    /// Gets or sets the pricking type.
    /// </summary>
    public string? Pricking { get; set; }

    /// <summary>
    /// Gets or sets the columns count.
    /// </summary>
    public int ColumnCount { get; set; }

    /// <summary>
    /// Gets or sets the counts and/or description about any desired
    /// property, with different levels of precision: for instance, you
    /// might have rowMinCount, rowMaxCount, lineCount, approxLineCount,
    /// lineMinCount, lineMaxCount, prickCount, etc.; for descriptions,
    /// you might have columns, direction, blanks, ruling, execution,
    /// etc., eventually with a count (which might represent an average,
    /// or the most frequent value, etc.).</summary>
    public List<DecoratedCount> Counts { get; set; }

    /// <summary>
    /// Gets or sets an optional tag.
    /// </summary>
    public string? Tag { get; set; }

    /// <summary>
    /// Gets or sets an optional note.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodLayout"/> class.
    /// </summary>
    public CodLayout()
    {
        Ranges = [];
        Dimensions = [];
        Counts = [];
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
        if (Sample != null)
        {
            if (sb.Length > 0) sb.Append(' ');
            sb.Append('(').Append(Sample).Append(')');
        }
        if (ColumnCount > 0) sb.Append(": c").Append(ColumnCount);
        return sb.ToString();
    }
}
