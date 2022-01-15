using Cadmus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Codicology.Parts.Test
{
    public sealed class CodQuiresPartTest
    {
        private static CodQuiresPart GetPart(int count)
        {
            CodQuiresPart part = new()
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
            };

            for (int n = 1; n <= count; n++)
            {
                part.Types.Add("t" + n);
                part.Quires.Add(new CodQuire
                {
                    Tag = "tag",
                    StartNr = n * 4,
                    EndNr = n * 4 + 3,
                    SheetCount = 4,
                    SheetDelta = 1,
                    Note = "note"
                });
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            CodQuiresPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            CodQuiresPart part2 =
                TestHelper.DeserializePart<CodQuiresPart>(json)!;

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Quires.Count);
        }

        [Fact]
        public void GetDataPins_NoEntries_Ok()
        {
            CodQuiresPart part = GetPart(0);

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
            CodQuiresPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(5, pins.Count);
            TestHelper.AssertValidDataPinNames(pins);

            DataPin? pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);
            Assert.Equal("3", pin!.Value);

            pin = pins.Find(p => p.Name == "sheet-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);
            Assert.Equal("4", pin!.Value);

            // t1 t2 t3
            for (int n = 1; n <= 3; n++)
            {
                pin = pins.Find(p => p.Name == "type" && p.Value == "t" + n);
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin!);
            }
        }
    }
}
