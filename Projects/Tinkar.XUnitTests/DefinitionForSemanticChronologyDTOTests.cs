using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Tinkar.XUnitTests
{
    public class DefinitionForSemanticChronologyDTOTests
    {
        [Fact]
        public void DefinitionForSemanticChronologyDTOFieldsTest()
        {
            DefinitionForSemanticChronologyDTO dtoStart = Misc.CreateDefinitionForSemanticChronologyDTO;
            Misc.Compare(dtoStart.ComponentUuids, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Misc.Compare(dtoStart.ChronologySetUuids, Misc.h1, Misc.h2, Misc.h3);
            Misc.Compare<DefinitionForSemanticVersionDTO>(dtoStart.DefinitionVersions,
                new DefinitionForSemanticVersionDTO[] 
                    { Misc.CreateDefinitionForSemanticVersionDTO }
                );
        }

        [Fact]
        public void DefinitionForSemanticChronologyDTOIsEquivalentTest()
        {
            {
                DefinitionForSemanticChronologyDTO a = Misc.CreateDefinitionForSemanticChronologyDTO;
                DefinitionForSemanticChronologyDTO b = Misc.CreateDefinitionForSemanticChronologyDTO;
                Assert.True(a.IsEquivalent(b));
            }

            {
                DefinitionForSemanticChronologyDTO a = Misc.CreateDefinitionForSemanticChronologyDTO;
                DefinitionForSemanticChronologyDTO b = Misc.CreateDefinitionForSemanticChronologyDTO
                with
                {
                    ComponentUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                DefinitionForSemanticChronologyDTO a = Misc.CreateDefinitionForSemanticChronologyDTO;
                DefinitionForSemanticChronologyDTO b = Misc.CreateDefinitionForSemanticChronologyDTO
                with
                {
                    ChronologySetUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                }
                ;
                Assert.False(a.IsEquivalent(b));
            }

            {
                DefinitionForSemanticChronologyDTO a = Misc.CreateDefinitionForSemanticChronologyDTO;
                DefinitionForSemanticChronologyDTO b = Misc.CreateDefinitionForSemanticChronologyDTO
                with
                {
                    DefinitionVersions = new DefinitionForSemanticVersionDTO[]
                    {
                        Misc.CreateDefinitionForSemanticVersionDTO
                        with
                        {
                            ComponentUuids = new Guid[] { Misc.g3, Misc.g2, Misc.g1, Misc.g4 }
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
            DefinitionForSemanticChronologyDTO dtoStart = Misc.CreateDefinitionForSemanticChronologyDTO;

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
