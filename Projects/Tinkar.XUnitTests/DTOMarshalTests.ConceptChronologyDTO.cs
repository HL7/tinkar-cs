using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Tinkar.XUnitTests
{
    public partial class DTOMarshalTests
    {
        [Fact]
        public void ConceptChronologyDTOFieldsTest()
        {
            DateTime time = new DateTime(1920, 4, 5);

            ConceptVersionDTO cv1 = new ConceptVersionDTO(
                new Guid[] { this.g1 },
                new StampDTO(
                    new Guid[] { this.g2 },
                    time,
                    new Guid[] { this.g3 },
                    new Guid[] { this.g4 },
                    new Guid[] { this.h1 }
                ));

            ConceptVersionDTO cv2 = new ConceptVersionDTO(
                new Guid[] { this.g2 },
                new StampDTO(
                    new Guid[] { this.g3 },
                    time,
                    new Guid[] { this.g4 },
                    new Guid[] { this.j1 },
                    new Guid[] { this.j2 }
                ));
            ConceptVersionDTO[] conceptVersionsBase =
                new ConceptVersionDTO[] { cv1, cv2 };

            ConceptVersionDTO cv1a = new ConceptVersionDTO(
                new Guid[] { this.g1 },
                new StampDTO(
                    new Guid[] { this.g2 },
                    time,
                    new Guid[] { this.g3 },
                    new Guid[] { this.g4 },
                    new Guid[] { this.h1 }
                ));

            ConceptVersionDTO cv2a = new ConceptVersionDTO(
                new Guid[] { this.g2 },
                new StampDTO(
                    new Guid[] { this.g3 },
                    time,
                    new Guid[] { this.g4 },
                    new Guid[] { this.j1 },
                    new Guid[] { this.j2 }
                ));
            ConceptVersionDTO[] conceptVersionsBaseA =
                new ConceptVersionDTO[] { cv1a, cv2a };

            ConceptChronologyDTO dtoStart = new ConceptChronologyDTO(
                new Guid[] {this.g1, this.g2, this.g3, this.g4},
                new Guid[] {this.h1, this.h2, this.h3, this.h4},
                conceptVersionsBase);
            Compare(dtoStart.ComponentUuids, this.g1, this.g2, this.g3, this.g4);
            Compare(dtoStart.ChronologySetUuids, this.h1, this.h2, this.h3, this.h4);
            Compare(dtoStart.ConceptVersions,
                new ConceptVersionDTO[] { cv1a, cv2a });
        }

        [Fact]
        public void ConceptChronologyDTOIsEquivalentTest()
        {
            DateTime time = new DateTime(1920, 4, 5);

            ConceptVersionDTO cv1 = new ConceptVersionDTO(
                new Guid[] { this.g1 },
                new StampDTO(
                    new Guid[] { this.g2 },
                    time,
                    new Guid[] { this.g3 },
                    new Guid[] { this.g4 },
                    new Guid[] { this.h1 }
                ));

            ConceptVersionDTO cv2 = new ConceptVersionDTO(
                new Guid[] { this.g2 },
                new StampDTO(
                    new Guid[] { this.g3 },
                    time,
                    new Guid[] { this.g4 },
                    new Guid[] { this.j1 },
                    new Guid[] { this.j2 }
                ));
            ConceptVersionDTO[] conceptVersionsBase =
                new ConceptVersionDTO[] { cv1, cv2 };

            ConceptVersionDTO cv1a = new ConceptVersionDTO(
                new Guid[] { this.g1 },
                new StampDTO(
                    new Guid[] { this.g2 },
                    time,
                    new Guid[] { this.g3 },
                    new Guid[] { this.g4 },
                    new Guid[] { this.h1 }
                ));

            ConceptVersionDTO cv2a = new ConceptVersionDTO(
                new Guid[] { this.g2 },
                new StampDTO(
                    new Guid[] { this.g3 },
                    time,
                    new Guid[] { this.g4 },
                    new Guid[] { this.j1 },
                    new Guid[] { this.j2 }
                ));
            ConceptVersionDTO[] conceptVersionsBaseA =
                new ConceptVersionDTO[] { cv1a, cv2a };

            {
                ConceptChronologyDTO a = new ConceptChronologyDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    conceptVersionsBase
                );
                ConceptChronologyDTO b = new ConceptChronologyDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    conceptVersionsBaseA
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
                    conceptVersionsBaseA
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                ConceptChronologyDTO a = new ConceptChronologyDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    conceptVersionsBaseA
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
            //ConceptChronologyDTO dtoStart = new ConceptChronologyDTO(
            //    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
            //    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
            //    new Guid[] { this.i1, this.i2, this.i3, this.i4 }
            //);

            //MemoryStream ms = new MemoryStream();
            //TinkarOutput output = new TinkarOutput(ms);
            //dtoStart.Marshal(output);

            //ms.Position = 0;
            //TinkarInput input = new TinkarInput(ms);
            //ConceptChronologyDTO dtoRead = ConceptChronologyDTO.Make(input);
            //Assert.True(dtoStart.IsEquivalent(dtoRead));
        }
    }
}
