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
    public class GraphDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void GraphDTOFieldsTest()
        {
            GraphDTO<GraphVertexDTO> dto = Misc.CreateGraphDTO();
            Assert.True(dto.VertexMap.Count == 4);
            Assert.True(dto.VertexMap[0] == dto.Vertex(Misc.g1));
            Assert.True(dto.VertexMap[1] == dto.Vertex(Misc.g2));
            Assert.True(dto.VertexMap[2] == dto.Vertex(Misc.g3));
            Assert.True(dto.VertexMap[3] == dto.Vertex(Misc.g4));

            Assert.True(dto.VertexMap[0] == dto.Vertex(0));
            Assert.True(dto.VertexMap[1] == dto.Vertex(1));
            Assert.True(dto.VertexMap[2] == dto.Vertex(2));
            Assert.True(dto.VertexMap[3] == dto.Vertex(3));

            {
                VertexDTO[] successors = dto.Successors(dto.Vertex(0)).ToArray();
                Assert.True(successors.Length == 2);
                Assert.Contains(dto.Vertex(1), successors);
                Assert.Contains(dto.Vertex(2), successors);
            }
            {
                VertexDTO[] successors = dto.Successors(dto.Vertex(1)).ToArray();
                Assert.True(successors.Length == 1);
                Assert.Contains(dto.Vertex(3), successors);
            }
            {
                VertexDTO[] successors = dto.Successors(dto.Vertex(2)).ToArray();
                Assert.True(successors.Length == 0);
            }
        }

        [DoNotParallelize]
        [Fact]
        public void GraphDTOIsEqTest()
        {
            {
                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Misc.CreateGraphDTO();
                Assert.True(a.IsEquivalent(b));
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO.Builder b = Misc.CreateGraphDTOBuilder();
                b.Vertex(Misc.g4).SetMeaning(new ConceptDTO(Misc.k1));
                Assert.False(a.IsEquivalent(b.Create()));
                Assert.False(a.CompareTo(b.Create()) == 0);
            }
        }


        [DoNotParallelize]
        [Fact]
        public void GraphDTOCompareToTest()
        {
            {
                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Misc.CreateGraphDTO();
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                GraphDTO Local()
                {
                    GraphDTO.Builder bldr = new GraphDTO.Builder();
                    bldr.AppendVertex(Misc.g1, new ConceptDTO(Misc.PublicIdG))
                        .AppendProperty(new ConceptDTO(Misc.GID(0x1)), (Int32)1)
                        ;
                    bldr.AppendVertex(Misc.g2, new ConceptDTO(Misc.PublicIdH))
                        .AppendProperty(new ConceptDTO(Misc.GID(0x1)), (Int32)2)
                        ;
                    bldr.AppendVertex(Misc.g3, new ConceptDTO(Misc.PublicIdI))
                        .AppendProperty(new ConceptDTO(Misc.GID(0x1)), (Int32)3)
                        ;

                    bldr.Vertex(Misc.g1).AppendSuccessors(bldr.Vertex(Misc.g2), bldr.Vertex(Misc.g3));

                    return bldr.Create();
                }

                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Local();

                Assert.True(a.CompareTo(b) != 0);
            }

            {
                GraphDTO Local()
                {
                    GraphDTO.Builder bldr = new GraphDTO.Builder();
                    bldr.AppendVertex(Misc.g1, new ConceptDTO(Misc.PublicIdG))
                        .AppendProperty(new ConceptDTO(Misc.GID(0x1)), (Int32)1)
                        ;
                    bldr.AppendVertex(Misc.g2, new ConceptDTO(Misc.PublicIdH))
                        .AppendProperty(new ConceptDTO(Misc.GID(0x1)), (Int32)2)
                        ;
                    bldr.AppendVertex(Misc.g3, new ConceptDTO(Misc.PublicIdI))
                        .AppendProperty(new ConceptDTO(Misc.GID(0x1)), (Int32)3)
                        ;
                    bldr.AppendVertex(Misc.g4, new ConceptDTO(Misc.PublicIdK))
                        .AppendProperty(new ConceptDTO(Misc.GID(0x1)), (Int32)4)
                        ;

                    bldr.Vertex(Misc.g1).AppendSuccessors(bldr.Vertex(Misc.g2), bldr.Vertex(Misc.g3));

                    return bldr.Create();
                }

                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Local();
                Assert.True(a.CompareTo(b) != 0);
            }

            {
                GraphDTO Local()
                {
                    GraphDTO.Builder bldr = new GraphDTO.Builder();
                    bldr.AppendVertex(Misc.g1, new ConceptDTO(Misc.PublicIdG))
                        .AppendProperty(new ConceptDTO(Misc.GID(0x1)), (Int32)1)
                        ;
                    bldr.AppendVertex(Misc.g2, new ConceptDTO(Misc.PublicIdH))
                        .AppendProperty(new ConceptDTO(Misc.GID(0x1)), (Int32)2)
                        ;
                    bldr.AppendVertex(Misc.g3, new ConceptDTO(Misc.PublicIdI))
                        .AppendProperty(new ConceptDTO(Misc.GID(0x1)), (Int32)3)
                        ;
                    bldr.AppendVertex(Misc.g4, new ConceptDTO(Misc.PublicIdK))
                        .AppendProperty(new ConceptDTO(Misc.GID(0x1)), (Int32)4)
                        ;

                    bldr.Vertex(Misc.g1).AppendSuccessors(bldr.Vertex(Misc.g2));

                    return bldr.Create();
                }


                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Local();
                Assert.True(a.CompareTo(b) != 0);
            }
        }

        [DoNotParallelize]
        [Fact]
        public void GraphDTOMarshalTest()
        {
            throw new NotImplementedException();
            //    GraphDTO dtoStart = Misc.CreateGraphDTO();

            //    MemoryStream ms = new MemoryStream();
            //    using (TinkarOutput output = new TinkarOutput(ms))
            //    {
            //        dtoStart.MarshalVertexMap(output);
            //    }

            //    ms.Position = 0;
            //    using (TinkarInput input = new TinkarInput(ms))
            //    {
            //        ImmutableList<VertexDTO> dtoRead = GraphDTO.UnmarshalVertexMap(input);
            //        Assert.True(FieldCompare.CompareSequence(dtoRead, dtoStart.VertexMap) == 0);
            //    }
        }
    }
}
