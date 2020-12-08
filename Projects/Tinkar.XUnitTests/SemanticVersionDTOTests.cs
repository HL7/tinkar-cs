using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Tinkar.XUnitTests
{
    public class SemanticVersionDTOTests
    {
        [Fact]
        public void SemanticVersionDTOFieldsTest()
        {
            SemanticVersionDTO dtoStart = Misc.CreateSemanticVersionDTO;
            Misc.Compare(dtoStart.ComponentUuids, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Misc.Compare(dtoStart.DefinitionForSemanticUuids, Misc.h1, Misc.h2, Misc.h3, Misc.h4);
            Misc.Compare(dtoStart.ReferencedComponentUuids, Misc.i1, Misc.i2, Misc.i3, Misc.i4);
            Assert.True(dtoStart.StampDTO.IsEquivalent(Misc.CreateStampDTO));
            FieldCompare.Compare(dtoStart.Fields,
                new Object[] { 1, "abcdef", 0.3F });
        }

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
                    ComponentUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                SemanticVersionDTO a = Misc.CreateSemanticVersionDTO;
                SemanticVersionDTO b = Misc.CreateSemanticVersionDTO
                with
                {
                    DefinitionForSemanticUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                SemanticVersionDTO a = Misc.CreateSemanticVersionDTO;
                SemanticVersionDTO b = Misc.CreateSemanticVersionDTO
                with
                {
                    ReferencedComponentUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                SemanticVersionDTO a = Misc.CreateSemanticVersionDTO;
                SemanticVersionDTO b = Misc.CreateSemanticVersionDTO
                with
                {
                    StampDTO = Misc.CreateStampDTO with { StatusUuids = new Guid[] { Misc.g2 } }
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

        [Fact]
        public void SemanticVersionDTOMarshalTest()
        {
            SemanticVersionDTO dtoStart = Misc.CreateSemanticVersionDTO;

            MemoryStream ms = new MemoryStream();
            TinkarOutput output = new TinkarOutput(ms);
            dtoStart.Marshal(output);

            ms.Position = 0;
            TinkarInput input = new TinkarInput(ms);
            SemanticVersionDTO dtoRead = SemanticVersionDTO.Make(input,
                dtoStart.ComponentUuids,
                dtoStart.DefinitionForSemanticUuids,
                dtoStart.ReferencedComponentUuids);
            Assert.True(dtoStart.IsEquivalent(dtoRead));
        }
    }
}
