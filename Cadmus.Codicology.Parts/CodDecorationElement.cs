using Cadmus.Refs.Bricks;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// An element of a <see cref="CodDecoration"/>.
/// </summary>
public class CodDecorationElement
{
    /// <summary>
    /// Gets or sets the key used for this element when it represents also
    /// a parent of other elements. Its scope is limited to the part.
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// Gets or sets the parent element's key, used to group sub-elements
    /// under an element. Its scope is limited to the part.
    /// </summary>
    public string? ParentKey { get; set; }

    /// <summary>
    /// Gets or sets the element's type, usually drawn from a thesaurus,
    /// like "pagina incipitaria", "pagina decorata", "illustrazione",
    /// "ornamentazione", "iniziali", etc.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets a general-purpose tag.
    /// </summary>
    public string? Tag { get; set; }

    /// <summary>
    /// Gets or sets the flags. These are typically drawn from a thesaurus,
    /// and represent single features of the element, which may or not be
    /// present in it, like "original", "unitary", "complete", "has tips",
    /// etc.
    /// </summary>
    public List<string> Flags { get; set; } = [];

    /// <summary>
    /// Gets or sets the ranges of locations this element spans for.
    /// </summary>
    public List<CodLocationRange> Ranges { get; set; } = [];

    /// <summary>
    /// Links towards other entities like iconographies.
    /// </summary>
    public List<AssertedCompositeId> Links { get; set; } = [];

    /// <summary>
    /// Gets or sets the count of other instances of the same element
    /// which is described just once about its parent decoration, but occurs
    /// several times in other decorations of the same manuscript. When
    /// not used this is just 0.
    /// </summary>
    public int InstanceCount { get; set; }

    /// <summary>
    /// Gets or sets the typologies assigned to this element. These are
    /// typically drawn from a thesaurus, organized in sub-sets according
    /// to the element's <see cref="Type"/>; for instance, for type
    /// "ornamentation" you would have typologies like "fregi", "cornici",
    /// "grottesche", "stemmi", etc.
    /// </summary>
    public List<string> Typologies { get; set; } = [];

    /// <summary>
    /// Gets or sets the decoration subject, when applicable. For letters,
    /// it might be the letter itself.
    /// </summary>
    public string? Subject { get; set; }

    /// <summary>
    /// Gets or sets the colors, usually drawn from a thesaurus.
    /// </summary>
    public List<string> Colors { get; set; } = [];

    /// <summary>
    /// Gets or sets the gilding types, usually drawn from a thesaurus.
    /// </summary>
    public List<string> Gildings { get; set; } = [];

    /// <summary>
    /// Gets or sets the techniques, usually drawn from a thesaurus.
    /// </summary>
    public List<string> Techniques { get; set; } = [];

    /// <summary>
    /// Gets or sets the tools used for the element, usually drawn from
    /// a thesaurus.
    /// </summary>
    public List<string> Tools { get; set; } = [];

    /// <summary>
    /// Gets or sets the position of the element relative to the page,
    /// usually drawn from a thesaurus.
    /// </summary>
    public List<string> Positions { get; set; } = [];

    /// <summary>
    /// The optional reference sign from some script, e.g. the letter
    /// corresponding to a decorated initial.
    /// </summary>
    public string? RefSign { get; set; }

    /// <summary>
    /// Gets or sets the height of the element, measured in lines.
    /// </summary>
    public short LineHeight { get; set; }

    /// <summary>
    /// Gets or sets the relation of this element with the text.
    /// </summary>
    public string? TextRelation { get; set; }

    /// <summary>
    /// Gets or sets the element's description. Usually this is a rich
    /// text (Markdown).
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the list of images depicting this element.
    /// </summary>
    public List<CodImage> Images { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of document references for this element.
    /// </summary>
    public List<DocReference> References { get; set; } = [];

    /// <summary>
    /// Gets or sets an optional note.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        StringBuilder sb = new();

        if (!string.IsNullOrEmpty(Key))
            sb.Append('#').Append(Key);

        if (!string.IsNullOrEmpty(ParentKey))
            sb.Append("<= #").Append(ParentKey);

        sb.Append('[').Append(Type).Append("] ");

        sb.Append(Subject);

        return sb.ToString();
    }
}
