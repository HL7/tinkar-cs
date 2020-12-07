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
        [Fact]
        public void StampCommentDTOFieldsTest()
        {
            DateTime time = new DateTime(2020, 12, 31);
            StampCommentDTO dtoStart = new StampCommentDTO(
                CreateStampDTO,
                "xxyyz");
            Assert.True(dtoStart.Comment == "xxyyz");
            Assert.True(dtoStart.StampDTO.IsEquivalent(CreateStampDTO));
        }

        [Fact]
        public void StampCommentDTOIsEquivalentTest()
        {
            DateTime time = new DateTime(2020, 12, 31);
            {
                StampCommentDTO a = new StampCommentDTO(
                    CreateStampDTO,
                    "xxyyz"
                );
                StampCommentDTO b = new StampCommentDTO(
                    CreateStampDTO,
                    "xxyyz"
                );
                Assert.True(a.IsEquivalent(b));
            }

            {
                StampCommentDTO a = new StampCommentDTO(
                    CreateStampDTO,
                    "xxyyz"
                );
                StampCommentDTO b = new StampCommentDTO(
                    CreateStampDTO with { StatusUuids = new Guid[] { this.g2, this.g3, this.g4 } },
                    "xxyyz"
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                StampCommentDTO a = new StampCommentDTO(
                    CreateStampDTO,
                    "xxyyz"
                );
                StampCommentDTO b = new StampCommentDTO(
                    CreateStampDTO,
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
                CreateStampDTO,
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
