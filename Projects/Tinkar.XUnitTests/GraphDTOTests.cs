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
    public class GraphDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void GraphDTOFieldsTest()
        {
            GraphDTO dto = Misc.CreateGraphDTO();
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
        public void GraphDTOIsEquivalentTest()
        {
            {
                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Misc.CreateGraphDTO();
                Assert.True(a.IsEquivalent(b));
            }

            {
                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Misc.CreateGraphDTO()
                with
                {
                    GraphId = new GraphId(Misc.h1)
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Misc.CreateGraphDTO()
                with
                {
                    GraphIndex = 55
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Misc.CreateGraphDTO()
                with
                {
                    Meaning = new ConceptDTO(a.Meaning.PublicId.AsUuidArray[0], Misc.other)
                };
                Assert.True(a.IsEquivalent(b));
            }

            {
                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Misc.CreateGraphDTO()
                with
                {
                    Meaning = new ConceptDTO(Misc.other)
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Misc.CreateGraphDTO()
                with
                {
                    Properties = new KeyValuePair<IConcept, Object>[]
                    {
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x1)), (Int32) 1),
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x2)), (Int64) 2),
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x3)), (Single) 3),
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x4)), (Double) 4),
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x5)), "abcdef"),
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x6)), true),
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x7)), new DateTime(2000, 12, 31))
                    }.ToImmutableDict()
                };
                Assert.False(a.IsEquivalent(b));
            }
        }


        //[DoNotParallelize]
        //[Fact]
        //public void GraphDTOIsSameTest()
        //{
        //    {
        //        GraphDTO a = Misc.CreateGraphDTO();
        //        GraphDTO b = Misc.CreateGraphDTO();
        //        Assert.True(a.CompareTo(b) == 0);
        //    }

        //    {
        //        GraphDTO a = Misc.CreateGraphDTO();
        //        GraphDTO b = Misc.CreateGraphDTO()
        //        with
        //        {
        //            GraphId = new GraphId(Misc.h1)
        //        };
        //        Assert.False(a.CompareTo(b) == 0);
        //    }

        //    {
        //        GraphDTO a = Misc.CreateGraphDTO();
        //        GraphDTO b = Misc.CreateGraphDTO()
        //        with
        //        {
        //            GraphIndex = 55
        //        };
        //        Assert.False(a.CompareTo(b) == 0);
        //    }

        //    {
        //        GraphDTO a = Misc.CreateGraphDTO();
        //        GraphDTO b = Misc.CreateGraphDTO()
        //        with
        //        {
        //            Meaning = new ConceptDTO(a.Meaning.PublicId.AsUuidArray[0], Misc.other)
        //        };
        //        Assert.False(a.CompareTo(b) == 0);
        //    }

        //    {
        //        GraphDTO a = Misc.CreateGraphDTO();
        //        GraphDTO b = Misc.CreateGraphDTO()
        //        with
        //        {
        //            Meaning = new ConceptDTO(Misc.other)
        //        };
        //        Assert.False(a.CompareTo(b) == 0);
        //    }

        //    {
        //        GraphDTO a = Misc.CreateGraphDTO();
        //        GraphDTO b = Misc.CreateGraphDTO()
        //        with
        //        {
        //            Properties = new KeyValuePair<IConcept, Object>[]
        //            {
        //                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x1)), (Int32) 1),
        //                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x2)), (Int64) 2),
        //                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x3)), (Single) 3),
        //                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x4)), (Double) 4),
        //                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x5)), "abcdef"),
        //                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x6)), true),
        //                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x7)), new DateTime(2000, 12, 31))
        //            }.ToImmutableDict()
        //        };
        //        Assert.False(a.CompareTo(b) == 0);
        //    }
        //}

        //[DoNotParallelize]
        //[Fact]
        //public void GraphDTOMarshalTest()
        //{
        //    GraphDTO dtoStart = Misc.CreateGraphDTO();

        //    MemoryStream ms = new MemoryStream();
        //    using (TinkarOutput output = new TinkarOutput(ms))
        //    {
        //        dtoStart.Marshal(output);
        //    }

        //    ms.Position = 0;
        //    using (TinkarInput input = new TinkarInput(ms))
        //    {
        //        GraphDTO dtoRead = GraphDTO.Make(input,
        //            dtoStart.PublicId,
        //            dtoStart.DefinitionForSemanticPublicId,
        //            dtoStart.ReferencedComponentPublicId);
        //        Assert.True(dtoStart.CompareTo(dtoRead) == 0);
        //    }
        //}
        //[DoNotParallelize]
        //[Fact]
        //public void GraphDTOJsonMarshal()
        //{
        //    VertexDTO dtoStart = Misc.CreateVertexDTO();
        //    MemoryStream ms = new MemoryStream();
        //    using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
        //    {
        //        dtoStart.Marshal(output);
        //    }

        //    ms.Dump();
        //    ms.Position = 0;
        //    using (TinkarJsonInput input = new TinkarJsonInput(ms))
        //    {
        //        VertexDTO dtoEnd = VertexDTO.Make(
        //            input.ReadJsonObject(),
        //            dtoStart.PublicId,
        //            dtoStart.DefinitionForSemanticPublicId,
        //            dtoStart.ReferencedComponentPublicId);
        //        Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
        //    }
        //}

    }
}
