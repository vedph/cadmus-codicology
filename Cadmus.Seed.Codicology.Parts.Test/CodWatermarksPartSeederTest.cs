using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Codicology.Parts.Test
{
    public class CodWatermarksPartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static CodWatermarksPartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(CodWatermarksPartSeeder);
            TagAttribute? attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.codicology.watermarks", attr!.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            CodWatermarksPartSeeder seeder = new();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            CodWatermarksPart? p = part as CodWatermarksPart;
            Assert.NotNull(p);

            TestHelper.AssertPartMetadata(p!);

            Assert.NotEmpty(p!.Watermarks);
        }
    }
}
