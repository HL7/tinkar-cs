using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Tinkar.XUnitTests
{
    public class SemanticDTOTests
    {
        [Fact]
        public void SemanticDTOFieldsTest()
        {
            SemanticDTO dtoStart = Misc.CreateSemanticDTO;
            Misc.Compare(dtoStart.ComponentUuids, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Misc.Compare(dtoStart.DefinitionForSemanticUuids, Misc.h1, Misc.h2, Misc.h3, Misc.h4);
            Misc.Compare(dtoStart.ReferencedComponentUuids, Misc.i1, Misc.i2, Misc.i3, Misc.i4);
        }

        [Fact]
        public void SemanticDTOIsEquivalentTest()
        {
            {
                SemanticDTO a = Misc.CreateSemanticDTO;
                SemanticDTO b = new SemanticDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                    new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 }
                );
                Assert.True(a.IsEquivalent(b));
            }

            {
                SemanticDTO a = Misc.CreateSemanticDTO;
                SemanticDTO b = new SemanticDTO(
                    new Guid[] { Misc.g2, Misc.g2, Misc.g3, Misc.g4 },
                    new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                    new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 }
                );
                Assert.False(a.IsEquivalent(b));
            }

            {
                SemanticDTO a = Misc.CreateSemanticDTO;
                SemanticDTO b = new SemanticDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    new Guid[] { Misc.h1, Misc.h3, Misc.h3, Misc.h4 },
                    new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i4 }
                );
                Assert.False(a.IsEquivalent(b));
            }
            
            {
                SemanticDTO a = Misc.CreateSemanticDTO;
                SemanticDTO b = new SemanticDTO(
                    new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 },
                    new Guid[] { Misc.h1, Misc.h2, Misc.h3, Misc.h4 },
                    new Guid[] { Misc.i1, Misc.i2, Misc.i3, Misc.i3 }
                );
                Assert.False(a.IsEquivalent(b));
            }
        }

        [Fact]
        public void SemanticDTOMarshalTest()
        {
            SemanticDTO dtoStart = Misc.CreateSemanticDTO;

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                output.WriteField(dtoStart);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                SemanticDTO dtoRead = (SemanticDTO) input.ReadField();
                Assert.True(dtoStart.IsEquivalent(dtoRead));
            }
        }
    }
}
