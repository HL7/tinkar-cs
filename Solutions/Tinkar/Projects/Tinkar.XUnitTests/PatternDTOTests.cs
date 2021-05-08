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
    public class PatternDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void TypePatternDTOFieldsTest()
        {
            PatternDTO dtoStart = new PatternDTO(new PublicId(Misc.g1, Misc.g2, Misc.g3, Misc.g4 ));
            Misc.Compare(dtoStart.PublicId, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
        }

        [DoNotParallelize]
        [Fact]
        public void TypePatternDTOIsEquivalentTest()
        {
            {
                PatternDTO a = Misc.CreateTypePatternDTO;
                PatternDTO b = Misc.CreateTypePatternDTO;
                Assert.True(a.IsEquivalent(b));
            }

            {
                PatternDTO a = Misc.CreateTypePatternDTO;
                PatternDTO b = new PatternDTO(new PublicId(Misc.other));
                Assert.False(a.IsEquivalent(b));
            }
        }

        [DoNotParallelize]
        [Fact]
        public void TypePatternDTOCompareToTest()
        {
            {
                PatternDTO a = Misc.CreateTypePatternDTO;
                PatternDTO b = Misc.CreateTypePatternDTO;
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                PatternDTO a = Misc.CreateTypePatternDTO;
                PatternDTO b = new PatternDTO(new PublicId(Misc.h2, Misc.g1, Misc.g3, Misc.g4));
                Assert.False(a.CompareTo(b) == 0);
            }
        }

        [DoNotParallelize]
        [Fact]
        public void TypePatternDTOMarshalTest()
        {
            PatternDTO dtoStart = Misc.CreateTypePatternDTO;

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                output.WriteField(dtoStart);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                PatternDTO dtoRead = (PatternDTO)input.GetField();
                Assert.True(dtoStart.CompareTo(dtoRead) == 0);
            }
        }
        [DoNotParallelize]
        [Fact]
        public void TypePatternDTOJsonMarshal()
        {
            PatternDTO dtoStart = Misc.CreateTypePatternDTO;
            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
            {
                dtoStart.Marshal(output);
            }

            ms.Dump();
            ms.Position = 0;
            using (TinkarJsonInput input = new TinkarJsonInput(ms))
            {
                PatternDTO dtoEnd = PatternDTO.Make(input.ReadJsonObject());
                Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
            }
        }
    }
}
