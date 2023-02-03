using Cadmus.Refs.Bricks;
using System.Collections.Generic;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// A single decoration in a manuscript.
/// </summary>
public class CodDecoration
{
    /// <summary>
    /// Gets or sets an identifier which can be arbitrarily assigned to this
    /// decoration.
    /// </summary>
    public string? Eid { get; set; }

    /// <summary>
    /// Gets or sets the human-friendly name of this decoration.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the flags. These are typically drawn from a thesaurus,
    /// and represent single features of the element, which may or not be
    /// present in it, like "original", "unitary", "complete", "has tips",
    /// etc.
    /// </summary>
    public List<string> Flags { get; set; }

    /// <summary>
    /// Gets or sets the date/place indication(s) for this decoration.
    /// </summary>
    public List<AssertedChronotope> Chronotopes { get; set; }

    /// <summary>
    /// Gets or sets the decoration's artist(s).
    /// </summary>
    public List<CodDecorationArtist> Artists { get; set; }

    /// <summary>
    /// Gets or sets an optional note.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Gets or sets the optional references for this decoration.
    /// </summary>
    public List<DocReference> References { get; set; }

    /// <summary>
    /// Gets or sets the elements this decoration consists of.
    /// </summary>
    public List<CodDecorationElement> Elements { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MsDecoration"/> class.
    /// </summary>
    public CodDecoration()
    {
        Flags = new List<string>();
        Chronotopes = new List<AssertedChronotope>();
        Artists = new List<CodDecorationArtist>();
        References = new List<DocReference>();
        Elements = new List<CodDecorationElement>();
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return $"{Name} ({Elements?.Count ?? 0})";
    }
}
