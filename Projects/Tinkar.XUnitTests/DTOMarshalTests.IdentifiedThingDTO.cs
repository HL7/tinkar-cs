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
        public void IdentifiedThingDTOFieldsTest()
        {
            IdentifiedThingDTO dtoStart = new IdentifiedThingDTO(new Guid[] { this.g1, this.g2, this.g3, this.g4 });
            Compare(dtoStart.ComponentUuids, this.g1, this.g2, this.g3, this.g4);
        }

        [Fact]
        public void IdentifiedThingDTOIsEquivalentTest()
        {
            {
                IdentifiedThingDTO a = new IdentifiedThingDTO(new Guid[] { this.g1, this.g2, this.g3, this.g4 });
                IdentifiedThingDTO b = new IdentifiedThingDTO(new Guid[] { this.g1, this.g2, this.g3, this.g4 });
                Assert.True(a.IsEquivalent(b));
            }

            {
                IdentifiedThingDTO a = new IdentifiedThingDTO(new Guid[] { this.g1, this.g2, this.g3, this.g4 });
                IdentifiedThingDTO b = new IdentifiedThingDTO(new Guid[] { this.g2, this.g1, this.g3, this.g4 });
                Assert.False(a.IsEquivalent(b));
            }
        }
    }
}
