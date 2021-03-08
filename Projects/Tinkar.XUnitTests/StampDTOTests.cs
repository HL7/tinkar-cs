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
                Misc.PublicIdG,
                Misc.PublicIdH,
                time,
                Misc.PublicIdI,
                Misc.PublicIdJ,
                Misc.PublicIdK
                );
            Misc.Compare(dtoStart.PublicId, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Misc.Compare(dtoStart.StatusPublicId, Misc.h1, Misc.h2, Misc.h3, Misc.h4);
            Misc.Compare(dtoStart.AuthorPublicId, Misc.i1, Misc.i2, Misc.i3, Misc.i4);
            Misc.Compare(dtoStart.ModulePublicId, Misc.j1, Misc.j2, Misc.j3, Misc.j4);
            Misc.Compare(dtoStart.PathPublicId, Misc.k1, Misc.k2, Misc.k3, Misc.k4);
            Assert.True(dtoStart.Time == time);
        }

        [DoNotParallelize]
        [Fact]
        public void StampDTOIsEquivalentTest()
        {
            DateTime time = new DateTime(2020, 12, 31);
            {
                StampDTO a = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                StampDTO b = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                Assert.True(a.IsEquivalent(b));
            }

            {
                StampDTO a = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                StampDTO b = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    new DateTime(2001, 1, 1),
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                StampDTO a = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                StampDTO b = new StampDTO(
                    new PublicId(Misc.other),
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                Assert.False(a.IsEquivalent(b));
            }
            {
                StampDTO a = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                StampDTO b = new StampDTO(
                    Misc.PublicIdG,
                    new PublicId(Misc.other),
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                StampDTO a = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                StampDTO b = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    new PublicId(Misc.other),
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                StampDTO a = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                StampDTO b = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    new PublicId(Misc.other),
                    Misc.PublicIdK
                );
                Assert.False(a.IsEquivalent(b));
            }


            {
                StampDTO a = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                StampDTO b = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    new PublicId(Misc.other)
                );
                Assert.False(a.IsEquivalent(b));
            }
        }


        [DoNotParallelize]
        [Fact]
        public void StampDTOIsSameTest()
        {
            DateTime time = new DateTime(2020, 12, 31);
            {
                StampDTO a = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                StampDTO b = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                StampDTO a = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                StampDTO b = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    new DateTime(2001, 1, 1),
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                StampDTO a = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                StampDTO b = new StampDTO(
                    new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4),
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                Assert.False(a.CompareTo(b) == 0);
            }
            {
                StampDTO a = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                StampDTO b = new StampDTO(
                    Misc.PublicIdG,
                    new PublicId(Misc.h2, Misc.h2, Misc.h3, Misc.h4),
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                StampDTO a = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                StampDTO b = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    new PublicId(Misc.i2, Misc.i2, Misc.i3, Misc.i4),
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                StampDTO a = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                StampDTO b = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    new PublicId(Misc.j2, Misc.j2, Misc.j3, Misc.j4),
                    Misc.PublicIdK
                );
                Assert.False(a.CompareTo(b) == 0);
            }


            {
                StampDTO a = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    Misc.PublicIdK
                );
                StampDTO b = new StampDTO(
                    Misc.PublicIdG,
                    Misc.PublicIdH,
                    time,
                    Misc.PublicIdI,
                    Misc.PublicIdJ,
                    new PublicId(Misc.k1, Misc.k2, Misc.k3, Misc.k3)
                );
                Assert.False(a.CompareTo(b) == 0);
            }
        }



        [DoNotParallelize]
        [Fact]
        public void StampDTOMarshalTest()
        {
            DateTime time = new DateTime(2020, 12, 31);

            StampDTO dtoStart = new StampDTO(
                Misc.PublicIdG,
                Misc.PublicIdH,
                time,
                Misc.PublicIdI,
                Misc.PublicIdJ,
                Misc.PublicIdK
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
                Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
            }
        }

        [DoNotParallelize]
        [Fact]
        public void StampDTOJsonMarshalTest()
        {
            DateTime time = new DateTime(2020, 12, 31);

            StampDTO dtoStart = new StampDTO(
                Misc.PublicIdG,
                Misc.PublicIdH,
                time,
                Misc.PublicIdI,
                Misc.PublicIdJ,
                Misc.PublicIdK
            );

            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
            {
                dtoStart.Marshal(output);
            }

            ms.Position = 0;
            TinkarJsonInput input = new TinkarJsonInput(ms);
            StampDTO dtoRead = StampDTO.Make(input.ReadJsonObject());
            Assert.True(dtoStart.CompareTo(dtoRead) == 0);
        }
    }
}
