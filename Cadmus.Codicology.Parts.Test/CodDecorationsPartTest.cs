using Cadmus.Core;
using Cadmus.Refs.Bricks;
using Fusi.Antiquity.Chronology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Codicology.Parts.Test;

public sealed class CodDecorationsPartTest
{
    private static List<CodDecorationElement> GetElements(int count)
    {
        List<CodDecorationElement> elements = [];

        for (int n = 1; n <= count; n++)
        {
            string alt = n % 2 == 0 ? "even" : "odd";

            elements.Add(new CodDecorationElement
            {
                Key = n == 1 ? "e1" : null,
                ParentKey = n == 2 ? "e1" : null,
                Type = alt,
                Flags = [.. new[] { "f-" + alt }],
                Ranges = [.. new[]
                {
                    new CodLocationRange
                    {
                        Start = new CodLocation
                        {
                            N = 2,
                            S = n % 2 == 0
                                ? "v"
                                : "r",
                            L = 3
                        },
                        End = new CodLocation
                        {
                            N = 4,
                            S = n % 2 == 0
                                ? "v"
                                : "r",
                            L = 5
                        }
                    }
                }],
                Typologies = [.. new[] { "t-" + alt }],
                Subject = "s" + n,
                Colors = [.. new[] { "c" + n }],
                Gildings = ["gilding"],
                Techniques = ["technique"],
                Tools = ["tool"],
                Positions = ["position"],
                LineHeight = (short)n,
                TextRelation = "relation",
                Description = "description",
                Note = "note"
            });
        }

        return elements;
    }

    private static CodDecorationsPart GetPart(int count)
    {
        CodDecorationsPart part = new()
        {
            ItemId = Guid.NewGuid().ToString(),
            RoleId = "some-role",
            CreatorId = "zeus",
            UserId = "another",
        };

        for (int n = 1; n <= count; n++)
        {
            string alt = n % 2 == 0 ? "even" : "odd";
            part.Decorations.Add(new CodDecoration
            {
                Eid = $"d{n}",
                Name = "Decoration " + n,
                Chronotopes =
                [
                    new AssertedChronotope
                    {
                        Place = new AssertedPlace { Value = "Paris" },
                        Date = new AssertedDate(HistoricalDate.Parse($"{1300 + n}")!)
                    }
                ],
                Flags = [.. new[] { "f-" + alt }],
                Note = "Note",
                Artists = [.. new[]
                    {
                        new CodDecorationArtist
                        {
                            Name = "Petrus",
                            Ids =
                            [
                                new AssertedCompositeId
                                {
                                    Target = new PinTarget
                                    {
                                        Gid = "petrus123"
                                    }
                                }
                            ]
                        }
                    }],
                References = TestHelper.GetDocReferences(1),
                Elements = GetElements(3)
            });
        }

        return part;
    }

    [Fact]
    public void Part_Is_Serializable()
    {
        CodDecorationsPart part = GetPart(2);

        string json = TestHelper.SerializePart(part);
        CodDecorationsPart part2 =
            TestHelper.DeserializePart<CodDecorationsPart>(json)!;

        Assert.Equal(part.Id, part2.Id);
        Assert.Equal(part.TypeId, part2.TypeId);
        Assert.Equal(part.ItemId, part2.ItemId);
        Assert.Equal(part.RoleId, part2.RoleId);
        Assert.Equal(part.CreatorId, part2.CreatorId);
        Assert.Equal(part.UserId, part2.UserId);

        Assert.Equal(2, part.Decorations.Count);
    }

    [Fact]
    public void GetDataPins_NoEntries_Ok()
    {
        CodDecorationsPart part = GetPart(0);

        List<DataPin> pins = part.GetDataPins(null).ToList();
        TestHelper.AssertValidDataPinNames(pins);

        Assert.Single(pins);
        DataPin pin = pins[0];
        Assert.Equal("tot-count", pin.Name);
        TestHelper.AssertPinIds(part, pin);
        Assert.Equal("0", pin.Value);
    }

    [Fact]
    public void GetDataPins_Entries_Ok()
    {
        CodDecorationsPart part = GetPart(3);

        List<DataPin> pins = part.GetDataPins(null).ToList();

        Assert.Equal(18, pins.Count);
        TestHelper.AssertValidDataPinNames(pins);

        DataPin? pin = pins.Find(p => p.Name == "tot-count");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        Assert.Equal("3", pin!.Value);

        pin = pins.Find(p => p.Name == "type-odd-count");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        Assert.Equal("6", pin!.Value);

        pin = pins.Find(p => p.Name == "type-even-count");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        Assert.Equal("3", pin!.Value);

        pin = pins.Find(p => p.Name == "artist-id" && p.Value == "petrus123");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "artist-name" && p.Value == "petrus");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        for (int n = 1; n <= 3; n++)
        {
            pin = pins.Find(p => p.Name == "eid" && p.Value == $"d{n}");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == $"subject-s{n}-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);
            Assert.Equal("3", pin!.Value);

            pin = pins.Find(p => p.Name == $"color-c{n}-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);
            Assert.Equal("3", pin!.Value);

            pin = pins.Find(p => p.Name == "date-value" &&
                p.Value == $"{1300 + n}");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);
        }
    }
}
