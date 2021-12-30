using System;
using Xunit;
using Cadmus.Core;
using Cadmus.Seed.Codicology.Parts;
using System.Collections.Generic;
using System.Linq;

namespace Cadmus.Codicology.Parts.Test
{
    public sealed class CodQuireLabelsPartTest
    {
        private static CodQuireLabelsPart GetPart()
        {
            CodQuireLabelsPartSeeder seeder = new();
            IItem item = new Item
            {
                FacetId = "default",
                CreatorId = "zeus",
                UserId = "zeus",
                Description = "Test item",
                Title = "Test Item",
                SortKey = ""
            };
            return (CodQuireLabelsPart)seeder.GetPart(item, null, null);
        }

        private static CodQuireLabelsPart GetEmptyPart()
        {
            return new CodQuireLabelsPart
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
            CodQuireLabelsPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            CodQuireLabelsPart part2 =
                TestHelper.DeserializePart<CodQuireLabelsPart>(json)!;

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);
            // TODO: check parts data here...
        }

        [Fact]
        public void GetDataPins_()
        {
            CodQuireLabelsPart part = GetEmptyPart();
            part.Catchwords.Add(new CodCatchword
            {
                Position = "cpos",
                IsVertical = true,
                Decoration = "decoration"
            });
            part.QuireSignatures.Add(new CodQuireSignature
            {
                Position = "spos",
                System = "ssys"
            });
            part.QuireRegSignatures.Add(new CodQuireRegSignature
            {
                Position = "rpos"
            });

            List<DataPin> pins = part.GetDataPins(null).ToList();
            Assert.Equal(9, pins.Count);

            DataPin? pin = pins.Find(p => p.Name == "catchword-count"
                && p.Value == "1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "catchword-vertical" && p.Value == "1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "catchword-has-dec" && p.Value == "1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "signature-count" && p.Value == "1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "reg-signature-count" && p.Value == "1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "catchword-position" && p.Value == "cpos");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "signature-position"
                && p.Value == "spos");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "signature-system"
                && p.Value == "ssys");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "reg-signature-position"
                && p.Value == "rpos");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);
        }
    }
}
