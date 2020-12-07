using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;

namespace Tinkar.XUnitTests
{
    public partial class DTOMarshalTests
    {
        DefinitionForSemanticVersionDTO CreateDefinitionForSemanticVersionDTO =>
            new DefinitionForSemanticVersionDTO(
                new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                CreateStampDTO,
                new Guid[] { this.g1, this.g2, this.g3, this.g4 },

                new FieldDefinitionDTO[] {
                    CreateFieldDefinition
                }
            );

        [Fact]
        public void DefinitionForSemanticVersionDTOFieldsTest()
        {
            FieldDefinitionDTO fdoa = CreateFieldDefinition;
            FieldDefinitionDTO fdob = CreateFieldDefinition with
            {
                DataTypeUuids = new Guid[] { this.g2, this.g2, this.g3, this.g4 }
            }
            ;

            DefinitionForSemanticVersionDTO dtoStart = new DefinitionForSemanticVersionDTO(
                new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                CreateStampDTO,
                new Guid[] { this.h1, this.h2, this.h3 },
                new FieldDefinitionDTO[] { fdoa, fdob }
                );
            Compare(dtoStart.ComponentUuids, this.g1, this.g2, this.g3, this.g4);
            Assert.True(dtoStart.StampDTO.IsEquivalent(CreateStampDTO));
            Compare<FieldDefinitionDTO>(dtoStart.FieldDefinitionDTOs,
                new FieldDefinitionDTO[]
                {
                    CreateFieldDefinition,
                    CreateFieldDefinition with
                    {
                        DataTypeUuids = new Guid[] { this.g2, this.g2, this.g3, this.g4 }
                    }
                }
                );
        }

        [Fact]
        public void DefinitionForSemanticVersionDTOIsEquivalentTest()
        {
            {
                DefinitionForSemanticVersionDTO a = CreateDefinitionForSemanticVersionDTO;
                DefinitionForSemanticVersionDTO b = CreateDefinitionForSemanticVersionDTO;
                Assert.True(a.IsEquivalent(b));
            }

            {
                DefinitionForSemanticVersionDTO a = CreateDefinitionForSemanticVersionDTO;
                DefinitionForSemanticVersionDTO b = CreateDefinitionForSemanticVersionDTO
                with
                {
                    ComponentUuids = new Guid[] { this.g2, this.g2, this.g3, this.g4 }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                DefinitionForSemanticVersionDTO a = CreateDefinitionForSemanticVersionDTO;
                DefinitionForSemanticVersionDTO b = CreateDefinitionForSemanticVersionDTO
                with
                {
                    StampDTO = CreateStampDTO with { StatusUuids = new Guid[] { this.g2 } }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                DefinitionForSemanticVersionDTO a = CreateDefinitionForSemanticVersionDTO;
                DefinitionForSemanticVersionDTO b = CreateDefinitionForSemanticVersionDTO
                with
                {
                    ReferencedComponentPurposeUuids = new Guid[] { this.g2, this.g2, this.g3, this.g4 }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                FieldDefinitionDTO fdoa = CreateFieldDefinition with
                {
                    DataTypeUuids = new Guid[] { this.g2, this.g2, this.g3, this.g4 }
                } ;

                DefinitionForSemanticVersionDTO a = CreateDefinitionForSemanticVersionDTO;
                DefinitionForSemanticVersionDTO b = CreateDefinitionForSemanticVersionDTO
                with
                {
                    FieldDefinitionDTOs = new FieldDefinitionDTO[] { fdoa }
                };
                Assert.False(a.IsEquivalent(b));
            }
        }

        [Fact]
        public void DefinitionForSemanticVersionDTOMarshalTest()
        {
            DefinitionForSemanticVersionDTO dtoStart = CreateDefinitionForSemanticVersionDTO;

            MemoryStream ms = new MemoryStream();
            TinkarOutput output = new TinkarOutput(ms);
            dtoStart.Marshal(output);

            ms.Position = 0;
            TinkarInput input = new TinkarInput(ms);
            DefinitionForSemanticVersionDTO dtoRead = DefinitionForSemanticVersionDTO.Make(input, dtoStart.ComponentUuids);
            Assert.True(dtoStart.IsEquivalent(dtoRead));
        }
    }
}
