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

The models of some bricks are summarized here for the reader's commodity.

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

📖 [More information](https://github.com/vedph/cadmus-bricks-shell/blob/master/projects/myrmidon/cadmus-cod-location/README.md)

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

## History

### 7.0.0

- 2024-11-18: ⚠️ upgraded to .NET 9.

### 6.0.5

- 2024-09-27: updated packages.

### 6.0.4

- 2024-06-09: updated packages.

### 6.0.3

- 2024-05-24: updated packages.
- 2024-04-14: updated test packages.
- 2024-02-01: updated documentation.

### 6.0.1

- 2023-11-21: updated packages.

### 6.0.0

- 2023-11-18: ⚠️ Upgraded to .NET 8.

### 5.0.11

- 2023-09-11: updated packages.

### 5.0.10

- 2023-09-04: updated packages.

### 5.0.9

- 2023-08-28: updated packages.
- 2023-08-06:
  - add `CodLocation` macro for graph mappers in new library `Cadmus.Codicology.Graph`. If you need this macro, in your API startup DI configuration add the macro to the existing set when building `GraphUpdater`.
  - fixed `CodLocationEndleaf` out of synch values (frontend was more up to date).

### 5.0.8

- 2023-07-30: added `workId` to `CodContent`.

### 5.0.7

- 2023-07-24: added `authorIds` to `CodEdit`.

### 5.0.6

- 2023-07-17: added `ids` to `CodHand` for hand's identifications.

### 5.0.5

- 2023-06-23: updated packages.

### 5.0.4

- 2023-06-21: updated packages for Service library.

### 5.0.3

- 2023-06-21: updated packages.

### 5.0.2

- 2023-06-17: updated packages.

### 5.0.1

- 2023-06-02: updated packages.

### 5.0.0

- 2023-05-23: breaking changes following the introduction of [AssertedCompositeId](https://github.com/vedph/cadmus-bricks-shell/blob/master/projects/myrmidon/cadmus-refs-asserted-ids/README.md#asserted-composite-id) in general parts:
  - decorations part
  - watermarks part

### 4.2.0

- 2023-05-17: minor changes to models:
  - changed `CodWatermark` `chronotope` in `chronotopes`.
  - added `note` to `CodContentAnnotation`.

### 4.1.3

- 2023-05-16: updated packages.

### 4.1.2

- 2023-05-16: updated packages for services.

### 4.1.1

- 2023-05-12: updated packages.

### 4.1.0

- 2023-03-25:
  - changed `script` to `scripts` for hand instance. This allows for multiple scripts, in their relevance order.
  - added `isByScribe` to N-col definition.

### 4.0.2

- 2023-02-08: changed `CodHandSubscription.Range` (single range) into `Ranges` (multiple ranges).

### 4.0.1

- 2023-02-06: changed `CodUnit.Range` (single range) into `Ranges` (multiple ranges).

### 4.0.0

- 2023-02-02: migrated to new components factory. This is a breaking change for backend components, please see [this page](https://myrmex.github.io/overview/cadmus/dev/history/#2023-02-01---backend-infrastructure-upgrade). Anyway, in the end you just have to update your libraries and a single namespace reference. Benefits include:
  - more streamlined component instantiation.
  - more functionality in components factory, including DI.
  - dropped third party dependencies.
  - adopted standard MS technologies for DI.

### 3.0.1

- 2023-01-24: added `eid` pin to decorations part.

### 3.0.0

- 2022-11-10: upgraded to NET 7.

### 2.2.1

- 2022-11-04: updated packages.

### 2.2.0

- 2022-11-04: updated packages (nullability enabled in Cadmus core).

### 2.1.1

- 2022-11-03: updated packages.

### 2.1.0

- 2022-10-10: updated packages for new `IRepositoryProvider`.

### 2.0.8

- 2022-09-15: updated packages.

### 2.0.7

- 2022-08-04: fixed some thesaurus entries IDs in seeder.

### 2.0.6

- 2022-08-04: replaced `ExternalId` list with `AssertedId` list in `CodDecorationArtist`.
- 2022-08-03: fix codicology seeder location number.

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
