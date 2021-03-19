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
    public class DiTreeDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void DiTreeDTOFieldsTest()
        {
            DiTreeDTO dto = Misc.CreateDiTreeDTO();
            Assert.True(dto.VertexMap.Length == 4);
            Assert.True(dto.VertexMap[0] == dto.Vertex(Misc.g1));
            Assert.True(dto.VertexMap[1] == dto.Vertex(Misc.g2));
            Assert.True(dto.VertexMap[2] == dto.Vertex(Misc.g3));
            Assert.True(dto.VertexMap[3] == dto.Vertex(Misc.g4));

            Assert.True(dto.VertexMap[0] == dto.Vertex(0));
            Assert.True(dto.VertexMap[1] == dto.Vertex(1));
            Assert.True(dto.VertexMap[2] == dto.Vertex(2));
            Assert.True(dto.VertexMap[3] == dto.Vertex(3));

            {
                Assert.True(dto.Predecessor(dto.Vertex(0)) == null);
                Assert.True(dto.Successors(dto.Vertex(0)).Count() == 1);
                Assert.True(dto.Successors(dto.Vertex(0)).ElementAt(0) == dto.Vertex(1));
            }
            {
                Assert.True(dto.Predecessor(dto.Vertex(1)) == dto.Vertex(0));
                Assert.True(dto.Successors(dto.Vertex(1)).Count() == 1);
                Assert.True(dto.Successors(dto.Vertex(1)).ElementAt(0) == dto.Vertex(2));
            }
            {
                Assert.True(dto.Predecessor(dto.Vertex(2)) == dto.Vertex(1));
                Assert.True(dto.Successors(dto.Vertex(2)).Count() == 1);
                Assert.True(dto.Successors(dto.Vertex(2)).ElementAt(0) == dto.Vertex(3));
            }
            {
                Assert.True(dto.Predecessor(dto.Vertex(3)) == dto.Vertex(2));
                Assert.True(dto.Successors(dto.Vertex(3)).Count() == 0);
            }
        }



        [DoNotParallelize]
        [Fact]
        public void DiTreeDTOEqTest()
        {
            {
                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO b = Misc.CreateDiTreeDTO();
                Assert.True(a.CompareTo(b) == 0);
                Assert.True(a.IsEquivalent(b));
            }
            {
                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO.Builder b = Misc.CreateDiTreeDTOBuilder();
                b.SetRoot(b.Vertex(Misc.g2));
                b.Vertex(Misc.g2)
                    .ClearSuccessors()
                    .AppendSuccessors(b.Vertex(Misc.g1));
                b.Vertex(Misc.g1)
                    .SetPredecessor(b.Vertex(Misc.g2));

                Assert.False(a.CompareTo(b.Create()) == 0);
                Assert.False(a.IsEquivalent(b.Create()));
            }

            {
                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO.Builder b = Misc.CreateDiTreeDTOBuilder();
                b.Vertex(Misc.g1).ClearProperties();

                Assert.False(a.CompareTo(b.Create()) == 0);
                Assert.False(a.IsEquivalent(b.Create()));
            }
        }



        [DoNotParallelize]
        [Fact]
        public void DiTreeDTOMarshalTest()
        {
            DiTreeDTO dtoStart = Misc.CreateDiTreeDTO();

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                dtoStart.Marshal(output);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                DiTreeDTO dtoRead = DiTreeDTO.Make(input);
                Assert.True(dtoStart.CompareTo(dtoRead) == 0);
            }
        }
    }
}
