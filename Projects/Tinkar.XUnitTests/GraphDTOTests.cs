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
                    VertexMap = new VertexDTO[]
                    {
                        new VertexDTO(
                            Misc.g1,
                            101,
                            new ConceptDTO(Misc.PublicIdG),
                            new KeyValuePair<IConcept, Object>[]
                            {
                                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x1)), (Int32) 1),
                            }
                        ),
                        new VertexDTO(
                            Misc.g2,
                            102,
                            new ConceptDTO(Misc.PublicIdH),
                            new KeyValuePair<IConcept, Object>[]
                            {
                                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x1)), (Int32) 2),
                            }
                        ),
                        new VertexDTO(
                            Misc.g3,
                            103,
                            new ConceptDTO(Misc.PublicIdI),
                            new KeyValuePair<IConcept, Object>[]
                            {
                                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x1)), (Int32) 3),
                            }
                        ),
                        new VertexDTO(
                            Misc.g4,
                            104,
                            new ConceptDTO(Misc.j1),
                            new KeyValuePair<IConcept, Object>[]
                            {
                                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x1)), (Int32) 4),
                            }
                        )
                    }.ToImmutableList()
                };
                Assert.True(a.IsEquivalent(b));
            }

            {
                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Misc.CreateGraphDTO()
                with
                {
                    VertexMap = new VertexDTO[]
                    {
                        new VertexDTO(
                            Misc.g1,
                            101,
                            new ConceptDTO(Misc.PublicIdG),
                            new KeyValuePair<IConcept, Object>[]
                            {
                                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x1)), (Int32) 1),
                            }
                        ),
                        new VertexDTO(
                            Misc.g2,
                            102,
                            new ConceptDTO(Misc.PublicIdH),
                            new KeyValuePair<IConcept, Object>[]
                            {
                                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x1)), (Int32) 2),
                            }
                        ),
                        new VertexDTO(
                            Misc.g3,
                            103,
                            new ConceptDTO(Misc.PublicIdI),
                            new KeyValuePair<IConcept, Object>[]
                            {
                                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x1)), (Int32) 3),
                            }
                        ),
                        new VertexDTO(
                            Misc.g4,
                            104,
                            new ConceptDTO(Misc.k1),
                            new KeyValuePair<IConcept, Object>[]
                            {
                                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x1)), (Int32) 4),
                            }
                        )
                    }.ToImmutableList()
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                List<KeyValuePair<Int32, ImmutableList<Int32>>> items = new List<KeyValuePair<int, ImmutableList<int>>>();
                items.Add(KeyValuePair.Create<Int32, ImmutableList<Int32>>(101, new Int32[] { 1, 2 }.ToImmutableList()));
                items.Add(KeyValuePair.Create<Int32, ImmutableList<Int32>>(102, new Int32[] { 3 }.ToImmutableList()));

                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Misc.CreateGraphDTO()
                with
                {
                    SuccessorMap = items.ToImmutableDict()
                };
                Assert.True(a.IsEquivalent(b));
            }

            {
                List<KeyValuePair<Int32, ImmutableList<Int32>>> items = new List<KeyValuePair<int, ImmutableList<int>>>();
                items.Add(KeyValuePair.Create<Int32, ImmutableList<Int32>>(101, new Int32[] { 1, 3 }.ToImmutableList()));
                items.Add(KeyValuePair.Create<Int32, ImmutableList<Int32>>(102, new Int32[] { 3 }.ToImmutableList()));

                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Misc.CreateGraphDTO()
                with
                {
                    SuccessorMap = items.ToImmutableDict()
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                List<KeyValuePair<Int32, ImmutableList<Int32>>> items = new List<KeyValuePair<int, ImmutableList<int>>>();
                items.Add(KeyValuePair.Create<Int32, ImmutableList<Int32>>(101, new Int32[] { 1, 2 }.ToImmutableList()));

                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Misc.CreateGraphDTO()
                with
                {
                    SuccessorMap = items.ToImmutableDict()
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                List<KeyValuePair<Int32, ImmutableList<Int32>>> items = new List<KeyValuePair<int, ImmutableList<int>>>();
                items.Add(KeyValuePair.Create<Int32, ImmutableList<Int32>>(101, new Int32[] { 1, 2 }.ToImmutableList()));
                items.Add(KeyValuePair.Create<Int32, ImmutableList<Int32>>(103, new Int32[] { 3 }.ToImmutableList()));

                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Misc.CreateGraphDTO()
                with
                {
                    SuccessorMap = items.ToImmutableDict()
                };
                Assert.False(a.IsEquivalent(b));
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
                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Misc.CreateGraphDTO()
                with
                {
                    VertexMap = new VertexDTO[]
                    {
                        new VertexDTO(
                            Misc.g1,
                            101,
                            new ConceptDTO(Misc.PublicIdG),
                            new KeyValuePair<IConcept, Object>[]
                            {
                                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x1)), (Int32) 1),
                            }
                        ),
                        new VertexDTO(
                            Misc.g2,
                            102,
                            new ConceptDTO(Misc.PublicIdH),
                            new KeyValuePair<IConcept, Object>[]
                            {
                                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x1)), (Int32) 2),
                            }
                        ),
                        new VertexDTO(
                            Misc.g3,
                            103,
                            new ConceptDTO(Misc.PublicIdI),
                            new KeyValuePair<IConcept, Object>[]
                            {
                                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x1)), (Int32) 3),
                            }
                        ),
                        new VertexDTO(
                            Misc.g4,
                            104,
                            new ConceptDTO(Misc.j1),
                            new KeyValuePair<IConcept, Object>[]
                            {
                                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x1)), (Int32) 4),
                            }
                        )
                    }.ToImmutableList()
                };
                Assert.True(a.CompareTo(b) != 0);
            }

            {
                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Misc.CreateGraphDTO()
                with
                {
                    VertexMap = new VertexDTO[]
                    {
                        new VertexDTO(
                            Misc.g1,
                            101,
                            new ConceptDTO(Misc.PublicIdG),
                            new KeyValuePair<IConcept, Object>[]
                            {
                                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x1)), (Int32) 1),
                            }
                        ),
                        new VertexDTO(
                            Misc.g2,
                            102,
                            new ConceptDTO(Misc.PublicIdH),
                            new KeyValuePair<IConcept, Object>[]
                            {
                                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x1)), (Int32) 2),
                            }
                        ),
                        new VertexDTO(
                            Misc.g3,
                            103,
                            new ConceptDTO(Misc.PublicIdI),
                            new KeyValuePair<IConcept, Object>[]
                            {
                                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x1)), (Int32) 3),
                            }
                        ),
                        new VertexDTO(
                            Misc.g4,
                            104,
                            new ConceptDTO(Misc.k1),
                            new KeyValuePair<IConcept, Object>[]
                            {
                                KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x1)), (Int32) 4),
                            }
                        )
                    }.ToImmutableList()
                };
                Assert.True(a.CompareTo(b) != 0);
            }

            {
                List<KeyValuePair<Int32, ImmutableList<Int32>>> items = new List<KeyValuePair<int, ImmutableList<int>>>();
                items.Add(KeyValuePair.Create<Int32, ImmutableList<Int32>>(101, new Int32[] { 1, 2 }.ToImmutableList()));
                items.Add(KeyValuePair.Create<Int32, ImmutableList<Int32>>(102, new Int32[] { 3 }.ToImmutableList()));

                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Misc.CreateGraphDTO()
                with
                {
                    SuccessorMap = items.ToImmutableDict()
                };
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                List<KeyValuePair<Int32, ImmutableList<Int32>>> items = new List<KeyValuePair<int, ImmutableList<int>>>();
                items.Add(KeyValuePair.Create<Int32, ImmutableList<Int32>>(101, new Int32[] { 1, 3 }.ToImmutableList()));
                items.Add(KeyValuePair.Create<Int32, ImmutableList<Int32>>(102, new Int32[] { 3 }.ToImmutableList()));

                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Misc.CreateGraphDTO()
                with
                {
                    SuccessorMap = items.ToImmutableDict()
                };
                Assert.True(a.CompareTo(b) != 0);
            }

            {
                List<KeyValuePair<Int32, ImmutableList<Int32>>> items = new List<KeyValuePair<int, ImmutableList<int>>>();
                items.Add(KeyValuePair.Create<Int32, ImmutableList<Int32>>(101, new Int32[] { 1, 2 }.ToImmutableList()));

                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Misc.CreateGraphDTO()
                with
                {
                    SuccessorMap = items.ToImmutableDict()
                };
                Assert.True(a.CompareTo(b) != 0);
            }

            {
                List<KeyValuePair<Int32, ImmutableList<Int32>>> items = new List<KeyValuePair<int, ImmutableList<int>>>();
                items.Add(KeyValuePair.Create<Int32, ImmutableList<Int32>>(101, new Int32[] { 1, 2 }.ToImmutableList()));
                items.Add(KeyValuePair.Create<Int32, ImmutableList<Int32>>(103, new Int32[] { 3 }.ToImmutableList()));

                GraphDTO a = Misc.CreateGraphDTO();
                GraphDTO b = Misc.CreateGraphDTO()
                with
                {
                    SuccessorMap = items.ToImmutableDict()
                };
                Assert.True(a.CompareTo(b) != 0);
            }
        }

        [DoNotParallelize]
        [Fact]
        public void GraphDTOMarshalTest()
        {
            GraphDTO dtoStart = Misc.CreateGraphDTO();

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                dtoStart.MarshalVertexMap(output);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                ImmutableList<VertexDTO> dtoRead = GraphDTO.UnmarshalVertexMap(input);
                Assert.True(FieldCompare.CompareSequence(dtoRead, dtoStart.VertexMap) == 0);
            }
        }
    }
}
