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
    /// Seeder for <see cref="CodNumberingsPart"/>.
    /// Tag: <c>seed.it.vedph.codicology.numberings</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.codicology.numberings")]
    public sealed class CodNumberingsPartSeeder : PartSeederBase
    {
        private static IList<CodNumberingSpan> GetNumberingSpans(int count)
        {
            List<CodNumberingSpan> spans = new List<CodNumberingSpan>();

            for (int n = 1; n <= count; n++)
            {
                int first = (n - 1) * 3;

                spans.Add(new Faker<CodNumberingSpan>()
                    .RuleFor(s => s.Range, new CodLocationRange
                    {
                        Start = new CodLocation { N = first },
                        End = new CodLocation { N = first + 2 }
                    })
                    .RuleFor(s => s.Start, RomanNumber.ToRoman(first))
                    .RuleFor(s => s.End, RomanNumber.ToRoman(first + 2))
                    .Generate());
            }

            return spans;
        }

        private static IList<CodNumbering> GetNumberings(int count)
        {
            List<CodNumbering> numberings = new List<CodNumbering>();
            for (int n = 1; n <= count; n++)
            {
                numberings.Add(new Faker<CodNumbering>()
                    .RuleFor(p => p.Eid, f => f.Lorem.Word())
                    .RuleFor(p => p.IsMain, n == 1)
                    .RuleFor(p => p.IsPagination, n == 2)
                    .RuleFor(p => p.System, f => f.PickRandom("arabic", "roman"))
                    .RuleFor(p => p.Technique, f => f.PickRandom("ink", "lapis"))
                    .RuleFor(p => p.Position, f => f.PickRandom("mse", "msc"))
                    .RuleFor(p => p.Colors, f => new string[]
                        {
                            f.PickRandom("black", "red")
                        })
                    .RuleFor(p => p.Date, HistoricalDate.Parse($"{1400+n} AD"))
                    .RuleFor(p => p.Ranges,
                        f => SeedHelper.GetLocationRanges(f.Random.Number(1, 3)))
                    .RuleFor(p => p.Spans,
                        f => GetNumberingSpans(f.Random.Number(1, 3)))
                    .RuleFor(p => p.Issues, f => n % 2 > 0? f.Lorem.Sentence() : null)
                    .Generate());
            }
            return numberings;
        }

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
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            CodNumberingsPart part = new Faker<CodNumberingsPart>()
               .RuleFor(p => p.Numberings, f => GetNumberings(f.Random.Number(1, 3)))
               .Generate();
            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
