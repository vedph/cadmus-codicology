# Cadmus.Codicology

This solution contains a number of Cadmus parts related to codicology, originally stemming from the Itinera project, but designed to be generic enough to be useful in other projects.

## Parts

### CodShelfmarksPart

ID: `it.vedph.codicology.shelfmarks`.

Manuscript's shelfmark(s). Usually there is just one, unless you are also adding some historical signatures; in this case, assign a tag to the non-current (default) one.

- shelfmarks (CodShelfmark[]):
  - tag (string)
  - city* (string)
  - library* (string) T:cod-shelfmark-libraries
  - fund* (string)
  - location* (string)

### CodNumberingsPart

ID: `it.vedph.codicology.numberings`

Numberings on the manuscript's sheets. Each numbering system is fully described. One system is defined as the main system. This is the default reference system for manuscript locations.

While `ranges` represents the global extent of the numbering system in the manuscript, `spans` provides more details by specifying the extent of each span of regular numbering as related to some reference system. Each numbering span is defined with reference to a range in the system used as base (usually corresponding to the current physical state of the manuscript, where the first sheet is 1, the second is 2, etc.), and has a start value and an end value. It is assumed that all the values in between can be calculated according to the numbering system the span refers to (e.g. 1,2,3... or i,ii,iii... or A,B,C... etc.); so, whenever there is some incongruence we must start a new span.

- numberings (CodNumbering[]):
  - eid (string)
  - isMain (boolean)
  - isPagination (boolean)
  - system* (string) T:cod-numbering-systems
  - technique* (string) T:cod-numbering-techniques
  - position* (string) T:cod-numbering-positions
  - colors* (string[]) T:cod-numbering-colors
  - date (HistoricalDate)
  - ranges* (CodLocationRange[])
  - spans (CodNumberingSpan[]):
    - range (CodLocationRange)
    - start (string)
    - end (string)
  - issues (string)
