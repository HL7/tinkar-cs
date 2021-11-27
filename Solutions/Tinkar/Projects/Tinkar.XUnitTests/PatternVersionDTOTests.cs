using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;
using Assert = Xunit.Assert;
using Tinkar.Dto;
using System.Collections.Immutable;

namespace Tinkar.XUnitTests
{
    public class PatternVersionDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void PatternVersionDTOFieldsTest()
        {
            FieldDefinitionDTO fdoa = Misc.CreateFieldDefinition;
            FieldDefinitionDTO fdob = Misc.CreateFieldDefinition with
            {
                DataTypePublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
            }
            ;

            PatternVersionDTO dtoStart = new PatternVersionDTO(
                Misc.PublicIdG,
                Misc.CreateStampDTO,
                Misc.PublicIdH,
                Misc.PublicIdI,
                new FieldDefinitionDTO[] { fdoa, fdob }.ToImmutableArray()
                );
            Misc.Compare(dtoStart.PublicId, Misc.PublicIdG);
            Assert.True(dtoStart.StampDTO.CompareTo(Misc.CreateStampDTO) == 0);
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
        public void PatternVersionDTOIsEquivalentTest()
        {
            {
                PatternVersionDTO a = Misc.CreatePatternVersionDTO;
                PatternVersionDTO b = Misc.CreatePatternVersionDTO;
                Assert.True(a.IsEquivalent(b));
            }

            {
                PatternVersionDTO a = Misc.CreatePatternVersionDTO;
                PatternVersionDTO b = Misc.CreatePatternVersionDTO
                with
                {
                    PublicId = new PublicId(Misc.other)
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                PatternVersionDTO a = Misc.CreatePatternVersionDTO;
                PatternVersionDTO b = Misc.CreatePatternVersionDTO
                with
                {
                    StampDTO = Misc.CreateStampDTO with { StatusPublicId = new PublicId(Misc.g2 ) }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                PatternVersionDTO a = Misc.CreatePatternVersionDTO;
                PatternVersionDTO b = Misc.CreatePatternVersionDTO
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

                PatternVersionDTO a = Misc.CreatePatternVersionDTO;
                PatternVersionDTO b = Misc.CreatePatternVersionDTO
                with
                {
                    FieldDefinitionDTOs = new FieldDefinitionDTO[] { fdoa }.ToImmutableArray()
                };
                Assert.False(a.IsEquivalent(b));
            }
        }



        [DoNotParallelize]
        [Fact]
        public void PatternVersionDTOCompareToTest()
        {
            {
                PatternVersionDTO a = Misc.CreatePatternVersionDTO;
                PatternVersionDTO b = Misc.CreatePatternVersionDTO;
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                PatternVersionDTO a = Misc.CreatePatternVersionDTO;
                PatternVersionDTO b = Misc.CreatePatternVersionDTO
                with
                {
                    PublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
                };
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                PatternVersionDTO a = Misc.CreatePatternVersionDTO;
                PatternVersionDTO b = Misc.CreatePatternVersionDTO
                with
                {
                    StampDTO = Misc.CreateStampDTO with { StatusPublicId = new PublicId(Misc.g2) }
                };
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                PatternVersionDTO a = Misc.CreatePatternVersionDTO;
                PatternVersionDTO b = Misc.CreatePatternVersionDTO
                with
                {
                    ReferencedComponentPurposePublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
                };
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                FieldDefinitionDTO fdoa = Misc.CreateFieldDefinition with
                {
                    DataTypePublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
                };

                PatternVersionDTO a = Misc.CreatePatternVersionDTO;
                PatternVersionDTO b = Misc.CreatePatternVersionDTO
                with
                {
                    FieldDefinitionDTOs = new FieldDefinitionDTO[] { fdoa }.ToImmutableArray()
                };
                Assert.False(a.CompareTo(b) == 0);
            }
        }



        [DoNotParallelize]
        [Fact]
        public void PatternVersionDTOMarshalTest()
        {
            PatternVersionDTO dtoStart = Misc.CreatePatternVersionDTO;

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                dtoStart.Marshal(output);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                PatternVersionDTO dtoRead =
                    PatternVersionDTO.Make(input, dtoStart.PublicId);
                Assert.True(dtoStart.CompareTo(dtoRead) == 0);
            }
        }
#if UNUSED
        [DoNotParallelize]
        [Fact]
        public void PatternVersionDTOJsonMarshal()
        {
            PatternVersionDTO dtoStart = Misc.CreatePatternVersionDTO;
            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
            {
                dtoStart.Marshal(output);
            }

            ms.Dump();
            ms.Position = 0;
            using (TinkarJsonInput input = new TinkarJsonInput(ms))
            {
                PatternVersionDTO dtoEnd = PatternVersionDTO.Make(input.ReadJsonObject(),
                    dtoStart.PublicId);
                Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
            }
        }
#endif
    }
}
