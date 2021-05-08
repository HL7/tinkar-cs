using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;
using Assert = Xunit.Assert;
using Tinkar.Dto;
using System.Collections.Immutable;

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
            Misc.Compare(dtoStart.PublicId, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Misc.Compare(dtoStart.Versions,
                Misc.ConceptVersionsBase(Misc.PublicIdG));
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
                    PublicId = new PublicId(Misc.other)
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                ConceptChronologyDTO a = new ConceptChronologyDTO(
                    Misc.PublicIdG,
                    Misc.ConceptVersionsBase(Misc.PublicIdG).ToImmutableArray()
                );
                ConceptChronologyDTO b = new ConceptChronologyDTO(
                    Misc.PublicIdG,
                    new ConceptVersionDTO[]
                    {
                        Misc.cv1(Misc.PublicIdG)
                    }.ToImmutableArray()
                );
                Assert.False(a.IsEquivalent(b));
            }
        }



        [DoNotParallelize]
        [Fact]
        public void ConceptChronologyDTOCompareToTest()
        {
            {
                ConceptChronologyDTO a = Misc.CreateConceptChronologyDTO;
                ConceptChronologyDTO b = Misc.CreateConceptChronologyDTO;
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                ConceptChronologyDTO a = Misc.CreateConceptChronologyDTO;
                ConceptChronologyDTO b = Misc.CreateConceptChronologyDTO
                with
                {
                    PublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
                };
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                ConceptChronologyDTO a = new ConceptChronologyDTO(
                    Misc.PublicIdG,
                    Misc.ConceptVersionsBase(Misc.PublicIdG).ToImmutableArray()
                );
                ConceptChronologyDTO b = new ConceptChronologyDTO(
                    Misc.PublicIdG,
                    new ConceptVersionDTO[]
                    {
                        Misc.cv1(Misc.PublicIdG)
                    }.ToImmutableArray()
                );
                Assert.False(a.CompareTo(b) == 0);
            }
        }



        [DoNotParallelize]
        [Fact]
        public void ConceptChronologyDTOMarshalTest()
        {
            ConceptChronologyDTO dtoStart = new ConceptChronologyDTO(
                Misc.PublicIdG,
                Misc.ConceptVersionsBase(Misc.PublicIdG).ToImmutableArray()
            );

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                output.WriteField(dtoStart);
            }


            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                ConceptChronologyDTO dtoRead = (ConceptChronologyDTO)input.GetField();
                Assert.True(dtoStart.CompareTo(dtoRead) == 0);
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
                Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
            }
        }
    }
}
