# Codicology Contents

🔑 ID: `it.vedph.codicology.contents`

A set of content entries representing in the manuscript. Each is a different work, with location in the manuscript, author, title, claimed author and title, citation, incipit, explicit, and optional annotations.

- contents (`CodContent[]`):
  - eid (`string`)
  - workId (🧱 [AssertedCompositeId](https://github.com/vedph/cadmus-bricks/blob/master/docs/asserted-composite-id.md)): the optional reference to an authority work. When set, usually you do not need to set `author` and `title`.
  - ranges\* ([CodLocationRange[]](cod-location-range.md)): the unit's location expressed as the range(s) covered by it.
  - states\* (`string[]` 📚 `cod-content-states`): various features related to the unit's preservation state, like headless, crippled, etc.
  - author (`string`): the author conventional name.
  - title (`string`): the work's conventional title.
  - location (`string`): the location in the work, e.g. 12,34-78 for Iliad book 12 line 34-78.
  - claimedAuthor (`string`): the author name as claimed in the unit.
  - claimedAuthorRanges ([CodLocationRange[]](cod-location-range.md)): the location of the author's name claim.
  - claimedTitle (`string`): the work's title as claimed in the unit.
  - claimedTitleRanges ([CodLocationRange[]](cod-location-range.md)): the location of the work's title claim.
  - tag (`string` 📚 `cod-content-tags`)
  - note (`string`)
  - incipit (`string`): the _incipit_'s text.
  - explicit (`string`): the _explicit_'s text.
  - annotations (`CodContentAnnotation[]`): optional annotations found in the unit, each having:
    - type\* (`string` 📚 `cod-content-annotation-types`): the annotation's type: rubric, preface, dedications, etc.
    - range\* ([CodLocationRange](cod-location-range.md)): the range of pages covered by the annotation.
    - features (`string[]` 📚 `cod-content-annotation-features`): any relevant annotation's features.
    - languages (`string[]` 📚 `cod-content-annotation-languages`): the language(s) used in the annotation's text. Usually these are codes from BCP47, ISO639, etc.
    - incipit\* (`string`): the annotation's _incipit_, when applicable.
    - explicit (`string`): the annotation's _explicit_, when applicable.
    - text (`string`): the annotation's text.
    - note (`string`)
