using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Xunit;
using Assert = Xunit.Assert;

namespace Tinkar.XUnitTests
{
    public class DateTimeTests
    {
        [DoNotParallelize]
        [Fact]
        public void InstantTest()
        {
            const Int64 nannoSecondsPerMilliSecond = 1000000L;

            {
                DateTime dt = new DateTime(1970, 1, 1);
                Assert.True(dt.EpochSecond() == 0);
                Assert.True(dt.Nano() == 0);
            }

            {
                DateTime dt = new DateTime(1970, 1, 1, 0, 0, 30, 10);
                Assert.True(dt.EpochSecond() == 30);
                Assert.True(dt.Nano() == 10 * nannoSecondsPerMilliSecond);
            }

            {
                DateTime dt = DateTimeExtensions.FromInstant(0, 0);
                Assert.True(dt == new DateTime(1970, 1, 1));
            }
        }
    }
}
