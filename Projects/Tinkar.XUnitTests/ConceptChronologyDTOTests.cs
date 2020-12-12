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
    public class ConceptChronologyDTOTests
    {
        DateTime cvTime => new DateTime(1920, 4, 5);

        [DoNotParallelize]
        [Fact]
        public void ConceptChronologyDTOFieldsTest()
        {
            ConceptChronologyDTO dtoStart = Misc.CreateConceptChronologyDTO;
            Misc.Compare(dtoStart.ComponentUuids, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Misc.Compare(dtoStart.ChronologySetUuids, Misc.h1, Misc.h2, Misc.h3, Misc.h4);
            Misc.Compare(dtoStart.ConceptVersions,
                Misc.ConceptVersionsBase(new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 }));
        }

        [DoNotParallelize]
        [Fact]
        public void ConceptChronologyDTOIsEquivalentTest()
        {
            {
                ConceptChronologyDTO a = Misc.CreateConceptChronologyDTO;
                ConceptChronologyDTO b = Misc.CreateConceptChronologyDTO;
                Assert.True(a.IsEquivalent(b));
            }

            {
                ConceptChronologyDTO a = Misc.CreateConceptChronologyDTO;
                ConceptChronologyDTO b = Misc.CreateConceptChronologyDTO
                with
                {
                    ComponentUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                ConceptChronologyDTO a = new ConceptChronologyDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                    Misc.ConceptVersionsBase(new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 })
                );
                ConceptChronologyDTO b = new ConceptChronologyDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    new Guid[] { Misc.h1, Misc.h3, Misc.h3, Misc.h4 },
                    Misc.ConceptVersionsBase(new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 })
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                ConceptChronologyDTO a = new ConceptChronologyDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                    Misc.ConceptVersionsBase(new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 })
                );
                ConceptChronologyDTO b = new ConceptChronologyDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                    new ConceptVersionDTO[]
                    {
                        Misc.cv1(new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 })
                    }
                );
                Assert.False(a.IsEquivalent(b));
            }
        }

        [DoNotParallelize]
        [Fact]
        public void ConceptChronologyDTOMarshalTest()
        {
            ConceptChronologyDTO dtoStart = new ConceptChronologyDTO(
                new Guid[] {Misc.g1, Misc.g2, Misc.g3, Misc.g4},
                new Guid[] {Misc.h1, Misc.h2, Misc.h3, Misc.h4},
                Misc.ConceptVersionsBase(new Guid[] {Misc.g1, Misc.g2, Misc.g3, Misc.g4})
            );

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                output.WriteField(dtoStart);
            }


            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                ConceptChronologyDTO dtoRead = (ConceptChronologyDTO) input.ReadField();
                Assert.True(dtoStart.IsEquivalent(dtoRead));
            }
        }
        [DoNotParallelize]
        [Fact]
        public void ConceptChronologyDTOJsonMarshal()
        {
            ConceptChronologyDTO dtoStart = Misc.CreateConceptChronologyDTO;
            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
            {
                dtoStart.Marshal(output);
            }

            ms.Dump();
            ms.Position = 0;
            using (TinkarJsonInput input = new TinkarJsonInput(ms))
            {
                ConceptChronologyDTO dtoEnd = ConceptChronologyDTO.Make(input.ReadJsonObject());
                Assert.True(dtoStart.IsEquivalent(dtoEnd));
            }
        }
    }
}
