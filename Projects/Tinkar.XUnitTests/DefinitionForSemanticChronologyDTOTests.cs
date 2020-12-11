using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;

namespace Tinkar.XUnitTests
{
    public class DefinitionForSemanticChronologyDTOTests
    {
        [DoNotParallelize]
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

        [DoNotParallelize]
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

        [DoNotParallelize]
        [Fact]
        public void DefinitionForSemanticChronologyDTOMarshalTest()
        {
            DefinitionForSemanticChronologyDTO dtoStart = Misc.CreateDefinitionForSemanticChronologyDTO;

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                output.WriteField(dtoStart);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                DefinitionForSemanticChronologyDTO dtoRead = (DefinitionForSemanticChronologyDTO) input.ReadField();
                Assert.True(dtoStart.IsEquivalent(dtoRead));
            }
        }
        [DoNotParallelize]
        [Fact]
        public void DefinitionForSemanticChronologyDTOJsonMarshal()
        {
            DefinitionForSemanticChronologyDTO dtoStart = Misc.CreateDefinitionForSemanticChronologyDTO;
            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms))
            {
                dtoStart.Marshal(output);
            }

            ms.Dump();
            ms.Position = 0;
            using (TinkarJsonInput input = new TinkarJsonInput(ms))
            {
                DefinitionForSemanticChronologyDTO dtoEnd = DefinitionForSemanticChronologyDTO.Make(input.ReadJsonObject());
                Assert.True(dtoStart.IsEquivalent(dtoEnd));
            }
        }
    }
}
