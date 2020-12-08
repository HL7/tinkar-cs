using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Tinkar.XUnitTests
{
    public class IdentifiedThingDTOTests
    {
        [Fact]
        public void IdentifiedThingDTOFieldsTest()
        {
            IdentifiedThingDTO dtoStart = new IdentifiedThingDTO(new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 });
            Misc.Compare(dtoStart.ComponentUuids, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
        }

        [Fact]
        public void IdentifiedThingDTOIsEquivalentTest()
        {
            {
                IdentifiedThingDTO a = new IdentifiedThingDTO(new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 });
                IdentifiedThingDTO b = new IdentifiedThingDTO(new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 });
                Assert.True(a.IsEquivalent(b));
            }

            {
                IdentifiedThingDTO a = new IdentifiedThingDTO(new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 });
                IdentifiedThingDTO b = new IdentifiedThingDTO(new Guid[] { Misc.g2, Misc.g1, Misc.g3, Misc.g4 });
                Assert.False(a.IsEquivalent(b));
            }
        }
    }
}
