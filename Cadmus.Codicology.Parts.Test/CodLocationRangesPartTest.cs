using Cadmus.Core;
using Cadmus.Seed.Codicology.Parts;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cadmus.Codicology.Parts.Test;


public sealed class CodLocationRangesPartTest
{
    private static CodLocationRangesPart GetPart()
    {
        CodLocationRangesPartSeeder seeder = new();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (CodLocationRangesPart)seeder.GetPart(item, null, null)!;
    }

    private static CodLocationRangesPart GetEmptyPart()
    {
        return new CodLocationRangesPart
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
        CodLocationRangesPart part = GetPart();

        string json = TestHelper.SerializePart(part);
        CodLocationRangesPart part2 =
            TestHelper.DeserializePart<CodLocationRangesPart>(json)!;

        Assert.Equal(part.Id, part2.Id);
        Assert.Equal(part.TypeId, part2.TypeId);
        Assert.Equal(part.ItemId, part2.ItemId);
        Assert.Equal(part.RoleId, part2.RoleId);
        Assert.Equal(part.CreatorId, part2.CreatorId);
        Assert.Equal(part.UserId, part2.UserId);

        Assert.Equal(part.Ranges.Count, part2.Ranges.Count);
    }

    [Fact]
    public void GetDataPins_NoEntries_Ok()
    {
        CodLocationRangesPart part = GetPart();
        part.Ranges.Clear();

        List<DataPin> pins = [.. part.GetDataPins(null)];

        Assert.Single(pins);
        DataPin pin = pins[0];
        Assert.Equal("tot-count", pin.Name);
        TestHelper.AssertPinIds(part, pin);
        Assert.Equal("0", pin.Value);
    }

    [Fact]
    public void GetDataPins_Entries_Ok()
    {
        CodLocationRangesPart part = GetEmptyPart();

        for (int i = 0; i < 3; i++)
        {
            int n = i * 2 + 1;
            bool odd = n % 2 != 0;

            part.Ranges.Add(new CodLocationRange
            {
                Start = new CodLocation
                {
                    N = n,
                    V = !odd,
                },
                End = new CodLocation
                {
                    N = n + 1,
                    V = odd,
                } 
            });
        }

        List<DataPin> pins = [.. part.GetDataPins(null)];

        Assert.Equal(3 + 1, pins.Count);

        DataPin? pin = pins.Find(p => p.Name == "tot-count");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        Assert.Equal("3", pin!.Value);

        for (int i = 0; i < 3; i++)
        {
            pin = pins.Find(p => p.Name == "range" &&
                p.Value == part.Ranges[i].ToString());
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);
        }
    }
}
