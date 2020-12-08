using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;

namespace Tinkar.XUnitTests
{
    public class SemanticChronologyDTOTests
    {
        [Fact]
        public void SemanticChronologyDTOFieldsTest()
        {
            SemanticChronologyDTO dtoStart = Misc.CreateSemanticChronologyDTO;
            Misc.Compare(dtoStart.ComponentUuids, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Misc.Compare(dtoStart.DefinitionForSemanticUuids, Misc.h1, Misc.h2, Misc.h3, Misc.h4);
            Misc.Compare(dtoStart.ReferencedComponentUuids, Misc.i1, Misc.i2, Misc.i3, Misc.i4);
        }

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
                    ComponentUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                SemanticChronologyDTO a = Misc.CreateSemanticChronologyDTO;
                SemanticChronologyDTO b = Misc.CreateSemanticChronologyDTO
                with
                {
                    DefinitionForSemanticUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                SemanticChronologyDTO a = Misc.CreateSemanticChronologyDTO;
                SemanticChronologyDTO b = Misc.CreateSemanticChronologyDTO
                with
                {
                    ReferencedComponentUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
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

        [Fact]
        public void SemanticChronologyDTOMarshalTest()
        {
            SemanticChronologyDTO dtoStart = Misc.CreateSemanticChronologyDTO;

            MemoryStream ms = new MemoryStream();
            TinkarOutput output = new TinkarOutput(ms);
            output.WriteField(dtoStart);

            ms.Position = 0;
            TinkarInput input = new TinkarInput(ms);
            SemanticChronologyDTO dtoRead = (SemanticChronologyDTO)input.ReadField();
            Assert.True(dtoStart.IsEquivalent(dtoRead));
        }
    }
}
