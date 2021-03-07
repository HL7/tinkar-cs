using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;

namespace Tinkar.XUnitTests
{
    public class ComponentDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void ComponentDTOFieldsTest()
        {
            ComponentDTO dtoStart = new ComponentDTO(Misc.PublicIdG);
            Misc.Compare(dtoStart.PublicId, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
        }

        [DoNotParallelize]
        [Fact]
        public void ComponentDTOIsEquivalentTest()
        {
            {
                ComponentDTO a = new ComponentDTO(Misc.PublicIdG);
                ComponentDTO b = new ComponentDTO(Misc.PublicIdG);
                Assert.True(a.IsEquivalent(b));
            }

            {
                ComponentDTO a = new ComponentDTO(Misc.PublicIdG);
                ComponentDTO b = new ComponentDTO(new PublicId(Misc.other));
                Assert.False(a.IsEquivalent(b));
            }
        }

        [DoNotParallelize]
        [Fact]
        public void ComponentDTOIsSameTest()
        {
            {
                ComponentDTO a = new ComponentDTO(Misc.PublicIdG);
                ComponentDTO b = new ComponentDTO(Misc.PublicIdG);
                Assert.True(a.IsSame(b) == 0);
            }

            {
                ComponentDTO a = new ComponentDTO(Misc.PublicIdG);
                ComponentDTO b = new ComponentDTO(new PublicId(Misc.g2, Misc.g1, Misc.g3, Misc.g4));
                Assert.False(a.IsSame(b) != 0);
            }
        }
    }
}
