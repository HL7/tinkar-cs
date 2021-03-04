using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;

namespace Tinkar.XUnitTests
{
    public class FieldDefinitionDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void FieldDefinitionDTOFieldsTest()
        {
            FieldDefinitionDTO dtoStart = new FieldDefinitionDTO(
                Misc.PublicIdG,
                Misc.PublicIdH,
                Misc.PublicIdI
                );
            Misc.Compare(dtoStart.DataTypePublicId, Misc.g1, Misc.g2, Misc.g3, Misc.g4);
            Misc.Compare(dtoStart.PurposePublicId, Misc.h1, Misc.h2, Misc.h3, Misc.h4);
            Misc.Compare(dtoStart.MeaningPublicId, Misc.i1, Misc.i2, Misc.i3, Misc.i4);
        }

        [DoNotParallelize]
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
                    DataTypePublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
                }
                ;
                Assert.False(a.IsEquivalent(b));
            }

            {
                FieldDefinitionDTO a = Misc.CreateFieldDefinition;
                FieldDefinitionDTO b = Misc.CreateFieldDefinition with
                {
                    PurposePublicId = Misc.PublicIdH
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                FieldDefinitionDTO a = Misc.CreateFieldDefinition;
                FieldDefinitionDTO b = Misc.CreateFieldDefinition with
                {
                    MeaningPublicId = new PublicId(Misc.i1, Misc.i2, Misc.i3, Misc.i3)
                };
                Assert.False(a.IsEquivalent(b));
            }
        }

        [DoNotParallelize]
        [Fact]
        public void FieldDefinitionDTOMarshalTest()
        {
            FieldDefinitionDTO dtoStart = new FieldDefinitionDTO(
                Misc.PublicIdG,
                Misc.PublicIdH,
                Misc.PublicIdI
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
        [DoNotParallelize]
        [Fact]
        public void FieldDefinitionDTOJsonMarshal()
        {
            FieldDefinitionDTO dtoStart = Misc.CreateFieldDefinitionDTO;
            MemoryStream ms = new MemoryStream();
            using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
            {
                dtoStart.Marshal(output);
            }

            ms.Dump();
            ms.Position = 0;
            using (TinkarJsonInput input = new TinkarJsonInput(ms))
            {
                FieldDefinitionDTO dtoEnd = FieldDefinitionDTO.Make(input.ReadJsonObject());
                Assert.True(dtoStart.IsEquivalent(dtoEnd));
            }
        }
    }
}
