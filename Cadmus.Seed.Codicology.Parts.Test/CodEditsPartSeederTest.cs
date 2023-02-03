using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Tools.Configuration;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Codicology.Parts.Test;

public sealed class CodEditsPartSeederTest
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
        Type t = typeof(CodEditsPartSeeder);
        TagAttribute? attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
        Assert.NotNull(attr);
        Assert.Equal("seed.it.vedph.codicology.edits", attr!.Tag);
    }

    [Fact]
    public void Seed_Ok()
    {
        CodEditsPartSeeder seeder = new();
        seeder.SetSeedOptions(_seedOptions);

        IPart part = seeder.GetPart(_item, null, _factory);

        Assert.NotNull(part);

        CodEditsPart? p = part as CodEditsPart;
        Assert.NotNull(p);

        TestHelper.AssertPartMetadata(p!);

        Assert.NotEmpty(p!.Edits);
    }
}
