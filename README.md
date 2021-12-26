# Cadmus.Codicology

- [Cadmus.Codicology](#cadmuscodicology)
  - [Bricks](#bricks)
  - [Parts](#parts)
    - [CodBindingsPart](#codbindingspart)
    - [CodCatchwordsPart](#codcatchwordspart)
    - [CodEditsPart](#codeditspart)
    - [CodMaterialDscPart](#codmaterialdscpart)
    - [CodNumberingsPart](#codnumberingspart)
    - [CodQuiresPart](#codquirespart)
    - [CodShelfmarksPart](#codshelfmarkspart)
    - [CodWatermarksPart](#codwatermarkspart)

This solution contains a number of Cadmus parts related to codicology, originally stemming from the Itinera project, but designed to be generic enough to be useful in other projects.

## Bricks

The models of some bricks are summarized here for the reader's commodity.

- **PhysicalSize**:

  - tag (string) T:physical-size-tags
  - w\* (**PhysicalDimension**):
    - tag (string) T:physical-size-dim-tags
    - value\* (number)
    - unit\* (string) T:physical-size-units
  - h (PhysicalDimension)
  - d (PhysicalDimension)
  - note (string)

- **CodLocation**:

  - s (string): system
  - n\* (number): sheet number
  - v (boolean): verso or recto
  - c (string): column
  - l (string): line

- **CodLocationRange**:

  - start\* (CodLocation)
  - end\* (CodLocation)

- **DocReference**:

  - type (string) T:doc-reference-types
  - tag (string) T:doc-reference-tags
  - citation\* (string)
  - note (string)

- **Assertion**:

  - tag (string) T:assertion-tags
  - rank\* (number)
  - note (string)
  - references (DocReference[])

- **AssertedPlace**:

  - tag (string) T:asserted-place-tags
  - value\* (string)
  - assertion (Assertion)

- **AssertedDate**:

  - tag (string) T:asserted-date-tags
  - value\* (HistoricalDateModel)
  - assertion (Assertion)

- **AssertedChronotope**:

  - place (AssertedPlace)
  - date (AssertedDate)

## Parts

### CodBindingsPart

ID: `it.vedph.codicology.bindings`

- bindings (CodBinding[]):
  - tag (string) T:cod-binding-tags
  - chronotope (AssertedChronotope)
  - coverMaterial\* (string) T:cod-binding-cover-materials
  - supportMaterial\* (string) T:cod-binding-support-materials
  - size (PhysicalSize)
  - description (string)

### CodCatchwordsPart

ID: `it.vedph.codicology.catchwords`

- catchwords (CodCatchword[]):
  - range* (CodLocationRange)
  - position* (string) T:cod-catchwords-positions
  - isVertical (boolean)
  - decoration (string)
  - note (string)
- quireSignatures (CodQuireSignature[]):
  - range* (CodLocationRange)
  - position* (string) T:cod-quiresig-positions
  - system* (string) T:cod-quiresig-systems
  - note (string)
- quireRegSignatures (CodQuireRegSignature[]):
  - range* (CodLocationRange)
  - position* (string) T:cod-quiresig-positions
  - note (string)

### CodEditsPart

ID: `it.vedph.codicology.edits`

Specialized events related to any kind of text editing on the manuscript.

- edits (CodEdit[]):
  - eid (string)
  - type\* (string) T:cod-edit-types
  - language\* (string) T:cod-edit-languages
  - colors\* (string[]) T:cod-edit-colors
  - ranges\* (CodLocationRange[])
  - date (HistoricalDate)
  - description (string)
  - text (string)
  - references (DocReference[])

### CodMaterialDscPart

ID: `it.vedph.codicology.material-dsc`

- units (CodUnit[]):
  - eid (string)
  - tag (string) T:cod-unit-tag
  - material\* (string) T:cod-unit-materials
  - format\* (string) T:cod-unit-formats
  - state\* (string) T:cod-unit-states
  - range\* (CodLocationRange)
  - chronotopes (AssertedChronotope[])
  - noGregory (boolean)
  - note (string)
- palimpsests (Palimpsest[]):
  - range\* (CodLocationRange)
  - chronotope (AssertedChronotope)
  - note
- endLeaves (EndLeaf[]):
  - type\* (string) T:cod-endleaves-types
  - material\* (string) T:cod-endleaves-material
  - range\* (CodLocationRange)
  - date (AssertedDate)
  - note (string)

### CodNumberingsPart

ID: `it.vedph.codicology.numberings`

Numberings on the manuscript's sheets. Each numbering system is fully described. One system is defined as the main system. This is the default reference system for manuscript locations.

While `ranges` represents the global extent of the numbering system in the manuscript, `spans` provides more details by specifying the extent of each span of regular numbering as related to some reference system. Each numbering span is defined with reference to a range in the system used as base (usually corresponding to the current physical state of the manuscript, where the first sheet is 1, the second is 2, etc.), and has a start value and an end value. It is assumed that all the values in between can be calculated according to the numbering system the span refers to (e.g. 1,2,3... or i,ii,iii... or A,B,C... etc.); so, whenever there is some incongruence we must start a new span.

- numberings (CodNumbering[]):
  - eid (string)
  - isMain (boolean)
  - isPagination (boolean)
  - system\* (string) T:cod-numbering-systems
  - technique\* (string) T:cod-numbering-techniques
  - position\* (string) T:cod-numbering-positions
  - colors\* (string[]) T:cod-numbering-colors
  - date (HistoricalDate)
  - ranges\* (CodLocationRange[])
  - spans (CodNumberingSpan[]):
    - range (CodLocationRange)
    - start (string)
    - end (string)
  - issues (string)

### CodQuiresPart

ID: `it.vedph.codicology.quires`

- quires (CodQuire[]):
  - tag (string) T:cod-quire-tags
  - startNr* (number)
  - endNr* (number)
  - sheetCount* (number)
  - sheetDelta (number)
  - note (string)

### CodShelfmarksPart

ID: `it.vedph.codicology.shelfmarks`.

Manuscript's shelfmark(s). Usually there is just one, unless you are also adding some historical signatures; in this case, assign a tag to the non-current (default) one.

- shelfmarks (CodShelfmark[]):
  - tag (string)
  - city\* (string)
  - library\* (string) T:cod-shelfmark-libraries
  - fund\* (string)
  - location\* (string)

### CodWatermarksPart

ID: `it.vedph.codicology.watermarks`

Manuscript's watermarks.

- watermarks (CodWatermark[]):
  - name\* (string)
  - sampleRange\* (CodLocationRange)
  - ranges\* (CodLocationRange[])
  - ids (RankedExternalId[])
  - size (PhysicalSize)
  - chronotope (AssertedChronotope)
  - description (string)
