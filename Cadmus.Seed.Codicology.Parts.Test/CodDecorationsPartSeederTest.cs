using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Codicology.Parts.Test
{
    public sealed class CodDecorationsPartSeederTest
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
            Type t = typeof(CodDecorationsPartSeeder);
            TagAttribute? attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.codicology.decorations", attr!.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            CodDecorationsPartSeeder seeder = new();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            CodDecorationsPart? dp = part as CodDecorationsPart;
            Assert.NotNull(dp);

            TestHelper.AssertPartMetadata(dp!);
            Assert.NotEmpty(dp!.Decorations);
        }
    }
}
