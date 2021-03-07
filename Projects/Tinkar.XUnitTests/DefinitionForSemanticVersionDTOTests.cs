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
                DataTypePublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
            }
            ;

            PatternForSemanticVersionDTO dtoStart = new PatternForSemanticVersionDTO(
                Misc.PublicIdG,
                Misc.CreateStampDTO,
                Misc.PublicIdH,
                Misc.PublicIdI,
                new FieldDefinitionDTO[] { fdoa, fdob }
                );
            Misc.Compare(dtoStart.PublicId, Misc.PublicIdG);
            Assert.True(dtoStart.StampDTO.IsSame(Misc.CreateStampDTO) == 0);
            Misc.Compare(dtoStart.FieldDefinitions,
                new FieldDefinitionDTO[]
                {
                    Misc.CreateFieldDefinition,
                    Misc.CreateFieldDefinition with
                    {
                        DataTypePublicId = new PublicId( Misc.g2, Misc.g2, Misc.g3, Misc.g4)
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
                    PublicId = new PublicId(Misc.other)
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                PatternForSemanticVersionDTO a = Misc.CreatePatternForSemanticVersionDTO;
                PatternForSemanticVersionDTO b = Misc.CreatePatternForSemanticVersionDTO
                with
                {
                    StampDTO = Misc.CreateStampDTO with { StatusPublicId = new PublicId(Misc.g2 ) }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                PatternForSemanticVersionDTO a = Misc.CreatePatternForSemanticVersionDTO;
                PatternForSemanticVersionDTO b = Misc.CreatePatternForSemanticVersionDTO
                with
                {
                    ReferencedComponentPurposePublicId = new PublicId(Misc.other)
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                FieldDefinitionDTO fdoa = Misc.CreateFieldDefinition with
                {
                    DataTypePublicId = new PublicId(Misc.other)
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
        public void PatternForSemanticVersionDTOIsSameTest()
        {
            {
                PatternForSemanticVersionDTO a = Misc.CreatePatternForSemanticVersionDTO;
                PatternForSemanticVersionDTO b = Misc.CreatePatternForSemanticVersionDTO;
                Assert.True(a.IsSame(b) == 0);
            }

            {
                PatternForSemanticVersionDTO a = Misc.CreatePatternForSemanticVersionDTO;
                PatternForSemanticVersionDTO b = Misc.CreatePatternForSemanticVersionDTO
                with
                {
                    PublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
                };
                Assert.False(a.IsSame(b) == 0);
            }

            {
                PatternForSemanticVersionDTO a = Misc.CreatePatternForSemanticVersionDTO;
                PatternForSemanticVersionDTO b = Misc.CreatePatternForSemanticVersionDTO
                with
                {
                    StampDTO = Misc.CreateStampDTO with { StatusPublicId = new PublicId(Misc.g2) }
                };
                Assert.False(a.IsSame(b) == 0);
            }

            {
                PatternForSemanticVersionDTO a = Misc.CreatePatternForSemanticVersionDTO;
                PatternForSemanticVersionDTO b = Misc.CreatePatternForSemanticVersionDTO
                with
                {
                    ReferencedComponentPurposePublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
                };
                Assert.False(a.IsSame(b) == 0);
            }

            {
                FieldDefinitionDTO fdoa = Misc.CreateFieldDefinition with
                {
                    DataTypePublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
                };

                PatternForSemanticVersionDTO a = Misc.CreatePatternForSemanticVersionDTO;
                PatternForSemanticVersionDTO b = Misc.CreatePatternForSemanticVersionDTO
                with
                {
                    FieldDefinitionDTOs = new FieldDefinitionDTO[] { fdoa }
                };
                Assert.False(a.IsSame(b) == 0);
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
                    PatternForSemanticVersionDTO.Make(input, dtoStart.PublicId);
                Assert.True(dtoStart.IsSame(dtoRead) == 0);
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
                    dtoStart.PublicId);
                Assert.True(dtoStart.IsSame(dtoEnd) == 0);
            }
        }
    }
}
