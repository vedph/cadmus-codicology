using Cadmus.Core;
using Cadmus.Seed.Codicology.Parts;
using Fusi.Antiquity.Chronology;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cadmus.Codicology.Parts.Test;

public sealed class CodIllumInstructionsPartTest
{
    private static CodIllumInstructionsPart GetPart()
    {
        CodIllumInstructionsPartSeeder seeder = new();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (CodIllumInstructionsPart)seeder.GetPart(item, null, null)!;
    }

    private static CodIllumInstructionsPart GetEmptyPart()
    {
        return new CodIllumInstructionsPart
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
        CodIllumInstructionsPart part = GetPart();

        string json = TestHelper.SerializePart(part);
        CodIllumInstructionsPart part2 =
            TestHelper.DeserializePart<CodIllumInstructionsPart>(json)!;

        Assert.Equal(part.Id, part2.Id);
        Assert.Equal(part.TypeId, part2.TypeId);
        Assert.Equal(part.ItemId, part2.ItemId);
        Assert.Equal(part.RoleId, part2.RoleId);
        Assert.Equal(part.CreatorId, part2.CreatorId);
        Assert.Equal(part.UserId, part2.UserId);

        Assert.Equal(part.Instructions.Count, part2.Instructions.Count);
    }

    [Fact]
    public void GetDataPins_NoEntries_Ok()
    {
        CodIllumInstructionsPart part = GetPart();
        part.Instructions.Clear();

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
        CodIllumInstructionsPart part = GetEmptyPart();

        for (int n = 1; n <= 2; n++)
        {
            part.Instructions.Add(new CodIllumInstruction
            {
                Types = [n == 1? "odd" : "even"],
                Position = n == 1 ? "margin-left" : "margin-right",
                Differences =
                [
                    new CodIllumInstructionDiff
                    {
                        Type = "type",
                    }
                ],
                Features = [n == 1 ? "feature1" : "feature2"],
                Languages = [n == 1 ? "lat" : "ita"],
                Date = n == 1? HistoricalDate.Parse("1200") : null
            });
        }

        List<DataPin> pins = [.. part.GetDataPins(null)];

        Assert.Equal(11, pins.Count);

        DataPin? pin = pins.Find(p => p.Name == "tot-count");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        Assert.Equal("2", pin!.Value);

        // type
        pin = pins.Find(p => p.Name == "type" && p.Value == "odd");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "type" && p.Value == "even");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        // diff-count
        pin = pins.Find(p => p.Name == "diff-count" && p.Value == "2");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        // feature
        pin = pins.Find(p => p.Name == "feature" && p.Value == "feature1");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "feature" && p.Value == "feature2");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        // lang
        pin = pins.Find(p => p.Name == "lang" && p.Value == "lat");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "lang" && p.Value == "ita");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        // date-value
        pin = pins.Find(p => p.Name == "date-value" && p.Value == "1200");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
    }
}
