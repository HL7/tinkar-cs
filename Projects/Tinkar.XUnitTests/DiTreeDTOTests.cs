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
                VertexDTO predecessor = dto.Predecessor(dto.Vertex(0));
                Assert.True(predecessor == dto.Vertex(1));
            }
            {
                VertexDTO predecessor = dto.Predecessor(dto.Vertex(1));
                Assert.True(predecessor == dto.Vertex(3));
            }
        }



        [DoNotParallelize]
        [Fact]
        public void DiTreeDTOIsEquivalentTest()
        {
            {
                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO b = Misc.CreateDiTreeDTO();
                Assert.True(a.IsEquivalent(b));
            }

            {
                ImmutableDictionary<IConcept, Object>.Builder pb1 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                pb1.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)1);
                pb1.Add(new ConceptDTO(Misc.GID(0x2)), (Int64)2);
                pb1.Add(new ConceptDTO(Misc.GID(0x3)), (Single)3);
                pb1.Add(new ConceptDTO(Misc.GID(0x4)), (Double)4);
                pb1.Add(new ConceptDTO(Misc.GID(0x5)), "abcdef");
                pb1.Add(new ConceptDTO(Misc.GID(0x7)), new DateTime(2000, 1, 1));

                VertexDTO newRoot = new VertexDTO(
                        Misc.g1,
                        123,
                        new ConceptDTO(Misc.PublicIdH),
                        pb1.ToImmutable()
                    );

                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO b = Misc.CreateDiTreeDTO()
                with
                {
                    Root = newRoot
                };
                Assert.True(a.IsEquivalent(b) == false);
            }

            {
                ImmutableDictionary<Int32, Int32>.Builder predecessors = ImmutableDictionary<Int32, Int32>.Empty.ToBuilder();
                predecessors.Add(101, 1);
                predecessors.Add(102, 2);

                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO b = Misc.CreateDiTreeDTO()
                with
                {
                    PredecessorMap = predecessors.ToImmutable()
                };
                Assert.True(a.IsEquivalent(b) == false);
            }

            {
                ImmutableDictionary<IConcept, Object>.Builder pBuilder1 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                pBuilder1.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)1);

                ImmutableDictionary<IConcept, Object>.Builder pBuilder2 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                pBuilder2.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)2);

                ImmutableDictionary<IConcept, Object>.Builder pBuilder3 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                pBuilder3.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)3);

                ImmutableDictionary<IConcept, Object>.Builder pBuilder4 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                pBuilder4.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)4);

                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO b = Misc.CreateDiTreeDTO()
                with
                {
                    VertexMap = new VertexDTO[]
                    {
                        new VertexDTO(
                            Misc.g1,
                            101,
                            new ConceptDTO(Misc.PublicIdG),
                            pBuilder1.ToImmutable()
                        ),
                        new VertexDTO(
                            Misc.g2,
                            102,
                            new ConceptDTO(Misc.PublicIdH),
                            pBuilder2.ToImmutable()
                        ),
                        new VertexDTO(
                            Misc.g3,
                            103,
                            new ConceptDTO(Misc.PublicIdI),
                            pBuilder3.ToImmutable()
                        ),
                        new VertexDTO(
                            Misc.g4,
                            104,
                            new ConceptDTO(Misc.j1),
                            pBuilder4.ToImmutable()
                        )
                    }.ToImmutableList()
                };
                Assert.True(a.IsEquivalent(b));
            }

            {
                ImmutableDictionary<IConcept, Object>.Builder pBuilder1 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                pBuilder1.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)1);

                ImmutableDictionary<IConcept, Object>.Builder pBuilder2 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                pBuilder2.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)2);

                ImmutableDictionary<IConcept, Object>.Builder pBuilder3 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                pBuilder3.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)3);

                ImmutableDictionary<IConcept, Object>.Builder pBuilder4 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                pBuilder4.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)4);

                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO b = Misc.CreateDiTreeDTO()
                with
                {
                    VertexMap = new VertexDTO[]
                    {
                        new VertexDTO(
                            Misc.g1,
                            101,
                            new ConceptDTO(Misc.PublicIdG),
                            pBuilder1.ToImmutable()
                        ),
                        new VertexDTO(
                            Misc.g2,
                            102,
                            new ConceptDTO(Misc.PublicIdH),
                            pBuilder2.ToImmutable()
                        ),
                        new VertexDTO(
                            Misc.g3,
                            103,
                            new ConceptDTO(Misc.PublicIdI),
                            pBuilder3.ToImmutable()
                        ),
                        new VertexDTO(
                            Misc.g4,
                            104,
                            new ConceptDTO(Misc.k1),
                            pBuilder4.ToImmutable()
                        )
                    }.ToImmutableList()
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder items = ImmutableDictionary<Int32, ImmutableList<Int32>>.Empty.ToBuilder();
                items.Add(101, new Int32[] { 1, 2 }.ToImmutableList());
                items.Add(102, new Int32[] { 3 }.ToImmutableList());

                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO b = Misc.CreateDiTreeDTO()
                with
                {
                    SuccessorMap = items.ToImmutable()
                };
                Assert.True(a.IsEquivalent(b));
            }

            {
                ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder items = ImmutableDictionary<Int32, ImmutableList<Int32>>.Empty.ToBuilder();
                items.Add(101, new Int32[] { 1, 3 }.ToImmutableList());
                items.Add(102, new Int32[] { 3 }.ToImmutableList());

                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO b = Misc.CreateDiTreeDTO()
                with
                {
                    SuccessorMap = items.ToImmutable()
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder items = ImmutableDictionary<Int32, ImmutableList<Int32>>.Empty.ToBuilder();
                items.Add(101, new Int32[] { 1, 2 }.ToImmutableList());

                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO b = Misc.CreateDiTreeDTO()
                with
                {
                    SuccessorMap = items.ToImmutable()
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder items = ImmutableDictionary<Int32, ImmutableList<Int32>>.Empty.ToBuilder();
                items.Add(101, new Int32[] { 1, 2 }.ToImmutableList());
                items.Add(103, new Int32[] { 3 }.ToImmutableList());

                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO b = Misc.CreateDiTreeDTO()
                with
                {
                    SuccessorMap = items.ToImmutable()
                };
                Assert.False(a.IsEquivalent(b));
            }
        }


        [DoNotParallelize]
        [Fact]
        public void DiTreeDTOCompareToTest()
        {
            {
                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO b = Misc.CreateDiTreeDTO();
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                ImmutableDictionary<IConcept, Object>.Builder pb1 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                pb1.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)1);
                pb1.Add(new ConceptDTO(Misc.GID(0x2)), (Int64)2);
                pb1.Add(new ConceptDTO(Misc.GID(0x3)), (Single)3);
                pb1.Add(new ConceptDTO(Misc.GID(0x4)), (Double)4);
                pb1.Add(new ConceptDTO(Misc.GID(0x5)), "abcdef");
                pb1.Add(new ConceptDTO(Misc.GID(0x7)), new DateTime(2000, 1, 1));

                VertexDTO newRoot = new VertexDTO(
                        Misc.g1,
                        123,
                        new ConceptDTO(Misc.PublicIdH),
                        pb1.ToImmutable()
                    );

                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO b = Misc.CreateDiTreeDTO()
                with
                {
                    Root = newRoot
                };
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                ImmutableDictionary<Int32, Int32>.Builder predecessors = ImmutableDictionary<Int32, Int32>.Empty.ToBuilder();
                predecessors.Add(101, 1);
                predecessors.Add(102, 2);

                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO b = Misc.CreateDiTreeDTO()
                with
                {
                    PredecessorMap = predecessors.ToImmutable()
                };
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                ImmutableDictionary<IConcept, Object>.Builder pb1 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                pb1.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)1);

                ImmutableDictionary<IConcept, Object>.Builder pb2 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                pb2.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)2);

                ImmutableDictionary<IConcept, Object>.Builder pb3 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                pb3.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)3);

                ImmutableDictionary<IConcept, Object>.Builder pb4 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                pb4.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)4);

                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO b = Misc.CreateDiTreeDTO()
                with
                {
                    VertexMap = new VertexDTO[]
                    {
                        new VertexDTO(Misc.g1, 101, new ConceptDTO(Misc.PublicIdG),pb1.ToImmutable()),
                        new VertexDTO(Misc.g2, 102, new ConceptDTO(Misc.PublicIdH), pb2.ToImmutable()),
                        new VertexDTO(Misc.g3, 103, new ConceptDTO(Misc.PublicIdI), pb3.ToImmutable()),
                        new VertexDTO(Misc.g4, 104, new ConceptDTO(Misc.j1), pb4.ToImmutable())
                    }.ToImmutableList()
                };
                Assert.True(a.CompareTo(b) != 0);
            }

            {
                ImmutableDictionary<IConcept, Object>.Builder pb1 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                pb1.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)1);

                ImmutableDictionary<IConcept, Object>.Builder pb2 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                pb2.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)2);

                ImmutableDictionary<IConcept, Object>.Builder pb3 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                pb3.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)3);

                ImmutableDictionary<IConcept, Object>.Builder pb4 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                pb4.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)4);

                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO b = Misc.CreateDiTreeDTO()
                with
                {
                    VertexMap = new VertexDTO[]
                    {
                        new VertexDTO(
                            Misc.g1,
                            101,
                            new ConceptDTO(Misc.PublicIdG),
                            pb1.ToImmutable()
                        ),
                        new VertexDTO(
                            Misc.g2,
                            102,
                            new ConceptDTO(Misc.PublicIdH),
                            pb2.ToImmutable()
                        ),
                        new VertexDTO(
                            Misc.g3,
                            103,
                            new ConceptDTO(Misc.PublicIdI),
                            pb3.ToImmutable()
                        ),
                        new VertexDTO(
                            Misc.g4,
                            104,
                            new ConceptDTO(Misc.k1),
                            pb4.ToImmutable()
                        )
                    }.ToImmutableList()
                };
                Assert.True(a.CompareTo(b) != 0);
            }

            {
                ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder items = ImmutableDictionary<Int32, ImmutableList<Int32>>.Empty.ToBuilder();
                items.Add(101, new Int32[] { 1, 2 }.ToImmutableList());
                items.Add(102, new Int32[] { 3 }.ToImmutableList());

                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO b = Misc.CreateDiTreeDTO()
                with
                {
                    SuccessorMap = items.ToImmutable()
                };
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder items = ImmutableDictionary<Int32, ImmutableList<Int32>>.Empty.ToBuilder();
                items.Add(101, new Int32[] { 1, 3 }.ToImmutableList());
                items.Add(102, new Int32[] { 3 }.ToImmutableList());

                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO b = Misc.CreateDiTreeDTO()
                with
                {
                    SuccessorMap = items.ToImmutable()
                };
                Assert.True(a.CompareTo(b) != 0);
            }

            {
                ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder items = ImmutableDictionary<Int32, ImmutableList<Int32>>.Empty.ToBuilder();
                items.Add(101, new Int32[] { 1, 2 }.ToImmutableList());

                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO b = Misc.CreateDiTreeDTO()
                with
                {
                    SuccessorMap = items.ToImmutable()
                };
                Assert.True(a.CompareTo(b) != 0);
            }

            {
                ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder items = ImmutableDictionary<Int32, ImmutableList<Int32>>.Empty.ToBuilder();
                items.Add(101, new Int32[] { 1, 2 }.ToImmutableList());
                items.Add(103, new Int32[] { 3 }.ToImmutableList());

                DiTreeDTO a = Misc.CreateDiTreeDTO();
                DiTreeDTO b = Misc.CreateDiTreeDTO()
                with
                {
                    SuccessorMap = items.ToImmutable()
                };
                Assert.True(a.CompareTo(b) != 0);
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
                Assert.True(dtoStart.CompareTo(dtoRead)== 0);
            }
        }
    }
}
