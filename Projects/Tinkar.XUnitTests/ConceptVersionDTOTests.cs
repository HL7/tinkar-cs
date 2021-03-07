using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xunit;
using Assert = Xunit.Assert;

namespace Tinkar.XUnitTests
{
    public class ConceptVersionDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void ConceptVersionDTOFieldsTest()
        {
            DateTime time = new DateTime(2020, 12, 31);

            ConceptVersionDTO dtoStart = Misc.CreateConceptVersionDTO;
            Misc.Compare(dtoStart.PublicId, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Assert.True(dtoStart.StampDTO.IsSame(Misc.CreateStampDTO) == 0);
        }

        [DoNotParallelize]
        [Fact]
        public void ConceptVersionDTOIsEquivalentTest()
        {
            DateTime time = new DateTime(2020, 12, 31);
            {
                ConceptVersionDTO a = Misc.CreateConceptVersionDTO;
                ConceptVersionDTO b = Misc.CreateConceptVersionDTO;
                Assert.True(a.IsEquivalent(b));
            }

            {
                ConceptVersionDTO a = Misc.CreateConceptVersionDTO;

                ConceptVersionDTO b = new ConceptVersionDTO(
                    new PublicId(Misc.other),
                    Misc.CreateStampDTO);
                Assert.False(a.IsEquivalent(b));
            }
        }

        [DoNotParallelize]
        [Fact]
        public void ConceptVersionDTOIsSameTest()
        {
            DateTime time = new DateTime(2020, 12, 31);
            {
                ConceptVersionDTO a = Misc.CreateConceptVersionDTO;
                ConceptVersionDTO b = Misc.CreateConceptVersionDTO;
                Assert.True(a.IsSame(b) == 0);
            }

            {
                ConceptVersionDTO a = Misc.CreateConceptVersionDTO;

                ConceptVersionDTO b = new ConceptVersionDTO(
                    new PublicId(Misc.g1, Misc.g3, Misc.g4),
                    Misc.CreateStampDTO);
                Assert.False(a.IsSame(b) == 0);
            }
        }

        [DoNotParallelize]
        [Fact]
        public void ConceptVersionDTOMarshalTest()
        {
            ConceptVersionDTO dtoStart = Misc.CreateConceptVersionDTO;

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                dtoStart.Marshal(output);
            }


            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                ConceptVersionDTO dtoRead = ConceptVersionDTO.Make(input,
                    new PublicId(Misc.g1, Misc.g2, Misc.g3, Misc.g4 ));
                Assert.True(dtoStart.IsSame(dtoRead) == 0);
            }
        }

        [DoNotParallelize]
        [Fact]
        public void ConceptVersionDTOJsonMarshalTest()
        {
            ConceptVersionDTO dtoStart = Misc.CreateConceptVersionDTO;

            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
            {
                dtoStart.Marshal(output);
            }

            ms.Dump();
            ms.Position = 0;
            TinkarJsonInput input = new TinkarJsonInput(ms);
            ConceptVersionDTO dtoRead = ConceptVersionDTO.Make(input.ReadJsonObject(),
                new PublicId(Misc.g1, Misc.g2, Misc.g3, Misc.g4));
            Assert.True(dtoStart.IsSame(dtoRead) == 0);
        }
        [DoNotParallelize]
        [Fact]
        public void ConceptVersionDTOJsonMarshal()
        {
            ConceptVersionDTO dtoStart = Misc.CreateConceptVersionDTO;
            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
            {
                dtoStart.Marshal(output);
            }

            ms.Dump();
            ms.Position = 0;
            using (TinkarJsonInput input = new TinkarJsonInput(ms))
            {
                ConceptVersionDTO dtoEnd = ConceptVersionDTO.Make(
                    input.ReadJsonObject(),
                    dtoStart.PublicId);
                Assert.True(dtoStart.IsSame(dtoEnd) == 0);
            }
        }
    }
}
