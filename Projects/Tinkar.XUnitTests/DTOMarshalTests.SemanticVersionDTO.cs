using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Tinkar.XUnitTests
{
    public partial class DTOMarshalTests
    {
        SemanticVersionDTO CreateSemanticVersionDTO =>
            new SemanticVersionDTO(
                new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                CreateStampDTO,
                new Object[] { 1, "abcdef", 0.3F }
                );

        [Fact]
        public void SemanticVersionDTOFieldsTest()
        {
            SemanticVersionDTO dtoStart = CreateSemanticVersionDTO;
            Compare(dtoStart.ComponentUuids, this.g1, this.g2, this.g3, this.g4);
            Compare(dtoStart.DefinitionForSemanticUuids, this.h1, this.h2, this.h3, this.h4);
            Compare(dtoStart.ReferencedComponentUuids, this.i1, this.i2, this.i3, this.i4);
            Assert.True(dtoStart.StampDTO.IsEquivalent(CreateStampDTO));
            FieldCompare.Compare(dtoStart.Fields,
                new Object[] { 1, "abcdef", 0.3F });
        }

        [Fact]
        public void SemanticVersionDTOIsEquivalentTest()
        {
            {
                SemanticVersionDTO a = CreateSemanticVersionDTO;
                SemanticVersionDTO b = CreateSemanticVersionDTO;
                Assert.True(a.IsEquivalent(b));
            }

            {
                SemanticVersionDTO a = CreateSemanticVersionDTO;
                SemanticVersionDTO b = CreateSemanticVersionDTO
                with
                {
                    ComponentUuids = new Guid[] { this.g2, this.g2, this.g3, this.g4 }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                SemanticVersionDTO a = CreateSemanticVersionDTO;
                SemanticVersionDTO b = CreateSemanticVersionDTO
                with
                {
                    DefinitionForSemanticUuids = new Guid[] { this.g2, this.g2, this.g3, this.g4 }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                SemanticVersionDTO a = CreateSemanticVersionDTO;
                SemanticVersionDTO b = CreateSemanticVersionDTO
                with
                {
                    ReferencedComponentUuids = new Guid[] { this.g2, this.g2, this.g3, this.g4 }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                SemanticVersionDTO a = CreateSemanticVersionDTO;
                SemanticVersionDTO b = CreateSemanticVersionDTO
                with
                {
                    StampDTO = CreateStampDTO with { StatusUuids = new Guid[] { this.g2 } }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                SemanticVersionDTO a = CreateSemanticVersionDTO;
                SemanticVersionDTO b = CreateSemanticVersionDTO
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
            SemanticVersionDTO dtoStart = CreateSemanticVersionDTO;

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
