using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;
using Assert = Xunit.Assert;

namespace Tinkar.XUnitTests
{
    public class PatternForSemanticVersionDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void PatternForSemanticVersionDTOFieldsTest()
        {
            FieldDefinitionDTO fdoa = Misc.CreateFieldDefinition;
            FieldDefinitionDTO fdob = Misc.CreateFieldDefinition with
            {
                DataTypePublicId = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
            }
            ;

            PatternForSemanticVersionDTO dtoStart = new PatternForSemanticVersionDTO(
                new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                Misc.CreateStampDTO,
                new Guid[] { Misc.h1, Misc.h2, Misc.h3 },
                new FieldDefinitionDTO[] { fdoa, fdob }
                );
            Misc.Compare(dtoStart.ComponentUuids, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Assert.True(dtoStart.StampDTO.IsEquivalent(Misc.CreateStampDTO));
            Misc.Compare<FieldDefinitionDTO>(dtoStart.FieldDefinitionDTOs,
                new FieldDefinitionDTO[]
                {
                    Misc.CreateFieldDefinition,
                    Misc.CreateFieldDefinition with
                    {
                        DataTypePublicId = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                    }
                }
                );
        }

        [DoNotParallelize]
        [Fact]
        public void PatternForSemanticVersionDTOIsEquivalentTest()
        {
            {
                PatternForSemanticVersionDTO a = Misc.CreatePatternForSemanticVersionDTO;
                PatternForSemanticVersionDTO b = Misc.CreatePatternForSemanticVersionDTO;
                Assert.True(a.IsEquivalent(b));
            }

            {
                PatternForSemanticVersionDTO a = Misc.CreatePatternForSemanticVersionDTO;
                PatternForSemanticVersionDTO b = Misc.CreatePatternForSemanticVersionDTO
                with
                {
                    ComponentUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                PatternForSemanticVersionDTO a = Misc.CreatePatternForSemanticVersionDTO;
                PatternForSemanticVersionDTO b = Misc.CreatePatternForSemanticVersionDTO
                with
                {
                    StampDTO = Misc.CreateStampDTO with { StatusPublicId = new Guid[] { Misc.g2 } }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                PatternForSemanticVersionDTO a = Misc.CreatePatternForSemanticVersionDTO;
                PatternForSemanticVersionDTO b = Misc.CreatePatternForSemanticVersionDTO
                with
                {
                    ReferencedComponentPurposeUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                FieldDefinitionDTO fdoa = Misc.CreateFieldDefinition with
                {
                    DataTypePublicId = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                };

                PatternForSemanticVersionDTO a = Misc.CreatePatternForSemanticVersionDTO;
                PatternForSemanticVersionDTO b = Misc.CreatePatternForSemanticVersionDTO
                with
                {
                    FieldDefinitionDTOs = new FieldDefinitionDTO[] { fdoa }
                };
                Assert.False(a.IsEquivalent(b));
            }
        }

        [DoNotParallelize]
        [Fact]
        public void PatternForSemanticVersionDTOMarshalTest()
        {
            PatternForSemanticVersionDTO dtoStart = Misc.CreatePatternForSemanticVersionDTO;

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                dtoStart.Marshal(output);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                PatternForSemanticVersionDTO dtoRead =
                    PatternForSemanticVersionDTO.Make(input, dtoStart.ComponentUuids);
                Assert.True(dtoStart.IsEquivalent(dtoRead));
            }
        }
        [DoNotParallelize]
        [Fact]
        public void PatternForSemanticVersionDTOJsonMarshal()
        {
            PatternForSemanticVersionDTO dtoStart = Misc.CreatePatternForSemanticVersionDTO;
            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
            {
                dtoStart.Marshal(output);
            }

            ms.Dump();
            ms.Position = 0;
            using (TinkarJsonInput input = new TinkarJsonInput(ms))
            {
                PatternForSemanticVersionDTO dtoEnd = PatternForSemanticVersionDTO.Make(input.ReadJsonObject(),
                    dtoStart.ComponentUuids);
                Assert.True(dtoStart.IsEquivalent(dtoEnd));
            }
        }
    }
}
