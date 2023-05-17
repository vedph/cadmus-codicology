using Cadmus.Core;
using Cadmus.Refs.Bricks;
using Cadmus.Seed.Codicology.Parts;
using Fusi.Antiquity.Chronology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Codicology.Parts.Test;

public class CodWatermarksPartTest
{
    private static CodWatermarksPart GetPart()
    {
        CodWatermarksPartSeeder seeder = new();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (CodWatermarksPart)seeder.GetPart(item, null, null);
    }

    private static CodWatermarksPart GetEmptyPart()
    {
        return new CodWatermarksPart
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
        CodWatermarksPart part = GetPart();

        string json = TestHelper.SerializePart(part);
        CodWatermarksPart part2 =
            TestHelper.DeserializePart<CodWatermarksPart>(json)!;

        Assert.Equal(part.Id, part2.Id);
        Assert.Equal(part.TypeId, part2.TypeId);
        Assert.Equal(part.ItemId, part2.ItemId);
        Assert.Equal(part.RoleId, part2.RoleId);
        Assert.Equal(part.CreatorId, part2.CreatorId);
        Assert.Equal(part.UserId, part2.UserId);

        Assert.Equal(part.Watermarks.Count, part2.Watermarks.Count);
    }

    [Fact]
    public void GetDataPins_NoWatermarks_Ok()
    {
        CodWatermarksPart part = GetPart();
        part.Watermarks.Clear();

        List<DataPin> pins = part.GetDataPins(null).ToList();

        Assert.Single(pins);
        DataPin pin = pins[0];
        Assert.Equal("tot-count", pin.Name);
        TestHelper.AssertPinIds(part, pin);
        Assert.Equal("0", pin.Value);
    }

    [Fact]
    public void GetDataPins_Watermarks_Ok()
    {
        CodWatermarksPart part = GetEmptyPart();

        for (int n = 1; n <= 3; n++)
        {
            bool even = n % 2 == 0;
            part.Watermarks.Add(new CodWatermark
            {
                Name = "n" + n,
                Ids = new List<AssertedId>
                {
                    new AssertedId{Value = "mock/" + n}
                },
                Chronotopes = new List<AssertedChronotope>
                {
                    new AssertedChronotope
                    {
                        Place = new AssertedPlace
                        {
                            Value = even? "Even" : "Odd"
                        },
                        Date  = new AssertedDate(
                            HistoricalDate.Parse($"{1400 + n}")!)
                    }
                }
            });
        }

        List<DataPin> pins = part.GetDataPins(null).ToList();

        Assert.Equal(12, pins.Count);

        DataPin? pin = pins.Find(p => p.Name == "tot-count");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        Assert.Equal("3", pin!.Value);

        // odd: n1 n3 mock/1 mock/3 Odd
        pin = pins.Find(p => p.Name == "name" && p.Value == "n1");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "name" && p.Value == "n3");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "id" && p.Value == "mock/1");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "id" && p.Value == "mock/3");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "place" && p.Value == "Odd");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        // even: n2 mock/2 Even
        pin = pins.Find(p => p.Name == "name" && p.Value == "n2");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "id" && p.Value == "mock/2");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "place" && p.Value == "Even");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
    }
}
