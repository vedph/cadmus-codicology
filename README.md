# Cadmus Codicology

- [bindings](docs/cod-bindings.md)
- [contents](docs/cod-contents.md)
- [decorations](docs/cod-decorations.md)
- [edits](docs/cod-edits.md)
- [hands](docs/cod-hands.md)
- [layouts](docs/cod-layouts.md)
- [material](docs/cod-material-dsc.md)
- [sheet labels](docs/cod-sheet-labels.md)
- [shelfmarks](docs/cod-shelfmarks.md)
- [watermarks](docs/cod-watermarks.md)

This solution contains a number of Cadmus parts related to codicology, originally stemming from the *Itinera* project, but designed to be generic enough to be useful in other projects.

## Bricks

The models of some [bricks](https://github.com/vedph/cadmus-bricks-shell-v3) are summarized here for the reader's commodity.

- **PhysicalSize**:

  - tag (string) T:physical-size-tags
  - w\* (`PhysicalDimension`):
    - tag (string) T:physical-size-dim-tags
    - value\* (number)
    - unit\* (string) T:physical-size-units
  - h (`PhysicalDimension`)
  - d (`PhysicalDimension`)
  - note (string)

- **HistoricalDate**:
  - a\* (`Datation`):
    - value\* (int)
    - isCentury (boolean)
    - isSpan (boolean)
    - isApproximate (boolean)
    - isDubious (boolean)
    - day (int)
    - month (int)
    - hint (string)
  - b (`Datation`)

- **CodLocation**:

  - endleaf (int): 0=none 1=start 2=end
  - s (string): system
  - n\* (int): sheet number
  - rmn (boolean): Roman system for `n`
  - sfx (string): arbitrary suffix
  - v (boolean?): verso or recto or unspecified/not-applicable
  - c (string): column
  - l (string): line
  - word (string): reference word

- **CodLocationRange**:

  - start\* (`CodLocation`)
  - end\* (`CodLocation`)

- **DocReference**:

  - type (string) T:doc-reference-types
  - tag (string) T:doc-reference-tags
  - citation\* (string)
  - note (string)

- **Assertion**:

  - tag (string) T:assertion-tags
  - rank\* (number)
  - note (string)
  - references (`DocReference[]`) T:doc-reference-types, T:doc-reference-tags

- **AssertedPlace**:

  - tag (string) T:asserted-place-tags
  - value\* (string)
  - assertion (`Assertion`) T:assertion-tags, T:doc-reference-types, T:doc-reference-tags

- **AssertedDate**: equal to `HistoricalDate` plus:

  - tag (string) T:asserted-date-tags
  - assertion (`Assertion`) T:assertion-tags, T:doc-reference-types, T:doc-reference-tags

- **AssertedChronotope**:

  - place (`AssertedPlace`) T:chronotope-tags, T:assertion-tags, T:doc-reference-types, T:doc-reference-tags
  - date (`AssertedDate`) T:chronotope-tags, T:assertion-tags, T:doc-reference-types, T:doc-reference-tags
