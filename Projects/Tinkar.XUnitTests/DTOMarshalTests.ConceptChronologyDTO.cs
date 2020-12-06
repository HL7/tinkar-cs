using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Tinkar.XUnitTests
{
    public partial class DTOMarshalTests
    {
        DateTime cvTime => new DateTime(1920, 4, 5);

        ConceptVersionDTO cv1 => new ConceptVersionDTO(
            new Guid[] { this.g1 },
            new StampDTO(
                new Guid[] { this.g2 },
                cvTime,
                new Guid[] { this.g3 },
                new Guid[] { this.g4 },
                new Guid[] { this.h1 }
            ));

        ConceptVersionDTO cv2 => new ConceptVersionDTO(
            new Guid[] { this.g2 },
            new StampDTO(
                new Guid[] { this.g3 },
                cvTime,
                new Guid[] { this.g4 },
                new Guid[] { this.j1 },
                new Guid[] { this.j2 }
            ));

        ConceptVersionDTO[] conceptVersionsBase =>
            new ConceptVersionDTO[] { cv1, cv2 };
        [Fact]
        public void ConceptChronologyDTOFieldsTest()
        {
            ConceptChronologyDTO dtoStart = new ConceptChronologyDTO(
                new Guid[] {this.g1, this.g2, this.g3, this.g4},
                new Guid[] {this.h1, this.h2, this.h3, this.h4},
                conceptVersionsBase);
            Compare(dtoStart.ComponentUuids, this.g1, this.g2, this.g3, this.g4);
            Compare(dtoStart.ChronologySetUuids, this.h1, this.h2, this.h3, this.h4);
            Compare(dtoStart.ConceptVersions, conceptVersionsBase);
        }

        [Fact]
        public void ConceptChronologyDTOIsEquivalentTest()
        {
            {
                ConceptChronologyDTO a = new ConceptChronologyDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    conceptVersionsBase
                );
                ConceptChronologyDTO b = new ConceptChronologyDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    conceptVersionsBase
                );
                Assert.True(a.IsEquivalent(b));
            }

            {
                ConceptChronologyDTO a = new ConceptChronologyDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    conceptVersionsBase
                );
                ConceptChronologyDTO b = new ConceptChronologyDTO(
                    new Guid[] { this.g2, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    conceptVersionsBase
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                ConceptChronologyDTO a = new ConceptChronologyDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    conceptVersionsBase
                );
                ConceptChronologyDTO b = new ConceptChronologyDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h3, this.h3, this.h4 },
                    conceptVersionsBase
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                ConceptChronologyDTO a = new ConceptChronologyDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    conceptVersionsBase
                );
                ConceptChronologyDTO b = new ConceptChronologyDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new ConceptVersionDTO[] { cv1 }
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
                conceptVersionsBase
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
