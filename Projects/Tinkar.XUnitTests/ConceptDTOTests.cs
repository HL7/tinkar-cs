using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Tinkar.XUnitTests
{
    public class ConceptDTOTests
    {
        [Fact]
        public void ConceptDTOJsonMarshal()
        {
            ConceptDTO dtoStart = Misc.CreateConceptDTO;
            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms))
            {
                dtoStart.Marshal(output);
            }

            ms.Dump();
            ms.Position = 0;
            using (TinkarJsonInput input = new TinkarJsonInput(ms))
            {
                ConceptDTO dtoEnd = ConceptDTO.Make(input.ReadJsonObject());
                Assert.True(dtoStart.IsEquivalent(dtoEnd));
            }
        }

        [Fact]
        public void ConceptDTOFieldsTest()
        {
            ConceptDTO dtoStart = Misc.CreateConceptDTO;
            Misc.Compare(dtoStart.ComponentUuids, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
        }

        [Fact]
        public void ConceptDTOIsEquivalentTest()
        {
            {
                ConceptDTO a = Misc.CreateConceptDTO;
                ConceptDTO b = Misc.CreateConceptDTO;
                Assert.True(a.IsEquivalent(b));
            }

            {
                ConceptDTO a = Misc.CreateConceptDTO;
                ConceptDTO b = new ConceptDTO(new Guid[] { Misc.g2, Misc.g1, Misc.g3, Misc.g4 });
                Assert.False(a.IsEquivalent(b));
            }
        }

        [Fact]
        public void ConceptDTOMarshalTest()
        {
            ConceptDTO dtoStart = Misc.CreateConceptDTO;

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                output.WriteField(dtoStart);
            }

            ms.Position = 0;
            TinkarInput input = new TinkarInput(ms);
            ConceptDTO dtoRead = (ConceptDTO) input.ReadField();
            Assert.True(dtoStart.IsEquivalent(dtoRead));
        }
    }
}
