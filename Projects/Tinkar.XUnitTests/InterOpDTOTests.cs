using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tinkar.Dto;
using Xunit;
using Assert = Xunit.Assert;

namespace Tinkar.XUnitTests
{
    public class InterOpDTOTests
    {
        String TestFile(String fileName) => Path.Combine(@"c:\Temp", fileName);

        Int64 guidNumber = -1;

        Guid NextGuid()
        {
            GuidUnion g = new GuidUnion(guidNumber, ~guidNumber);
            guidNumber += 1;
            return g.Uuid;
        }

        IPublicId NextPublicId(Int32 guidCount)
        {
            List<Guid> guids = new List<Guid>();
            for (Int32 i = 0; i < guidCount; i++)
                guids.Add(NextGuid());
            return new PublicId(guids.ToArray());
        }

        public StampDTO NextStamp()
        {
            return new StampDTO(
                NextPublicId(2),
                NextPublicId(2),
                new DateTime(1990, 3, 4),
                NextPublicId(2),
                NextPublicId(1),
                NextPublicId(1));
        }

        /// <summary>
        /// Create serialized output test cases.
        /// These can be read into java side to compare java and c# implementations.
        /// </summary>
        public List<ComponentDTO> CreateComponents()
        {
            List<ComponentDTO> retVal = new List<ComponentDTO>();
            {
                IPublicId pid = NextPublicId(1);
                // Write ConceptChronologyDTO.
                ConceptChronologyDTO dto = new ConceptChronologyDTO(
                    pid,
                    NextPublicId(2),
                    new ConceptVersionDTO[]
                    {
                        new ConceptVersionDTO(pid, NextStamp()) ,
                        new ConceptVersionDTO(pid, NextStamp())
                    }
                );
                retVal.Add(dto);
            }
            {
                ConceptDTO dto = new ConceptDTO(NextPublicId(4));
                retVal.Add(dto);
            }
            {
                IPublicId pid = NextPublicId(1);
                PatternForSemanticChronologyDTO dto = new PatternForSemanticChronologyDTO(
                    pid,
                    NextPublicId(2),
                    new PatternForSemanticVersionDTO[]
                    {
                        new PatternForSemanticVersionDTO(
                            pid,
                            NextStamp(),
                            NextPublicId(4),
                            NextPublicId(5),
                            new FieldDefinitionDTO[] {
                                new FieldDefinitionDTO(
                                    NextPublicId(4),
                                    NextPublicId(3),
                                    NextPublicId(2)
                                ),
                                new FieldDefinitionDTO(
                                    NextPublicId(1),
                                    NextPublicId(2),
                                    NextPublicId(3)
                                )
                            }
                        )
                    }
                );

                retVal.Add(dto);
            }
            {
                PatternForSemanticDTO dto = new PatternForSemanticDTO(NextPublicId(2));
                retVal.Add(dto);
            }
            {
                IPublicId componentPublicId = NextPublicId(4);
                IPublicId patternForSemanticPublicId = NextPublicId(3);
                IPublicId referencedComponentPublicId = NextPublicId(2);

                SemanticChronologyDTO dto = new SemanticChronologyDTO(
                    componentPublicId,
                    patternForSemanticPublicId,
                    referencedComponentPublicId,
                    new SemanticVersionDTO[]
                    {
                        new SemanticVersionDTO(
                            componentPublicId,
                            patternForSemanticPublicId,
                            referencedComponentPublicId,
                            NextStamp(),
                            new Object[]
                            {
                                true, false,
                                new byte[] { }, new byte[] {1, 2, 3 },
                                0.3F,
                                -1, 0, 1,
                                "abcdef",
                                new DateTime(2020, 1, 2),
                                new SpatialPointDTO(1, 2, 3),
                                new PlanarPointDTO(-1, -2)
                            }
                        )
                    }
                );

                retVal.Add(dto);
            }
            {
                SemanticDTO dto = new SemanticDTO(
                    NextPublicId(4),
                    NextPublicId(5),
                    NextPublicId(4));
                retVal.Add(dto);
            }
            {
                SemanticVersionDTO dto =
                    new SemanticVersionDTO(
                        NextPublicId(3),
                        NextPublicId(2),
                        NextPublicId(1),
                        NextStamp(),
                        new Object[]
                        {
                            true, false,
                            new byte[] { }, new byte[] {1, 2, 3 },
                            0.3F,
                            -1, 0, 1,
                            "abcdef",
                            new DateTime(2020, 1, 2),
                            new SpatialPointDTO(1, 2, 3),
                            new PlanarPointDTO(-1, -2)
                        }
                    );
            }
            return retVal;
        }

        [DoNotParallelize]
        [Fact]
        public void A_BinaryWriteObjects()
        {
            guidNumber = 0xfedd;
            String outputPath = TestFile("TinkarExport.tink");

            using FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            using TinkarOutput output = new TinkarOutput(fs);
            foreach (Object dto in this.CreateComponents())
                output.WriteField(dto);
        }

        [DoNotParallelize]
        [Fact]
        public void B_BinaryReadObjects()
        {
            guidNumber = 0xfedd;
            String inputPath = TestFile("TinkarExport.tink");

            using FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            using TinkarInput input = new TinkarInput(fs);
            ComponentDTO[] readFields = input.GetComponents().ToArray();
            ComponentDTO[] compareFields = this.CreateComponents().ToArray();
            Assert.True(readFields.Length == compareFields.Length);
            for (Int32 i = 0; i < readFields.Length; i++)
                Assert.True(readFields[i].CompareTo(compareFields[i]) == 0);
        }

        [DoNotParallelize]
        [Fact]
        public void A_JsonWriteObjects()
        {
            guidNumber = 0xfedd;
            String outputPath = TestFile("TinkarExport.json");

            List<ComponentDTO> items = new List<ComponentDTO>();
            foreach (ComponentDTO dto in this.CreateComponents())
                items.Add(dto);

            using FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            using TinkarJsonOutput output = new TinkarJsonOutput(fs, true);
            output.WriteStartObject();
            output.Put("root", items);
            output.WriteEndObject();
        }


        [DoNotParallelize]
        [Fact]
        public void B_JsonReadObjects()
        {
            guidNumber = 0xfedd;
            String inputPath = TestFile("TinkarExport.json");

            using FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            using TinkarJsonInput input = new TinkarJsonInput(fs);
            ComponentDTO[] readFields = input.GetComponents().ToArray();
            ComponentDTO[] compareFields = this.CreateComponents().ToArray();
            Assert.True(readFields.Length == compareFields.Length);
            for (Int32 i = 0; i < readFields.Length; i++)
                Assert.True(readFields[i].CompareTo(compareFields[i]) == 0);
        }
    }
}
