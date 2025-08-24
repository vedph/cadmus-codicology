# Codicology Material Description

🔑 `it.vedph.codicology.material-dsc`

Material description of the manuscript via its codicological units (defined as "a sample of folios or queries whose production can be regarded a unic operation, realised in the same conditions (place, time, technics)", Muzerelle 1985).

- units\* (`CodUnit[]`):
  - eid (`string`)
  - tag (`string`) 📚 `cod-unit-tags`
  - material\* (`string` 📚 `cod-unit-materials`)
  - format\* (`string` 📚 `cod-unit-formats`)
  - state\* (`string` 📚 `cod-unit-states`)
  - ranges\* ([CodLocationRange[]](cod-location-range.md)):
  - chronotopes\* (🧱 [AssertedChronotope[]](https://github.com/vedph/cadmus-bricks/blob/master/docs/asserted-chronotope.md)):
    - place (🧱 [AssertedPlace](https://github.com/vedph/cadmus-bricks/blob/master/docs/asserted-place.md))
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
      - assertion (🧱 [Assertion](https://github.com/vedph/cadmus-bricks/blob/master/docs/assertion.md))
  - noGregory (`boolean`): a value indicating whether this unit does _not_ follow Gregory's law (which determines that during the creation of the parchment quires the pages with the same side -hair/flesh- always touch).
  - note (`string`)
- palimpsests (`CodPalimpsest[]`):
  - range\* ([CodLocationRange](cod-location-range.md))
  - chronotope (🧱 [AssertedChronotope](https://github.com/vedph/cadmus-bricks/blob/master/docs/asserted-chronotope.md))
  - note (`string`)

>Endleaves are described in [sheet labels](cod-sheet-labels.md).
