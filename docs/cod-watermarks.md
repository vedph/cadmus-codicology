# Codicology Watermarks

🔑 `it.vedph.codicology.watermarks`

Manuscript's watermarks.

- watermarks (`CodWatermark[]`):
  - name\* (`string`)
  - sampleRange\* ([CodLocationRange](cod-location-range.md)):
    - start\* (`CodLocation`):
      - endleaf (int): 0=none 1=start 2=end
      - s (string): system
      - n\* (int): sheet number
      - rmn (boolean): Roman system for `n`
      - sfx (string): arbitrary suffix
      - v (boolean?): verso or recto or unspecified/not-applicable
      - c (string): column
      - l (string): line
      - word (string): reference word
    - end\* (`CodLocation`)
  - ranges (`CodLocationRange[]`)
  - ids (`AssertedCompositeId[]`):
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
    - assertion (`Assertion`)
  - size (`PhysicalSize`):
    - tag (string 📚 `physical-size-tags`)
    - w (`PhysicalDimension`):
      - value\* (number)
      - unit\* (string 📚 `physical-size-units`)
      - tag (string 📚 `physical-size-dim-tags`)
    - h (`PhysicalDimension`)
    - d (`PhysicalDimension`)
  - chronotopes (`AssertedChronotope[]`):
    - place (`AssertedPlace`)
      - tag (`string` 📚 `chronotope-tags`)
      - value (`string`)
      - assertion (`Assertion`):
        - tag (`string` 📚 `assertion-tags`)
        - rank (`short`)
        - references (`DocReference[]`):
          - type (`string` 📚 `doc-reference-types`)
          - tag (`string` 📚 `doc-reference-tags`)
          - citation (`string`)
          - note (`string`)
    - date (`AssertedDate`):
      - a* (`Datation`):
        - value* (`int`): the numeric value of the point. Its interpretation depends on other points properties: it may represent a year or a century, or a span between two consecutive Gregorian years.
        - isCentury (`boolean`): true if value is a century number; false if it's a Gregorian year.
        - isSpan (`boolean`): true if the value is the first year of a pair of two consecutive years. This is used for calendars which span across two Gregorian years, e.g. 776/5 BC.
        - month (`short`): the month number (1-12) or 0.
        - day (`short`): the day number (1-31) or 0.
        - isApproximate (`boolean`): true if the point is approximate ("about").
        - isDubious (`boolean`): true if the point is dubious ("perhaphs").
        - hint (`string`): a short textual hint used to better explain or motivate the datation point.
      - b (`Datation`)
      - tag (`string`)
      - assertion (`Assertion`)
  - description (`string`)

> ⚠️ `ids` was of type `AssertedId[]` before version 5.
