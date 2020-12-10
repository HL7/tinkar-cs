using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;

namespace Tinkar.XUnitTests
{
    public class DefinitionForSemanticVersionDTOTests
    {
        [Fact]
        public void DefinitionForSemanticVersionDTOFieldsTest()
        {
            FieldDefinitionDTO fdoa = Misc.CreateFieldDefinition;
            FieldDefinitionDTO fdob = Misc.CreateFieldDefinition with
            {
                DataTypeUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
            }
            ;

            DefinitionForSemanticVersionDTO dtoStart = new DefinitionForSemanticVersionDTO(
                new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                Misc.CreateStampDTO,
                new Guid[] { Misc.h1, Misc.h2, Misc.h3 },
                new FieldDefinitionDTO[] { fdoa, fdob }
                );
            Misc.Compare(dtoStart.ComponentUuids, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Assert.True(dtoStart.StampDTO.IsEquivalent(Misc.CreateStampDTO));
            Misc.Compare<FieldDefinitionDTO>(dtoStart.FieldDefinitionDTOs,
                new FieldDefinitionDTO[]
                {
                    Misc.CreateFieldDefinition,
                    Misc.CreateFieldDefinition with
                    {
                        DataTypeUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                    }
                }
                );
        }

        [Fact]
        public void DefinitionForSemanticVersionDTOIsEquivalentTest()
        {
            {
                DefinitionForSemanticVersionDTO a = Misc.CreateDefinitionForSemanticVersionDTO;
                DefinitionForSemanticVersionDTO b = Misc.CreateDefinitionForSemanticVersionDTO;
                Assert.True(a.IsEquivalent(b));
            }

            {
                DefinitionForSemanticVersionDTO a = Misc.CreateDefinitionForSemanticVersionDTO;
                DefinitionForSemanticVersionDTO b = Misc.CreateDefinitionForSemanticVersionDTO
                with
                {
                    ComponentUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                DefinitionForSemanticVersionDTO a = Misc.CreateDefinitionForSemanticVersionDTO;
                DefinitionForSemanticVersionDTO b = Misc.CreateDefinitionForSemanticVersionDTO
                with
                {
                    StampDTO = Misc.CreateStampDTO with { StatusUuids = new Guid[] { Misc.g2 } }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                DefinitionForSemanticVersionDTO a = Misc.CreateDefinitionForSemanticVersionDTO;
                DefinitionForSemanticVersionDTO b = Misc.CreateDefinitionForSemanticVersionDTO
                with
                {
                    ReferencedComponentPurposeUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                FieldDefinitionDTO fdoa = Misc.CreateFieldDefinition with
                {
                    DataTypeUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                } ;

                DefinitionForSemanticVersionDTO a = Misc.CreateDefinitionForSemanticVersionDTO;
                DefinitionForSemanticVersionDTO b = Misc.CreateDefinitionForSemanticVersionDTO
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
            DefinitionForSemanticVersionDTO dtoStart = Misc.CreateDefinitionForSemanticVersionDTO;

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                dtoStart.Marshal(output);
            }

            ms.Position = 0;
            TinkarInput input = new TinkarInput(ms);
            DefinitionForSemanticVersionDTO dtoRead = DefinitionForSemanticVersionDTO.Make(input, dtoStart.ComponentUuids);
            Assert.True(dtoStart.IsEquivalent(dtoRead));
        }
    }
}
