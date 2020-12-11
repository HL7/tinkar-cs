using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;

namespace Tinkar.XUnitTests
{
    public class DefinitionForSemanticDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void DefinitionForSemanticDTOFieldsTest()
        {
            DefinitionForSemanticDTO dtoStart = new DefinitionForSemanticDTO(new Guid[] { Misc.g1, Misc.g2, Misc.g3, Misc.g4 });
            Misc.Compare(dtoStart.ComponentUuids, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
        }

        [DoNotParallelize]
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

        [DoNotParallelize]
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
        [DoNotParallelize]
        [Fact]
        public void DefinitionForSemanticDTOJsonMarshal()
        {
            DefinitionForSemanticDTO dtoStart = Misc.CreateDefinitionForSemanticDTO;
            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms))
            {
                dtoStart.Marshal(output);
            }

            ms.Dump();
            ms.Position = 0;
            using (TinkarJsonInput input = new TinkarJsonInput(ms))
            {
                DefinitionForSemanticDTO dtoEnd = DefinitionForSemanticDTO.Make(input.ReadJsonObject());
                Assert.True(dtoStart.IsEquivalent(dtoEnd));
            }
        }
    }
}
