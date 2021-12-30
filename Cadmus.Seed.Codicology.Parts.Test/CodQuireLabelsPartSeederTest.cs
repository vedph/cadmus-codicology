using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Codicology.Parts.Test
{
    public sealed class CodQuireLabelsPartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static CodQuireLabelsPartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(CodQuireLabelsPartSeeder);
            TagAttribute? attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.codicology.quire-labels", attr!.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            CodQuireLabelsPartSeeder seeder = new();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            CodQuireLabelsPart? p = part as CodQuireLabelsPart;
            Assert.NotNull(p);

            TestHelper.AssertPartMetadata(p!);

            Assert.NotEmpty(p!.Catchwords);
            Assert.NotEmpty(p!.QuireSignatures);
            Assert.NotEmpty(p!.QuireRegSignatures);
        }
    }
}
