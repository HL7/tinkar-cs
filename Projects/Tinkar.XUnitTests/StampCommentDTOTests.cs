using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;

namespace Tinkar.XUnitTests
{
    public class StampCommentDTOTests
    {
        [Fact]
        public void StampCommentDTOFieldsTest()
        {
            DateTime time = new DateTime(2020, 12, 31);
            StampCommentDTO dtoStart = new StampCommentDTO(
                Misc.CreateStampDTO,
                "xxyyz");
            Assert.True(dtoStart.Comment == "xxyyz");
            Assert.True(dtoStart.StampDTO.IsEquivalent(Misc.CreateStampDTO));
        }

        [Fact]
        public void StampCommentDTOIsEquivalentTest()
        {
            DateTime time = new DateTime(2020, 12, 31);
            {
                StampCommentDTO a = new StampCommentDTO(
                    Misc.CreateStampDTO,
                    "xxyyz"
                );
                StampCommentDTO b = new StampCommentDTO(
                    Misc.CreateStampDTO,
                    "xxyyz"
                );
                Assert.True(a.IsEquivalent(b));
            }

            {
                StampCommentDTO a = new StampCommentDTO(
                    Misc.CreateStampDTO,
                    "xxyyz"
                );
                StampCommentDTO b = new StampCommentDTO(
                    Misc.CreateStampDTO with { StatusUuids = new Guid[] { Misc.g2, Misc.g3, Misc.g4 } },
                    "xxyyz"
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                StampCommentDTO a = new StampCommentDTO(
                    Misc.CreateStampDTO,
                    "xxyyz"
                );
                StampCommentDTO b = new StampCommentDTO(
                    Misc.CreateStampDTO,
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
                Misc.CreateStampDTO,
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
