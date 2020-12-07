using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Tinkar.XUnitTests
{
    public partial class DTOMarshalTests
    {
        DefinitionForSemanticChronologyDTO CreateDefinitionForSemanticChronologyDTO =>
            new DefinitionForSemanticChronologyDTO(
                new Guid[] { g1, g2, g3, g4 },
                new Guid[] { h1, h2, h3 },
                new DefinitionForSemanticVersionDTO[] {CreateDefinitionForSemanticVersionDTO}
            );

        [Fact]
        public void DefinitionForSemanticChronologyDTOFieldsTest()
        {
            DefinitionForSemanticChronologyDTO dtoStart = CreateDefinitionForSemanticChronologyDTO;
            Compare(dtoStart.ComponentUuids, this.g1, this.g2, this.g3, this.g4);
            Compare(dtoStart.ChronologySetUuids, this.h1, this.h2, this.h3);
            Compare<DefinitionForSemanticVersionDTO>(dtoStart.DefinitionVersions,
                new DefinitionForSemanticVersionDTO[] { CreateDefinitionForSemanticVersionDTO }
                );
        }

        [Fact]
        public void DefinitionForSemanticChronologyDTOIsEquivalentTest()
        {
            {
                DefinitionForSemanticChronologyDTO a = CreateDefinitionForSemanticChronologyDTO;
                DefinitionForSemanticChronologyDTO b = CreateDefinitionForSemanticChronologyDTO;
                Assert.True(a.IsEquivalent(b));
            }

            {
                DefinitionForSemanticChronologyDTO a = CreateDefinitionForSemanticChronologyDTO;
                DefinitionForSemanticChronologyDTO b = CreateDefinitionForSemanticChronologyDTO
                with
                {
                    ComponentUuids = new Guid[] { this.g2, this.g2, this.g3, this.g4 }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                DefinitionForSemanticChronologyDTO a = CreateDefinitionForSemanticChronologyDTO;
                DefinitionForSemanticChronologyDTO b = CreateDefinitionForSemanticChronologyDTO
                with
                {
                    ChronologySetUuids = new Guid[] { this.g2, this.g2, this.g3, this.g4 }
                }
                ;
                Assert.False(a.IsEquivalent(b));
            }

            {
                DefinitionForSemanticChronologyDTO a = CreateDefinitionForSemanticChronologyDTO;
                DefinitionForSemanticChronologyDTO b = CreateDefinitionForSemanticChronologyDTO
                with
                {
                    DefinitionVersions = new DefinitionForSemanticVersionDTO[]
                    {
                        CreateDefinitionForSemanticVersionDTO
                        with
                        {
                            ComponentUuids = new Guid[] { this.g3, this.g2, this.g1, this.g4 }
                        }
                    }
                }
                ;
                Assert.False(a.IsEquivalent(b));
            }
        }

        [Fact]
        public void DefinitionForSemanticChronologyDTOMarshalTest()
        {
            DefinitionForSemanticChronologyDTO dtoStart = CreateDefinitionForSemanticChronologyDTO;

            MemoryStream ms = new MemoryStream();
            TinkarOutput output = new TinkarOutput(ms);
            output.WriteField(dtoStart);

            ms.Position = 0;
            TinkarInput input = new TinkarInput(ms);
            DefinitionForSemanticChronologyDTO dtoRead = (DefinitionForSemanticChronologyDTO)input.ReadField();
            Assert.True(dtoStart.IsEquivalent(dtoRead));
        }
    }
}
