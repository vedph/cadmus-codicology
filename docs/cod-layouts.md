# Codicology Layouts

🔑 `it.vedph.codicology.layouts`

- layouts (`CodLayout[]`):
  - sample\* ([CodLocation](cod-location.md))
  - ranges\* ([CodLocationRange[]](cod-location-range.md)):
  - dimensions (🧱 [PhysicalDimension[]](https://github.com/vedph/cadmus-bricks/blob/master/docs/physical-dimension.md)):
    - value\* (`number`)
    - unit\* (`string` 📚 `physical-size-units`)
    - tag (`string` 📚 `physical-size-dim-tags`)
  - rulingTechnique (`string` 📚 `cod-layout-ruling-techniques`)
  - derolez (`string` 📚 `cod-layout-derolez`)
  - pricking (`string` 📚 `cod-layout-prickings`)
  - columnCount\* (int)
  - counts (🧱 [DecoratedCount[]](https://github.com/vedph/cadmus-bricks/blob/master/docs/decorated-count.md)):
    - id* (`string` 📚 `cod-layout-counts`)
    - value* (`int`)
    - tag (`string`)
    - note (`string`)
  - tag (`string` 📚 `cod-layout-tags`)
  - note (`string`)
