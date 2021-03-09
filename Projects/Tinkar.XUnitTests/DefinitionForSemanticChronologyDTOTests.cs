using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;
using Tinkar.Dto;

namespace Tinkar.XUnitTests
{
    public class PatternForSemanticChronologyDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void PatternForSemanticChronologyDTOFieldsTest()
        {
            PatternForSemanticChronologyDTO dtoStart = Misc.CreatePatternForSemanticChronologyDTO;
            Misc.Compare(dtoStart.PublicId, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Misc.Compare(dtoStart.ChronologySetPublicId, Misc.h1, Misc.h2, Misc.h3);
            Misc.Compare(dtoStart.Versions,
                new PatternForSemanticVersionDTO[]
                    { Misc.CreatePatternForSemanticVersionDTO }
                );
        }

        [DoNotParallelize]
        [Fact]
        public void PatternForSemanticChronologyDTOIsEquivalentTest()
        {
            {
                PatternForSemanticChronologyDTO a = Misc.CreatePatternForSemanticChronologyDTO;
                PatternForSemanticChronologyDTO b = Misc.CreatePatternForSemanticChronologyDTO;
                Assert.True(a.IsEquivalent(b));
            }

            {
                PatternForSemanticChronologyDTO a = Misc.CreatePatternForSemanticChronologyDTO;
                PatternForSemanticChronologyDTO b = Misc.CreatePatternForSemanticChronologyDTO
                with
                {
                    PublicId = new PublicId(Misc.other)
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                PatternForSemanticChronologyDTO a = Misc.CreatePatternForSemanticChronologyDTO;
                PatternForSemanticChronologyDTO b = Misc.CreatePatternForSemanticChronologyDTO
                with
                {
                    ChronologySetPublicId = new PublicId(Misc.other)
                }
                ;
                Assert.False(a.IsEquivalent(b));
            }

            {
                PatternForSemanticChronologyDTO a = Misc.CreatePatternForSemanticChronologyDTO;
                PatternForSemanticChronologyDTO b = Misc.CreatePatternForSemanticChronologyDTO
                with
                {
                    Versions = new PatternForSemanticVersionDTO[]
                    {
                        Misc.CreatePatternForSemanticVersionDTO
                        with
                        {
                            PublicId = new PublicId(Misc.other)
                        }
                    }
                }
                ;
                Assert.False(a.IsEquivalent(b));
            }
        }

        [DoNotParallelize]
        [Fact]
        public void PatternForSemanticChronologyDTOIsSameTest()
        {
            {
                PatternForSemanticChronologyDTO a = Misc.CreatePatternForSemanticChronologyDTO;
                PatternForSemanticChronologyDTO b = Misc.CreatePatternForSemanticChronologyDTO;
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                PatternForSemanticChronologyDTO a = Misc.CreatePatternForSemanticChronologyDTO;
                PatternForSemanticChronologyDTO b = Misc.CreatePatternForSemanticChronologyDTO
                with
                {
                    PublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3)
                };
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                PatternForSemanticChronologyDTO a = Misc.CreatePatternForSemanticChronologyDTO;
                PatternForSemanticChronologyDTO b = Misc.CreatePatternForSemanticChronologyDTO
                with
                {
                    ChronologySetPublicId = new PublicId(Misc.g1, Misc.g2, Misc.g3, Misc.i4)
                }
                ;
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                PatternForSemanticChronologyDTO a = Misc.CreatePatternForSemanticChronologyDTO;
                PatternForSemanticChronologyDTO b = Misc.CreatePatternForSemanticChronologyDTO
                with
                {
                    Versions = new PatternForSemanticVersionDTO[]
                    {
                        Misc.CreatePatternForSemanticVersionDTO
                        with
                        {
                            PublicId = new PublicId(Misc.g3, Misc.g2, Misc.g1, Misc.h4)
                        }
                    }
                }
                ;
                Assert.False(a.CompareTo(b) == 0);
            }
        }

        [DoNotParallelize]
        [Fact]
        public void PatternForSemanticChronologyDTOMarshalTest()
        {
            PatternForSemanticChronologyDTO dtoStart = Misc.CreatePatternForSemanticChronologyDTO;

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                output.WriteField(dtoStart);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                PatternForSemanticChronologyDTO dtoRead = (PatternForSemanticChronologyDTO)input.GetField();
                Assert.True(dtoStart.CompareTo(dtoRead) == 0);
            }
        }
        [DoNotParallelize]
        [Fact]
        public void PatternForSemanticChronologyDTOJsonMarshal()
        {
            PatternForSemanticChronologyDTO dtoStart = Misc.CreatePatternForSemanticChronologyDTO;
            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
            {
                dtoStart.Marshal(output);
            }

            ms.Dump();
            ms.Position = 0;
            using (TinkarJsonInput input = new TinkarJsonInput(ms))
            {
                PatternForSemanticChronologyDTO dtoEnd = PatternForSemanticChronologyDTO.Make(input.ReadJsonObject());
                Misc.JsonDump(dtoEnd);
                Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
            }
        }
    }
}
