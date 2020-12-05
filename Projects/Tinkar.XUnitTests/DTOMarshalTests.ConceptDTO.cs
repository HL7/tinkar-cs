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
        public void ConceptDTOFieldsTest()
        {
            ConceptDTO dtoStart = new ConceptDTO(new Guid[] { this.g1, this.g2, this.g3, this.g4 });
            Compare(dtoStart.ComponentUuids, this.g1, this.g2, this.g3, this.g4);
        }

        [Fact]
        public void ConceptDTOIsEquivalentTest()
        {
            {
                ConceptDTO a = new ConceptDTO(new Guid[] { this.g1, this.g2, this.g3, this.g4 });
                ConceptDTO b = new ConceptDTO(new Guid[] { this.g1, this.g2, this.g3, this.g4 });
                Assert.True(a.IsEquivalent(b));
            }

            {
                ConceptDTO a = new ConceptDTO(new Guid[] { this.g1, this.g2, this.g3, this.g4 });
                ConceptDTO b = new ConceptDTO(new Guid[] { this.g2, this.g1, this.g3, this.g4 });
                Assert.False(a.IsEquivalent(b));
            }
        }

        [Fact]
        public void ConceptDTOMarshalTest()
        {
            ConceptDTO dtoStart = new ConceptDTO(new Guid[] { this.g1, this.g2, this.g3, this.g4 });

            MemoryStream ms = new MemoryStream();
            TinkarOutput output = new TinkarOutput(ms);
            output.WriteField(dtoStart);

            ms.Position = 0;
            TinkarInput input = new TinkarInput(ms);
            ConceptDTO dtoRead = (ConceptDTO) input.ReadField();
            Assert.True(dtoStart.IsEquivalent(dtoRead));
        }
    }
}
