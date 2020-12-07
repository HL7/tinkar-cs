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
        DateTime cvTime => new DateTime(1920, 4, 5);

        ConceptVersionDTO cv1(IEnumerable<Guid> componentGuids) => 
            new ConceptVersionDTO(
                componentGuids,
                CreateStampDTO
            );

        ConceptVersionDTO cv2(IEnumerable<Guid> componentGuids) => 
            new ConceptVersionDTO(
                componentGuids,
                CreateStampDTO with { StatusUuids = new Guid[] { this.g2 } }
                );

        ConceptVersionDTO[] conceptVersionsBase(IEnumerable<Guid> componentGuids) =>
            new ConceptVersionDTO[] { cv1(componentGuids), cv2(componentGuids) };

        [Fact]
        public void ConceptChronologyDTOFieldsTest()
        {
            ConceptChronologyDTO dtoStart = new ConceptChronologyDTO(
                new Guid[] {this.g1, this.g2, this.g3, this.g4},
                new Guid[] {this.h1, this.h2, this.h3, this.h4},
                conceptVersionsBase(new Guid[] { this.g1, this.g2, this.g3, this.g4 }));
            Compare(dtoStart.ComponentUuids, this.g1, this.g2, this.g3, this.g4);
            Compare(dtoStart.ChronologySetUuids, this.h1, this.h2, this.h3, this.h4);
            Compare(dtoStart.ConceptVersions,
                conceptVersionsBase(new Guid[] { this.g1, this.g2, this.g3, this.g4 }));
        }

        [Fact]
        public void ConceptChronologyDTOIsEquivalentTest()
        {
            {
                ConceptChronologyDTO a = new ConceptChronologyDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    conceptVersionsBase(new Guid[] { this.g1, this.g2, this.g3, this.g4 })
                );
                ConceptChronologyDTO b = new ConceptChronologyDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    conceptVersionsBase(new Guid[] { this.g1, this.g2, this.g3, this.g4 })
                );
                Assert.True(a.IsEquivalent(b));
            }

            {
                ConceptChronologyDTO a = new ConceptChronologyDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    conceptVersionsBase(new Guid[] { this.g1, this.g2, this.g3, this.g4 })
                );
                ConceptChronologyDTO b = new ConceptChronologyDTO(
                    new Guid[] { this.g2, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    conceptVersionsBase(new Guid[] { this.g2, this.g2, this.g3, this.g4 })
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                ConceptChronologyDTO a = new ConceptChronologyDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    conceptVersionsBase(new Guid[] { this.g1, this.g2, this.g3, this.g4 })
                );
                ConceptChronologyDTO b = new ConceptChronologyDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h3, this.h3, this.h4 },
                    conceptVersionsBase(new Guid[] { this.g1, this.g2, this.g3, this.g4 })
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                ConceptChronologyDTO a = new ConceptChronologyDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    conceptVersionsBase(new Guid[] { this.g1, this.g2, this.g3, this.g4 })
                );
                ConceptChronologyDTO b = new ConceptChronologyDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new ConceptVersionDTO[]
                    {
                        cv1(new Guid[] { this.g1, this.g2, this.g3, this.g4 })
                    }
                );
                Assert.False(a.IsEquivalent(b));
            }
        }

        [Fact]
        public void ConceptChronologyDTOMarshalTest()
        {
            ConceptChronologyDTO dtoStart = new ConceptChronologyDTO(
                new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                conceptVersionsBase(new Guid[] { this.g1, this.g2, this.g3, this.g4 })
            );

            MemoryStream ms = new MemoryStream();
            TinkarOutput output = new TinkarOutput(ms);
            output.WriteField(dtoStart);

            ms.Position = 0;
            TinkarInput input = new TinkarInput(ms);
            ConceptChronologyDTO dtoRead = (ConceptChronologyDTO)input.ReadField();
            Assert.True(dtoStart.IsEquivalent(dtoRead));
        }
    }
}
