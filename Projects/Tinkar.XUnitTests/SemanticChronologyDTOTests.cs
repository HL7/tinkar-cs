using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;
using Assert = Xunit.Assert;

namespace Tinkar.XUnitTests
{
    public class SemanticChronologyDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void SemanticChronologyDTOFieldsTest()
        {
            SemanticChronologyDTO dtoStart = Misc.CreateSemanticChronologyDTO;
            Misc.Compare(dtoStart.PublicId, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Misc.Compare(dtoStart.PatternForSemanticPublicId, Misc.h1, Misc.h2, Misc.h3, Misc.h4);
            Misc.Compare(dtoStart.ReferencedComponentPublicId, Misc.i1, Misc.i2, Misc.i3, Misc.i4);
        }

        [DoNotParallelize]
        [Fact]
        public void SemanticChronologyDTOIsEquivalentTest()
        {
            {
                SemanticChronologyDTO a = Misc.CreateSemanticChronologyDTO;
                SemanticChronologyDTO b = Misc.CreateSemanticChronologyDTO;
                Assert.True(a.IsEquivalent(b));
            }

            {
                SemanticChronologyDTO a = Misc.CreateSemanticChronologyDTO;
                SemanticChronologyDTO b = Misc.CreateSemanticChronologyDTO
                with
                {
                    PublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                SemanticChronologyDTO a = Misc.CreateSemanticChronologyDTO;
                SemanticChronologyDTO b = Misc.CreateSemanticChronologyDTO
                with
                {
                    PatternForSemanticPublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                SemanticChronologyDTO a = Misc.CreateSemanticChronologyDTO;
                SemanticChronologyDTO b = Misc.CreateSemanticChronologyDTO
                with
                {
                    ReferencedComponentPublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
                };
                Assert.False(a.IsEquivalent(b));
            }
            {
                SemanticChronologyDTO a = Misc.CreateSemanticChronologyDTO;
                SemanticChronologyDTO b = Misc.CreateSemanticChronologyDTO
                with
                {
                    SemanticVersions = new SemanticVersionDTO[] { Misc.CreateSemanticVersionDTO, Misc.CreateSemanticVersionDTO }
                };
                Assert.False(a.IsEquivalent(b));
            }
        }

        [DoNotParallelize]
        [Fact]
        public void SemanticChronologyDTOMarshalTest()
        {
            SemanticChronologyDTO dtoStart = Misc.CreateSemanticChronologyDTO;

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                output.WriteField(dtoStart);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                SemanticChronologyDTO dtoRead = (SemanticChronologyDTO)input.GetField();
                Assert.True(dtoStart.IsEquivalent(dtoRead));
            }
        }
        [DoNotParallelize]
        [Fact]
        public void SemanticChronologyDTOJsonMarshal()
        {
            SemanticChronologyDTO dtoStart = Misc.CreateSemanticChronologyDTO;
            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
            {
                dtoStart.Marshal(output);
            }

            ms.Dump();
            ms.Position = 0;
            using (TinkarJsonInput input = new TinkarJsonInput(ms))
            {
                SemanticChronologyDTO dtoEnd = SemanticChronologyDTO.Make(input.ReadJsonObject());
                Assert.True(dtoStart.IsEquivalent(dtoEnd));
            }
        }
    }
}
