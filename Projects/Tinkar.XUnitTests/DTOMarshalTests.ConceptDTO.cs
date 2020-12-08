using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Tinkar.XUnitTests
{
    public partial class DTOMarshalTests
    {
        ConceptDTO CreateConceptDTO => new ConceptDTO(new Guid[] { this.g1, this.g2, this.g3, this.g4 });
        [Fact]
        public void ConceptDTOFieldsTest()
        {
            ConceptDTO dtoStart = this.CreateConceptDTO;
            Compare(dtoStart.ComponentUuids, this.g1, this.g2, this.g3, this.g4);
        }

        [Fact]
        public void ConceptDTOIsEquivalentTest()
        {
            {
                ConceptDTO a = this.CreateConceptDTO;
                ConceptDTO b = this.CreateConceptDTO;
                Assert.True(a.IsEquivalent(b));
            }

            {
                ConceptDTO a = this.CreateConceptDTO;
                ConceptDTO b = new ConceptDTO(new Guid[] { this.g2, this.g1, this.g3, this.g4 });
                Assert.False(a.IsEquivalent(b));
            }
        }

        [Fact]
        public void ConceptDTOMarshalTest()
        {
            ConceptDTO dtoStart = this.CreateConceptDTO;

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
