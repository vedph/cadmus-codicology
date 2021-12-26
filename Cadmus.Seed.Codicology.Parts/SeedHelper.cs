using Cadmus.Codicology.Parts;
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
    }
}
