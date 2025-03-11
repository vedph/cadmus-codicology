using Cadmus.Core;
using Cadmus.Seed.Codicology.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Codicology.Parts.Test;

public sealed class CodHandsPartTest
{
    private static CodHandsPart GetPart()
    {
        CodHandsPartSeeder seeder = new();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (CodHandsPart)seeder.GetPart(item, null, null);
    }

    private static CodHandsPart GetEmptyPart()
    {
        return new CodHandsPart
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
        CodHandsPart part = GetPart();

        string json = TestHelper.SerializePart(part);
        CodHandsPart part2 =
            TestHelper.DeserializePart<CodHandsPart>(json)!;

        Assert.Equal(part.Id, part2.Id);
        Assert.Equal(part.TypeId, part2.TypeId);
        Assert.Equal(part.ItemId, part2.ItemId);
        Assert.Equal(part.RoleId, part2.RoleId);
        Assert.Equal(part.CreatorId, part2.CreatorId);
        Assert.Equal(part.UserId, part2.UserId);

        Assert.Equal(part.Hands.Count, part2.Hands.Count);
    }

    [Fact]
    public void GetDataPins_NoEntries_Ok()
    {
        CodHandsPart part = GetPart();
        part.Hands.Clear();

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
        CodHandsPart part = GetEmptyPart();

        for (int n = 1; n <= 3; n++)
        {
            part.Hands.Add(new CodHand
            {
                Eid = "h" + n,
                Instances =
                [
                    new CodHandInstance
                    {
                        Scripts = ["script" + n],
                        Typologies =
                        [
                            "typology" + n
                        ],
                        Colors =
                        [
                            "color" + n
                        ]
                    }
                ],
                Subscriptions =
                [
                    new CodHandSubscription
                    {
                        Language = n % 2 == 0 ? "even" : "odd"
                    }
                ]
            });
        }

        List<DataPin> pins = part.GetDataPins(null).ToList();

        Assert.Equal(16, pins.Count);

        DataPin? pin = pins.Find(p => p.Name == "tot-count");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        Assert.Equal("3", pin!.Value);

        pin = pins.Find(p => p.Name == "subs-count" && p.Value == "3");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "subs-language" && p.Value == "even");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "subs-language" && p.Value == "odd");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        for (int n = 1; n <= 3; n++)
        {
            pin = pins.Find(p => p.Name == "eid" && p.Value == "h" + n);
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "script" && p.Value == "script" + n);
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "typology" && p.Value == "typology" + n);
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "color" && p.Value == "color" + n);
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "color" && p.Value == "color" + n);
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);
        }
    }
}
