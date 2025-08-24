# Codicology Sheet Labels

🔑 `it.vedph.codicology.sheet-labels`

This part allows you to edit data about labels (numbering, catchwords and signatures) attached to manuscript sheets and their relationship with quires. This is a higher-abstraction level part deduced from refactoring parts initially designed as independent, but semantically and pragmatically connected.

The model focuses on the sequence of _physical sheets_ in the current arrangement of the manuscript (i.e. `1r`, `1v`, `2r`, `2v`, etc.). In relation with this sequence, quires describe sheets "grouping", while all the other properties describe labels on some sheets, usually following a pattern.

So, we can first imagine a bidimensional **table**, having 1 **row** per _physical sheet_ in the current manuscript, and thus being uniformly labelled like `1r`, `1v`, `2r`, `2v`, etc.

Each of these rows has a number of **columns** equal to all the labels we want to attach to the sheets, plus a fixed column to describe how sheets are related to _quires_. So, at a minimum we have a single column for quires. Usually anyway several other columns get added for numbering systems, catchwords, quire signatures, and quire register signatures.

- rows (`CodSheetRow[]`):
  - id\* (`string`): the physical sheet ID, equal to the physical sheet [CodLocation](cod-location.md) string value (`1r`, `1v`, `2r`, `2v`... etc).
  - columns (`CodSheetColumn[]`):
    - id\* (`string`): assigned according to a convention (see below).
    - value (`string`): the cell's value.
    - note (`string`)
- endleaves (`CodEndleaf[]`):
  - location\* (`string`): the endleaf location. This links these data to the endleaf row's ID in the table.
  - material\* (`string`) 📚 `cod-endleaf-materials`
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
- quireDescription (`CodQuireDescription`): specific data about quires:
  - features (`string[]` 📚 cod-quire-features)
  - note (`string`)
  - scopedNotes (dictionary of notes keyed by the quire ordinal number)
- nDefinitions (`CodSheetNColumnDefinition[]`): numbering on sheets:
  - id\* (`string`)
  - rank (`short`): a generic `rank` property which defines the rank for N definitions of the same type: e.g. the main numbering has rank=1, the second has rank=2, etc. Two numberings might also have the same rank if neither prevails. Also, this has the advantage of allowing several columns for quires, signatures, etc. should this be ever required because of different, conflicting descriptions.
  - note (`string`)
  - isPagination (bool)
  - isByScribe (bool)
  - system\* (`string` 📚 `cod-numbering-systems`)
  - technique\* (`string` 📚 `cod-numbering-techniques`)
  - position\* (`string` 📚 `cod-numbering-positions`)
  - colors (`string[]`)
  - date (`HistoricalDate`)
  - canonicalRanges (`CodLocationRange[]`)
- cDefinitions (`CodSheetCColumnDefinition[]`): catchwords on sheets:
  - id\* (`string`)
  - rank (short)
  - note (`string`)
  - position\* (`string` 📚 `cod-catchwords-positions`)
  - isVertical (bool)
  - decoration (`string`)
- sDefinitions (`CodSheetSColumnDefinition[]`): quire signatures on sheets:
  - id\* (`string`)
  - rank (short)
  - note (`string`)
  - system\* (`string` 📚 `cod-quiresig-systems`)
  - position\* (`string` 📚 `cod-quiresig-positions`)
- rDefinitions (`CodSheetRColumnDefinition[]`): quire register signatures:
  - id\* (`string`)
  - rank (short)
  - note (`string`)
  - position\* (`string` 📚 `cod-quiresig-positions`)

## Rows

Rows (=physical sheets) are numbered according to these conventions:

- front endleaf: `(Nr)` or `(Nv)`: e.g. `(1r)`=1 recto.
- back endleaf: `(/Nr)` or `(/Nv)`: e.g.
  `(/1r)`=1 recto.
- body sheet: `Nr` or `Nv`: e.g.  `1v`=1 verso.

## Columns

### Quires

Quires need no definitions, but they can have a description. Their labels have syntax `N.S/T` where:

- `N`=quire ordinal number;
- `S`=sheet ordinal number;
- `T`=total sheets in quire.

>`S` may be greater than `T` when sheets were added (e.g. `1.5/4`) or less than `T` when sheets were removed.

So, a typical quire sequence is like this:

sheet | quire
------|------
1r    | 1.1/4
1v    | 1.1/4
2r    | 1.2/4
2v    | 1.2/4
3r    | 1.3/4
3v    | 1.3/4
4r    | 1.4/4
4v    | 1.4/4
5r    | 2.1/4
5v    | 2.1/4

... etc.

### Other Columns

🔖 Conventions for assigning **column IDs**:

(1) (required) _prefix_ representing the ID type: one of:

- `n`: a numbering system.
- `q`: the quires description.
- `c`: catchwords.
- `s`: quire signatures.
- `r`: quire register signatures.

(2) dot (`.`) + an arbitrary _name_. If this part is missing, it is assumed that this is the default item for its type of column. For instance, `q` is the (obviously unique and thus default) description for quires; while `n` is the default numbering system, side by side with another numbering system `n.some-other`.

For instance, `n.roman-endleaf` is a numbering system named `roman-endleaf`; `q` is the quires description; `c` the catchwords description, etc.

## UI

The main view shows a grid having a row for each physical sheet, and a column for each label. The quires column is unique, and always has the ID `q`. All the other columns have an ID starting with their type prefix plus dot plus an arbitrary suffix (e.g. `n.roman`).

The `q` column is the quires column, which is unique. All the other columns are added at will, each with its own type and ID. To add a column, use the _adder tool_ by picking a type, and entering the ID when required. You can edit data about each column using the edit button next to the action's tool column picker.

The _action tool_ allows you to (1) pick a target column and (2) either edit it, or add new sheets (=rows) with the specified labels on it. To this end, it uses a simple **formula** allowing you to quickly enter rows and their labels in batches.

>Alternatively, you can always edit each cell's value by clicking on it. This is the only way to edit additional notes about any of the cell values.

The formula syntax for batch data entry is:

1. `N`=sheet number;
2. `r/v`=recto/verso (always `r` for quires);
3. `*`=sheet or `%`=page;
4. `N`=count of sheet/page labels to add;
5. `value`=value of the label(s) to add. Include in double quotes when it's a literal constant. Else, it can be an Arabic number, a Roman number, or a Latin/Greek letter, either uppercase or lowercase. In all these cases, labels will be generated automatically: e.g.:
   - `1r*3=xi` = assign labels `xi`, `xii`, and  `xiii` to sheets 1r, 2r, and 3r. Quires have a   special numbering, with form `qN/M` where N=sheet number and M=count of sheets in that quire.
   - `1x2=q1/4` = generate 16 labels like `1.1/4` for 1r and 1v, `1.2/4` for 2r and 2v, etc. where the initial number followed by dot is the quire ordinal number, while others correspond to N and M.

Also, you can use another syntax to just set the value of an arbitrarily defined set of cells. In this case, you specify any number of physical locations (e.g. `1r`) or location ranges (e.g. `1r-4v`), followed by `:=` and a single value to assign to all the listed cells.

---

## Model History

The original design, reflecting more traditional approaches, had these 3 parts:

- CodQuiresPart
- CodQuireLabelsPart
- CodNumberingsPart

The model for `CodQuiresPart` included any number of quires, each described with a start/end sheet number, a count of its sheets, and an optional delta number representing sheets added or removed to the original quire, plus eventually a note:

- quires (`CodQuire[]`):
  - tag (`string`) 📚cod-quire-tags
  - startNr\* (number)
  - endNr\* (number)
  - sheetCount\* (number)
  - sheetDelta (number)
  - note (`string`)

The model for `CodQuireLabelsPart` included all the "labels" attached to quires for structural purposes connected to the assembly of the book, i.e. catchwords and signatures on manuscript's quires:

- catchwords (`CodCatchword[]`):
  - range\* ([CodLocationRange](cod-location-range.md))
  - position\* (`string`) 📚cod-catchwords-positions
  - isVertical (boolean)
  - decoration (`string`)
  - note (`string`)
- quireSignatures (`CodQuireSignature[]`):
  - range\* ([CodLocationRange](cod-location-range.md))
  - position\* (`string`) 📚cod-quiresig-positions
  - system\* (`string`) 📚cod-quiresig-systems
  - note (`string`)
- quireRegSignatures (`CodQuireRegSignature[]`):
  - range\* ([CodLocationRange](cod-location-range.md))
  - position\* (`string`) 📚cod-quiresig-positions
  - note (`string`)

Here we had 3 lists for catchwords, quire signatures, and quire register signatures, each with the range of sheets covered.

Finally, the model for `CodNumberingsPart` described numberings on the manuscript's sheets:

- numberings (`CodNumbering[]`):
  - eid (`string`)
  - isMain (boolean)
  - isPagination (boolean)
  - system\* (`string`) 📚cod-numbering-systems
  - technique\* (`string`) 📚cod-numbering-techniques
  - position\* (`string`) 📚cod-numbering-positions
  - colors (`string`[]) 📚cod-numbering-colors
  - date (`HistoricalDate`)
  - ranges\* (`CodLocationRange[]`)
  - spans (`CodNumberingSpan[]`):
    - range ([CodLocationRange](cod-location-range.md))
    - start (`string`)
    - end (`string`)
  - issues (`string`)

While `ranges` represents the global extent of the numbering system in the manuscript, `spans` provides more details by specifying the extent of each span of regular numbering as related to some reference system. Each numbering span is defined with reference to a range in the system used as base (usually corresponding to the current physical state of the manuscript, where the first sheet is 1, the second is 2, etc.), and has a start value and an end value. It is assumed that all the values in between can be calculated according to the numbering system the span refers to (e.g. 1,2,3... or i,ii,iii... or A,B,C... etc.); so, whenever there is some incongruence we must start a new span.

Now, all these data are semantically connected, and one would want to see e.g. all the numberings systems together with their mapping to the physical quires, with their catchwords and signatures.

Also, from an abstract point of view, a more generic description model could be defined, focused on the sequence of physical sheets in the current arrangement of the manuscript (i.e. `1r`, `1v`, `2r`, `2v`, etc.). In relation with this sequence, quires describe sheets "grouping", while all the other properties describe labels on some sheets, usually following a pattern. That's why I named this part `CodSheetLabelsPart`: the name of the part derives from the prevalent meaning of its data.

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

|      | q     | n.alpha | n.beta | n.gamma | c | s | r |
|------|-------|---------|--------|---------|---|---|---|
| (1)  |       |         |        |         |   |   |   |
| (2)  |       |         |        |         |   |   |   |
| 1r   | 1.1/4 | i       | d      |         |   |   |   |
| 1v   | 1.1/4 |         | e      |         |   |   |   |
| 2r   | 1.2/4 | ii      | f      |         |   |   |   |
| 2v   | 1.2/4 |         | g      |         |   |   |   |
| 3r   | 1.3/4 |         |        | 1       |   |   |   |
| 3v   | 1.3/4 |         |        |         |   |   |   |
| 4r   | 1.4/4 |         |        | 2       |   |   |   |
| 4v   | 1.4/4 |         | x      |         |   |   |   |
| 5r   | 2.1/4 |         |        | 3       |   |   |   |
| 5v   | 2.2/4 |         |        |         |   |   |   |
| (/1) |       |         |        |         |   |   |   |

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

- _n.alpha_: `1r*2=i` i.e. insert a Roman number (the type of numbering is specified once in the column definition) starting from `1`, from sheet 1r for 2 sheets. This produces 1r=`i` and 2r=`ii`. We can allow users enter either the digits, i.e. `1r*2=1`, or the Roman number itself. The system will be able to process both.
- _n.beta_: `1r%4=d` (this is an alphabetic numeration; here too, we could have also written `1r%4=4`) and `4v=x` (non-predictable value). The first operation produces 1r=`d`, 1v=`e`, 2r=`f`, 2v=`g`; the second operation produces 4v=`x`.
- _n.gamma_: `3r*3=1` (an Arabic numeration): this produces 3r=`1`, 4r=`2`, 5r=`3`.

The same would go for any other column. The quires column should have its own syntax, which reflects the traditional formula. This is not a problem because the quire type stands on its own in the context of this model. The UI will be the same, because it changes it behavior whenever the selected column changes.

So, you can imagine a user starting with a blank table. He adds a column of a specific type, fills its details in the bottom editor, and then starts entering formulas to fill the column. He enters one, presses enter, and sees the table filled; then another one, presses enters, and sees the table filled again; etc. This will be very quick and effective.

We could also add another way of entering data in a cell by letting user clicking a value in the cell to edit it with its note. So, this adds a point-and-click edit useful for specific, non predictable cells; while most of them will be automatically filled using formulas.

This provides an editor where the data entry UI also provides real-time feedback about all the numbering and other labeling systems, their equivalences, and their relationships with quires. Also, the model is uniform and so is the data entry strategy.
