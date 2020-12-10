using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Tinkar.XUnitTests
{
    public class DefinitionForSemanticDTOTests
    {
        [Fact]
        public void DefinitionForSemanticDTOFieldsTest()
        {
            DefinitionForSemanticDTO dtoStart = new DefinitionForSemanticDTO(new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 });
            Misc.Compare(dtoStart.ComponentUuids, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
        }

        [Fact]
        public void DefinitionForSemanticDTOIsEquivalentTest()
        {
            {
                DefinitionForSemanticDTO a = Misc.CreateDefinitionForSemanticDTO;
                DefinitionForSemanticDTO b = Misc.CreateDefinitionForSemanticDTO;
                Assert.True(a.IsEquivalent(b));
            }

            {
                DefinitionForSemanticDTO a = Misc.CreateDefinitionForSemanticDTO;
                DefinitionForSemanticDTO b = new DefinitionForSemanticDTO(new Guid[] { Misc.g2, Misc.g1, Misc.g3, Misc.g4 });
                Assert.False(a.IsEquivalent(b));
            }
        }

        [Fact]
        public void DefinitionForSemanticDTOMarshalTest()
        {
            DefinitionForSemanticDTO dtoStart = Misc.CreateDefinitionForSemanticDTO;

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                output.WriteField(dtoStart);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                DefinitionForSemanticDTO dtoRead = (DefinitionForSemanticDTO) input.ReadField();
                Assert.True(dtoStart.IsEquivalent(dtoRead));
            }
        }
    }
}
