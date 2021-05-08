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
    public class SemanticDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void SemanticDTOFieldsTest()
        {
            SemanticDTO dtoStart = Misc.CreateSemanticDTO;
            Misc.Compare(dtoStart.PublicId, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Misc.Compare(dtoStart.Pattern.PublicId, Misc.h1, Misc.h2, Misc.h3, Misc.h4);
            Misc.Compare(dtoStart.ReferencedComponent.PublicId, Misc.i1, Misc.i2, Misc.i3, Misc.i4);
        }

        [DoNotParallelize]
        [Fact]
        public void SemanticDTOIsEquivalentTest()
        {
            {
                SemanticDTO a = Misc.CreateSemanticDTO;
                SemanticDTO b = new SemanticDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    Misc.PublicIdI
                );
                Assert.True(a.IsEquivalent(b));
            }

            {
                SemanticDTO a = Misc.CreateSemanticDTO;
                SemanticDTO b = new SemanticDTO(
                    new PublicId(Misc.other),
                    Misc.PublicIdH,
                    Misc.PublicIdI
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                SemanticDTO a = Misc.CreateSemanticDTO;
                SemanticDTO b = new SemanticDTO(
                    Misc.PublicIdG,
                    new PublicId(Misc.other),
                    Misc.PublicIdI
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                SemanticDTO a = Misc.CreateSemanticDTO;
                SemanticDTO b = new SemanticDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    new PublicId(Misc.other)
                );
                Assert.False(a.IsEquivalent(b));
            }
        }



        [DoNotParallelize]
        [Fact]
        public void SemanticDTOCompareToTest()
        {
            {
                SemanticDTO a = Misc.CreateSemanticDTO;
                SemanticDTO b = new SemanticDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    Misc.PublicIdI
                );
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                SemanticDTO a = Misc.CreateSemanticDTO;
                SemanticDTO b = new SemanticDTO(
                    new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4),
                    Misc.PublicIdH,
                    Misc.PublicIdI
                );
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                SemanticDTO a = Misc.CreateSemanticDTO;
                SemanticDTO b = new SemanticDTO(
                    Misc.PublicIdG,
                    new PublicId(Misc.h1, Misc.h3, Misc.h3, Misc.h4),
                    Misc.PublicIdI
                );
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                SemanticDTO a = Misc.CreateSemanticDTO;
                SemanticDTO b = new SemanticDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    new PublicId(Misc.i1, Misc.i2, Misc.i3, Misc.i3)
                );
                Assert.False(a.CompareTo(b) == 0);
            }
        }




        [DoNotParallelize]
        [Fact]
        public void SemanticDTOMarshalTest()
        {
            SemanticDTO dtoStart = Misc.CreateSemanticDTO;

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                output.WriteField(dtoStart);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                SemanticDTO dtoRead = (SemanticDTO)input.GetField();
                Assert.True(dtoStart.CompareTo(dtoRead) == 0);
            }
        }
        [DoNotParallelize]
        [Fact]
        public void SemanticDTOJsonMarshal()
        {
            SemanticDTO dtoStart = Misc.CreateSemanticDTO;
            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
            {
                dtoStart.Marshal(output);
            }

            ms.Dump();
            ms.Position = 0;
            using (TinkarJsonInput input = new TinkarJsonInput(ms))
            {
                SemanticDTO dtoEnd = SemanticDTO.Make(input.ReadJsonObject());
                Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
            }
        }
    }
}
