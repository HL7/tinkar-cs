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
    public class SemanticVersionDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void SemanticVersionDTOFieldsTest()
        {
            SemanticVersionDTO dtoStart = Misc.CreateSemanticVersionDTO;
            Misc.Compare(dtoStart.PublicId, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Misc.Compare(dtoStart.DefinitionForSemanticPublicId, Misc.h1, Misc.h2, Misc.h3, Misc.h4);
            Misc.Compare(dtoStart.ReferencedComponentPublicId, Misc.i1, Misc.i2, Misc.i3, Misc.i4);
            Assert.True(dtoStart.StampDTO.CompareTo(Misc.CreateStampDTO) == 0);
            FieldCompare.Same(dtoStart.Fields,
                new Object[] { 1, "abcdef", 0.3F });
        }

        [DoNotParallelize]
        [Fact]
        public void SemanticVersionDTOIsEquivalentTest()
        {
            {
                SemanticVersionDTO a = Misc.CreateSemanticVersionDTO;
                SemanticVersionDTO b = Misc.CreateSemanticVersionDTO;
                Assert.True(a.IsEquivalent(b));
            }

            {
                SemanticVersionDTO a = Misc.CreateSemanticVersionDTO;
                SemanticVersionDTO b = Misc.CreateSemanticVersionDTO
                with
                {
                    PublicId = new PublicId(Misc.other)
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                SemanticVersionDTO a = Misc.CreateSemanticVersionDTO;
                SemanticVersionDTO b = Misc.CreateSemanticVersionDTO
                with
                {
                    DefinitionForSemanticPublicId = new PublicId(Misc.other)
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                SemanticVersionDTO a = Misc.CreateSemanticVersionDTO;
                SemanticVersionDTO b = Misc.CreateSemanticVersionDTO
                with
                {
                    ReferencedComponentPublicId = new PublicId(Misc.other)
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                SemanticVersionDTO a = Misc.CreateSemanticVersionDTO;
                SemanticVersionDTO b = Misc.CreateSemanticVersionDTO
                with
                {
                    StampDTO = Misc.CreateStampDTO with { StatusPublicId = new PublicId(Misc.g2 ) }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                SemanticVersionDTO a = Misc.CreateSemanticVersionDTO;
                SemanticVersionDTO b = Misc.CreateSemanticVersionDTO
                with
                {
                    Fields = new Object[] { 1, "abcdef" }
                };
                Assert.False(a.IsEquivalent(b));
            }
        }


        [DoNotParallelize]
        [Fact]
        public void SemanticVersionDTOCompareToTest()
        {
            {
                SemanticVersionDTO a = Misc.CreateSemanticVersionDTO;
                SemanticVersionDTO b = Misc.CreateSemanticVersionDTO;
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                SemanticVersionDTO a = Misc.CreateSemanticVersionDTO;
                SemanticVersionDTO b = Misc.CreateSemanticVersionDTO
                with
                {
                    PublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
                };
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                SemanticVersionDTO a = Misc.CreateSemanticVersionDTO;
                SemanticVersionDTO b = Misc.CreateSemanticVersionDTO
                with
                {
                    DefinitionForSemanticPublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
                };
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                SemanticVersionDTO a = Misc.CreateSemanticVersionDTO;
                SemanticVersionDTO b = Misc.CreateSemanticVersionDTO
                with
                {
                    ReferencedComponentPublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
                };
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                SemanticVersionDTO a = Misc.CreateSemanticVersionDTO;
                SemanticVersionDTO b = Misc.CreateSemanticVersionDTO
                with
                {
                    StampDTO = Misc.CreateStampDTO with { StatusPublicId = new PublicId(Misc.g2) }
                };
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                SemanticVersionDTO a = Misc.CreateSemanticVersionDTO;
                SemanticVersionDTO b = Misc.CreateSemanticVersionDTO
                with
                {
                    Fields = new Object[] { 1, "abcdef" }
                };
                Assert.False(a.CompareTo(b) == 0);
            }
        }



        [DoNotParallelize]
        [Fact]
        public void SemanticVersionDTOMarshalTest()
        {
            SemanticVersionDTO dtoStart = Misc.CreateSemanticVersionDTO;

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                dtoStart.Marshal(output);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                SemanticVersionDTO dtoRead = SemanticVersionDTO.Make(input,
                    dtoStart.PublicId,
                    dtoStart.DefinitionForSemanticPublicId,
                    dtoStart.ReferencedComponentPublicId);
                Assert.True(dtoStart.CompareTo(dtoRead) == 0);
            }
        }
        [DoNotParallelize]
        [Fact]
        public void SemanticVersionDTOJsonMarshal()
        {
            SemanticVersionDTO dtoStart = Misc.CreateSemanticVersionDTO;
            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
            {
                dtoStart.Marshal(output);
            }

            ms.Dump();
            ms.Position = 0;
            using (TinkarJsonInput input = new TinkarJsonInput(ms))
            {
                SemanticVersionDTO dtoEnd = SemanticVersionDTO.Make(
                    input.ReadJsonObject(),
                    dtoStart.PublicId,
                    dtoStart.DefinitionForSemanticPublicId,
                    dtoStart.ReferencedComponentPublicId);
                Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
            }
        }

    }
}
