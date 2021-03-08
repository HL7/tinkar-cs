using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;

namespace Tinkar.XUnitTests
{
    public class LocationTests
    {
        [DoNotParallelize]
        [Fact]
        public void PalanarPointTest()
        {
            {
                PlanarPoint pp = new PlanarPoint(10, 20);
                Assert.True(pp.X == 10);
                Assert.True(pp.Y == 20);
                {
                    PlanarPoint other = new PlanarPoint(10, 20);
                    Assert.True(pp.CompareTo(other) == 0);
                }
                {
                    PlanarPoint other = new PlanarPoint(100, 20);
                    Assert.True(pp.CompareTo(other) < 0);
                }
                {
                    PlanarPoint other = new PlanarPoint(1, 20);
                    Assert.True(pp.CompareTo(other) > 0);
                }
                {
                    PlanarPoint other = new PlanarPoint(10, 200);
                    Assert.True(pp.CompareTo(other) < 0);
                }
                {
                    PlanarPoint other = new PlanarPoint(10, 2);
                    Assert.True(pp.CompareTo(other) > 0);
                }
            }
        }

        [DoNotParallelize]
        [Fact]
        public void SpatialPointTest()
        {
            {
                SpatialPoint pp = new SpatialPoint(10, 20, 30);
                Assert.True(pp.X == 10);
                Assert.True(pp.Y == 20);
                Assert.True(pp.Z == 30);
                {
                    SpatialPoint other = new SpatialPoint(10, 20, 30);
                    Assert.True(pp.CompareTo(other) == 0);
                }
                {
                    SpatialPoint other = new SpatialPoint(100, 20, 30);
                    Assert.True(pp.CompareTo(other) < 0);
                }
                {
                    SpatialPoint other = new SpatialPoint(1, 20, 30);
                    Assert.True(pp.CompareTo(other) > 0);
                }
                {
                    SpatialPoint other = new SpatialPoint(10, 200, 30);
                    Assert.True(pp.CompareTo(other) < 0);
                }
                {
                    SpatialPoint other = new SpatialPoint(10, 2, 30);
                    Assert.True(pp.CompareTo(other) > 0);
                }
                {
                    SpatialPoint other = new SpatialPoint(10, 20, 300);
                    Assert.True(pp.CompareTo(other) < 0);
                }
                {
                    SpatialPoint other = new SpatialPoint(10, 20, 3);
                    Assert.True(pp.CompareTo(other) > 0);
                }
            }
        }

    }
}
