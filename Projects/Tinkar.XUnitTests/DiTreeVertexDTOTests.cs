using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;
using Tinkar.Dto;
using System.Collections.Immutable;

namespace Tinkar.XUnitTests
{
    public class DiTreeVertexDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void DiTreeVertexDTOFieldsTest()
        {
            DiTreeVertexDTO dto = Misc.CreateDiTreeVertexDTO();

            GraphVertexDTOTests.DoGraphVertexDTOFieldsTest(dto);
            Assert.True(dto.Predecessor == 135);
        }

        [DoNotParallelize]
        [Fact]
        public void DiTreeVertexDTOIsEqTest()
        {
            {
                DiTreeVertexDTO a = Misc.CreateDiTreeVertexDTO();
                DiTreeVertexDTO b = Misc.CreateDiTreeVertexDTO();
                Assert.True(a.IsEquivalent(b));
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                DiTreeVertexDTO.Builder predecessor = new DiTreeVertexDTO.Builder()
                    .SetVertexIndex(246)
                    .SetMeaning(new ConceptDTO(Misc.PublicIdH))
                    .SetVertexId(Misc.h2)
                    ;

                DiTreeVertexDTO a = Misc.CreateDiTreeVertexDTO();
                DiTreeVertexDTO.Builder b = Misc.CreateDiTreeVertexDTOBuilder();
                b.SetPredecessor(predecessor)
                ;

                Assert.False(a.IsEquivalent(b.Create()));
                Assert.False(a.CompareTo(b.Create()) == 0);
            }
        }


        [DoNotParallelize]
        [Fact]
        public void DiTreeVertexDTOMarshalTest()
        {
            DiTreeVertexDTO dtoStart = Misc.CreateDiTreeVertexDTO();

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                dtoStart.Marshal(output);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                DiTreeVertexDTO dtoRead = DiTreeVertexDTO.Make(input);
                Assert.True(dtoStart.CompareTo(dtoRead) == 0);
            }
        }


        //[DoNotParallelize]
        //[Fact]
        //public void DiTreeVertexDTOJsonMarshal()
        //{
        //    DiTreeVertexDTO dtoStart = Misc.CreateDiTreeVertexDTO();
        //    MemoryStream ms = new MemoryStream();
        //    using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
        //    {
        //        dtoStart.Marshal(output);
        //    }

        //    ms.Dump();
        //    ms.Position = 0;
        //    using (TinkarJsonInput input = new TinkarJsonInput(ms))
        //    {
        //        DiTreeVertexDTO dtoEnd = DiTreeVertexDTO.Make(input);
        //        Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
        //    }
        //}
    }
}
