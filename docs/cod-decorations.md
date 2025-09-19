# Codicology Decorations

🔑 ID: `it.vedph.codicology.decorations`

Essential description of the decorations found in a manuscript. This is a relatively synthetic, yet highly granular description, thus included in a single part rathern than being an item.

Each decoration has some generic metadata (ID, name and features), date and/or place, artist(s), references, and any number of elements.

Each element has a more detailed set of descriptive metadata, and can also be positioned in an elements hierarchy shaped like a tree, where each element can have one parent and any number of children. An element has type, features, range(s) covered by it in the manuscript, typologies, subject, colors, gildings, techniques and tools, positions in the page, reference sign, height, relation with text, free text description, and zero or more images.

- decorations (`CodDecoration[]`):
  - eid (`string`)
  - name\* (`string`)
  - flags (`string`[]) 📚 `cod-decoration-flags`
  - chronotopes (🧱 [AssertedChronotope[]](https://github.com/vedph/cadmus-bricks/blob/master/docs/asserted-chronotope.md)):
    - place (`AssertedPlace`)
      - tag (`string` 📚 `chronotope-tags`)
      - value (`string`)
      - assertion (🧱 [Assertion](https://github.com/vedph/cadmus-bricks/blob/master/docs/assertion.md)):
        - tag (`string` 📚 `assertion-tags`)
        - rank (`short`)
        - references (🧱 [DocReference[]](https://github.com/vedph/cadmus-bricks/blob/master/docs/doc-reference.md)):
          - type (`string` 📚 `doc-reference-types`)
          - tag (`string` 📚 `doc-reference-tags`)
          - citation (`string`)
          - note (`string`)
    - date (🧱 [AssertedDate](https://github.com/vedph/cadmus-bricks/blob/master/docs/asserted-date.md)):
      - a* (🧱 [Datation](https://github.com/vedph/cadmus-bricks/blob/master/docs/datation.md)):
        - value* (`int`): the numeric value of the point. Its interpretation depends on other points properties: it may represent a year or a century, or a span between two consecutive Gregorian years.
        - isCentury (`boolean`): true if value is a century number; false if it's a Gregorian year.
        - isSpan (`boolean`): true if the value is the first year of a pair of two consecutive years. This is used for calendars which span across two Gregorian years, e.g. 776/5 BC.
        - month (`short`): the month number (1-12) or 0.
        - day (`short`): the day number (1-31) or 0.
        - isApproximate (`boolean`): true if the point is approximate ("about").
        - isDubious (`boolean`): true if the point is dubious ("perhaphs").
        - hint (`string`): a short textual hint used to better explain or motivate the datation point.
      - b (🧱 [Datation](https://github.com/vedph/cadmus-bricks/blob/master/docs/datation.md))
      - tag (`string`)
      - assertion (🧱 [Assertion](https://github.com/vedph/cadmus-bricks/blob/master/docs/assertion.md)):
  - artists (`CodDecorationArtist[]`):
    - eid (`string`)
    - type\* (`string`) 📚 cod-decoration-artist-types
    - name\* (`string`)
    - ids (🧱 [AssertedCompositeId[]](https://github.com/vedph/cadmus-bricks/blob/master/docs/asserted-composite-id.md)):
      - target (`PinTarget`):
        - gid\* (`string`)
        - label\* (`string`)
        - itemId (`string`)
        - partId (`string`)
        - partTypeId (`string`)
        - roleId (`string`)
        - name (`string`)
        - value (`string`)
      - scope (`string`)
      - assertion (🧱 [Assertion](https://github.com/vedph/cadmus-bricks/blob/master/docs/assertion.md)):
    - styles (`CodDecorationArtistStyle[]`):
      - name\* (`string`) 📚 cod-decoration-artist-style-names
      - chronotope (🧱 [AssertedChronotope](https://github.com/vedph/cadmus-bricks/blob/master/docs/asserted-chronotope.md))
      - assertion (🧱 [Assertion](https://github.com/vedph/cadmus-bricks/blob/master/docs/assertion.md)):
    - elementKeys (`string`[])
    - note (`string`)
  - note (`string`)
  - references (🧱 [DocReference[]](https://github.com/vedph/cadmus-bricks/blob/master/docs/doc-reference.md)):
  - elements (`CodDecorationElement[]`):
    - key (`string`): the element's key. Used for the element when it represents also a parent of other elements. Its scope is limited to the part.
    - parentKey (`string`): the element parent's key if any. Its scope is limited to the part.
    - type\* (`string`) 📚 `cod-decoration-element-types`: the type of the element.
    - flags (`string`[]) 📚 `cod-decoration-element-flags`: binary features assigned to this element.
    - ranges\* ([CodLocationRange[]](cod-location-range.md)): the ranges of locations this element spans for.
    - links (`AssertedCompositeId[]`): links towards other entities like iconographies.
    - instanceCount (int): the count of other instances of the same element which is described just once about its parent decoration, but occurs several times in other decorations of the same manuscript. When not used this is just 0.
    - typologies (`string`) 📚 `cod-decoration-element-typologies`: the typologies assigned to this element. These are typically drawn from a thesaurus, organized in sub-sets according to the element's type; for instance, for type "ornamentation" you would have typologies like "fregi", "cornici", "grottesche", "stemmi", etc.
    - subject (`string`): the decoration subject, when applicable. For letters, it might be the letter itself.
    - colors (`string`[]) 📚 `cod-decoration-element-colors`: color(s) used for this element.
    - gildings (`string`[]) 📚 `cod-decoration-element-gildings`: gilding type(s) used for this element.
    - techniques (`string`[]) 📚 `cod-decoration-element-techniques`: technique(s) used for this element.
    - tools (`string`[]) 📚 `cod-decoration-element-tools`: tool(s) used for this element.
    - positions (`string`[]) 📚 `cod-decoration-element-positions`: the position of the element relative to the page (e.g. top margin, bottom margin, etc.).
    - refSign (`string`): the optional reference sign from some script, e.g. the letter corresponding to a decorated initial.
    - lineHeight (int): the element's height, measured in lines.
    - textRelation (`string`): a free textual dscription of the relation of this element with the text.
    - description (`string` MD): a rich-text description of the element.
    - images (`CodImage[]`): optional images of this element:
      - id\* (`string`): the image's ID.
      - type\* (`string`) 📚 `cod-image-types`: the image's type.
      - sourceId (`string`): the image's source ID. This depends on the image source; e.g. in a file system it could just be the image's file path, or just a numeric value if image files are progressively numbered in a folder, etc.; for a web image it could be a URL; etc.
      - label (`string`): a human-readable label for the image.
      - copyright (`string`)
      - references (🧱 [DocReference[]](https://github.com/vedph/cadmus-bricks/blob/master/docs/doc-reference.md)):
        - type (`string` 📚 `doc-reference-types`)
        - tag (`string` 📚 `doc-reference-tags`)
        - citation (`string`)
        - note (`string`)
    - note (`string`)

> ⚠️ `artists.ids` was of type `AssertedId[]` before version 5.

## Thesauri

The decorations part uses some thesauri to dynamically update its UI according to data being entered. The core thesaurus here is 📚 `cod-decoration-element-types`, required, which defines the types of decoration elements, such as initial pages, illustrations, headletters, paragraphematic signs, etc.

The UI structure is hierarchical:

- list of decorations:
  - edited decoration:
    - ID, name, features (📚 `cod-decoration-flags`), note, chronotopes, references, artists, list of elements:
      - edited element

In the edited elements, when the edited element type is selected from the list of element types:

- all the thesauri entries (except for `cod-decoration-type-hidden`, which is a settings thesaurus) are filtered according to the selected type. To this end, the IDs of these thesauri follow the syntax of hierarchical thesauri, e.g. `par.rubrication` is the entry for "rubrication" which must be displayed only when the element type is `par`.
- some areas of the UI are hidden according to the selected type. To this end, the 📚 `cod-decoration-type-hidden` thesaurus contains a list of identifiers from `cod-decoration-element-types`, each having as value a space-delimited list of tokens. In this thesaurus, each token represents a portion of the UI which should be hidden when that element type is selected. For instance, the paragraphematic type has ID `par` in the types thesaurus; in the hidden thesaurus, an entry has `par` as its ID, and `subject textRelation lineHeight` as its value. The value contains 3 UI portions to hide, each identified by a name.

The portions which can be hidden are:

- flags
- typologies
- subject
- colors
- gildings
- techniques
- tools
- positions
- lineHeight
- textRelation
- refSign
