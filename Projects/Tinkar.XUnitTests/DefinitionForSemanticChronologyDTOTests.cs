using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;

namespace Tinkar.XUnitTests
{
    public class PatternForSemanticChronologyDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void PatternForSemanticChronologyDTOFieldsTest()
        {
            PatternForSemanticChronologyDTO dtoStart = Misc.CreatePatternForSemanticChronologyDTO;
            Misc.Compare(dtoStart.ComponentUuids, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Misc.Compare(dtoStart.ChronologySetUuids, Misc.h1, Misc.h2, Misc.h3);
            Misc.Compare<PatternForSemanticVersionDTO>(dtoStart.DefinitionVersions,
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
                    ComponentUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                PatternForSemanticChronologyDTO a = Misc.CreatePatternForSemanticChronologyDTO;
                PatternForSemanticChronologyDTO b = Misc.CreatePatternForSemanticChronologyDTO
                with
                {
                    ChronologySetUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                }
                ;
                Assert.False(a.IsEquivalent(b));
            }

            {
                PatternForSemanticChronologyDTO a = Misc.CreatePatternForSemanticChronologyDTO;
                PatternForSemanticChronologyDTO b = Misc.CreatePatternForSemanticChronologyDTO
                with
                {
                    DefinitionVersions = new PatternForSemanticVersionDTO[]
                    {
                        Misc.CreatePatternForSemanticVersionDTO
                        with
                        {
                            ComponentUuids = new Guid[] { Misc.g3, Misc.g2, Misc.g1, Misc.g4 }
                        }
                    }
                }
                ;
                Assert.False(a.IsEquivalent(b));
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
                PatternForSemanticChronologyDTO dtoRead = (PatternForSemanticChronologyDTO)input.ReadField();
                Assert.True(dtoStart.IsEquivalent(dtoRead));
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
                Assert.True(dtoStart.IsEquivalent(dtoEnd));
            }
        }
    }
}
