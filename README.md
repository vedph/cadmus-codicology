# Cadmus Codicology

- [Cadmus Codicology](#cadmus-codicology)
  - [Bricks](#bricks)
  - [Parts](#parts)
    - [CodBindingsPart](#codbindingspart)
    - [CodContentsPart](#codcontentspart)
    - [CodDecorationsPart](#coddecorationspart)
    - [CodEditsPart](#codeditspart)
    - [CodHandsPart](#codhandspart)
    - [CodLayoutsPart](#codlayoutspart)
    - [CodMaterialDscPart](#codmaterialdscpart)
    - [CodSheetLabelsPart](#codsheetlabelspart)
    - [CodShelfmarksPart](#codshelfmarkspart)
    - [CodWatermarksPart](#codwatermarkspart)
  - [History](#history)
    - [2.0.5](#205)
    - [2.0.3](#203)
    - [2.0.2](#202)
    - [2.0.1](#201)
    - [2.0.0](#200)

This solution contains a number of Cadmus parts related to codicology, originally stemming from the *Itinera* project, but designed to be generic enough to be useful in other projects.

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

- **HistoricalDate**:
  - a\* (Datation):
    - value\* (int)
    - isCentury (boolean)
    - isSpan (boolean)
    - isApproximate (boolean)
    - isDubious (boolean)
    - day (int)
    - month (int)
    - hint (string
  - b (Datation)

- **CodLocation**:

  - endleaf (int): 0=none 1=start 2=end
  - s (string): system
  - n\* (int): sheet number
  - rmn (boolean): Roman system for n
  - sfx (string): arbitrary suffix
  - v (boolean?): verso or recto or unspecified/not-applicable
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
  - references (DocReference[]) T:doc-reference-types, T:doc-reference-tags

- **AssertedPlace**:

  - tag (string) T:asserted-place-tags
  - value\* (string)
  - assertion (Assertion) T:assertion-tags, T:doc-reference-types, T:doc-reference-tags

- **AssertedDate**: equal to `HistoricalDate` plus:

  - tag (string) T:asserted-date-tags
  - assertion (Assertion) T:assertion-tags, T:doc-reference-types, T:doc-reference-tags

- **AssertedChronotope**:

  - place (AssertedPlace) T:chronotope-tags, T:assertion-tags, T:doc-reference-types, T:doc-reference-tags
  - date (AssertedDate) T:chronotope-tags, T:assertion-tags, T:doc-reference-types, T:doc-reference-tags

## Parts

### CodBindingsPart

ID: `it.vedph.codicology.bindings`

- bindings (CodBinding[]):
  - tag (string) T:cod-binding-tags
  - coverMaterial\* (string) T:cod-binding-cover-materials
  - boardMaterial\* (string) T:cod-binding-board-materials
  - chronotope\* (AssertedChronotope) T:chronotope-tags, T:assertion-tags, T:doc-reference-types, T:doc-reference-tags
  - size (PhysicalSize) T:physical-size-tags, T:physical-size-dim-tags, T:physical-size-units
  - description (string)

### CodContentsPart

ID: `it.vedph.codicology.contents`

- contents (CodContent[]):
  - eid (string)
  - range\* (CodLocationRange)
  - states\* (string[]) T:cod-content-states
  - title\* (string)
  - location (string)
  - claimedAuthor (string)
  - claimedTitle (string)
  - tag (string) T:cod-content-tags
  - note (string)
  - incipit (string)
  - explicit (string)
  - annotations (CodContentAnnotation[]):
    - type\* (string) T:cod-content-annotation-types
    - range\* (CodLocationRange)
    - incipit\* (string)
    - explicit\* (string)
    - text\* (string)

### CodDecorationsPart

ID: `it.vedph.codicology.decorations`

- decorations (CodDecoration[]):
  - eid (string)
  - name\* (string)
  - flags (string[]) T:cod-decoration-flags
  - chronotopes (AssertedChronotope[]) T:assertion-tags, T:doc-reference-types, T:doc-reference-tags
  - artists (CodDecorationArtist[]):
    - eid (string)
    - type\* (string) T:cod-decoration-artist-types
    - name\* (string)
    - ids (ExternalId[])
    - styles (CodDecorationArtistStyle[]):
      - name\* (string) T:cod-decoration-artist-style-names
      - chronotope (AssertedChronotope)
      - assertion (Assertion)
    - elementKeys (string[])
    - note (string)
  - note (string)
  - references (DocReference[]) T:doc-reference-types, T:doc-reference-tags
  - elements (CodDecorationElement[]):
    - key (string)
    - parentKey (string)
    - type\* (string) T:cod-decoration-element-types
    - flags (string[]) T:cod-decoration-element-flags
    - ranges\* (CodLocationRange[])
    - instanceCount (int)
    - typologies (string) T:cod-decoration-element-typologies
    - subject (string)
    - colors (string[]) T:cod-decoration-element-colors
    - gildings (string[]) T:cod-decoration-element-gildings
    - techniques (string[]) T:cod-decoration-element-techniques
    - tools (string[]) T:cod-decoration-element-tools
    - positions (string[]) T:cod-decoration-element-positions
    - lineHeight (int)
    - textRelation (string)
    - description (string MD)
    - images (CodImage[]):
      - id\* (string)
      - type\* (string) T:cod-image-types
      - sourceId (string)
      - label (string)
      - copyright (string)
    - note (string)

### CodEditsPart

ID: `it.vedph.codicology.edits`

Specialized events related to any kind of text editing on the manuscript.

- edits (CodEdit[]):
  - eid (string)
  - type\* (string) T:cod-edit-types
  - tag (string) T:cod-edit-tags
  - techniques (string[]) T:cod-edit-techniques
  - ranges\* (CodLocationRange[])
  - language (string) T:cod-edit-languages
  - colors (string[]) T:cod-edit-colors
  - date (HistoricalDate)
  - description (string)
  - text (string)
  - references (DocReference[]) T:doc-reference-types, T:doc-reference-tags

### CodHandsPart

ID: `it.vedph.codicology.hands`

- hands (CodHand[]):
  - eid (string)
  - name (string)
  - instances (CodHandInstance)
    - script\* (string) T:cod-hand-scripts
    - typologies\* (string[]) T:cod-hand-typologies
    - colors (string[]) T:cod-hand-colors
    - ranges\* (CodLocationRange[])
    - rank (short)
    - descriptionKey (string)
    - chronotope (AssertedChronotope) T:assertion-tags, T:doc-reference-types, T:doc-reference-tags
    - images (CodImage[]):
      - id\* (string)
      - type\* (string) T:cod-image-types
      - sourceId (string)
      - label (string)
      - copyright (string)
  - descriptions (CodHandDescription[]):
    - key (string): this is referenced by `CodHandInstance.descriptionKey`
    - description (string)
    - initials (string)
    - corrections (string)
    - punctuation (string)
    - abbreviations (string)
    - signs (CodHandSign[]):
      - eid (string)
      - type\* (string) T:cod-hand-sign-types
      - sampleLocation\* (CodLocation)
      - description (string)
  - subscriptions (CodHandSubscription[]):
    - range\* (CodLocationRange)
    - language\* (string) T:cod-hand-subscription-languages
    - text (string)
    - note (string)
  - references (DocReference[]) T:doc-reference-types, T:doc-reference-tags

### CodLayoutsPart

ID: `it.vedph.codicology.layouts`

- layouts (CodLayout[]):
  - sample\* (CodLocation)
  - ranges\* (CodLocationRange[])
  - dimensions (PhysicalDimension[]) T for dimension tag: cod-layout-dimension-tags, T:physical-size-dim-tags, T:physical-size-units
  - rulingTechnique (string) T:cod-layout-ruling-techniques
  - derolez (string) T:cod-layout-derolez
  - pricking (string) T:cod-layout-prickings
  - columnCount\* (int)
  - counts (DecoratedCount[]):
    - id* (string) T:cod-layout-counts
    - value* (int)
    - tag (string)
    - note (string)
  - tag (string) T:cod-layout-tags
  - note (string)

### CodMaterialDscPart

ID: `it.vedph.codicology.material-dsc`

- units\* (CodUnit[]):
  - eid (string)
  - tag (string) T:cod-unit-tags
  - material\* (string) T:cod-unit-materials
  - format\* (string) T:cod-unit-formats
  - state\* (string) T:cod-unit-states
  - range\* (CodLocationRange)
  - chronotopes\* (AssertedChronotope[]) T:chronotope-tags, T:assertion-tags, T:doc-reference-types, T:doc-reference-tags
  - noGregory (boolean)
  - note (string)
- palimpsests (CodPalimpsest[]):
  - range\* (CodLocationRange)
  - chronotope (AssertedChronotope) T:chronotope-tags, T:assertion-tags, T:doc-reference-types, T:doc-reference-tags
  - note (string)

Note: endleaves are described in `CodSheetLabelsPart`.

### CodSheetLabelsPart

ID: `it.vedph.codicology.sheet-labels`

This is a higher-abstraction level part deduced from refactoring parts initially designed as independent, but semantically and pragmatically connected.

The original design, reflecting more traditional approaches, had these 3 parts:

- CodQuiresPart
- CodQuireLabelsPart
- CodNumberingsPart

The model for `CodQuiresPart` included any number of quires, each described with a start/end sheet number, a count of its sheets, and an optional delta number representing sheets added or removed to the original quire, plus eventually a note:

- quires (CodQuire[]):
  - tag (string) T:cod-quire-tags
  - startNr\* (number)
  - endNr\* (number)
  - sheetCount\* (number)
  - sheetDelta (number)
  - note (string)

The model for `CodQuireLabelsPart` included all the "labels" attached to quires for structural purposes connected to the assembly of the book, i.e. catchwords and signatures on manuscript's quires:

- catchwords (CodCatchword[]):
  - range\* (CodLocationRange)
  - position\* (string) T:cod-catchwords-positions
  - isVertical (boolean)
  - decoration (string)
  - note (string)
- quireSignatures (CodQuireSignature[]):
  - range\* (CodLocationRange)
  - position\* (string) T:cod-quiresig-positions
  - system\* (string) T:cod-quiresig-systems
  - note (string)
- quireRegSignatures (CodQuireRegSignature[]):
  - range\* (CodLocationRange)
  - position\* (string) T:cod-quiresig-positions
  - note (string)

Here we had 3 lists for catchwords, quire signatures, and quire register signatures, each with the range of sheets covered.

Finally, the model for `CodNumberingsPart` described numberings on the manuscript's sheets:

- numberings (CodNumbering[]):
  - eid (string)
  - isMain (boolean)
  - isPagination (boolean)
  - system\* (string) T:cod-numbering-systems
  - technique\* (string) T:cod-numbering-techniques
  - position\* (string) T:cod-numbering-positions
  - colors (string[]) T:cod-numbering-colors
  - date (HistoricalDate)
  - ranges\* (CodLocationRange[])
  - spans (CodNumberingSpan[]):
    - range (CodLocationRange)
    - start (string)
    - end (string)
  - issues (string)

While `ranges` represents the global extent of the numbering system in the manuscript, `spans` provides more details by specifying the extent of each span of regular numbering as related to some reference system. Each numbering span is defined with reference to a range in the system used as base (usually corresponding to the current physical state of the manuscript, where the first sheet is 1, the second is 2, etc.), and has a start value and an end value. It is assumed that all the values in between can be calculated according to the numbering system the span refers to (e.g. 1,2,3... or i,ii,iii... or A,B,C... etc.); so, whenever there is some incongruence we must start a new span.

Now, all these data are semantically connected, and one would want to see e.g. all the numberings systems together with their mapping to the physical quires, with their catchwords and signatures.

Also, from an abstract point of view, a more generic description model could be defined, focused on the sequence of physical sheets in the current arrangement of the manuscript (i.e. `1r`, `1v`, `2r`, `2v`, etc.). In relation with this sequence, quires describe sheets "grouping", while all the other properties describe labels on some sheets, usually following a pattern. That's why I named this part `CodSheetLabelsPart`: the name of the part derives from the prevalent meaning of its data.

So, we can first imagine a bidimensional **table**, having 1 **row** per *physical sheet* in the current manuscript, and thus being uniformly labelled like `1r`, `1v`, `2r`, `2v`... etc.

Each of these rows has a number of **columns** equal to all the labels we want to attach to the sheets, plus 0 or 1 column to describe how sheets are related to quires. So, in the minimalist scenario we just have a single column for quires. Usually anyway several other columns get added for numbering systems, catchwords, quire signatures, and quire register signatures.

Let us start with our table model:

- rows (`CodRow[]`):
  - id\*: this is the row's ID, like `1r`. While it could be easily calculated from the row's position, we prefer an explicit ID because this is safer while editing: it's the real ID of each record representing a row.
  - columns (`CodColumn[]`):
    - id\*: this is the column ID: as above, we prefer an explicit ID because these data are subject to editing. The ID is arbitrarily assigned following a set of conventions (see below).
    - value: the value in the cell.
    - note: an optional short note.

Conventions for assigning column IDs:

(1) (required) prefix representing the ID type: one of:

- `n`: a numbering system.
- `q`: the quires description.
- `c`: catchwords.
- `s`: quire signatures.
- `r`: quire register signatures.

(2) dot (`.`) + an arbitrary name. If this part is missing, it is assumed that this is the default item for its type of column. For instance, `q` is the (obviously unique and thus default) description for quires; while `n` is the default numbering system, side by side with another numbering system `n.some-other`.

For instance, `n.roman-endleaf` is a numbering system, `q` is the quires description, `c` the catchwords description, etc.

We then have the definitions for the numbering systems:

- nDefinitions (`CodNColDefinition[]`):
  - id\* (string): the column ID.
  - rank (short): a generic `rank` property which defines the rank for N definitions of the same type: e.g. the main numbering has rank=1, the second has rank=2, etc. Two numberings might also have the same rank if neither prevails. Also, this has the advantage of allowing several columns for quires, signatures, etc. should this be ever required because of different conflicting descriptions.
  - isPagination (boolean)
  - system\* (string) T:cod-numbering-systems
  - technique\* (string) T:cod-numbering-techniques
  - position\* (string) T:cod-numbering-positions
  - colors (string[]) T:cod-numbering-colors
  - date (`HistoricalDate`)
  - note (string)

The definitions for catchwords:

- cDefinitions (`CodCColDefinition[]`):
  - id\* (string): the column ID.
  - rank (short)
  - position\* (string) T:cod-catchwords-positions
  - isVertical (boolean)
  - decoration (string)
  - note (string)

The definitions for quire signatures:

- sDefinitions (`CodSColDefinition[]`):
  - id\* (string): the column ID.
  - rank (short)
  - system* (string) T:cod-quiresig-systems
  - position* (string) T:cod-quiresig-positions
  - note (string)

The definitions for quire register signatures (identical to the preceding one, except for the missing `system`):

- rDefinitions (`CodRColDefinition[]`):
  - id\* (string): the column ID.
  - rank (short)
  - position* (string) T:cod-quiresig-positions
  - note (string)

Quires need no definitions. In the end, the top level properties of the part's model would thus be:

- rows
- nDefinitions
- cDefinitions
- sDefinitions
- rDefinitions

Finally, as endleaves are detailed here (as head/tail rows in the table, with their own location numbering based on sheet 0: see below), we also add some more details about each endleaf where required:

- endleaves (EndLeaf[]):
  - location\* (string): the endleaf location. This links these data to the endleaf row's ID in the table.
  - material\* (string) T:cod-endleaf-materials
  - chronotope (AssertedChronotope)

Once we have this model, let us see the imagined input method. The UI is focused on the table. The table will be displayed in its entirety, having a row per sheet, and a number of columns from 1 to N.

The first column is the quires column. All the other columns are added by user.

The controls are:

```txt
[column|V](x) [type|V](+)
[operation](+)

[table...]

[editor...]
```

The UI has 3 bands:

- the top band has a dropdown to select the current column, a button to remove it, and a button to add a new column of a specific type (the type is specified by another dropdown). The operation textbox is used to enter value-insertion operations which can insert a lot of values at once using a simple formula. This is the preferred input method except for corner cases (see below). We will also add controls to add rows and endleaves.

- the mid band is the table (see below).

- the bottom band is the editor for each column definition. It allows editing n-definitions, c-definitions, or s-definition for the currently selected column.

The table is like in this sample, where I just fill `n` and `q` types for brevity:

|    | q     | n.alpha | n.beta | n.gamma | c | s | r |
|----|-------|---------|--------|---------|---|---|---|
|(1) |       |         |        |         |   |   |   |
|(2) |       |         |        |         |   |   |   |
| 1r | 1.1/4 | i       | d      |         |   |   |   |
| 1v | 1.1/4 |         | e      |         |   |   |   |
| 2r | 1.2/4 | ii      | f      |         |   |   |   |
| 2v | 1.2/4 |         | g      |         |   |   |   |
| 3r | 1.3/4 |         |        | 1       |   |   |   |
| 3v | 1.3/4 |         |        |         |   |   |   |
| 4r | 1.4/4 |         |        | 2       |   |   |   |
| 4v | 1.4/4 |         | x      |         |   |   |   |
| 5r | 2.1/4 |         |        | 3       |   |   |   |
| 5v | 2.2/4 |         |        |         |   |   |   |
|(/1)|       |         |        |         |   |   |   |

As you can see, the reference physical sheets represent rows; each added column is shown next to it. Here the user added quires, 3 numberings, catchwords, signatures, and register signatures.

In most cases it's easy to fill a column with values without having to click on the cell and editing it. This happens because most of these annotations are distributed according to patterns, which can be expressed with formulas.

Quire labels have syntax `N.S/T` where `N`=quire ordinal number, `S`=sheet ordinal number, `T`=total sheets in quire; `S` may be greater than `T` when sheets were added (e.g. `1.5/4`) or less than `T` when sheets were removed.

A generic way of representing an insert-values action uses the syntax:

`([0-9]+)([rv])?\s*[*%]\s*([0-9]+)\s*=\s*([^\s]+)`

i.e. it defines:

- the sheet to start with (e.g. `1r`);
- the sheets/pages to repeat the value for. This is the count of sheets/pages preceded by `*` or `%` respectively. The count is omitted for single values, which are input whenever there is a non-predictable value.
- after the `=` sign, the value to start with in the respective system.

So, looking at the above table, we could use these operations:

- *n.alpha*: `1r*2=i` i.e. insert a Roman number (the type of numbering is specified once in the column definition) starting from `1`, from sheet 1r for 2 sheets. This produces 1r=`i` and 2r=`ii`. We can allow users enter either the digits, i.e. `1r*2=1`, or the Roman number itself. The system will be able to process both.
- *n.beta*: `1r%4=d` (this is an alphabetic numeration; here too, we could have also written `1r%4=4`) and `4v=x` (non-predictable value). The first operation produces 1r=`d`, 1v=`e`, 2r=`f`, 2v=`g`; the second operation produces 4v=`x`.
- *n.gamma*: `3r*3=1` (an Arabic numeration): this produces 3r=`1`, 4r=`2`, 5r=`3`.

The same would go for any other column. The quires column should have its own syntax, which reflects the traditional formula. This is not a problem because the quire type stands on its own in the context of this model. The UI will be the same, because it changes it behavior whenever the selected column changes.

So, you can imagine a user starting with a blank table. He adds a column of a specific type, fills its details in the bottom editor, and then starts entering formulas to fill the column. He enters one, presses enter, and sees the table filled; then another one, presses enters, and sees the table filled again; etc. This will be very quick and effective.

We could also add another way of entering data in a cell by letting user clicking a value in the cell to edit it with its note. So, this adds a point-and-click edit useful for specific, non predictable cells; while most of them will be automatically filled using formulas.

This provides an editor where the data entry UI also provides real-time feedback about all the numbering and other labeling systems, their equivalences, and their relationships with quires. Also, the model is uniform and so is the data entry strategy.

### CodShelfmarksPart

ID: `it.vedph.codicology.shelfmarks`

Manuscript's shelfmark(s). Usually there is just one, unless you are also adding some historical signatures; in this case, assign a tag to the non-current (default) one.

- shelfmarks (CodShelfmark[]):
  - tag (string) T:cod-shelfmark-tags
  - city\* (string)
  - library\* (string) T:cod-shelfmark-libraries
  - fund (string)
  - location\* (string)

### CodWatermarksPart

ID: `it.vedph.codicology.watermarks`

Manuscript's watermarks.

- watermarks (CodWatermark[]):
  - name\* (string)
  - sampleRange\* (CodLocationRange)
  - ranges (CodLocationRange[])
  - ids (AssertedId[]) T:id-tags, T:id-scopes, T:assertion-tags, T:doc-reference-types, T:doc-reference-tags
  - size (PhysicalSize) T:physical-size-tags, T:physical-size-dim-tags, T:physical-size-units
  - chronotope (AssertedChronotope) T:chronotope-tags, T:assertion-tags, T:doc-reference-types, T:doc-reference-tags
  - description (string)

## History

### 2.0.5

- 2022-08-03: replaced `ExternalId` with `AssertedId` in `CodWatermark`.
- 2022-08-01: fix to `SeedHelper.Truncate` (float instead of double).

### 2.0.3

- 2022-07-23:
  - made projects nullable.
  - `CodContent`: added `Author` and changed `Range` into `Ranges`.

### 2.0.2

- 2022-06-19: updated packages.

### 2.0.1

- 2022-05-18: updated packages.

### 2.0.0

- 2022-04-29: upgraded to NET 6.0.
