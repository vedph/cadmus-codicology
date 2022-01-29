using System;
using Xunit;
using Cadmus.Core;
using System.Collections.Generic;
using System.Linq;
using Cadmus.Seed.Codicology.Parts;

namespace Cadmus.Codicology.Parts.Test
{
    public sealed class CodSheetLabelsPartTest
    {
        private static CodSheetLabelsPart GetPart()
        {
            CodSheetLabelsPartSeeder seeder = new();
            IItem item = new Item
            {
                FacetId = "default",
                CreatorId = "zeus",
                UserId = "zeus",
                Description = "Test item",
                Title = "Test Item",
                SortKey = ""
            };
            return (CodSheetLabelsPart)seeder.GetPart(item, null, null);
        }

        private static CodSheetLabelsPart GetEmptyPart()
        {
            return new CodSheetLabelsPart
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
            CodSheetLabelsPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            CodSheetLabelsPart part2 = TestHelper.DeserializePart<CodSheetLabelsPart>(json)!;

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);
        }

        [Fact]
        public void GetDataPins_Ok()
        {
            CodSheetLabelsPart part = GetEmptyPart();

            part.Rows.Add(new CodSheetRow());
            part.NDefinitions.Add(new CodSheetNColumnDefinition
            {
                Id = "ndef"
            });
            part.CDefinitions.Add(new CodSheetCColumnDefinition
            {
                Id = "cdef"
            });
            part.SDefinitions.Add(new CodSheetSColumnDefinition
            {
                Id = "sdef"
            });
            part.RDefinitions.Add(new CodSheetRColumnDefinition
            {
                Id = "rdef"
            });

            List<DataPin> pins = part.GetDataPins(null).ToList();
            Assert.Equal(5, pins.Count);

            DataPin? pin = pins.Find(p => p.Name == "row-count" && p.Value == "1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "n-id" && p.Value == "ndef");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "c-id" && p.Value == "cdef");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "s-id" && p.Value == "sdef");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "r-id" && p.Value == "rdef");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);
        }
    }
}
