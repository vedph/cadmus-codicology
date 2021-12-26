using Cadmus.Core;
using Cadmus.Seed.Codicology.Parts;
using Fusi.Antiquity.Chronology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Codicology.Parts.Test
{
    public sealed class CodNumberingsPartTest
    {
        private static CodNumberingsPart GetPart()
        {
            CodNumberingsPartSeeder seeder = new();
            IItem item = new Item
            {
                FacetId = "default",
                CreatorId = "zeus",
                UserId = "zeus",
                Description = "Test item",
                Title = "Test Item",
                SortKey = ""
            };
            return (CodNumberingsPart)seeder.GetPart(item, null, null);
        }

        private static CodNumberingsPart GetEmptyPart()
        {
            return new CodNumberingsPart
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
            CodNumberingsPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            CodNumberingsPart part2 =
                TestHelper.DeserializePart<CodNumberingsPart>(json)!;

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(part.Numberings.Count, part2.Numberings.Count);
        }

        [Fact]
        public void GetDataPins_NoEntries_Ok()
        {
            CodNumberingsPart part = GetPart();
            part.Numberings.Clear();

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
            CodNumberingsPart part = GetEmptyPart();

            for (int n = 1; n <= 3; n++)
            {
                bool even = n % 2 == 0;
                part.Numberings.Add(new CodNumbering
                {
                    Eid = "n" + n,
                    System = even? "arabic" : "roman",
                    Technique = even? "ink" : "lapis",
                    Position = even? "mse" : "msc",
                    Colors = new[] { even? "red" : "black" },
                    Date = HistoricalDate.Parse($"{1400 + n} AD")
                });
            }

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(15, pins.Count);

            DataPin? pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);
            Assert.Equal("3", pin!.Value);

            // odd: n1, n3, roman, lapis, msc, black, 1401, 1403
            pin = pins.Find(p => p.Name == "eid" && p.Value == "n1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "eid" && p.Value == "n3");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "system" && p.Value == "roman");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "technique" && p.Value == "lapis");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "position" && p.Value == "msc");
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

            // even: n2, arabic, ink, mse, red, 1402
            pin = pins.Find(p => p.Name == "eid" && p.Value == "n2");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "system" && p.Value == "arabic");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "technique" && p.Value == "ink");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "position" && p.Value == "mse");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "color" && p.Value == "red");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "date-value" && p.Value == "1402");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);
        }
    }
}
