using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;

namespace Tinkar.XUnitTests
{
    public class ConceptDTOTests
    {
        [DoNotParallelize]
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

        [DoNotParallelize]
        [Fact]
        public void ConceptDTOFieldsTest()
        {
            ConceptDTO dtoStart = Misc.CreateConceptDTO;
            Misc.Compare(dtoStart.ComponentUuids, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
        }

        [DoNotParallelize]
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

        [DoNotParallelize]
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
            using (TinkarInput input = new TinkarInput(ms))
            {
                ConceptDTO dtoRead = (ConceptDTO) input.ReadField();
                Assert.True(dtoStart.IsEquivalent(dtoRead));
            }
        }
    }
}
