using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Tools.Config;
using System;
using System.Diagnostics;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Codicology.Parts.Test
{
    public sealed class CodSheetLabelsPartSeederTest
    {
        private static readonly PartSeederFactory _factory =
            TestHelper.GetFactory();
        private static readonly SeedOptions _seedOptions =
            _factory.GetSeedOptions();
        private static readonly IItem _item =
            _factory.GetItemSeeder().GetItem(1, "facet");

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(CodSheetLabelsPartSeeder);
            TagAttribute? attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.codicology.sheet-labels", attr!.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            CodSheetLabelsPartSeeder seeder = new();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);
            string dump = CodSheetLabelsPart.DumpTable(((CodSheetLabelsPart)(part)).Rows);
            Debug.WriteLine(dump);

            Assert.NotNull(part);

            CodSheetLabelsPart? p = part as CodSheetLabelsPart;
            Assert.NotNull(p);

            TestHelper.AssertPartMetadata(p!);
        }
    }
}
