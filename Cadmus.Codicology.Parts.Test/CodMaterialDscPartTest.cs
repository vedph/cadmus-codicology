using System;
using Xunit;
using Cadmus.Core;
using System.Collections.Generic;
using System.Linq;
using Cadmus.Seed.Codicology.Parts;

namespace Cadmus.Codicology.Parts.Test;

public sealed class CodMaterialDscPartTest
{
    private static CodMaterialDscPart GetPart()
    {
        CodMaterialDscPartSeeder seeder = new();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (CodMaterialDscPart)seeder.GetPart(item, null, null);
    }

    private static CodMaterialDscPart GetEmptyPart()
    {
        return new CodMaterialDscPart
        {
            ItemId = Guid.NewGuid().ToString(),
            RoleId = "some-role",
            CreatorId = "zeus",
            UserId = "another",
        };
    }

    [Fact]
    public void Part_Is_Serializable()
    {
        CodMaterialDscPart part = GetPart();

        string json = TestHelper.SerializePart(part);
        CodMaterialDscPart part2 = TestHelper.DeserializePart<CodMaterialDscPart>(json)!;

        Assert.Equal(part.Id, part2.Id);
        Assert.Equal(part.TypeId, part2.TypeId);
        Assert.Equal(part.ItemId, part2.ItemId);
        Assert.Equal(part.RoleId, part2.RoleId);
        Assert.Equal(part.CreatorId, part2.CreatorId);
        Assert.Equal(part.UserId, part2.UserId);
    }

    [Fact]
    public void GetDataPins_Data_Ok()
    {
        CodMaterialDscPart part = GetEmptyPart();
        part.Units.Add(new CodUnit { Eid = "alpha" });
        part.Palimpsests.Add(new CodPalimpsest());

        List<DataPin> pins = part.GetDataPins(null).ToList();
        Assert.Equal(3, pins.Count);

        DataPin? pin = pins.Find(p => p.Name == "unit-eid" && p.Value == "alpha");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "unit-count" && p.Value == "1");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "palimpsest-count" && p.Value == "1");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
    }
}
