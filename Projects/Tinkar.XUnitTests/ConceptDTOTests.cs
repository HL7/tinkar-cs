using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;
using Tinkar.Dto;

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
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
            {
                dtoStart.Marshal(output);
            }

            ms.Dump();
            ms.Position = 0;
            using (TinkarJsonInput input = new TinkarJsonInput(ms))
            {
                ConceptDTO dtoEnd = ConceptDTO.Make(input.ReadJsonObject());
                Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
            }
        }

        [DoNotParallelize]
        [Fact]
        public void ConceptDTOConstructor()
        {
            String uuidString = $"[\"{Misc.g1.ToString()}\" \"{Misc.g2.ToString()}\" \"{Misc.g3.ToString()}\"]";
            ConceptDTO dto = ConceptDTO.Make(uuidString);
            Assert.True(dto.PublicId.UuidCount == 3);
            Assert.True(dto.PublicId.AsUuidArray[0] == Misc.g1);
            Assert.True(dto.PublicId.AsUuidArray[1] == Misc.g2);
            Assert.True(dto.PublicId.AsUuidArray[2] == Misc.g3);
        }

        [DoNotParallelize]
        [Fact]
        public void ConceptDTOFieldsTest()
        {
            ConceptDTO dtoStart = Misc.CreateConceptDTO;
            Misc.Compare(dtoStart.PublicId.AsUuidArray, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
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
                ConceptDTO b = new ConceptDTO(new PublicId(Misc.other));
                Assert.False(a.IsEquivalent(b));
            }
        }

        [DoNotParallelize]
        [Fact]
        public void ConceptDTOIsSameTest()
        {
            {
                ConceptDTO a = Misc.CreateConceptDTO;
                ConceptDTO b = Misc.CreateConceptDTO;
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                ConceptDTO a = Misc.CreateConceptDTO;
                ConceptDTO b = new ConceptDTO(new PublicId(Misc.g1, Misc.g3, Misc.g4));
                Assert.False(a.CompareTo(b) == 0);
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
                ConceptDTO dtoRead = (ConceptDTO)input.GetField();
                Assert.True(dtoStart.CompareTo(dtoRead) == 0);
            }
        }
    }
}
