# Codicology Contents

🔑 ID: `it.vedph.codicology.contents`

- contents (`CodContent[]`):
  - eid (`string`)
  - workId (🧱 [AssertedCompositeId](https://github.com/vedph/cadmus-bricks/blob/master/docs/asserted-composite-id.md)): the optional reference to an authority work. When set, usually you do not need to set `author` and `title`.
  - range\* ([CodLocationRange](cod-location-range.md)):
  - states\* (`string[]` 📚 `cod-content-states`)
  - author (`string`)
  - title (`string`)
  - location (`string`)
  - claimedAuthor (`string`)
  - claimedTitle (`string`)
  - tag (`string` 📚 `cod-content-tags`)
  - note (`string`)
  - incipit (`string`)
  - explicit (`string`)
  - annotations (`CodContentAnnotation[]`):
    - type\* (`string` 📚 `cod-content-annotation-types`)
    - range\* ([CodLocationRange](cod-location-range.md))
    - incipit\* (`string`)
    - explicit (`string`)
    - text (`string`)
    - note (`string`)
