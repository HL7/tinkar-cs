using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Tinkar.XUnitTests
{
    public class ConceptVersionDTOTests
    {
        [Fact]
        public void ConceptVersionDTOFieldsTest()
        {
            DateTime time = new DateTime(2020, 12, 31);

            ConceptVersionDTO dtoStart = new ConceptVersionDTO(
                new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                Misc.CreateStampDTO
                );
            Misc.Compare(dtoStart.ComponentUuids, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Assert.True(dtoStart.StampDTO.IsEquivalent(Misc.CreateStampDTO));
        }

        [Fact]
        public void ConceptVersionDTOIsEquivalentTest()
        {
            DateTime time = new DateTime(2020, 12, 31);
            {
                ConceptVersionDTO a = new ConceptVersionDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    Misc.CreateStampDTO);
                ConceptVersionDTO b = new ConceptVersionDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    Misc.CreateStampDTO);
                Assert.True(a.IsEquivalent(b));
            }

            {
                ConceptVersionDTO a = new ConceptVersionDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    Misc.CreateStampDTO);

                ConceptVersionDTO b = new ConceptVersionDTO(
                    new Guid[] { Misc.g2, Misc.g1, Misc.g3, Misc.g4 },
                    Misc.CreateStampDTO);
                Assert.False(a.IsEquivalent(b));
            }
        }

        [Fact]
        public void ConceptVersionDTOMarshalTest()
        {
            ConceptVersionDTO dtoStart = new ConceptVersionDTO(
                new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                Misc.CreateStampDTO);

            MemoryStream ms = new MemoryStream();
            TinkarOutput output = new TinkarOutput(ms);
            dtoStart.Marshal(output);

            ms.Position = 0;
            TinkarInput input = new TinkarInput(ms);
            ConceptVersionDTO dtoRead = ConceptVersionDTO.Make(input,
                new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 });
            Assert.True(dtoStart.IsEquivalent(dtoRead));
        }
    }
}
