using Bogus;
using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Codicology.Parts
{
    /// <summary>
    /// Seeder for <see cref="CodQuireLabelsPart"/>.
    /// Tag: <c>seed.it.vedph.codicology.quire-labels</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.codicology.quire-labels")]
    public sealed class CodQuireLabelsPartSeeder : PartSeederBase
    {
        private static List<CodCatchword> GetCatchwords(int count)
        {
            List<CodCatchword> catchwords = new List<CodCatchword>();
            for (int n = 1; n <= count; n++)
            {
                catchwords.Add(new Faker<CodCatchword>()
                    .RuleFor(c => c.Range, SeedHelper.GetLocationRanges(1)[0])
                    .RuleFor(c => c.Position, f => f.PickRandom("mis", "mic"))
                    .RuleFor(c => c.IsVertical, f => f.Random.Bool(0.25f))
                    .RuleFor(c => c.Decoration,
                        f => f.Random.Bool(0.25f)? f.Lorem.Sentence() : null)
                    .RuleFor(c => c.Note,
                        f => f.Random.Bool(0.25f) ? f.Lorem.Sentence() : null)
                    .Generate());
            }
            return catchwords;
        }

        private static List<CodQuireSignature> GetSignatures(int count)
        {
            List<CodQuireSignature> signatures = new List<CodQuireSignature>();
            for (int n = 1; n <= count; n++)
            {
                signatures.Add(new Faker<CodQuireSignature>()
                    .RuleFor(c => c.Range, SeedHelper.GetLocationRanges(1)[0])
                    // TODO get from thesauri
                    .RuleFor(c => c.Position, f => f.PickRandom("mis", "mic"))
                    .RuleFor(c => c.System, f => f.PickRandom("x", "y"))
                    .RuleFor(c => c.Note,
                        f => f.Random.Bool(0.25f) ? f.Lorem.Sentence() : null)
                    .Generate());
            }
            return signatures;
        }

        private static List<CodQuireRegSignature> GetRegSignatures(int count)
        {
            List<CodQuireRegSignature> signatures = new List<CodQuireRegSignature>();
            for (int n = 1; n <= count; n++)
            {
                signatures.Add(new Faker<CodQuireRegSignature>()
                    .RuleFor(c => c.Range, SeedHelper.GetLocationRanges(1)[0])
                    // TODO get from thesauri
                    .RuleFor(c => c.Position, f => f.PickRandom("mis", "mic"))
                    .RuleFor(c => c.Note,
                        f => f.Random.Bool(0.25f) ? f.Lorem.Sentence() : null)
                    .Generate());
            }
            return signatures;
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

            CodQuireLabelsPart part = new Faker<CodQuireLabelsPart>()
               .RuleFor(p => p.Catchwords, f => GetCatchwords(f.Random.Number(1, 3)))
               .RuleFor(p => p.QuireSignatures,
                    f => GetSignatures(f.Random.Number(1, 3)))
               .RuleFor(p => p.QuireRegSignatures,
                    f => GetRegSignatures(f.Random.Number(1, 3)))
               .Generate();
            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
