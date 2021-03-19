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
    public class TypePatternDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void TypePatternDTOFieldsTest()
        {
            TypePatternDTO dtoStart = new TypePatternDTO(new PublicId(Misc.g1, Misc.g2, Misc.g3, Misc.g4 ));
            Misc.Compare(dtoStart.PublicId, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
        }

        [DoNotParallelize]
        [Fact]
        public void TypePatternDTOIsEquivalentTest()
        {
            {
                TypePatternDTO a = Misc.CreateTypePatternDTO;
                TypePatternDTO b = Misc.CreateTypePatternDTO;
                Assert.True(a.IsEquivalent(b));
            }

            {
                TypePatternDTO a = Misc.CreateTypePatternDTO;
                TypePatternDTO b = new TypePatternDTO(new PublicId(Misc.other));
                Assert.False(a.IsEquivalent(b));
            }
        }

        [DoNotParallelize]
        [Fact]
        public void TypePatternDTOCompareToTest()
        {
            {
                TypePatternDTO a = Misc.CreateTypePatternDTO;
                TypePatternDTO b = Misc.CreateTypePatternDTO;
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                TypePatternDTO a = Misc.CreateTypePatternDTO;
                TypePatternDTO b = new TypePatternDTO(new PublicId(Misc.h2, Misc.g1, Misc.g3, Misc.g4));
                Assert.False(a.CompareTo(b) == 0);
            }
        }

        [DoNotParallelize]
        [Fact]
        public void TypePatternDTOMarshalTest()
        {
            TypePatternDTO dtoStart = Misc.CreateTypePatternDTO;

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                output.WriteField(dtoStart);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                TypePatternDTO dtoRead = (TypePatternDTO)input.GetField();
                Assert.True(dtoStart.CompareTo(dtoRead) == 0);
            }
        }
        [DoNotParallelize]
        [Fact]
        public void TypePatternDTOJsonMarshal()
        {
            TypePatternDTO dtoStart = Misc.CreateTypePatternDTO;
            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
            {
                dtoStart.Marshal(output);
            }

            ms.Dump();
            ms.Position = 0;
            using (TinkarJsonInput input = new TinkarJsonInput(ms))
            {
                TypePatternDTO dtoEnd = TypePatternDTO.Make(input.ReadJsonObject());
                Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
            }
        }
    }
}
