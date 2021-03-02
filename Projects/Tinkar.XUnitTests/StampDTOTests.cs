using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;

namespace Tinkar.XUnitTests
{
    public class StampDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void StampDTOFieldsTest()
        {
            DateTime time = new DateTime(2020, 12, 31);
            StampDTO dtoStart = new StampDTO(
                new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                time,
                new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 },
                new Guid[] { Misc.j1, Misc.j2, Misc.j3, Misc.j4 }
                );
            Misc.Compare(dtoStart.StatusPublicId, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Misc.Compare(dtoStart.AuthorPublicId, Misc.h1, Misc.h2, Misc.h3, Misc.h4);
            Misc.Compare(dtoStart.ModulePublicId, Misc.i1, Misc.i2, Misc.i3, Misc.i4);
            Misc.Compare(dtoStart.PathPublicId, Misc.j1, Misc.j2, Misc.j3, Misc.j4);
            Assert.True(dtoStart.Time == time);
        }

        [DoNotParallelize]
        [Fact]
        public void StampDTOIsEquivalentTest()
        {
            DateTime time = new DateTime(2020, 12, 31);
            {
                StampDTO a = new StampDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    time,
                    new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                    new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 },
                    new Guid[] { Misc.j1, Misc.j2, Misc.j3, Misc.j4 }
                );
                StampDTO b = new StampDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    time,
                    new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                    new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 },
                    new Guid[] { Misc.j1, Misc.j2, Misc.j3, Misc.j4 }
                );
                Assert.True(a.IsEquivalent(b));
            }

            {
                StampDTO a = new StampDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    time,
                    new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                    new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 },
                    new Guid[] { Misc.j1, Misc.j2, Misc.j3, Misc.j4 }
                );
                StampDTO b = new StampDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    new DateTime(2001, 1, 1),
                    new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                    new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 },
                    new Guid[] { Misc.j1, Misc.j2, Misc.j3, Misc.j4 }
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                StampDTO a = new StampDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    time,
                    new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                    new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 },
                    new Guid[] { Misc.j1, Misc.j2, Misc.j3, Misc.j4 }
                );
                StampDTO b = new StampDTO(
                    new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 },
                    time,
                    new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                    new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 },
                    new Guid[] { Misc.j1, Misc.j2, Misc.j3, Misc.j4 }
                );
                Assert.False(a.IsEquivalent(b));
            }
            {
                StampDTO a = new StampDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    time,
                    new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                    new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 },
                    new Guid[] { Misc.j1, Misc.j2, Misc.j3, Misc.j4 }
                );
                StampDTO b = new StampDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    time,
                    new Guid[] { Misc.h2, Misc.h2, Misc.h3, Misc.h4 },
                    new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 },
                    new Guid[] { Misc.j1, Misc.j2, Misc.j3, Misc.j4 }
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                StampDTO a = new StampDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    time,
                    new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                    new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 },
                    new Guid[] { Misc.j1, Misc.j2, Misc.j3, Misc.j4 }
                );
                StampDTO b = new StampDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    time,
                    new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                    new Guid[] { Misc.i2, Misc.i2, Misc.i3, Misc.i4 },
                    new Guid[] { Misc.j1, Misc.j2, Misc.j3, Misc.j4 }
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                StampDTO a = new StampDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    time,
                    new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                    new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 },
                    new Guid[] { Misc.j1, Misc.j2, Misc.j3, Misc.j4 }
                );
                StampDTO b = new StampDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    time,
                    new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                    new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 },
                    new Guid[] { Misc.j2, Misc.j2, Misc.j3, Misc.j4 }
                );
                Assert.False(a.IsEquivalent(b));
            }
        }

        [DoNotParallelize]
        [Fact]
        public void StampDTOMarshalTest()
        {
            DateTime time = new DateTime(2020, 12, 31);

            StampDTO dtoStart = new StampDTO(
                new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                time,
                new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 },
                new Guid[] { Misc.j1, Misc.j2, Misc.j3, Misc.j4 }
            );

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                dtoStart.Marshal(output);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                StampDTO dtoEnd = StampDTO.Make(input);
                Assert.True(dtoStart.IsEquivalent(dtoEnd));
            }
        }

        [DoNotParallelize]
        [Fact]
        public void StampDTOJsonMarshalTest()
        {
            DateTime time = new DateTime(2020, 12, 31);

            StampDTO dtoStart = new StampDTO(
                new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                time,
                new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 },
                new Guid[] { Misc.j1, Misc.j2, Misc.j3, Misc.j4 }
            );

            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
            {
                dtoStart.Marshal(output);
            }

            ms.Position = 0;
            TinkarJsonInput input = new TinkarJsonInput(ms);
            StampDTO dtoRead = StampDTO.Make(input.ReadJsonObject());
            Assert.True(dtoStart.IsEquivalent(dtoRead));
        }
    }
}
