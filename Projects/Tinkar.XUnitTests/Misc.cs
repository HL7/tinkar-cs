using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static byte[] zero => new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };

        public static Guid g1 => new Guid(1, 0, 0, zero);
        public static Guid g2 => new Guid(2, 0, 0, zero);
        public static Guid g3 => new Guid(3, 0, 0, zero);
        public static Guid g4 => new Guid(4, 0, 0, zero);

        public static Guid h1 => new Guid(0x11, 0, 0, zero);
        public static Guid h2 => new Guid(0x12, 0, 0, zero);
        public static Guid h3 => new Guid(0x13, 0, 0, zero);
        public static Guid h4 => new Guid(0x14, 0, 0, zero);


        public static Guid i1 => new Guid(0x21, 0, 0, zero);
        public static Guid i2 => new Guid(0x22, 0, 0, zero);
        public static Guid i3 => new Guid(0x23, 0, 0, zero);
        public static Guid i4 => new Guid(0x24, 0, 0, zero);


        public static Guid j1 => new Guid(0x31, 0, 0, zero);
        public static Guid j2 => new Guid(0x32, 0, 0, zero);
        public static Guid j3 => new Guid(0x33, 0, 0, zero);
        public static Guid j4 => new Guid(0x34, 0, 0, zero);

        public static ConceptVersionDTO cv1(IEnumerable<Guid> componentGuids) =>
            new ConceptVersionDTO(
                componentGuids,
                Misc.CreateStampDTO
            );

        public static ConceptVersionDTO cv2(IEnumerable<Guid> componentGuids) =>
            new ConceptVersionDTO(
                componentGuids,
                Misc.CreateStampDTO with { StatusUuids = new Guid[] { Misc.g2 } }
        );

        public static ConceptVersionDTO[] ConceptVersionsBase(IEnumerable<Guid> componentGuids) =>
            new ConceptVersionDTO[] { cv1(componentGuids), cv2(componentGuids) };

        public static ConceptChronologyDTO CreateConceptChronologyDTO => new ConceptChronologyDTO(
            new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
            new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
            Misc.ConceptVersionsBase(new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 }));

        public static ConceptDTO CreateConceptDTO => new ConceptDTO(new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 });

        public static FieldDefinitionDTO CreateFieldDefinitionDTO =>
            new FieldDefinitionDTO(
                new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 }
            );

        public static PatternForSemanticDTO CreatePatternForSemanticDTO =>
            new PatternForSemanticDTO(new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 });

        public static SemanticChronologyDTO CreateSemanticChronologyDTO =>
            new SemanticChronologyDTO(
                new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 },
                new SemanticVersionDTO[] { CreateSemanticVersionDTO }
            );

        public static SemanticDTO CreateSemanticDTO => new SemanticDTO(
            new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
            new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
            new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 }
        );

        public static SemanticVersionDTO CreateSemanticVersionDTO =>
            new SemanticVersionDTO(
                new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 },
                Misc.CreateStampDTO,
                new Object[]
                {
                    true, false,
                    new byte[] { }, new byte[] {1, 2, 3 },
                    0.3F,
                    -1, 0, 1,
                    "abcdef",
                    new DateTime(2020, 1, 2)
                }
            );


        public static FieldDefinitionDTO CreateFieldDefinition =>
            new FieldDefinitionDTO(
                new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 }
            );

        public static ConceptVersionDTO CreateConceptVersionDTO =>
            new ConceptVersionDTO(
                new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                Misc.CreateStampDTO
            );

        public static StampCommentDTO CreateStampCommentDTO =>
            new StampCommentDTO(Misc.CreateStampDTO, "xxyyz");

        public static StampDTO CreateStampDTO => new StampDTO(
            new Guid[]
            {
                new Guid(0x80, 0, 0, zero),
                new Guid(0x81, 0, 0, zero)
            },
            new DateTime(1990, 3, 4),
            new Guid[]
            {
                new Guid(0x80, 1, 0, zero),
                new Guid(0x81, 1, 0, zero)
            },
            new Guid[] { new Guid(0x80, 2, 0, zero) },
            new Guid[] { new Guid(0x80, 3, 0, zero) }
            );

        public static void Compare(IEnumerable<Guid> inGuids, params Guid[] cmpGuids)
        {
            Guid[] guidArr = inGuids.ToArray();
            Assert.True(guidArr.Length == cmpGuids.Length);
            for (Int32 i = 0; i < guidArr.Length; i++)
                Assert.True(guidArr[i].CompareTo(cmpGuids[i]) == 0);
        }

        public static void Compare<T>(IEnumerable<IEquivalent<T>> inItems,
            IEnumerable<IEquivalent<T>> cmpItems)
        {
            IEquivalent<T>[] inArr = inItems.ToArray();
            IEquivalent<T>[] cmpArr = cmpItems.ToArray();
            Assert.True(inArr.Length == cmpArr.Length);
            for (Int32 i = 0; i < inArr.Length; i++)
                Assert.True(inArr[i].IsEquivalent((T)cmpArr[i]));
        }

        public static PatternForSemanticChronologyDTO CreatePatternForSemanticChronologyDTO =>
            new PatternForSemanticChronologyDTO(
                new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                new Guid[] { Misc.h1, Misc.h2, Misc.h3 },
                new PatternForSemanticVersionDTO[] { Misc.CreatePatternForSemanticVersionDTO }
            );


        public static PatternForSemanticVersionDTO CreatePatternForSemanticVersionDTO =>
            new PatternForSemanticVersionDTO(
                new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                Misc.CreateStampDTO,
                new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },

                new FieldDefinitionDTO[] {
                    Misc.CreateFieldDefinition
                }
            );
    }
}
