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
            TypePatternChronologyDTO dtoStart = Misc.CreateTypePatternChronologyDTO;
            Misc.Compare(dtoStart.PublicId, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Misc.Compare(dtoStart.ChronologySetPublicId, Misc.h1, Misc.h2, Misc.h3);
            Misc.Compare(dtoStart.Versions,
                new TypePatternVersionDTO[]
                    { Misc.CreateTypePatternVersionDTO }
                );
        }

        [DoNotParallelize]
        [Fact]
        public void TypePatternChronologyDTOIsEquivalentTest()
        {
            {
                TypePatternChronologyDTO a = Misc.CreateTypePatternChronologyDTO;
                TypePatternChronologyDTO b = Misc.CreateTypePatternChronologyDTO;
                Assert.True(a.IsEquivalent(b));
            }

            {
                TypePatternChronologyDTO a = Misc.CreateTypePatternChronologyDTO;
                TypePatternChronologyDTO b = Misc.CreateTypePatternChronologyDTO
                with
                {
                    PublicId = new PublicId(Misc.other)
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                TypePatternChronologyDTO a = Misc.CreateTypePatternChronologyDTO;
                TypePatternChronologyDTO b = Misc.CreateTypePatternChronologyDTO
                with
                {
                    ChronologySetPublicId = new PublicId(Misc.other)
                }
                ;
                Assert.False(a.IsEquivalent(b));
            }

            {
                TypePatternChronologyDTO a = Misc.CreateTypePatternChronologyDTO;
                TypePatternChronologyDTO b = Misc.CreateTypePatternChronologyDTO
                with
                {
                    Versions = new TypePatternVersionDTO[]
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
                TypePatternChronologyDTO a = Misc.CreateTypePatternChronologyDTO;
                TypePatternChronologyDTO b = Misc.CreateTypePatternChronologyDTO;
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                TypePatternChronologyDTO a = Misc.CreateTypePatternChronologyDTO;
                TypePatternChronologyDTO b = Misc.CreateTypePatternChronologyDTO
                with
                {
                    PublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3)
                };
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                TypePatternChronologyDTO a = Misc.CreateTypePatternChronologyDTO;
                TypePatternChronologyDTO b = Misc.CreateTypePatternChronologyDTO
                with
                {
                    ChronologySetPublicId = new PublicId(Misc.g1, Misc.g2, Misc.g3, Misc.i4)
                }
                ;
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                TypePatternChronologyDTO a = Misc.CreateTypePatternChronologyDTO;
                TypePatternChronologyDTO b = Misc.CreateTypePatternChronologyDTO
                with
                {
                    Versions = new TypePatternVersionDTO[]
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
            TypePatternChronologyDTO dtoStart = Misc.CreateTypePatternChronologyDTO;

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                output.WriteField(dtoStart);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                TypePatternChronologyDTO dtoRead = (TypePatternChronologyDTO)input.GetField();
                Assert.True(dtoStart.CompareTo(dtoRead) == 0);
            }
        }
        [DoNotParallelize]
        [Fact]
        public void TypePatternChronologyDTOJsonMarshal()
        {
            TypePatternChronologyDTO dtoStart = Misc.CreateTypePatternChronologyDTO;
            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
            {
                dtoStart.Marshal(output);
            }

            ms.Dump();
            ms.Position = 0;
            using (TinkarJsonInput input = new TinkarJsonInput(ms))
            {
                TypePatternChronologyDTO dtoEnd = TypePatternChronologyDTO.Make(input.ReadJsonObject());
                Misc.JsonDump(dtoEnd);
                Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
            }
        }
    }
}
