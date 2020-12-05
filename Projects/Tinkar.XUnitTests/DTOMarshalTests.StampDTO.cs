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
        public void StampDTOFieldsTest()
        {
            DateTime time = new DateTime(2020, 12, 31);
            StampDTO dtoStart = new StampDTO(
                new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                time,
                new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                new Guid[] { this.j1, this.j2, this.j3, this.j4 }
                );
            Compare(dtoStart.StatusUuids, this.g1, this.g2, this.g3, this.g4);
            Compare(dtoStart.AuthorUuids, this.h1, this.h2, this.h3, this.h4);
            Compare(dtoStart.ModuleUuids, this.i1, this.i2, this.i3, this.i4);
            Compare(dtoStart.PathUuids, this.j1, this.j2, this.j3, this.j4);
            Assert.True(dtoStart.Time == time);
        }

        [Fact]
        public void StampDTOIsEquivalentTest()
        {
            DateTime time = new DateTime(2020, 12, 31);
            {
                StampDTO a = new StampDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    time,
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                    new Guid[] { this.j1, this.j2, this.j3, this.j4 }
                );
                StampDTO b = new StampDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    time,
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                    new Guid[] { this.j1, this.j2, this.j3, this.j4 }
                );
                Assert.True(a.IsEquivalent(b));
            }

            {
                StampDTO a = new StampDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    time,
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                    new Guid[] { this.j1, this.j2, this.j3, this.j4 }
                );
                StampDTO b = new StampDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new DateTime(2001, 1, 1),
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                    new Guid[] { this.j1, this.j2, this.j3, this.j4 }
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                StampDTO a = new StampDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    time,
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                    new Guid[] { this.j1, this.j2, this.j3, this.j4 }
                );
                StampDTO b = new StampDTO(
                    new Guid[] { this.g2, this.g2, this.g3, this.g4 },
                    time,
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                    new Guid[] { this.j1, this.j2, this.j3, this.j4 }
                );
                Assert.False(a.IsEquivalent(b));
            }
            {
                StampDTO a = new StampDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    time,
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                    new Guid[] { this.j1, this.j2, this.j3, this.j4 }
                );
                StampDTO b = new StampDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    time,
                    new Guid[] { this.h2, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                    new Guid[] { this.j1, this.j2, this.j3, this.j4 }
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                StampDTO a = new StampDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    time,
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                    new Guid[] { this.j1, this.j2, this.j3, this.j4 }
                );
                StampDTO b = new StampDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    time,
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i2, this.i2, this.i3, this.i4 },
                    new Guid[] { this.j1, this.j2, this.j3, this.j4 }
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                StampDTO a = new StampDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    time,
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                    new Guid[] { this.j1, this.j2, this.j3, this.j4 }
                );
                StampDTO b = new StampDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    time,
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                    new Guid[] { this.j2, this.j2, this.j3, this.j4 }
                );
                Assert.False(a.IsEquivalent(b));
            }
        }

        [Fact]
        public void StampDTOMarshalTest()
        {
            DateTime time = new DateTime(2020, 12, 31);

            StampDTO dtoStart = new StampDTO(
                new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                time,
                new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                new Guid[] { this.j1, this.j2, this.j3, this.j4 }
            );

            MemoryStream ms = new MemoryStream();
            TinkarOutput output = new TinkarOutput(ms);
            dtoStart.Marshal(output);

            ms.Position = 0;
            TinkarInput input = new TinkarInput(ms);
            StampDTO dtoRead = StampDTO.Make(input);
            Assert.True(dtoStart.IsEquivalent(dtoRead));
        }
    }
}
