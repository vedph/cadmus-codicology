using Bogus;
using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Cadmus.Refs.Bricks;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Codicology.Parts
{
    /// <summary>
    /// Seeder for <see cref="CodMaterialDscPart"/>.
    /// Tag: <c>seed.it.vedph.codicology.material-dsc</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.codicology.material-dsc")]
    public sealed class CodMaterialDscPartSeeder : PartSeederBase
    {
        private static List<CodUnit> GetUnits(int count)
        {
            List<CodUnit> units = new List<CodUnit>();
            for (int n = 1; n <= count; n++)
            {
                units.Add(new Faker<CodUnit>()
                    .RuleFor(u => u.Eid, f => f.Lorem.Word())
                    .RuleFor(u => u.Tag, f => f.PickRandom(null, "tag"))
                    // TODO use thesauri
                    .RuleFor(u => u.Material, f => f.PickRandom("parchment", "paper"))
                    .RuleFor(u => u.State, f => f.PickRandom("intact", "damaged"))
                    .RuleFor(u => u.Range, SeedHelper.GetLocationRanges(1)[0])
                    .RuleFor(u => u.Chronotopes,
                        f => SeedHelper.GetAssertedChronotopes(f.Random.Number(1, 2)))
                    .RuleFor(u => u.NoGregory, f => f.Random.Bool(0.25f))
                    .RuleFor(u => u.Note,
                        f => f.Random.Bool(0.25f)? f.Lorem.Sentence() : null)
                    .Generate());
            }
            return units;
        }

        private static List<CodPalimpsest> GetPalimpsests(int count)
        {
            List<CodPalimpsest> palimpsests = new List<CodPalimpsest>();
            for (int n = 1; n <= count; n++)
            {
                palimpsests.Add(new Faker<CodPalimpsest>()
                    .RuleFor(p => p.Range, SeedHelper.GetLocationRanges(1)[0])
                    .RuleFor(p => p.Chronotope, SeedHelper.GetAssertedChronotopes(1)[0])
                    .RuleFor(p => p.Note,
                        f => f.Random.Bool(0.25f)? f.Lorem.Sentence() : null)
                    .Generate());
            }
            return palimpsests;
        }

        private static List<CodEndLeaf> GetEndLeaves(int count)
        {
            List<CodEndLeaf> leaves = new List<CodEndLeaf>();
            for (int n = 1; n <= count; n++)
            {
                leaves.Add(new Faker<CodEndLeaf>()
                    // TODO use thesauri
                    .RuleFor(l => l.Type, f => f.PickRandom("start", "end"))
                    .RuleFor(l => l.Material, f => f.PickRandom("parchment", "paper"))
                    .RuleFor(l => l.Range, SeedHelper.GetLocationRanges(1)[0])
                    .RuleFor(l => l.Date, new AssertedDate
                        {
                            Value = HistoricalDate.Parse($"{1400+n} AD")
                        })
                    .RuleFor(l => l.Note, f => f.Random.Bool(0.25f)
                        ? f.Lorem.Sentence() : null)
                    .Generate());
            }
            return leaves;
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

            CodMaterialDscPart part = new Faker<CodMaterialDscPart>()
               .RuleFor(p => p.Units, f => GetUnits(f.Random.Number(2, 5)))
               .RuleFor(p => p.Palimpsests, f => GetPalimpsests(f.Random.Number(1, 2)))
               .RuleFor(p => p.EndLeaves, f => GetEndLeaves(f.Random.Number(1, 3)))
               .Generate();
            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
