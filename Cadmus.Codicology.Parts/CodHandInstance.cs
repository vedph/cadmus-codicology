using Cadmus.Refs.Bricks;
using System;
using System.Collections.Generic;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// The description of a manuscript's hand instance.
/// </summary>
public class CodHandInstance
{
    /// <summary>
    /// Gets or sets the script(s) types.
    /// </summary>
    public List<string> Scripts { get; set; }

    /// <summary>
    /// Gets or sets the typologies.
    /// </summary>
    public List<string> Typologies { get; set; }

    /// <summary>
    /// Gets or sets the colors.
    /// </summary>
    public List<string> Colors { get; set; }

    /// <summary>
    /// Gets or sets the ranges covered by this instance.
    /// </summary>
    public List<CodLocationRange> Ranges { get; set; }

    /// <summary>
    /// Gets or sets the confidence rank for this instance identification.
    /// </summary>
    public short Rank { get; set; }

    /// <summary>
    /// Gets or sets the description key. This is a link to a
    /// <see cref="CodHandDescription"/> in the scope of the same part.
    /// </summary>
    public string? DescriptionKey { get; set; }

    /// <summary>
    /// Gets or sets the place/time of this instance.
    /// </summary>
    public AssertedChronotope? Chronotope { get; set; }

    /// <summary>
    /// Gets or sets the images.
    /// </summary>
    public List<CodImage> Images { get; set; }

    /// <summary>
    /// Gets or sets a generic note.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodHandInstance"/> class.
    /// </summary>
    public CodHandInstance()
    {
        Scripts = [];
        Typologies = [];
        Colors = [];
        Ranges = [];
        Images = [];
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return $"{string.Join(", ", (IList<string>)Scripts
            ?? Array.Empty<string>())} ({Rank})";
    }
}
