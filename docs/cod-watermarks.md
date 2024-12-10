# Codicology Watermarks

🔑 `it.vedph.codicology.watermarks`

Manuscript's watermarks.

- watermarks (`CodWatermark[]`):
  - name\* (`string`)
  - sampleRange\* ([CodLocationRange](cod-location-range.md)):
  - ranges ([CodLocationRange[]](cod-location-range.md))
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
    - assertion (🧱 [Assertion](https://github.com/vedph/cadmus-bricks/blob/master/docs/assertion.md))
  - size (🧱 `PhysicalSize`):
    - tag (string 📚 `physical-size-tags`)
    - w (🧱 `PhysicalDimension`):
      - value\* (number)
      - unit\* (string 📚 `physical-size-units`)
      - tag (string 📚 `physical-size-dim-tags`)
    - h (🧱 `PhysicalDimension`)
    - d (🧱 `PhysicalDimension`)
  - chronotope\* (🧱 [AssertedChronotope](https://github.com/vedph/cadmus-bricks/blob/master/docs/asserted-chronotope.md)):
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
  - description (`string`)

> ⚠️ `ids` was of type `AssertedId[]` before version 5.
