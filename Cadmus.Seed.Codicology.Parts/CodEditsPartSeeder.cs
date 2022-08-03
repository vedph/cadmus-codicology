using Bogus;
using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Codicology.Parts
{
    /// <summary>
    /// Seeder for <see cref="CodEditsPart"/>.
    /// Tag: <c>seed.it.vedph.codicology.edits</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.codicology.edits")]
    public sealed class CodEditsPartSeeder : PartSeederBase
    {
        private static IList<CodEdit> GetEdits(int count)
        {
            List<CodEdit> edits = new();
            for (int n = 1; n <= count; n++)
            {
                edits.Add(new Faker<CodEdit>()
                    .RuleFor(p => p.Eid, f => f.Lorem.Word())
                    .RuleFor(p => p.Type, f => f.PickRandom("correction", "comment"))
                    .RuleFor(p => p.Tag, f => f.PickRandom(null, "tag"))
                    .RuleFor(p => p.Techniques, f =>
                        new List<string> { f.PickRandom("ink", "lapis") })
                    .RuleFor(p => p.Language, f => f.PickRandom("lat", "grc"))
                    .RuleFor(p => p.Colors, f => new string[]
                        {
                            f.PickRandom("black", "red")
                        })
                    .RuleFor(p => p.Ranges,
                        f => SeedHelper.GetLocationRanges(f.Random.Number(1, 3)))
                    .RuleFor(p => p.Date, HistoricalDate.Parse($"{1400 + n} AD"))
                    .RuleFor(p => p.Description,
                        f => f.PickRandom(f.Lorem.Sentence(), null))
                    .RuleFor(p => p.Text, f=> f.Lorem.Sentence())
                    .RuleFor(p => p.References,
                        f => SeedHelper.GetDocReferences(f.Random.Number(1, 3)))
                    .Generate());
            }
            return edits;
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
        public override IPart GetPart(IItem item, string? roleId,
            PartSeederFactory? factory)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            CodEditsPart part = new Faker<CodEditsPart>()
               .RuleFor(p => p.Edits, f => GetEdits(f.Random.Number(1, 3)))
               .Generate();
            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
