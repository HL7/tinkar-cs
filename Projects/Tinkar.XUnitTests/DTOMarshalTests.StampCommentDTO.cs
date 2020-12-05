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
        public void StampCommentDTOFieldsTest()
        {
            DateTime time = new DateTime(2020, 12, 31);
            StampCommentDTO dtoStart = new StampCommentDTO(
                new StampDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    time,
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                    new Guid[] { this.j1, this.j2, this.j3, this.j4 }),
                    "xxyyz"
                );
            Assert.True(dtoStart.Comment == "xxyyz");
            Assert.True(dtoStart.StampDTO.IsEquivalent(
                new StampDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    time,
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                    new Guid[] { this.j1, this.j2, this.j3, this.j4 })
                ));
        }

        [Fact]
        public void StampCommentDTOIsEquivalentTest()
        {
            DateTime time = new DateTime(2020, 12, 31);
            {
                StampCommentDTO a = new StampCommentDTO(
                    new StampDTO(
                        new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                        time,
                        new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                        new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                        new Guid[] { this.j1, this.j2, this.j3, this.j4 }),
                    "xxyyz"
                );
                StampCommentDTO b = new StampCommentDTO(
                    new StampDTO(
                        new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                        time,
                        new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                        new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                        new Guid[] { this.j1, this.j2, this.j3, this.j4 }),
                    "xxyyz"
                );
                Assert.True(a.IsEquivalent(b));
            }

            {
                StampCommentDTO a = new StampCommentDTO(
                    new StampDTO(
                        new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                        time,
                        new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                        new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                        new Guid[] { this.j1, this.j2, this.j3, this.j4 }),
                    "xxyyz"
                );
                StampCommentDTO b = new StampCommentDTO(
                    new StampDTO(
                        new Guid[] { this.g2, this.g3, this.g4 },
                        time,
                        new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                        new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                        new Guid[] { this.j1, this.j2, this.j3, this.j4 }),
                    "xxyyz"
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                StampCommentDTO a = new StampCommentDTO(
                    new StampDTO(
                        new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                        time,
                        new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                        new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                        new Guid[] { this.j1, this.j2, this.j3, this.j4 }),
                    "xxyyz"
                );
                StampCommentDTO b = new StampCommentDTO(
                    new StampDTO(
                        new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                        time,
                        new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                        new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                        new Guid[] { this.j1, this.j2, this.j3, this.j4 }),
                    "xxyyz1"
                );
                Assert.False(a.IsEquivalent(b));
            }
        }

        [Fact]
        public void StampCommentDTOMarshalTest()
        {
            DateTime time = new DateTime(2020, 12, 31);

            StampCommentDTO dtoStart = new StampCommentDTO(
                new StampDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    time,
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 },
                    new Guid[] { this.j1, this.j2, this.j3, this.j4 }),
                "xxyyz"
            );

            MemoryStream ms = new MemoryStream();
            TinkarOutput output = new TinkarOutput(ms);
            dtoStart.Marshal(output);

            ms.Position = 0;
            TinkarInput input = new TinkarInput(ms);
            StampCommentDTO dtoRead = StampCommentDTO.Make(input);
            Assert.True(dtoStart.IsEquivalent(dtoRead));
        }
    }
}
