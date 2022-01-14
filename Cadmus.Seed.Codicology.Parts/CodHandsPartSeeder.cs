using Bogus;
using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Codicology.Parts
{
    /// <summary>
    /// Seeder for <see cref="CodHandsPart"/>.
    /// Tag: <c>seed.it.vedph.codicology.hands</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.codicology.hands")]
    public sealed class CodHandsPartSeeder : PartSeederBase
    {
        private static List<CodHandSign> GetSigns(int count)
        {
            List<CodHandSign> signs = new List<CodHandSign>();
            for (int n = 1; n <= count; n++)
            {
                signs.Add(new Faker<CodHandSign>()
                    .RuleFor(s => s.Eid, f => f.Lorem.Word())
                    .RuleFor(s => s.Type, f => f.PickRandom("let", "pct"))
                    .RuleFor(s => s.SampleLocation,
                        SeedHelper.GetLocationRanges(1)[0].Start)
                    .RuleFor(s => s.Description, f => f.Lorem.Sentence())
                    .Generate());
            }
            return signs;
        }

        private static List<CodHand> GetHands(int count)
        {
            List<CodHand> hands = new List<CodHand>();
            for (int n = 1; n <= count; n++)
            {
                hands.Add(new Faker<CodHand>()
                    .RuleFor(h => h.Eid, f => f.Lorem.Word())
                    .RuleFor(h => h.Name, f => f.Lorem.Word())
                    // TODO
                    .RuleFor(h => h.References,
                        f => SeedHelper.GetDocReferences(f.Random.Number(1, 3)))
                    .Generate());
            }
            return hands;
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

            CodHandsPart part = new Faker<CodHandsPart>()
               .RuleFor(p => p.Hands, f => GetHands(f.Random.Number(1, 2)))
               .Generate();
            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
