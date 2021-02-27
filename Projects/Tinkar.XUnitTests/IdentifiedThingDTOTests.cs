using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;

namespace Tinkar.XUnitTests
{
    public class IdentifiedThingDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void IdentifiedThingDTOFieldsTest()
        {
            ConponentDTO dtoStart = new IdentifiedThingDTO(new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 });
            Misc.Compare(dtoStart.ComponentUuids, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
        }

        [DoNotParallelize]
        [Fact]
        public void IdentifiedThingDTOIsEquivalentTest()
        {
            {
                ConponentDTO a = new IdentifiedThingDTO(new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 });
                ConponentDTO b = new IdentifiedThingDTO(new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 });
                Assert.True(a.IsEquivalent(b));
            }

            {
                ConponentDTO a = new IdentifiedThingDTO(new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 });
                ConponentDTO b = new IdentifiedThingDTO(new Guid[] { Misc.g2, Misc.g1, Misc.g3, Misc.g4 });
                Assert.False(a.IsEquivalent(b));
            }
        }
    }
}
