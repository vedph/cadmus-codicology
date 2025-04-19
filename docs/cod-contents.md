# Codicology Contents

🔑 ID: `it.vedph.codicology.contents`

- contents (`CodContent[]`):
  - eid (`string`)
  - workId (🧱 [AssertedCompositeId](https://github.com/vedph/cadmus-bricks/blob/master/docs/asserted-composite-id.md)): the optional reference to an authority work. When set, usually you do not need to set `author` and `title`.
  - ranges\* ([CodLocationRange[]](cod-location-range.md)):
  - states\* (`string[]` 📚 `cod-content-states`)
  - author (`string`)
  - title (`string`)
  - location (`string`)
  - claimedAuthor (`string`)
  - claimedAuthorRanges ([CodLocationRange[]](cod-location-range.md))
  - claimedTitle (`string`)
  - claimedTitleRanges ([CodLocationRange[]](cod-location-range.md))
  - tag (`string` 📚 `cod-content-tags`)
  - note (`string`)
  - incipit (`string`)
  - explicit (`string`)
  - annotations (`CodContentAnnotation[]`):
    - type\* (`string` 📚 `cod-content-annotation-types`)
    - range\* ([CodLocationRange](cod-location-range.md))
    - features (`string[]` 📚 `cod-content-annotation-features`)
    - languages (`string[]` 📚 `cod-content-annotation-languages`)
    - incipit\* (`string`)
    - explicit (`string`)
    - text (`string`)
    - note (`string`)
