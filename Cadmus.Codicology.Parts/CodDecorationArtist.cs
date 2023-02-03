using Cadmus.Refs.Bricks;
using System.Collections.Generic;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// Essential information about the artist of a <see cref="CodDecoration"/>.
/// </summary>
public class CodDecorationArtist
{
    /// <summary>
    /// Gets or sets the artist's entity ID.
    /// </summary>
    public string? Eid { get; set; }

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the optional asserted IDs for this author.
    /// </summary>
    public List<AssertedId> Ids { get; set; }

    /// <summary>
    /// Gets or sets the styles.
    /// </summary>
    public List<CodDecorationArtistStyle> Styles { get; set; }

    /// <summary>
    /// Gets or sets the element keys. These refer this artist to one or
    /// more specific elements of a decoration.
    /// </summary>
    public List<string> ElementKeys { get; set; }

    /// <summary>
    /// Gets or sets an optional note.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodDecorationArtist"/>
    /// class.
    /// </summary>
    public CodDecorationArtist()
    {
        Ids = new List<AssertedId>();
        Styles = new List<CodDecorationArtistStyle>();
        ElementKeys = new List<string>();
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return $"#{Eid} [{Type}] {Name}";
    }
}
