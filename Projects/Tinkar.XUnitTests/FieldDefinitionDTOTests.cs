using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Tinkar.XUnitTests
{
    public class FieldDefinitionDTOTests
    {
        [Fact]
        public void FieldDefinitionDTOFieldsTest()
        {
            FieldDefinitionDTO dtoStart = new FieldDefinitionDTO(
                new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 }
                );
            Misc.Compare(dtoStart.DataTypeUuids, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Misc.Compare(dtoStart.PurposeUuids, Misc.h1, Misc.h2, Misc.h3, Misc.h4);
            Misc.Compare(dtoStart.UseUuids, Misc.i1, Misc.i2, Misc.i3, Misc.i4);
        }

        [Fact]
        public void FieldDefinitionDTOIsEquivalentTest()
        {
            {
                FieldDefinitionDTO a = Misc.CreateFieldDefinition;
                FieldDefinitionDTO b = Misc.CreateFieldDefinition;
                Assert.True(a.IsEquivalent(b));
            }

            {
                FieldDefinitionDTO a = Misc.CreateFieldDefinition;
                FieldDefinitionDTO b = Misc.CreateFieldDefinition with 
                {
                    DataTypeUuids = new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 }
                }
                ;
                Assert.False(a.IsEquivalent(b));
            }

            {
                FieldDefinitionDTO a = Misc.CreateFieldDefinition;
                FieldDefinitionDTO b = Misc.CreateFieldDefinition with
                {
                    PurposeUuids = new Guid[] { Misc.h1, Misc.h3, Misc.h3, Misc.h4 }
                };
                Assert.False(a.IsEquivalent(b));
            }
            
            {
                FieldDefinitionDTO a = Misc.CreateFieldDefinition;
                FieldDefinitionDTO b = Misc.CreateFieldDefinition with
                {
                    UseUuids = new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i3 }
                };
                Assert.False(a.IsEquivalent(b));
            }
        }

        [Fact]
        public void FieldDefinitionDTOMarshalTest()
        {
            FieldDefinitionDTO dtoStart = new FieldDefinitionDTO(
                new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 }
            );

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                dtoStart.Marshal(output);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                FieldDefinitionDTO dtoRead = FieldDefinitionDTO.Make(input);
                Assert.True(dtoStart.IsEquivalent(dtoRead));
            }
        }
    }
}
