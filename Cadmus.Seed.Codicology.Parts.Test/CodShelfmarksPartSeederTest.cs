﻿using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Codicology.Parts.Test
{
    public sealed class CodShelfmarksPartSeederTest
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
            Type t = typeof(CodShelfmarksPartSeeder);
            TagAttribute? attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.codicology.shelfmarks", attr!.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            CodShelfmarksPartSeeder seeder = new();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            CodShelfmarksPart? p = part as CodShelfmarksPart;
            Assert.NotNull(p);

            TestHelper.AssertPartMetadata(p!);

            Assert.NotEmpty(p!.Shelfmarks);
        }
    }
}
