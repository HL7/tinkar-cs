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
                Assert.True(FieldCompare.Equivalent(CreateConceptChronologyDTO,
                                                    CreateConceptChronologyDTO));
                Assert.False(FieldCompare.Equivalent(
                    CreateConceptChronologyDTO,
                    CreateConceptChronologyDTO
                    with
                    {
                        ComponentUuids = new Guid[] { this.g2, this.g2, this.g3, this.g4 }
                    }));
            }
            {
                Assert.True(FieldCompare.Equivalent(CreateConceptDTO,
                CreateConceptDTO));
                Assert.False(FieldCompare.Equivalent(
                    CreateConceptDTO,
                    CreateConceptDTO
                    with
                    {
                        ComponentUuids = new Guid[] { this.g2, this.g2, this.g3, this.g4 }
                    }));
            }
            {
                Assert.True(FieldCompare.Equivalent(CreateDefinitionForSemanticChronologyDTO,
                    CreateDefinitionForSemanticChronologyDTO));
                Assert.False(FieldCompare.Equivalent(
                    CreateDefinitionForSemanticChronologyDTO,
                    CreateDefinitionForSemanticChronologyDTO
                with
                    {
                        ComponentUuids = new Guid[] { this.g2, this.g2, this.g3, this.g4 }
                    }));
            }
            {
                Assert.True(FieldCompare.Equivalent(CreateDefinitionForSemanticDTO,
                    CreateDefinitionForSemanticDTO));
                Assert.False(FieldCompare.Equivalent(
                    CreateDefinitionForSemanticDTO,
                    CreateDefinitionForSemanticDTO
                with
                    {
                        ComponentUuids = new Guid[] { this.g2, this.g2, this.g3, this.g4 }
                    }));
            }
            {
                Assert.True(FieldCompare.Equivalent(CreateSemanticChronologyDTO,
                    CreateSemanticChronologyDTO));
                Assert.False(FieldCompare.Equivalent(
                    CreateSemanticChronologyDTO,
                    CreateSemanticChronologyDTO
                with
                    {
                        ComponentUuids = new Guid[] { this.g2, this.g2, this.g3, this.g4 }
                    }));
            }
            {
                Assert.True(FieldCompare.Equivalent(CreateSemanticDTO,
                    CreateSemanticDTO));
                Assert.False(FieldCompare.Equivalent(
                    CreateSemanticDTO,
                    CreateSemanticDTO
                with
                    {
                        ComponentUuids = new Guid[] { this.g2, this.g2, this.g3, this.g4 }
                    }));
            }
            {
                Object[] AArr() => new object[] {"a", 2, 0.1F};
                Object[] BArr() => new object[] { "a", 2, "ggg" };

                Assert.True(FieldCompare.Equivalent(AArr(), AArr()));
                Assert.False(FieldCompare.Equivalent(AArr(), BArr()));
            }
        }
    }
}
