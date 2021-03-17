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

        //[DoNotParallelize]
        //[Fact]
        //public void GraphDTOIsEquivalentTest()
        //{
        //    {
        //        GraphDTO a = Misc.CreateGraphDTO();
        //        GraphDTO b = Misc.CreateGraphDTO();
        //        Assert.True(a.IsEquivalent(b));
        //    }

        //    {
        //        ImmutableDictionary<IConcept, Object>.Builder pBuilder1 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
        //        pBuilder1.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)1);

        //        ImmutableDictionary<IConcept, Object>.Builder pBuilder2 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
        //        pBuilder2.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)2);

        //        ImmutableDictionary<IConcept, Object>.Builder pBuilder3 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
        //        pBuilder3.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)3);

        //        ImmutableDictionary<IConcept, Object>.Builder pBuilder4 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
        //        pBuilder4.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)4);

        //        ImmutableList<VertexDTO>.Builder bldr = ImmutableList<VertexDTO>.Empty.ToBuilder();
        //        bldr.AddRange(
        //                new VertexDTO(
        //                    Misc.g1,
        //                    101,
        //                    new ConceptDTO(Misc.PublicIdG),
        //                    pBuilder1.ToImmutable()
        //                ),
        //                new VertexDTO(
        //                    Misc.g2,
        //                    102,
        //                    new ConceptDTO(Misc.PublicIdH),
        //                    pBuilder2.ToImmutable()
        //                ),
        //                new VertexDTO(
        //                    Misc.g3,
        //                    103,
        //                    new ConceptDTO(Misc.PublicIdI),
        //                    pBuilder3.ToImmutable()
        //                ),
        //                new VertexDTO(
        //                    Misc.g4,
        //                    104,
        //                    new ConceptDTO(Misc.j1),
        //                    pBuilder4.ToImmutable()
        //                )
        //            );

        //        GraphDTO a = Misc.CreateGraphDTO();
        //        GraphDTO b = Misc.CreateGraphDTO()
        //        with
        //        {
        //            VertexMap = new VertexDTO[]
        //            {
        //            }.ToImmutableList()
        //        };
        //        Assert.True(a.IsEquivalent(b));
        //    }

        //    {
        //        ImmutableDictionary<IConcept, Object>.Builder pBuilder1 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
        //        pBuilder1.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)1);

        //        ImmutableDictionary<IConcept, Object>.Builder pBuilder2 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
        //        pBuilder2.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)2);

        //        ImmutableDictionary<IConcept, Object>.Builder pBuilder3 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
        //        pBuilder3.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)3);

        //        ImmutableDictionary<IConcept, Object>.Builder pBuilder4 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
        //        pBuilder4.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)4);

        //        GraphDTO a = Misc.CreateGraphDTO();
        //        GraphDTO b = Misc.CreateGraphDTO()
        //        with
        //        {
        //            VertexMap = new VertexDTO[]
        //            {
        //                new VertexDTO(
        //                    Misc.g1,
        //                    101,
        //                    new ConceptDTO(Misc.PublicIdG),
        //                    pBuilder1.ToImmutable()
        //                ),
        //                new VertexDTO(
        //                    Misc.g2,
        //                    102,
        //                    new ConceptDTO(Misc.PublicIdH),
        //                    pBuilder2.ToImmutable()
        //                ),
        //                new VertexDTO(
        //                    Misc.g3,
        //                    103,
        //                    new ConceptDTO(Misc.PublicIdI),
        //                    pBuilder3.ToImmutable()
        //                ),
        //                new VertexDTO(
        //                    Misc.g4,
        //                    104,
        //                    new ConceptDTO(Misc.k1),
        //                    pBuilder4.ToImmutable()
        //                )
        //            }.ToImmutableList()
        //        };
        //        Assert.False(a.IsEquivalent(b));
        //    }

        //    {
        //        ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder items = ImmutableDictionary<Int32, ImmutableList<Int32>>.Empty.ToBuilder();
        //        items.Add(101, new Int32[] { 1, 2 }.ToImmutableList());
        //        items.Add(102, new Int32[] { 3 }.ToImmutableList());

        //        GraphDTO a = Misc.CreateGraphDTO();
        //        GraphDTO b = Misc.CreateGraphDTO()
        //        with
        //        {
        //            SuccessorMap = items.ToImmutable()
        //        };
        //        Assert.True(a.IsEquivalent(b));
        //    }

        //    {
        //        ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder items = ImmutableDictionary<Int32, ImmutableList<Int32>>.Empty.ToBuilder();
        //        items.Add(101, new Int32[] { 1, 3 }.ToImmutableList());
        //        items.Add(102, new Int32[] { 3 }.ToImmutableList());

        //        GraphDTO a = Misc.CreateGraphDTO();
        //        GraphDTO b = Misc.CreateGraphDTO()
        //        with
        //        {
        //            SuccessorMap = items.ToImmutable()
        //        };
        //        Assert.False(a.IsEquivalent(b));
        //    }

        //    {
        //        ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder items = ImmutableDictionary<Int32, ImmutableList<Int32>>.Empty.ToBuilder();
        //        items.Add(101, new Int32[] { 1, 2 }.ToImmutableList());

        //        GraphDTO a = Misc.CreateGraphDTO();
        //        GraphDTO b = Misc.CreateGraphDTO()
        //        with
        //        {
        //            SuccessorMap = items.ToImmutable()
        //        };
        //        Assert.False(a.IsEquivalent(b));
        //    }

        //    {
        //        ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder items = ImmutableDictionary<Int32, ImmutableList<Int32>>.Empty.ToBuilder();
        //        items.Add(101, new Int32[] { 1, 2 }.ToImmutableList());
        //        items.Add(103, new Int32[] { 3 }.ToImmutableList());

        //        GraphDTO a = Misc.CreateGraphDTO();
        //        GraphDTO b = Misc.CreateGraphDTO()
        //        with
        //        {
        //            SuccessorMap = items.ToImmutable()
        //        };
        //        Assert.False(a.IsEquivalent(b));
        //    }
        //}


        //[DoNotParallelize]
        //[Fact]
        //public void GraphDTOCompareToTest()
        //{
        //    {
        //        GraphDTO a = Misc.CreateGraphDTO();
        //        GraphDTO b = Misc.CreateGraphDTO();
        //        Assert.True(a.CompareTo(b) == 0);
        //    }

        //    {
        //        ImmutableDictionary<IConcept, Object>.Builder pb1 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
        //        pb1.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)1);

        //        ImmutableDictionary<IConcept, Object>.Builder pb2 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
        //        pb2.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)2);

        //        ImmutableDictionary<IConcept, Object>.Builder pb3 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
        //        pb3.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)3);

        //        ImmutableDictionary<IConcept, Object>.Builder pb4 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
        //        pb4.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)4);

        //        GraphDTO a = Misc.CreateGraphDTO();
        //        GraphDTO b = Misc.CreateGraphDTO()
        //        with
        //        {
        //            VertexMap = new VertexDTO[]
        //            {
        //                new VertexDTO(Misc.g1, 101, new ConceptDTO(Misc.PublicIdG),pb1.ToImmutable()),
        //                new VertexDTO(Misc.g2, 102, new ConceptDTO(Misc.PublicIdH), pb2.ToImmutable()),
        //                new VertexDTO(Misc.g3, 103, new ConceptDTO(Misc.PublicIdI), pb3.ToImmutable()),
        //                new VertexDTO(Misc.g4, 104, new ConceptDTO(Misc.j1), pb4.ToImmutable())
        //            }.ToImmutableList()
        //        };
        //        Assert.True(a.CompareTo(b) != 0);
        //    }

        //    {
        //        ImmutableDictionary<IConcept, Object>.Builder pb1 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
        //        pb1.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)1);

        //        ImmutableDictionary<IConcept, Object>.Builder pb2 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
        //        pb2.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)2);

        //        ImmutableDictionary<IConcept, Object>.Builder pb3 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
        //        pb3.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)3);

        //        ImmutableDictionary<IConcept, Object>.Builder pb4 = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
        //        pb4.Add(new ConceptDTO(Misc.GID(0x1)), (Int32)4);

        //        GraphDTO a = Misc.CreateGraphDTO();
        //        GraphDTO b = Misc.CreateGraphDTO()
        //        with
        //        {
        //            VertexMap = new VertexDTO[]
        //            {
        //                new VertexDTO(
        //                    Misc.g1,
        //                    101,
        //                    new ConceptDTO(Misc.PublicIdG),
        //                    pb1.ToImmutable()
        //                ),
        //                new VertexDTO(
        //                    Misc.g2,
        //                    102,
        //                    new ConceptDTO(Misc.PublicIdH),
        //                    pb2.ToImmutable()
        //                ),
        //                new VertexDTO(
        //                    Misc.g3,
        //                    103,
        //                    new ConceptDTO(Misc.PublicIdI),
        //                    pb3.ToImmutable()
        //                ),
        //                new VertexDTO(
        //                    Misc.g4,
        //                    104,
        //                    new ConceptDTO(Misc.k1),
        //                    pb4.ToImmutable()
        //                )
        //            }.ToImmutableList()
        //        };
        //        Assert.True(a.CompareTo(b) != 0);
        //    }

        //    {
        //        ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder items = ImmutableDictionary<Int32, ImmutableList<Int32>>.Empty.ToBuilder();
        //        items.Add(101, new Int32[] { 1, 2 }.ToImmutableList());
        //        items.Add(102, new Int32[] { 3 }.ToImmutableList());

        //        GraphDTO a = Misc.CreateGraphDTO();
        //        GraphDTO b = Misc.CreateGraphDTO()
        //        with
        //        {
        //            SuccessorMap = items.ToImmutable()
        //        };
        //        Assert.True(a.CompareTo(b) == 0);
        //    }

        //    {
        //        ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder items = ImmutableDictionary<Int32, ImmutableList<Int32>>.Empty.ToBuilder();
        //        items.Add(101, new Int32[] { 1, 3 }.ToImmutableList());
        //        items.Add(102, new Int32[] { 3 }.ToImmutableList());

        //        GraphDTO a = Misc.CreateGraphDTO();
        //        GraphDTO b = Misc.CreateGraphDTO()
        //        with
        //        {
        //            SuccessorMap = items.ToImmutable()
        //        };
        //        Assert.True(a.CompareTo(b) != 0);
        //    }

        //    {
        //        ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder items = ImmutableDictionary<Int32, ImmutableList<Int32>>.Empty.ToBuilder();
        //        items.Add(101, new Int32[] { 1, 2 }.ToImmutableList());

        //        GraphDTO a = Misc.CreateGraphDTO();
        //        GraphDTO b = Misc.CreateGraphDTO()
        //        with
        //        {
        //            SuccessorMap = items.ToImmutable()
        //        };
        //        Assert.True(a.CompareTo(b) != 0);
        //    }

        //    {
        //        ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder items = ImmutableDictionary<Int32, ImmutableList<Int32>>.Empty.ToBuilder();
        //        items.Add(101, new Int32[] { 1, 2 }.ToImmutableList());
        //        items.Add(103, new Int32[] { 3 }.ToImmutableList());

        //        GraphDTO a = Misc.CreateGraphDTO();
        //        GraphDTO b = Misc.CreateGraphDTO()
        //        with
        //        {
        //            SuccessorMap = items.ToImmutable()
        //        };
        //        Assert.True(a.CompareTo(b) != 0);
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
        //        dtoStart.MarshalVertexMap(output);
        //    }

        //    ms.Position = 0;
        //    using (TinkarInput input = new TinkarInput(ms))
        //    {
        //        ImmutableList<VertexDTO> dtoRead = GraphDTO.UnmarshalVertexMap(input);
        //        Assert.True(FieldCompare.CompareSequence(dtoRead, dtoStart.VertexMap) == 0);
        //    }
        //}
    }
}
