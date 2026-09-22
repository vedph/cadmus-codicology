using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Tools.Configuration;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Codicology.Parts.Test;

public sealed class CodLocationRangesPartSeederTest
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
        Type t = typeof(CodLocationRangesPartSeeder);
        TagAttribute? attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
        Assert.NotNull(attr);
        Assert.Equal("seed.it.vedph.codicology.location-ranges", attr!.Tag);
    }

    [Fact]
    public void Seed_Ok()
    {
        CodLocationRangesPartSeeder seeder = new();
        seeder.SetSeedOptions(_seedOptions);

        IPart? part = seeder.GetPart(_item, null, _factory);

        Assert.NotNull(part);

        CodLocationRangesPart? p = part as CodLocationRangesPart;
        Assert.NotNull(p);

        TestHelper.AssertPartMetadata(p!);

        Assert.NotEmpty(p!.Ranges);
    }
}
