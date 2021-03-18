using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;
using Tinkar.Dto;
using System.Collections.Immutable;

#pragma warning disable xUnit1013 // Public method should be marked as test
namespace Tinkar.XUnitTests
{
    public class GraphVertexDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void GraphVertexDTOFieldsTest()
        {
            GraphVertexDTO dto = Misc.CreateGraphVertexDTO();
            DoGraphVertexDTOFieldsTest(dto);
        }

        public static void DoGraphVertexDTOFieldsTest(GraphVertexDTO dto)
        {
            VertexDTOTests.DoVertexDTOFieldsTest(dto);
        }

        [DoNotParallelize]
        [Fact]
        public void GraphVertexDTOIsEqTest()
        {
            {
                GraphVertexDTO a = Misc.CreateGraphVertexDTO();
                GraphVertexDTO b = Misc.CreateGraphVertexDTO();
                Assert.True(a.IsEquivalent(b));
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                GraphVertexDTO a = Misc.CreateGraphVertexDTO();
                GraphVertexDTO.Builder b = Misc.CreateGraphVertexDTOBuilder();
                b.ClearSuccessors()
                    .AppendSuccessors(new GraphVertexDTO.Builder()
                        .SetVertexIndex(456)
                        .SetMeaning(new ConceptDTO(Misc.PublicIdG))
                        .SetVertexId(Misc.g2))
                    ;

                Assert.False(a.IsEquivalent(b.Create()));
                Assert.False(a.CompareTo(b.Create()) == 0);
            }
        }


        [DoNotParallelize]
        [Fact]
        public void GraphVertexDTOMarshalTest()
        {
            GraphVertexDTO dtoStart = Misc.CreateGraphVertexDTO();

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                dtoStart.Marshal(output);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                GraphVertexDTO dtoRead = GraphVertexDTO.Make(input);
                Assert.True(dtoStart.CompareTo(dtoRead) == 0);
            }
        }


        //[DoNotParallelize]
        //[Fact]
        //public void GraphVertexDTOJsonMarshal()
        //{
        //    GraphVertexDTO dtoStart = Misc.CreateGraphVertexDTO();
        //    MemoryStream ms = new MemoryStream();
        //    using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
        //    {
        //        dtoStart.Marshal(output);
        //    }

        //    ms.Dump();
        //    ms.Position = 0;
        //    using (TinkarJsonInput input = new TinkarJsonInput(ms))
        //    {
        //        GraphVertexDTO dtoEnd = GraphVertexDTO.Make(input);
        //        Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
        //    }
        //}
    }
}
