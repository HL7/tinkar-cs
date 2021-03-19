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
    public class DiGraphVertexDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void DiGraphVertexDTOFieldsTest()
        {
            DiGraphVertexDTO dto = Misc.CreateDiGraphVertexDTO();

            GraphVertexDTOTests.DoGraphVertexDTOFieldsTest(dto);
            Assert.True(dto.Predecessors.Length == 1);
            Assert.True(dto.Predecessors[0] == 135);
        }

        [DoNotParallelize]
        [Fact]
        public void DiGraphVertexDTOIsEqTest()
        {
            {
                DiGraphVertexDTO a = Misc.CreateDiGraphVertexDTO();
                DiGraphVertexDTO b = Misc.CreateDiGraphVertexDTO();
                Assert.True(a.IsEquivalent(b));
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                DiGraphVertexDTO.Builder predecessor = new DiGraphVertexDTO.Builder()
                    .SetVertexIndex(246)
                    .SetMeaning(new ConceptDTO(Misc.PublicIdH))
                    .SetVertexId(Misc.h2)
                    ;

                DiGraphVertexDTO a = Misc.CreateDiGraphVertexDTO();
                DiGraphVertexDTO.Builder b = Misc.CreateDiGraphVertexDTOBuilder()
                    .ClearPredecessors()
                    .AppendPredecessors(predecessor)
                ;

                Assert.False(a.IsEquivalent(b.Create()));
                Assert.False(a.CompareTo(b.Create()) == 0);
            }
        }


        [DoNotParallelize]
        [Fact]
        public void DiGraphVertexDTOMarshalTest()
        {
            DiGraphVertexDTO dtoStart = Misc.CreateDiGraphVertexDTO();

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                dtoStart.Marshal(output);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                DiGraphVertexDTO dtoRead = DiGraphVertexDTO.Make(input);
                Assert.True(dtoStart.CompareTo(dtoRead) == 0);
            }
        }


        //[DoNotParallelize]
        //[Fact]
        //public void DiGraphVertexDTOJsonMarshal()
        //{
        //    DiGraphVertexDTO dtoStart = Misc.CreateDiGraphVertexDTO();
        //    MemoryStream ms = new MemoryStream();
        //    using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
        //    {
        //        dtoStart.Marshal(output);
        //    }

        //    ms.Dump();
        //    ms.Position = 0;
        //    using (TinkarJsonInput input = new TinkarJsonInput(ms))
        //    {
        //        DiGraphVertexDTO dtoEnd = DiGraphVertexDTO.Make(input);
        //        Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
        //    }
        //}
    }
}
