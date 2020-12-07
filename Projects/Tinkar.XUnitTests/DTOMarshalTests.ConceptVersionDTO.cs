using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Tinkar.XUnitTests
{
    public partial class DTOMarshalTests
    {
        [Fact]
        public void ConceptVersionDTOFieldsTest()
        {
            DateTime time = new DateTime(2020, 12, 31);

            ConceptVersionDTO dtoStart = new ConceptVersionDTO(
                new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                CreateStampDTO
                );
            Compare(dtoStart.ComponentUuids, this.g1, this.g2, this.g3, this.g4);
            Assert.True(dtoStart.StampDTO.IsEquivalent(CreateStampDTO));
        }

        [Fact]
        public void ConceptVersionDTOIsEquivalentTest()
        {
            DateTime time = new DateTime(2020, 12, 31);
            {
                ConceptVersionDTO a = new ConceptVersionDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    CreateStampDTO);
                ConceptVersionDTO b = new ConceptVersionDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    CreateStampDTO);
                Assert.True(a.IsEquivalent(b));
            }

            {
                ConceptVersionDTO a = new ConceptVersionDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    CreateStampDTO);

                ConceptVersionDTO b = new ConceptVersionDTO(
                    new Guid[] { this.g2, this.g1, this.g3, this.g4 },
                    CreateStampDTO);
                Assert.False(a.IsEquivalent(b));
            }
        }

        [Fact]
        public void ConceptVersionDTOMarshalTest()
        {
            ConceptVersionDTO dtoStart = new ConceptVersionDTO(
                new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                CreateStampDTO);

            MemoryStream ms = new MemoryStream();
            TinkarOutput output = new TinkarOutput(ms);
            dtoStart.Marshal(output);

            ms.Position = 0;
            TinkarInput input = new TinkarInput(ms);
            ConceptVersionDTO dtoRead = ConceptVersionDTO.Make(input,
                new Guid[] { this.g1, this.g2, this.g3, this.g4 });
            Assert.True(dtoStart.IsEquivalent(dtoRead));
        }
    }
}
