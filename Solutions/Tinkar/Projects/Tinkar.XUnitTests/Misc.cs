using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinkar.Dto;
using Xunit;

namespace Tinkar.XUnitTests
{
    public static class Misc
    {
        public static void Dump(this MemoryStream ms)
        {
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            String json = sr.ReadToEnd();
            Trace.WriteLine(json);
        }

        public static void JsonDump(IJsonMarshalable m)
        {
            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
            {
                m.Marshal(output);
            }
            ms.Dump();
        }

        public static byte[] zero => new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };

        public static IPublicId PublicIdG => new PublicId(g1, g2, g3, g4);
        public static Guid other => new Guid(0xff, 0xfe, 0xfd, zero);

        public static Guid g1 => new Guid(1, 0, 0, zero);
        public static Guid g2 => new Guid(2, 0, 0, zero);
        public static Guid g3 => new Guid(3, 0, 0, zero);
        public static Guid g4 => new Guid(4, 0, 0, zero);

        public static IPublicId PublicIdH => new PublicId(h1, h2, h3, h4);
        public static Guid h1 => new Guid(0x11, 0, 0, zero);
        public static Guid h2 => new Guid(0x12, 0, 0, zero);
        public static Guid h3 => new Guid(0x13, 0, 0, zero);
        public static Guid h4 => new Guid(0x14, 0, 0, zero);


        public static IPublicId PublicIdI => new PublicId(i1, i2, i3, i4);
        public static Guid i1 => new Guid(0x21, 0, 0, zero);
        public static Guid i2 => new Guid(0x22, 0, 0, zero);
        public static Guid i3 => new Guid(0x23, 0, 0, zero);
        public static Guid i4 => new Guid(0x24, 0, 0, zero);


        public static Guid j1 => new Guid(0x31, 0, 0, zero);
        public static Guid j2 => new Guid(0x32, 0, 0, zero);
        public static Guid j3 => new Guid(0x33, 0, 0, zero);
        public static Guid j4 => new Guid(0x34, 0, 0, zero);
        public static IPublicId PublicIdJ => new PublicId(j1, j2, j3, j4);

        public static Guid k1 => new Guid(0x41, 0, 0, zero);
        public static Guid k2 => new Guid(0x42, 0, 0, zero);
        public static Guid k3 => new Guid(0x43, 0, 0, zero);
        public static Guid k4 => new Guid(0x44, 0, 0, zero);
        public static IPublicId PublicIdK => new PublicId(k1, k2, k3, k4);

        public static ConceptVersionDTO cv1(IPublicId publicId) => new ConceptVersionDTO(publicId, Misc.CreateStampDTO);
        public static ConceptVersionDTO cv2(IPublicId publicId) => new ConceptVersionDTO(publicId, Misc.CreateStampDTO with { StatusPublicId = new PublicId(Misc.g2) });

        public static ConceptVersionDTO[] ConceptVersionsBase(IPublicId publicId) =>
            new ConceptVersionDTO[] { cv1(publicId), cv2(publicId) };

        public static ConceptChronologyDTO CreateConceptChronologyDTO => new ConceptChronologyDTO(
            PublicIdG,
            PublicIdH,
            Misc.ConceptVersionsBase(PublicIdG).ToImmutableArray());

        public static ConceptDTO CreateConceptDTO => new ConceptDTO(PublicIdG);

        public static FieldDefinitionDTO CreateFieldDefinitionDTO =>
            new FieldDefinitionDTO(
                PublicIdG,
                PublicIdH,
                PublicIdI
            );

        public static PatternDTO CreatePatternDTO =>
            new PatternDTO(PublicIdG);

        public static SemanticChronologyDTO CreateSemanticChronologyDTO =>
            new SemanticChronologyDTO(
                PublicIdG,
                PublicIdH,
                PublicIdI,
                new SemanticVersionDTO[] { CreateSemanticVersionDTO }.ToImmutableArray()
            );

        public static SemanticDTO CreateSemanticDTO => new SemanticDTO(
            PublicIdG,
            PublicIdH,
            PublicIdI
        );

        public static SemanticVersionDTO CreateSemanticVersionDTO =>
            new SemanticVersionDTO(
                PublicIdG,
                PublicIdH,
                PublicIdI,
                Misc.CreateStampDTO,
                new Object[]
                {
                    true, false,
                    new byte[] { }, new byte[] {1, 2, 3 },
                    0.3F,
                    -1, 0, 1,
                    "abcdef",
                    new DateTime(2020, 1, 2)
                }.ToImmutableArray()
            );


        public static FieldDefinitionDTO CreateFieldDefinition =>
            new FieldDefinitionDTO(
                PublicIdG,
                PublicIdH,
                PublicIdI
            );

        public static ConceptVersionDTO CreateConceptVersionDTO =>
            new ConceptVersionDTO(
                PublicIdG,
                Misc.CreateStampDTO
            );


        public static StampDTO CreateStampDTO => new StampDTO(
            new PublicId(new Guid(0x80, 0, 0, zero), new Guid(0x81, 0, 0, zero)),
            new PublicId(new Guid(0x80, 1, 0, zero), new Guid(0x81, 1, 0, zero)),
            new DateTime(1990, 3, 4),
            new PublicId(new Guid(0x80, 2, 0, zero), new Guid(0x81, 1, 0, zero)),
            new PublicId(new Guid(0x80, 3, 0, zero)),
            new PublicId(new Guid(0x80, 4, 0, zero))
            );

        public static void Compare(IPublicId inPublicId, IPublicId cmpPublicId) =>
            Compare(inPublicId.AsUuidArray, cmpPublicId.AsUuidArray);

        public static void Compare(IPublicId inPublicId, params Guid[] cmpGuids) =>
            Compare(inPublicId.AsUuidArray, cmpGuids);

        public static void Compare(IEnumerable<Guid> inGuids, params Guid[] cmpGuids) =>
            Compare(inGuids.ToArray(), cmpGuids);

        public static void Compare(Guid[] inGuids, Guid[] cmpGuids)
        {
            Assert.True(inGuids.Length == cmpGuids.Length);
            for (Int32 i = 0; i < inGuids.Length; i++)
                Assert.True(inGuids[i].CompareTo(cmpGuids[i]) == 0);
        }

        public static void Compare(IEnumerable<IComparable> inItems,
            IEnumerable<IComparable> cmpItems)
        {
            IComparable[] inArr = inItems.ToArray();
            IComparable[] cmpArr = cmpItems.ToArray();
            Assert.True(inArr.Length == cmpArr.Length);
            for (Int32 i = 0; i < inArr.Length; i++)
                Assert.True(inArr[i].CompareTo(cmpArr[i]) == 0);
        }

        public static PatternChronologyDTO CreatePatternChronologyDTO =>
            new PatternChronologyDTO(
                PublicIdG,
                new PublicId(Misc.h1, Misc.h2, Misc.h3),
                new PatternVersionDTO[] { Misc.CreatePatternVersionDTO }.ToImmutableArray()
            );


        public static PatternVersionDTO CreatePatternVersionDTO =>
            new PatternVersionDTO(
                PublicIdG,
                Misc.CreateStampDTO,
                PublicIdG,
                PublicIdH,
                new FieldDefinitionDTO[] {
                    Misc.CreateFieldDefinition
                }.ToImmutableArray()
            );

        public static Guid GID(Int32 i) => new Guid(i, 0, 0, zero);

        //$public static VertexDTO CreateVertexDTO() => CreateVertexDTOBuilder().Create();

        //$public static VertexDTO.Builder CreateVertexDTOBuilder()
        //{
        //    VertexDTO.Builder bldr = new VertexDTO.Builder().SetVertexIndex(123);
        //    SetVertexDTO(bldr);
        //    return bldr;
        //}

        //$public static void SetVertexDTO<TBuilder>(TBuilder bldr)
        //     where TBuilder : VertexDTO.Builder<TBuilder>
        //{
        //    bldr
        //        .SetVertexIndex(123)
        //        .SetMeaning(new ConceptDTO(PublicIdH))
        //        .SetVertexId(Misc.g1)
        //        .AppendProperty(new ConceptDTO(GID(0x1)), (Int32)1)
        //        .AppendProperty(new ConceptDTO(GID(0x2)), (Single)3)
        //        .AppendProperty(new ConceptDTO(GID(0x3)), "abcdef")
        //        .AppendProperty(new ConceptDTO(GID(0x4)), true)
        //        .AppendProperty(new ConceptDTO(GID(0x5)), new DateTime(2000, 1, 1))
        //        ;
        //}



        //$public static GraphVertexDTO CreateGraphVertexDTO() => CreateGraphVertexDTOBuilder().Create();

        //$public static GraphVertexDTO.Builder CreateGraphVertexDTOBuilder()
        //{
        //    GraphVertexDTO.Builder bldr = new GraphVertexDTO.Builder();
        //    SetGraphVertexDTO(bldr);
        //    return bldr;
        //}

        //$public static void SetGraphVertexDTO<TBuilder>(TBuilder bldr)
        //     where TBuilder : GraphVertexDTO.Builder<TBuilder>, new()
        //{
        //    TBuilder successor1 = new TBuilder()
        //        .SetVertexIndex(456)
        //        .SetMeaning(new ConceptDTO(PublicIdG))
        //        .SetVertexId(Misc.g2)
        //        ;

        //    TBuilder successor2 = new TBuilder()
        //        .SetVertexIndex(789)
        //        .SetMeaning(new ConceptDTO(PublicIdJ))
        //        .SetVertexId(Misc.g3)
        //        ;

        //    SetVertexDTO(bldr);
        //    bldr
        //        .AppendSuccessors(successor1, successor2)
        //        ;
        //}



        //$public static DiTreeVertexDTO CreateDiTreeVertexDTO() => CreateDiTreeVertexDTOBuilder().Create();

        //$public static DiTreeVertexDTO.Builder CreateDiTreeVertexDTOBuilder()
        //{
        //    DiTreeVertexDTO.Builder bldr = new DiTreeVertexDTO.Builder();
        //    SetDiTreeVertexDTO(bldr);
        //    return bldr;
        //}

        //$public static void SetDiTreeVertexDTO<TBuilder>(TBuilder bldr)
        //     where TBuilder : DiTreeVertexDTO.Builder<TBuilder>, new()
        //{
        //    TBuilder predecessor = new TBuilder()
        //        .SetVertexIndex(135)
        //        .SetMeaning(new ConceptDTO(PublicIdH))
        //        .SetVertexId(Misc.h2)
        //        ;

        //    SetGraphVertexDTO(bldr);
        //    bldr
        //        .SetPredecessor(predecessor)
        //        ;
        //}

        //$public static DiGraphVertexDTO CreateDiGraphVertexDTO() => CreateDiGraphVertexDTOBuilder().Create();

        //$public static DiGraphVertexDTO.Builder CreateDiGraphVertexDTOBuilder()
        //{
        //    DiGraphVertexDTO.Builder bldr = new DiGraphVertexDTO.Builder();
        //    SetDiGraphVertexDTO(bldr);
        //    return bldr;
        //}

        //$public static void SetDiGraphVertexDTO<TBuilder>(TBuilder bldr)
        //     where TBuilder : DiGraphVertexDTO.Builder<TBuilder>, new()
        //{
        //    TBuilder predecessor = new TBuilder()
        //        .SetVertexIndex(135)
        //        .SetMeaning(new ConceptDTO(PublicIdH))
        //        .SetVertexId(Misc.h2)
        //        ;

        //    SetGraphVertexDTO(bldr);
        //    bldr
        //        .AppendPredecessors(predecessor)
        //        ;
        //}



        public static GraphDTO CreateGraphDTO()
        {
            ImmutableList<VertexDTO>.Builder vertexMap = ImmutableList<VertexDTO>.Empty.ToBuilder();

            void AppendVertex(Guid vertexId, IPublicId propertyId, Int32 propertyKey, Int32 propertyValue)
            {
                ImmutableDictionary<IConcept, Object>.Builder properties = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                properties.Add(new ConceptDTO(Misc.GID(propertyKey)), propertyValue);
                VertexDTO vertex = new VertexDTO(vertexId,
                    vertexMap.Count,
                    new ConceptDTO(propertyId),
                    properties.ToImmutable());
                vertexMap.Add(vertex);
            }

            AppendVertex(g1, PublicIdG, 0x1, 0x1);
            AppendVertex(g2, PublicIdH, 0x1, 0x2);
            AppendVertex(g3, PublicIdI, 0x1, 0x3);
            AppendVertex(g4, PublicIdJ, 0x1, 0x4);

            ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder successors = ImmutableDictionary<int, ImmutableList<int>>.Empty.ToBuilder();
            {
                Int32[] successorList = new Int32[] { 1, 2 };
                successors.Add(0, successorList.ToImmutableList());
            }
            {
                Int32[] successorList = new Int32[] { 3 };
                successors.Add(1, successorList.ToImmutableList());
            }
            return new GraphDTO(vertexMap.ToImmutable(), successors.ToImmutable());
        }


        public static DiGraphDTO CreateDiGraphDTO()
        {
            ImmutableList<VertexDTO>.Builder vertexMap = ImmutableList<VertexDTO>.Empty.ToBuilder();

            VertexDTO AppendVertex(Guid vertexId, IPublicId propertyId, Int32 propertyKey, Int32 propertyValue)
            {
                ImmutableDictionary<IConcept, Object>.Builder properties = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                properties.Add(new ConceptDTO(Misc.GID(propertyKey)), propertyValue);
                VertexDTO vertex = new VertexDTO(vertexId,
                    vertexMap.Count,
                    new ConceptDTO(propertyId),
                    properties.ToImmutable());
                vertexMap.Add(vertex);
                return vertex;
            }

            VertexDTO root = AppendVertex(g1, PublicIdG, 0x1, 0x1);
            AppendVertex(g2, PublicIdH, 0x1, 0x2);
            AppendVertex(g3, PublicIdI, 0x1, 0x3);
            AppendVertex(g4, PublicIdJ, 0x1, 0x4);

            ImmutableList<Int32>.Builder roots = ImmutableList<Int32>.Empty.ToBuilder();
            roots.Add(root.VertexIndex);

            ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder successors = ImmutableDictionary<int, ImmutableList<int>>.Empty.ToBuilder();
            {
                Int32[] successorList = new Int32[] { 1, 2 };
                successors.Add(0, successorList.ToImmutableList());
            }
            {
                Int32[] successorList = new Int32[] { 3 };
                successors.Add(1, successorList.ToImmutableList());
                successors.Add(2, successorList.ToImmutableList());
            }

            ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder predecessors = ImmutableDictionary<int, ImmutableList<int>>.Empty.ToBuilder();
            {
                Int32[] predecessorList = new Int32[] { 0 };
                predecessors.Add(1, predecessorList.ToImmutableList());
                predecessors.Add(2, predecessorList.ToImmutableList());
            }
            {
                Int32[] predecessorList = new Int32[] { 1, 2 };
                predecessors.Add(3, predecessorList.ToImmutableList());
            }


            return new DiGraphDTO(
                roots.ToImmutable(),
                predecessors.ToImmutable(),
                vertexMap.ToImmutable(),
                successors.ToImmutable()
                );
        }

        public static DiTreeDTO CreateDiTreeDTO()
        {
            ImmutableList<VertexDTO>.Builder vertexMap = ImmutableList<VertexDTO>.Empty.ToBuilder();

            VertexDTO AppendVertex(Guid vertexId, IPublicId propertyId, Int32 propertyKey, Int32 propertyValue)
            {
                ImmutableDictionary<IConcept, Object>.Builder properties = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                properties.Add(new ConceptDTO(Misc.GID(propertyKey)), propertyValue);
                VertexDTO vertex = new VertexDTO(vertexId,
                    vertexMap.Count,
                    new ConceptDTO(propertyId),
                    properties.ToImmutable());
                vertexMap.Add(vertex);
                return vertex;
            }

            VertexDTO root = AppendVertex(g1, PublicIdG, 0x1, 0x1);
            AppendVertex(g2, PublicIdH, 0x1, 0x2);
            AppendVertex(g3, PublicIdI, 0x1, 0x3);
            AppendVertex(g4, PublicIdJ, 0x1, 0x4);

            ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder successors = ImmutableDictionary<int, ImmutableList<int>>.Empty.ToBuilder();
            {
                Int32[] successorList = new Int32[] { 1, 2 };
                successors.Add(0, successorList.ToImmutableList());
            }
            {
                Int32[] successorList = new Int32[] { 3 };
                successors.Add(1, successorList.ToImmutableList());
            }

            ImmutableDictionary<Int32, Int32>.Builder predecessors = ImmutableDictionary<int, int>.Empty.ToBuilder();
            predecessors.Add(1, 0);
            predecessors.Add(2, 0);
            predecessors.Add(3, 1);


            DiTreeDTO bldr = new DiTreeDTO(
                root,
                predecessors.ToImmutable(),
                vertexMap.ToImmutable(),
                successors.ToImmutable()
                );

            return bldr;
        }

    }
}
