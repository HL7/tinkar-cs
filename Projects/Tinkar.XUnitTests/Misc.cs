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

        public static PatternForSemanticDTO CreatePatternForSemanticDTO =>
            new PatternForSemanticDTO(PublicIdG);

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

        public static PatternForSemanticChronologyDTO CreatePatternForSemanticChronologyDTO =>
            new PatternForSemanticChronologyDTO(
                PublicIdG,
                new PublicId(Misc.h1, Misc.h2, Misc.h3),
                new PatternForSemanticVersionDTO[] { Misc.CreatePatternForSemanticVersionDTO }.ToImmutableArray()
            );


        public static PatternForSemanticVersionDTO CreatePatternForSemanticVersionDTO =>
            new PatternForSemanticVersionDTO(
                PublicIdG,
                Misc.CreateStampDTO,
                PublicIdG,
                PublicIdH,
                new FieldDefinitionDTO[] {
                    Misc.CreateFieldDefinition
                }.ToImmutableArray()
            );

        public static Guid GID(Int32 i) => new Guid(i, 0, 0, zero);

        public static VertexDTO CreateVertexDTO() => CreateVertexDTOBuilder().Create();

        public static VertexDTO.Builder CreateVertexDTOBuilder()
        {
            VertexDTO.Builder bldr = new VertexDTO.Builder().SetVertexIndex(123);
            SetVertexDTO(bldr);
            return bldr;
        }

        public static void SetVertexDTO<TBuilder>(TBuilder bldr)
             where TBuilder : VertexDTO.Builder<TBuilder>
        {
            bldr
                .SetVertexIndex(123)
                .SetMeaning(new ConceptDTO(PublicIdH))
                .SetVertexId(Misc.g1)
                .AppendProperty(new ConceptDTO(GID(0x1)), (Int32)1)
                .AppendProperty(new ConceptDTO(GID(0x2)), (Single)3)
                .AppendProperty(new ConceptDTO(GID(0x3)), "abcdef")
                .AppendProperty(new ConceptDTO(GID(0x4)), true)
                .AppendProperty(new ConceptDTO(GID(0x5)), new DateTime(2000, 1, 1))
                ;
        }



        public static GraphVertexDTO CreateGraphVertexDTO() => CreateGraphVertexDTOBuilder().Create();

        public static GraphVertexDTO.Builder CreateGraphVertexDTOBuilder()
        {
            GraphVertexDTO.Builder bldr = new GraphVertexDTO.Builder();
            SetGraphVertexDTO(bldr);
            return bldr;
        }

        public static void SetGraphVertexDTO<TBuilder>(TBuilder bldr)
             where TBuilder : GraphVertexDTO.Builder<TBuilder>, new()
        {
            TBuilder successor1 = new TBuilder()
                .SetVertexIndex(456)
                .SetMeaning(new ConceptDTO(PublicIdG))
                .SetVertexId(Misc.g2)
                ;

            TBuilder successor2 = new TBuilder()
                .SetVertexIndex(789)
                .SetMeaning(new ConceptDTO(PublicIdJ))
                .SetVertexId(Misc.g3)
                ;

            SetVertexDTO(bldr);
            bldr
                .AppendSuccessors(successor1, successor2)
                ;
        }



        public static DiTreeVertexDTO CreateDiTreeVertexDTO() => CreateDiTreeVertexDTOBuilder().Create();

        public static DiTreeVertexDTO.Builder CreateDiTreeVertexDTOBuilder()
        {
            DiTreeVertexDTO.Builder bldr = new DiTreeVertexDTO.Builder();
            SetDiTreeVertexDTO(bldr);
            return bldr;
        }

        public static void SetDiTreeVertexDTO<TBuilder>(TBuilder bldr)
             where TBuilder : DiTreeVertexDTO.Builder<TBuilder>, new()
        {
            TBuilder predecessor = new TBuilder()
                .SetVertexIndex(135)
                .SetMeaning(new ConceptDTO(PublicIdH))
                .SetVertexId(Misc.h2)
                ;

            SetGraphVertexDTO(bldr);
            bldr
                .SetPredecessor(predecessor)
                ;
        }

        public static DiGraphVertexDTO CreateDiGraphVertexDTO() => CreateDiGraphVertexDTOBuilder().Create();

        public static DiGraphVertexDTO.Builder CreateDiGraphVertexDTOBuilder()
        {
            DiGraphVertexDTO.Builder bldr = new DiGraphVertexDTO.Builder();
            SetDiGraphVertexDTO(bldr);
            return bldr;
        }

        public static void SetDiGraphVertexDTO<TBuilder>(TBuilder bldr)
             where TBuilder : DiGraphVertexDTO.Builder<TBuilder>, new()
        {
            TBuilder predecessor = new TBuilder()
                .SetVertexIndex(135)
                .SetMeaning(new ConceptDTO(PublicIdH))
                .SetVertexId(Misc.h2)
                ;

            SetGraphVertexDTO(bldr);
            bldr
                .AppendPredecessors(predecessor)
                ;
        }



        public static GraphDTO CreateGraphDTO() => CreateGraphDTOBuilder<GraphDTO.Builder, GraphVertexDTO.Builder>(new GraphDTO.Builder()).Create();

        public static TBuilder CreateGraphDTOBuilder<TBuilder, TVertexBuilder>(TBuilder bldr)
            where TBuilder : GraphDTO.Builder<TBuilder, TVertexBuilder>
            where TVertexBuilder : GraphVertexDTO.Builder<TVertexBuilder>, new()
        {
            bldr.AppendVertex(g1, new ConceptDTO(PublicIdG))
                .AppendProperty(new ConceptDTO(Misc.GID(0x1)), (Int32)1)
                ;
            bldr.AppendVertex(g2, new ConceptDTO(PublicIdH))
                .AppendProperty(new ConceptDTO(Misc.GID(0x1)), (Int32)2)
                ;
            bldr.AppendVertex(g3, new ConceptDTO(PublicIdI))
                .AppendProperty(new ConceptDTO(Misc.GID(0x1)), (Int32)3)
                ;
            bldr.AppendVertex(g4, new ConceptDTO(PublicIdJ))
                .AppendProperty(new ConceptDTO(Misc.GID(0x1)), (Int32)4)
                ;

            bldr.Vertex(g1).AppendSuccessors(bldr.Vertex(g2), bldr.Vertex(g3));

            return bldr;
        }



        public static DiTreeDTO CreateDiTreeDTO() => CreateDiTreeDTOBuilder().Create();

        public static DiTreeDTO.Builder CreateDiTreeDTOBuilder()
        {
            DiTreeDTO.Builder bldr = new DiTreeDTO.Builder();
            bldr.SetVertexId(Misc.g1);
            bldr.SetMeaning(new ConceptDTO(PublicIdH));
            bldr.AppendProperty(new ConceptDTO(GID(0x1)), (Int32)1);
            bldr.AppendProperty(new ConceptDTO(GID(0x2)), (Single)3);
            bldr.AppendProperty(new ConceptDTO(GID(0x3)), "abcdef");
            bldr.AppendProperty(new ConceptDTO(GID(0x4)), true);
            bldr.AppendProperty(new ConceptDTO(GID(0x5)), new DateTime(2000, 1, 1));

            DiTreeVertexDTO.Builder vertex1 = bldr.AppendVertex(g1, new ConceptDTO(PublicIdG));
            vertex1.AppendProperty(new ConceptDTO(Misc.GID(0x1)), (Int32)1);

            DiTreeVertexDTO.Builder vertex2 = bldr.AppendVertex(g2, new ConceptDTO(PublicIdH));
            vertex2.AppendProperty(new ConceptDTO(Misc.GID(0x2)), (Int32)2);
            vertex2.SetPredecessor(vertex1);

            var vertex3 = bldr.AppendVertex(g3, new ConceptDTO(PublicIdI));
            vertex3.AppendProperty(new ConceptDTO(Misc.GID(0x2)), (Int32)3);
            vertex3.SetPredecessor(vertex2);

            var vertex4 = bldr.AppendVertex(g4, new ConceptDTO(PublicIdJ));
            vertex4.AppendProperty(new ConceptDTO(Misc.GID(0x2)), (Int32)4);
            vertex4.SetPredecessor(vertex3);

            vertex1.AppendSuccessors(vertex2);
            vertex2.AppendSuccessors(vertex3);
            vertex3.AppendSuccessors(vertex4);

            bldr.SetRoot(vertex1);
            return bldr;
        }

        public static DiGraphDTO CreateDiGraphDTO() => CreateDiGraphDTOBuilder().Create();

        public static DiGraphDTO.Builder CreateDiGraphDTOBuilder()
        {
            DiGraphDTO.Builder bldr = new DiGraphDTO.Builder();
            bldr.SetVertexId(Misc.g1);
            bldr.SetMeaning(new ConceptDTO(PublicIdH));
            bldr.AppendProperty(new ConceptDTO(GID(0x1)), (Int32)1);
            bldr.AppendProperty(new ConceptDTO(GID(0x2)), (Single)3);
            bldr.AppendProperty(new ConceptDTO(GID(0x3)), "abcdef");
            bldr.AppendProperty(new ConceptDTO(GID(0x4)), true);
            bldr.AppendProperty(new ConceptDTO(GID(0x5)), new DateTime(2000, 1, 1));

            DiGraphVertexDTO.Builder vertex1 = bldr.AppendVertex(g1, new ConceptDTO(PublicIdG));
            vertex1.AppendProperty(new ConceptDTO(Misc.GID(0x1)), (Int32)1);

            DiGraphVertexDTO.Builder vertex2 = bldr.AppendVertex(g2, new ConceptDTO(PublicIdH));
            vertex2.AppendProperty(new ConceptDTO(Misc.GID(0x2)), (Int32)2);
            vertex2.AppendPredecessors(vertex1);

            var vertex3 = bldr.AppendVertex(g3, new ConceptDTO(PublicIdI));
            vertex3.AppendProperty(new ConceptDTO(Misc.GID(0x2)), (Int32)3);
            vertex3.AppendPredecessors(vertex2);

            var vertex4 = bldr.AppendVertex(g4, new ConceptDTO(PublicIdJ));
            vertex4.AppendProperty(new ConceptDTO(Misc.GID(0x2)), (Int32)4);
            vertex4.AppendPredecessors(vertex3);

            vertex1.AppendSuccessors(vertex2);
            vertex2.AppendSuccessors(vertex3);
            vertex3.AppendSuccessors(vertex4);

            return bldr;
        }
    }
}
