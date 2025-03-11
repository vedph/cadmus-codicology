using System;
using Xunit;
using Cadmus.Core;
using System.Collections.Generic;
using System.Linq;
using Cadmus.Seed.Codicology.Parts;
using Fusi.Antiquity.Chronology;

namespace Cadmus.Codicology.Parts.Test;

public sealed class CodEditsPartTest
{
    private static CodEditsPart GetPart()
    {
        CodEditsPartSeeder seeder = new();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (CodEditsPart)seeder.GetPart(item, null, null);
    }

    private static CodEditsPart GetEmptyPart()
    {
        return new CodEditsPart
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
        CodEditsPart part = GetPart();

        string json = TestHelper.SerializePart(part);
        CodEditsPart part2 =
            TestHelper.DeserializePart<CodEditsPart>(json)!;

        Assert.Equal(part.Id, part2.Id);
        Assert.Equal(part.TypeId, part2.TypeId);
        Assert.Equal(part.ItemId, part2.ItemId);
        Assert.Equal(part.RoleId, part2.RoleId);
        Assert.Equal(part.CreatorId, part2.CreatorId);
        Assert.Equal(part.UserId, part2.UserId);

        Assert.Equal(part.Edits.Count, part2.Edits.Count);
    }

    [Fact]
    public void GetDataPins_NoEntries_Ok()
    {
        CodEditsPart part = GetPart();
        part.Edits.Clear();

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
        CodEditsPart part = GetEmptyPart();

        for (int n = 1; n <= 3; n++)
        {
            bool even = n % 2 == 0;
            part.Edits.Add(new CodEdit
            {
                Eid = "n" + n,
                Type = even ? "correction" : "comment",
                Techniques = [even ? "even" : "odd"],
                Language = even ? "lat" : "grc",
                Colors = new[] { even ? "red" : "black" },
                Date = HistoricalDate.Parse($"{1400 + n} AD")
            });
        }

        List<DataPin> pins = part.GetDataPins(null).ToList();

        Assert.Equal(15, pins.Count);

        DataPin? pin = pins.Find(p => p.Name == "tot-count");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        Assert.Equal("3", pin!.Value);

        // odd: n1 n3 comment grc black 1401 1403 odd
        pin = pins.Find(p => p.Name == "eid" && p.Value == "n1");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "eid" && p.Value == "n3");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "type" && p.Value == "comment");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "language" && p.Value == "grc");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "color" && p.Value == "black");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "date-value" && p.Value == "1401");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "date-value" && p.Value == "1403");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "technique" && p.Value == "odd");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        // even: n2 correction lat red 1402 even
        pin = pins.Find(p => p.Name == "eid" && p.Value == "n2");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "type" && p.Value == "correction");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "language" && p.Value == "lat");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "color" && p.Value == "red");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "date-value" && p.Value == "1402");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "technique" && p.Value == "even");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
    }
}
