using Bogus;
using Cadmus.Codicology.Parts;
using Cadmus.Refs.Bricks;
using System.Collections.Generic;

namespace Cadmus.Seed.Codicology.Parts
{
    internal static class SeedHelper
    {
        public static IList<CodLocationRange> GetLocationRanges(int count)
        {
            List<CodLocationRange> ranges = new List<CodLocationRange>();
            for (int n = 1; n <= count; n++)
            {
                ranges.Add(new CodLocationRange
                {
                    Start = new CodLocation { N = (n - 1) * 3 },
                    End = new CodLocation { N = ((n - 1) * 3) + 2 }
                });
            }
            return ranges;
        }

        /// <summary>
        /// Gets a random number of document references.
        /// </summary>
        /// <param name="count">The number of references to get.</param>
        /// <returns>References.</returns>
        public static List<DocReference> GetDocReferences(int count)
        {
            List<DocReference> refs = new List<DocReference>();

            for (int n = 1; n <= count; n++)
            {
                refs.Add(new Faker<DocReference>()
                    .RuleFor(r => r.Tag, f => f.PickRandom(null, "tag"))
                    .RuleFor(r => r.Citation,
                        f => f.Person.LastName + " " + f.Date.Past(10))
                    .RuleFor(r => r.Note, f => f.Lorem.Sentence())
                    .Generate());
            }

            return refs;
        }

        public static List<ExternalId> GetExternalIds(int count)
        {
            List<ExternalId> ids = new List<ExternalId>();

            for (int n = 1; n <= count; n++)
            {
                ids.Add(new Faker<ExternalId>()
                    .RuleFor(r => r.Tag, f => f.PickRandom(null, "tag"))
                    .RuleFor(r => r.Scope, f => f.Lorem.Word())
                    .RuleFor(r => r.Value, f => f.Internet.Url())
                    .Generate());
            }

            return ids;
        }

        public static List<RankedExternalId> GetRankedExternalIds(int count)
        {
            List<RankedExternalId> ids = new List<RankedExternalId>();

            for (int n = 1; n <= count; n++)
            {
                ids.Add(new Faker<RankedExternalId>()
                    .RuleFor(r => r.Tag, f => f.PickRandom(null, "tag"))
                    .RuleFor(r => r.Scope, f => f.Lorem.Word())
                    .RuleFor(r => r.Value, f => f.Internet.Url())
                    .RuleFor(r => r.Rank, f => (short)f.Random.Number(1, 3))
                    .Generate());
            }

            return ids;
        }
    }
}
