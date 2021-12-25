# Cadmus.Codicology

This solution contains a number of Cadmus parts related to codicology, originally stemming from the Itinera project, but designed to be generic enough to be useful in other projects.

## Parts

### Shelfmark(s)

ID: `it.vedph.codicology.shelfmarks`.

Manuscript's shelfmark(s). Usually there is just one, unless you are also adding some historical signatures; in this case, assign a tag to the non-current (default) one.

- shelfmarks (CodShelfmark[]):
  - tag (string)
  - city (string)
  - library (string)
  - fund (string)
  - location (string)
