using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;
using Assert = Xunit.Assert;

namespace Tinkar.XUnitTests
{
    public class FieldCompareTests
    {
        [DoNotParallelize]
        [Fact]
        public void FieldCompareTest()
        {
            {
                Assert.True(FieldCompare.Equivalent("abc", "abc"));
                Assert.False(FieldCompare.Equivalent("abc", "abcd"));
            }
            {
                Assert.True(FieldCompare.Equivalent(0, 0));
                Assert.False(FieldCompare.Equivalent(0, 1));
            }
            {
                Assert.True(FieldCompare.Equivalent(0.1F, 0.1F));
                Assert.False(FieldCompare.Equivalent(0.0F, 1.0F));
            }
            {
                Assert.True(FieldCompare.Equivalent(true, true));
                Assert.False(FieldCompare.Equivalent(true, false));
            }
            {
                Assert.True(FieldCompare.Equivalent(new DateTime(2020, 1, 1), new DateTime(2020, 1, 1)));
                Assert.False(FieldCompare.Equivalent(new DateTime(2020, 1, 1), new DateTime(2020, 2, 1)));
            }
            {
                Assert.True(FieldCompare.Equivalent(new byte[] { 1, 2, 3 },
                                                    new byte[] { 1, 2, 3 }));
                Assert.False(FieldCompare.Equivalent(new byte[] { 1, 2, 3 },
                                                    new byte[] { 1, 3, 2 }));
            }
            {
                Assert.True(FieldCompare.Equivalent(Misc.CreateConceptChronologyDTO,
                    Misc.CreateConceptChronologyDTO));
                Assert.False(FieldCompare.Equivalent(
                    Misc.CreateConceptChronologyDTO,
                    Misc.CreateConceptChronologyDTO
                    with
                    {
                        ComponentUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                    }));
            }
            {
                Assert.True(FieldCompare.Equivalent(
                    Misc.CreateConceptDTO,
                    Misc.CreateConceptDTO));
                Assert.False(FieldCompare.Equivalent(
                    Misc.CreateConceptDTO,
                    Misc.CreateConceptDTO
                    with
                    {
                        ComponentUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                    }));
            }
            {
                Assert.True(FieldCompare.Equivalent(Misc.CreateDefinitionForSemanticChronologyDTO,
                    Misc.CreateDefinitionForSemanticChronologyDTO));
                Assert.False(FieldCompare.Equivalent(
                    Misc.CreateDefinitionForSemanticChronologyDTO,
                    Misc.CreateDefinitionForSemanticChronologyDTO
                with
                    {
                        ComponentUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                    }));
            }
            {
                Assert.True(FieldCompare.Equivalent(
                    Misc.CreateDefinitionForSemanticDTO,
                    Misc.CreateDefinitionForSemanticDTO));
                Assert.False(FieldCompare.Equivalent(
                    Misc.CreateDefinitionForSemanticDTO,
                    Misc.CreateDefinitionForSemanticDTO
                with
                    {
                        ComponentUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                    }));
            }
            {
                Assert.True(FieldCompare.Equivalent(
                    Misc.CreateSemanticChronologyDTO,
                    Misc.CreateSemanticChronologyDTO));
                Assert.False(FieldCompare.Equivalent(
                    Misc.CreateSemanticChronologyDTO,
                    Misc.CreateSemanticChronologyDTO
                with
                    {
                        ComponentUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                    }));
            }
            {
                Assert.True(FieldCompare.Equivalent(
                    Misc.CreateSemanticDTO,
                    Misc.CreateSemanticDTO));
                Assert.False(FieldCompare.Equivalent(
                    Misc.CreateSemanticDTO,
                    Misc.CreateSemanticDTO
                with
                    {
                        ComponentUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                    }));
            }
            {
                Object[] AArr() => new object[] { "a", 2, 0.1F };
                Object[] BArr() => new object[] { "a", 2, "ggg" };

                Assert.True(FieldCompare.Equivalent(AArr(), AArr()));
                Assert.False(FieldCompare.Equivalent(AArr(), BArr()));
            }
        }
    }
}
