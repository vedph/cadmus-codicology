using Cadmus.Core;
using Cadmus.Seed.Codicology.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Codicology.Parts.Test
{
    public sealed class CodContentsPartTest
    {
        private static CodContentsPart GetPart()
        {
            CodContentsPartSeeder seeder = new();
            IItem item = new Item
            {
                FacetId = "default",
                CreatorId = "zeus",
                UserId = "zeus",
                Description = "Test item",
                Title = "Test Item",
                SortKey = ""
            };
            return (CodContentsPart)seeder.GetPart(item, null, null);
        }

        private static CodContentsPart GetEmptyPart()
        {
            return new CodContentsPart
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
            CodContentsPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            CodContentsPart part2 =
                TestHelper.DeserializePart<CodContentsPart>(json)!;

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(part.Contents.Count, part2.Contents.Count);
        }

        [Fact]
        public void GetDataPins_NoEntries_Ok()
        {
            CodContentsPart part = GetPart();
            part.Contents.Clear();

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
            CodContentsPart part = GetEmptyPart();

            for (int n = 1; n <= 3; n++)
            {
                bool even = n % 2 == 0;
                part.Contents.Add(new CodContent
                {
                    Eid = "n" + n,
                    States = new List<string> { even ? "intact" : "fragments" },
                    Title = "Title " + n,
                    ClaimedAuthor = "Author " + n,
                    ClaimedTitle = "Title " + n
                });
            }

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(15, pins.Count);

            DataPin? pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);
            Assert.Equal("3", pin!.Value);

            // odd: n1 n3 fragments title1 author1 ctitle1 and 3 
            pin = pins.Find(p => p.Name == "eid" && p.Value == "n1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "eid" && p.Value == "n3");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "state" && p.Value == "fragments");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "title" && p.Value == "title 1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "title" && p.Value == "title 3");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "claimed-author" && p.Value == "author 1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "claimed-author" && p.Value == "author 3");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "claimed-title" && p.Value == "title 1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "claimed-title" && p.Value == "title 3");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            // even: n2 intact
            pin = pins.Find(p => p.Name == "eid" && p.Value == "n2");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "state" && p.Value == "intact");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "claimed-author" && p.Value == "author 2");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "claimed-title" && p.Value == "title 2");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);
        }
    }
}
