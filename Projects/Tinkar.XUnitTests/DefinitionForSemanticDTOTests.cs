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
    public class PatternForSemanticDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void PatternForSemanticDTOFieldsTest()
        {
            PatternForSemanticDTO dtoStart = new PatternForSemanticDTO(new PublicId(Misc.g1, Misc.g2, Misc.g3, Misc.g4 ));
            Misc.Compare(dtoStart.PublicId, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
        }

        [DoNotParallelize]
        [Fact]
        public void PatternForSemanticDTOIsEquivalentTest()
        {
            {
                PatternForSemanticDTO a = Misc.CreatePatternForSemanticDTO;
                PatternForSemanticDTO b = Misc.CreatePatternForSemanticDTO;
                Assert.True(a.IsEquivalent(b));
            }

            {
                PatternForSemanticDTO a = Misc.CreatePatternForSemanticDTO;
                PatternForSemanticDTO b = new PatternForSemanticDTO(new PublicId(Misc.other));
                Assert.False(a.IsEquivalent(b));
            }
        }

        [DoNotParallelize]
        [Fact]
        public void PatternForSemanticDTOCompareToTest()
        {
            {
                PatternForSemanticDTO a = Misc.CreatePatternForSemanticDTO;
                PatternForSemanticDTO b = Misc.CreatePatternForSemanticDTO;
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                PatternForSemanticDTO a = Misc.CreatePatternForSemanticDTO;
                PatternForSemanticDTO b = new PatternForSemanticDTO(new PublicId(Misc.h2, Misc.g1, Misc.g3, Misc.g4));
                Assert.False(a.CompareTo(b) == 0);
            }
        }

        [DoNotParallelize]
        [Fact]
        public void PatternForSemanticDTOMarshalTest()
        {
            PatternForSemanticDTO dtoStart = Misc.CreatePatternForSemanticDTO;

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                output.WriteField(dtoStart);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                PatternForSemanticDTO dtoRead = (PatternForSemanticDTO)input.GetField();
                Assert.True(dtoStart.CompareTo(dtoRead) == 0);
            }
        }
        [DoNotParallelize]
        [Fact]
        public void PatternForSemanticDTOJsonMarshal()
        {
            PatternForSemanticDTO dtoStart = Misc.CreatePatternForSemanticDTO;
            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
            {
                dtoStart.Marshal(output);
            }

            ms.Dump();
            ms.Position = 0;
            using (TinkarJsonInput input = new TinkarJsonInput(ms))
            {
                PatternForSemanticDTO dtoEnd = PatternForSemanticDTO.Make(input.ReadJsonObject());
                Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
            }
        }
    }
}
