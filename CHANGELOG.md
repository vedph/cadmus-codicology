## History

## 9.0.5

- 2026-07-21: updated packages and set packaging to release, adding the necessary configuration to each library project.
- 2026-06-13: updated packages.

## 9.0.2

- 2026-01-30: added optional gaps to `CodContent`.

## 9.0.1

- 2026-01-17: updated packages.

## 9.0.0

- 2025-11-23: ⚠️ upgraded to NET 10.

## 8.0.19

- 2025-11-21: configurable seeders.

## 8.0.18

- 2025-11-09: updated packages.

## 8.0.17

- 2025-09-19: added `links` to decoration element.

## 8.0.16

- 2025-09-15: updated packages.

## 8.0.15

- 2025-09-12:
  - updated test packages.
  - fix to sample page range in layout seeder.

## 8.0.14

- 2025-07-22: changed language codes in seeders to conform to BCP47.

## 8.0.13

- 2025-07-15: added optional `formula` to layout.

## 8.0.12

- 2025-07-14: updated packages.
- 2025-06-26: updated test packages.

## 8.0.11

- 2025-05-23: added `element-key` pin to decorations part.

## 8.0.10

- 2025-05-15: add `QuireDescription` to `CodSheetLabelsPart`.

## 8.0.8

- 2025-05-14:
  - added `CodSheetNColumnDefinition.canonicalRanges`.
  - removed `CodSheetLabelsPart`.`Note`.
- 2025-05-06: updated test packages.

## 8.0.6

- 2025-05-02: updated packages.

## 8.0.5

Version 8 is be the next major version, and includes minor changes to some of the models. Only a couple of changes are breaking changes; all the others are just additions.

- 2025-04-18: added two new properties to `CodDecorationElement`: ➕ `RefSign` and `References`.
- 2025-03-25:
  - added `position` to `CodEditsPart`.
- 2025-03-11: changes to models (⚠️=breaking changes!):
  - `CodDecorationElement`: ➕ add `tag` (`string`, optional thesaurus 📚 `cod-decoration-element-tags`).
  - `CodWatermark`: ➕ add `rangesAsQuire` checkbox (`boolean`) meaning that `ranges` refers to quires rather than sheets.
  - `CodContent`: ➕ add `ClaimedAuthorRanges` (`CodLocationRange[]`) and `ClaimedTitleRanges` (`CodLocationRange[]`) for ranges referring to claimed author and title.
  - `CodContentAnnotation`:
    - ➕ add `Features` (`string[]`, flags) with its thesaurus 📚 `cod-content-annotation-features`.
    - ➕ add `Languages` (`string[]`, flags) with its optional thesaurus 📚 `cod-content-annotation-languages`.
  - `CodHandInstance`: ➕ add `Note` (`string`).
  - `CodHandDescription`: ➕ add `Note` (`string`).
  - `CodHandSign`: ➕ add `Mufi` (`number`) for the corresponding MUFI code. This has also been added to the part's pins.
  - `CodEndleaf`: ➕ add `Note` (`string`).
  - ⚠️ `CodPalimpsest`: change `Range` (`CodLocationRange`) into `Ranges` (`CodLocationRange[]`).
  - ⚠️ change `RulingTechnique` (`string`) into `RulingTechniques` (`string[]`, flags).
  - `CodSheetLabelsPart`: ➕ add `Note` (`string`). This will be mainly used to add notes about quires.
  - `CodSheetColumnDefinition`: ➕ add `Links` (`AssertedCompositeIds[]`).
  - `CodSheetColumn`: ➕ add `Features` (`string[]`, flags from thesauri different according to the column type: 📚 `cod-labels-col-n-features`, 📚 `cod-labels-col-c-features`, 📚 `cod-labels-col-s-features`, 📚 `cod-labels-col-r-features`, plus one for quires: 📚 `cod-labels-col-q-features`.

- 2025-03-10: updated packages.

## 7.0.3

- 2025-02-14: updated packages.

## 7.0.2

- 2025-01-28: updated packages.
- 2024-12-26: updated test packages.

## 7.0.1

- 2024-11-30: updated packages.
- 2024-11-20: updated test packages.

## 7.0.0

- 2024-11-18: ⚠️ upgraded to .NET 9.

## 6.0.5

- 2024-09-27: updated packages.

## 6.0.4

- 2024-06-09: updated packages.

## 6.0.3

- 2024-05-24: updated packages.
- 2024-04-14: updated test packages.
- 2024-02-01: updated documentation.

## 6.0.1

- 2023-11-21: updated packages.

## 6.0.0

- 2023-11-18: ⚠️ Upgraded to .NET 8.

## 5.0.11

- 2023-09-11: updated packages.

## 5.0.10

- 2023-09-04: updated packages.

## 5.0.9

- 2023-08-28: updated packages.
- 2023-08-06:
  - add `CodLocation` macro for graph mappers in new library `Cadmus.Codicology.Graph`. If you need this macro, in your API startup DI configuration add the macro to the existing set when building `GraphUpdater`.
  - fixed `CodLocationEndleaf` out of synch values (frontend was more up to date).

## 5.0.8

- 2023-07-30: added `workId` to `CodContent`.

## 5.0.7

- 2023-07-24: added `authorIds` to `CodEdit`.

## 5.0.6

- 2023-07-17: added `ids` to `CodHand` for hand's identifications.

## 5.0.5

- 2023-06-23: updated packages.

## 5.0.4

- 2023-06-21: updated packages for Service library.

## 5.0.3

- 2023-06-21: updated packages.

## 5.0.2

- 2023-06-17: updated packages.

## 5.0.1

- 2023-06-02: updated packages.

## 5.0.0

- 2023-05-23: breaking changes following the introduction of [AssertedCompositeId](https://github.com/vedph/cadmus-bricks-shell/blob/master/projects/myrmidon/cadmus-refs-asserted-ids/README.md#asserted-composite-id) in general parts:
  - decorations part
  - watermarks part

## 4.2.0

- 2023-05-17: minor changes to models:
  - changed `CodWatermark` `chronotope` in `chronotopes`.
  - added `note` to `CodContentAnnotation`.

## 4.1.3

- 2023-05-16: updated packages.

## 4.1.2

- 2023-05-16: updated packages for services.

## 4.1.1

- 2023-05-12: updated packages.

## 4.1.0

- 2023-03-25:
  - changed `script` to `scripts` for hand instance. This allows for multiple scripts, in their relevance order.
  - added `isByScribe` to N-col definition.

## 4.0.2

- 2023-02-08: changed `CodHandSubscription.Range` (single range) into `Ranges` (multiple ranges).

## 4.0.1

- 2023-02-06: changed `CodUnit.Range` (single range) into `Ranges` (multiple ranges).

## 4.0.0

- 2023-02-02: migrated to new components factory. This is a breaking change for backend components, please see [this page](https://myrmex.github.io/overview/cadmus/dev/history/#2023-02-01---backend-infrastructure-upgrade). Anyway, in the end you just have to update your libraries and a single namespace reference. Benefits include:
  - more streamlined component instantiation.
  - more functionality in components factory, including DI.
  - dropped third party dependencies.
  - adopted standard MS technologies for DI.

## 3.0.1

- 2023-01-24: added `eid` pin to decorations part.

## 3.0.0

- 2022-11-10: upgraded to NET 7.

## 2.2.1

- 2022-11-04: updated packages.

## 2.2.0

- 2022-11-04: updated packages (nullability enabled in Cadmus core).

## 2.1.1

- 2022-11-03: updated packages.

## 2.1.0

- 2022-10-10: updated packages for new `IRepositoryProvider`.

## 2.0.8

- 2022-09-15: updated packages.

## 2.0.7

- 2022-08-04: fixed some thesaurus entries IDs in seeder.

## 2.0.6

- 2022-08-04: replaced `ExternalId` list with `AssertedId` list in `CodDecorationArtist`.
- 2022-08-03: fix codicology seeder location number.

## 2.0.5

- 2022-08-03: replaced `ExternalId` with `AssertedId` in `CodWatermark`.
- 2022-08-01: fix to `SeedHelper.Truncate` (float instead of double).

## 2.0.3

- 2022-07-23:
  - made projects nullable.
  - `CodContent`: added `Author` and changed `Range` into `Ranges`.

## 2.0.2

- 2022-06-19: updated packages.

## 2.0.1

- 2022-05-18: updated packages.

## 2.0.0

- 2022-04-29: upgraded to NET 6.0.
