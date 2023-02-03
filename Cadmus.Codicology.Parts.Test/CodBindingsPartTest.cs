using Cadmus.Core;
using Cadmus.Refs.Bricks;
using Cadmus.Seed.Codicology.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Codicology.Parts.Test;

public sealed class CodBindingsPartTest
{
    private static CodBindingsPart GetPart()
    {
        CodBindingsPartSeeder seeder = new();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (CodBindingsPart)seeder.GetPart(item, null, null);
    }

    private static CodBindingsPart GetEmptyPart()
    {
        return new CodBindingsPart
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
        CodBindingsPart part = GetPart();

        string json = TestHelper.SerializePart(part);
        CodBindingsPart part2 =
            TestHelper.DeserializePart<CodBindingsPart>(json)!;

        Assert.Equal(part.Id, part2.Id);
        Assert.Equal(part.TypeId, part2.TypeId);
        Assert.Equal(part.ItemId, part2.ItemId);
        Assert.Equal(part.RoleId, part2.RoleId);
        Assert.Equal(part.CreatorId, part2.CreatorId);
        Assert.Equal(part.UserId, part2.UserId);

        Assert.Equal(part.Bindings.Count, part2.Bindings.Count);
    }

    [Fact]
    public void GetDataPins_NoEntries_Ok()
    {
        CodBindingsPart part = GetPart();
        part.Bindings.Clear();

        List<DataPin> pins = part.GetDataPins(null).ToList();

        Assert.Single(pins);
        DataPin pin = pins[0];
        Assert.Equal("tot-count", pin.Name);
        TestHelper.AssertPinIds(part, pin);
        Assert.Equal("0", pin.Value);
    }

    [Fact]
    public void GetDataPins_Entries_Ok()
    {
        CodBindingsPart part = GetEmptyPart();
        List<AssertedChronotope> chronotopes =
            TestHelper.GetAssertedChronotopes(3);

        for (int n = 1; n <= 3; n++)
        {
            bool even = n % 2 == 0;
            part.Bindings.Add(new CodBinding
            {
                CoverMaterial = even ? "iron" : "wood",
                BoardMaterial = even ? "iron" : "wood",
                Chronotope = chronotopes[n - 1],
            });
        }

        List<DataPin> pins = part.GetDataPins(null).ToList();

        Assert.Equal(11, pins.Count);

        DataPin? pin = pins.Find(p => p.Name == "tot-count");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        Assert.Equal("3", pin!.Value);

        // odd: wood wood place-1 place-3 1301 1303
        pin = pins.Find(p => p.Name == "cover" && p.Value == "wood");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "support" && p.Value == "wood");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "place" && p.Value == "place-1");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "place" && p.Value == "place-3");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "date-value" && p.Value == "1301");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "date-value" && p.Value == "1303");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        // even: iron iron place-2 1302
        pin = pins.Find(p => p.Name == "cover" && p.Value == "iron");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "support" && p.Value == "iron");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "place" && p.Value == "place-2");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "date-value" && p.Value == "1302");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
    }
}
