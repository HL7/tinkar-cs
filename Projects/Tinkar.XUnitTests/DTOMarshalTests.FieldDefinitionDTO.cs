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
        public void FieldDefinitionDTOFieldsTest()
        {
            FieldDefinitionDTO dtoStart = new FieldDefinitionDTO(
                new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                new Guid[] { this.i1, this.i2, this.i3, this.i4 }
                );
            Compare(dtoStart.DataTypeUuids, this.g1, this.g2, this.g3, this.g4);
            Compare(dtoStart.PurposeUuids, this.h1, this.h2, this.h3, this.h4);
            Compare(dtoStart.UseUuids, this.i1, this.i2, this.i3, this.i4);
        }

        [Fact]
        public void FieldDefinitionDTOIsEquivalentTest()
        {
            {
                FieldDefinitionDTO a = new FieldDefinitionDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 }
                );
                FieldDefinitionDTO b = new FieldDefinitionDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 }
                );
                Assert.True(a.IsEquivalent(b));
            }

            {
                FieldDefinitionDTO a = new FieldDefinitionDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 }
                );
                FieldDefinitionDTO b = new FieldDefinitionDTO(
                    new Guid[] { this.g2, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 }
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                FieldDefinitionDTO a = new FieldDefinitionDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 }
                );
                FieldDefinitionDTO b = new FieldDefinitionDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h3, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 }
                );
                Assert.False(a.IsEquivalent(b));
            }
            
            {
                FieldDefinitionDTO a = new FieldDefinitionDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i4 }
                );
                FieldDefinitionDTO b = new FieldDefinitionDTO(
                    new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                    new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                    new Guid[] { this.i1, this.i2, this.i3, this.i3 }
                );
                Assert.False(a.IsEquivalent(b));
            }
        }

        [Fact]
        public void FieldDefinitionDTOMarshalTest()
        {
            FieldDefinitionDTO dtoStart = new FieldDefinitionDTO(
                new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                new Guid[] { this.i1, this.i2, this.i3, this.i4 }
            );

            MemoryStream ms = new MemoryStream();
            TinkarOutput output = new TinkarOutput(ms);
            dtoStart.Marshal(output);

            ms.Position = 0;
            TinkarInput input = new TinkarInput(ms);
            FieldDefinitionDTO dtoRead = FieldDefinitionDTO.Make(input);
            Assert.True(dtoStart.IsEquivalent(dtoRead));
        }
    }
}
