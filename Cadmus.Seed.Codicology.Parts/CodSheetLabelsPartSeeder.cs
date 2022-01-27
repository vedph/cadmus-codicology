using Bogus;
using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Antiquity.Chronology;
using Fusi.Tools;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Codicology.Parts
{
    /// <summary>
    /// Seeder for <see cref="CodSheetLabelsPart"/>.
    /// Tag: <c>seed.it.vedph.codicology.sheet-labels</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("it.vedph.codicology.sheet-labels")]
    public sealed class CodSheetLabelsPartSeeder : PartSeederBase
    {
        /// <summary>
        /// Creates and seeds a new part.
        /// </summary>
        /// <param name="item">The item this part should belong to.</param>
        /// <param name="roleId">The optional part role ID.</param>
        /// <param name="factory">The part seeder factory. This is used
        /// for layer parts, which need to seed a set of fragments.</param>
        /// <returns>A new part.</returns>
        /// <exception cref="ArgumentNullException">item or factory</exception>
        public override IPart GetPart(IItem item, string roleId,
            PartSeederFactory factory)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            CodSheetLabelsPart part = new CodSheetLabelsPart();
            SetPartMetadata(part, roleId, item);

            // n.alpha and n.beta
            for (int i = 0; i < 2; i++)
            {
                part.NDefinitions.Add(new Faker<CodSheetNColumnDefinition>()
                    .RuleFor(d => d.Id, i == 0? "n.alpha" : "n.beta")
                    .RuleFor(d => d.IsPagination, true)
                    .RuleFor(d => d.System, i == 0? "roman" : "arabic")
                    .RuleFor(d => d.Technique, f => f.PickRandom("ink", "lapis"))
                    .RuleFor(d => d.Position, f => f.PickRandom("mse", "msc"))
                    .RuleFor(d => d.Colors,
                        f => new List<string> { f.PickRandom("red", "dark-brown") })
                    .RuleFor(d => d.Date, HistoricalDate.Parse($"{1300 + i} AD"))
                    .Generate());
            }

            int div = new Random().Next(3, 14), a = 0, b = 0;
            for (int i = 0; i < 16; i++)
            {
                int n = i + 1;
                bool v = n % 2 == 0;
                CodSheetRow row = new CodSheetRow
                {
                    Id = n + (v ? "v" : "r")
                };
                // q
                row.Columns.Add(new CodSheetColumn
                {
                    Id = "q",
                    Value = $"{i / 8 + 1}.{i / 2 + 1}/4",
                });
                // n.alpha
                if (n < div)
                {
                    row.Columns.Add(new CodSheetColumn
                    {
                        Id = "n.alpha",
                        Value = RomanNumber.ToRoman(++a)
                    });
                }
                // n.beta
                else
                {
                    row.Columns.Add(new CodSheetColumn
                    {
                        Id = "n.beta",
                        Value = $"{++b}"
                    });
                }
                part.Rows.Add(row);
            }

            return part;
        }
    }
}
