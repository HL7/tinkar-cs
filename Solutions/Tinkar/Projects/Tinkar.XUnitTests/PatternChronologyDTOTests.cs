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
    public class PatternChronologyDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void TypePatternChronologyDTOFieldsTest()
        {
            PatternChronologyDTO dtoStart = Misc.CreateTypePatternChronologyDTO;
            Misc.Compare(dtoStart.PublicId, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Misc.Compare(dtoStart.ChronologySetPublicId, Misc.h1, Misc.h2, Misc.h3);
            Misc.Compare(dtoStart.Versions,
                new PatternVersionDTO[]
                    { Misc.CreateTypePatternVersionDTO }
                );
        }

        [DoNotParallelize]
        [Fact]
        public void TypePatternChronologyDTOIsEquivalentTest()
        {
            {
                PatternChronologyDTO a = Misc.CreateTypePatternChronologyDTO;
                PatternChronologyDTO b = Misc.CreateTypePatternChronologyDTO;
                Assert.True(a.IsEquivalent(b));
            }

            {
                PatternChronologyDTO a = Misc.CreateTypePatternChronologyDTO;
                PatternChronologyDTO b = Misc.CreateTypePatternChronologyDTO
                with
                {
                    PublicId = new PublicId(Misc.other)
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                PatternChronologyDTO a = Misc.CreateTypePatternChronologyDTO;
                PatternChronologyDTO b = Misc.CreateTypePatternChronologyDTO
                with
                {
                    ChronologySetPublicId = new PublicId(Misc.other)
                }
                ;
                Assert.False(a.IsEquivalent(b));
            }

            {
                PatternChronologyDTO a = Misc.CreateTypePatternChronologyDTO;
                PatternChronologyDTO b = Misc.CreateTypePatternChronologyDTO
                with
                {
                    Versions = new PatternVersionDTO[]
                    {
                        Misc.CreateTypePatternVersionDTO
                        with
                        {
                            PublicId = new PublicId(Misc.other)
                        }
                    }.ToImmutableArray()
                }
                ;
                Assert.False(a.IsEquivalent(b));
            }
        }

        [DoNotParallelize]
        [Fact]
        public void TypePatternChronologyDTOCompareToTest()
        {
            {
                PatternChronologyDTO a = Misc.CreateTypePatternChronologyDTO;
                PatternChronologyDTO b = Misc.CreateTypePatternChronologyDTO;
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                PatternChronologyDTO a = Misc.CreateTypePatternChronologyDTO;
                PatternChronologyDTO b = Misc.CreateTypePatternChronologyDTO
                with
                {
                    PublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3)
                };
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                PatternChronologyDTO a = Misc.CreateTypePatternChronologyDTO;
                PatternChronologyDTO b = Misc.CreateTypePatternChronologyDTO
                with
                {
                    ChronologySetPublicId = new PublicId(Misc.g1, Misc.g2, Misc.g3, Misc.i4)
                }
                ;
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                PatternChronologyDTO a = Misc.CreateTypePatternChronologyDTO;
                PatternChronologyDTO b = Misc.CreateTypePatternChronologyDTO
                with
                {
                    Versions = new PatternVersionDTO[]
                    {
                        Misc.CreateTypePatternVersionDTO
                        with
                        {
                            PublicId = new PublicId(Misc.g3, Misc.g2, Misc.g1, Misc.h4)
                        }
                    }.ToImmutableArray()
                }
                ;
                Assert.False(a.CompareTo(b) == 0);
            }
        }

        [DoNotParallelize]
        [Fact]
        public void TypePatternChronologyDTOMarshalTest()
        {
            PatternChronologyDTO dtoStart = Misc.CreateTypePatternChronologyDTO;

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                output.WriteField(dtoStart);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                PatternChronologyDTO dtoRead = (PatternChronologyDTO)input.GetField();
                Assert.True(dtoStart.CompareTo(dtoRead) == 0);
            }
        }
        [DoNotParallelize]
        [Fact]
        public void TypePatternChronologyDTOJsonMarshal()
        {
            PatternChronologyDTO dtoStart = Misc.CreateTypePatternChronologyDTO;
            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
            {
                dtoStart.Marshal(output);
            }

            ms.Dump();
            ms.Position = 0;
            using (TinkarJsonInput input = new TinkarJsonInput(ms))
            {
                PatternChronologyDTO dtoEnd = PatternChronologyDTO.Make(input.ReadJsonObject());
                Misc.JsonDump(dtoEnd);
                Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
            }
        }
    }
}
