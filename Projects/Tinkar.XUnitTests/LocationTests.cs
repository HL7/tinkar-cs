using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;
using Tinkar.Dto;

namespace Tinkar.XUnitTests
{
    public class LocationTests
    {
        [DoNotParallelize]
        [Fact]
        public void PalanarPointTest()
        {
            {
                PlanarPointDTO pp = new PlanarPointDTO(10, 20);
                Assert.True(pp.X == 10);
                Assert.True(pp.Y == 20);
                {
                    PlanarPointDTO other = new PlanarPointDTO(10, 20);
                    Assert.True(pp.CompareTo(other) == 0);
                }
                {
                    PlanarPointDTO other = new PlanarPointDTO(100, 20);
                    Assert.True(pp.CompareTo(other) < 0);
                }
                {
                    PlanarPointDTO other = new PlanarPointDTO(1, 20);
                    Assert.True(pp.CompareTo(other) > 0);
                }
                {
                    PlanarPointDTO other = new PlanarPointDTO(10, 200);
                    Assert.True(pp.CompareTo(other) < 0);
                }
                {
                    PlanarPointDTO other = new PlanarPointDTO(10, 2);
                    Assert.True(pp.CompareTo(other) > 0);
                }
            }
        }

        [DoNotParallelize]
        [Fact]
        public void SpatialPointTest()
        {
            {
                SpatialPointDTO pp = new SpatialPointDTO(10, 20, 30);
                Assert.True(pp.X == 10);
                Assert.True(pp.Y == 20);
                Assert.True(pp.Z == 30);
                {
                    SpatialPointDTO other = new SpatialPointDTO(10, 20, 30);
                    Assert.True(pp.CompareTo(other) == 0);
                }
                {
                    SpatialPointDTO other = new SpatialPointDTO(100, 20, 30);
                    Assert.True(pp.CompareTo(other) < 0);
                }
                {
                    SpatialPointDTO other = new SpatialPointDTO(1, 20, 30);
                    Assert.True(pp.CompareTo(other) > 0);
                }
                {
                    SpatialPointDTO other = new SpatialPointDTO(10, 200, 30);
                    Assert.True(pp.CompareTo(other) < 0);
                }
                {
                    SpatialPointDTO other = new SpatialPointDTO(10, 2, 30);
                    Assert.True(pp.CompareTo(other) > 0);
                }
                {
                    SpatialPointDTO other = new SpatialPointDTO(10, 20, 300);
                    Assert.True(pp.CompareTo(other) < 0);
                }
                {
                    SpatialPointDTO other = new SpatialPointDTO(10, 20, 3);
                    Assert.True(pp.CompareTo(other) > 0);
                }
            }
        }

    }
}
