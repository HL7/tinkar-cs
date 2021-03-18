using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;
using Tinkar.Dto;
using System.Collections.Immutable;

#pragma warning disable xUnit1013 // Public method should be marked as test

namespace Tinkar.XUnitTests
{
    public class VertexDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void VertexDTOFieldsTest() => DoVertexDTOFieldsTest(Misc.CreateVertexDTO());

        public static void DoVertexDTOFieldsTest(VertexDTO dto)
        {
            Assert.True(dto.VertexId.Uuid == Misc.GID(1));
            Assert.True(dto.VertexIndex == 123);
            Assert.True(dto.Meaning.CompareTo(new ConceptDTO(Misc.PublicIdH)) == 0);
            Assert.True(dto.Properties.Keys.Count() == 5);

            Assert.True(dto.Property<Int32>(new ConceptDTO(Misc.GID(0x1))) == 1);
            Assert.True(dto.Property<Single>(new ConceptDTO(Misc.GID(0x2))) == 3);
            Assert.True(dto.Property<String>(new ConceptDTO(Misc.GID(0x3))) == "abcdef");
            Assert.True(dto.Property<bool>(new ConceptDTO(Misc.GID(0x4))) == true);
            Assert.True(dto.Property<DateTime>(new ConceptDTO(Misc.GID(0x5))) == new DateTime(2000, 1, 1));
            var keys = dto.PropertyKeys.ToList();
            keys.Sort();
            Assert.True(keys.Count == 5);
            Assert.True(keys[0].CompareTo(new ConceptDTO(Misc.GID(0x1))) == 0);
            Assert.True(keys[1].CompareTo(new ConceptDTO(Misc.GID(0x2))) == 0);
            Assert.True(keys[2].CompareTo(new ConceptDTO(Misc.GID(0x3))) == 0);
            Assert.True(keys[3].CompareTo(new ConceptDTO(Misc.GID(0x4))) == 0);
            Assert.True(keys[4].CompareTo(new ConceptDTO(Misc.GID(0x5))) == 0);
        }

        [DoNotParallelize]
        [Fact]
        public void VertexDTOIsEqTest()
        {
            {
                VertexDTO a = Misc.CreateVertexDTO();
                VertexDTO b = Misc.CreateVertexDTO();
                Assert.True(a.IsEquivalent(b));
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                VertexDTO a = Misc.CreateVertexDTO();
                VertexDTO.Builder b = Misc.CreateVertexDTOBuilder();
                b.SetVertexId(Misc.h1);

                Assert.False(a.IsEquivalent(b.Create()));
                Assert.False(a.CompareTo(b.Create()) == 0);
            }

            {
                VertexDTO a = Misc.CreateVertexDTO();
                VertexDTO.Builder b = Misc.CreateVertexDTOBuilder();
                b.SetVertexIndex(55);
                Assert.False(a.IsEquivalent(b.Create()));
                Assert.False(a.CompareTo(b.Create()) == 0);
            }

            {
                VertexDTO a = Misc.CreateVertexDTO();
                VertexDTO.Builder b = Misc.CreateVertexDTOBuilder();
                b.SetMeaning(new ConceptDTO(a.Meaning.PublicId.AsUuidArray[0], Misc.other));
                Assert.True(a.IsEquivalent(b.Create()));
                Assert.False(a.CompareTo(b.Create()) == 0);
            }

            {
                VertexDTO a = Misc.CreateVertexDTO();
                VertexDTO.Builder b = Misc.CreateVertexDTOBuilder();
                b.SetMeaning(new ConceptDTO(Misc.other));
                Assert.False(a.IsEquivalent(b.Create()));
                Assert.False(a.CompareTo(b.Create()) == 0);
            }

            {
                VertexDTO a = Misc.CreateVertexDTO();
                VertexDTO.Builder b = Misc.CreateVertexDTOBuilder();
                b.ClearProperties()
                    .AppendProperty(new ConceptDTO(Misc.GID(0x1)), (Int32)1)
                    .AppendProperty(new ConceptDTO(Misc.GID(0x2)), (Single)3)
                    .AppendProperty(new ConceptDTO(Misc.GID(0x3)), "abcdef")
                    .AppendProperty(new ConceptDTO(Misc.GID(0x4)), true)
                    .AppendProperty(new ConceptDTO(Misc.GID(0x5)), new DateTime(2000, 12, 31))
                    ;

                Assert.False(a.IsEquivalent(b.Create()));
                Assert.False(a.CompareTo(b.Create()) == 0);
            }
        }


        [DoNotParallelize]
        [Fact]
        public void VertexDTOMarshalTest()
        {
            VertexDTO dtoStart = Misc.CreateVertexDTO();

            MemoryStream ms = new MemoryStream();
            using (TinkarOutput output = new TinkarOutput(ms))
            {
                dtoStart.Marshal(output);
            }

            ms.Position = 0;
            using (TinkarInput input = new TinkarInput(ms))
            {
                VertexDTO dtoRead = VertexDTO.Make(input);
                Assert.True(dtoStart.CompareTo(dtoRead) == 0);
            }
        }


        //[DoNotParallelize]
        //[Fact]
        //public void VertexDTOJsonMarshal()
        //{
        //    VertexDTO dtoStart = Misc.CreateVertexDTO();
        //    MemoryStream ms = new MemoryStream();
        //    using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
        //    {
        //        dtoStart.Marshal(output);
        //    }

        //    ms.Dump();
        //    ms.Position = 0;
        //    using (TinkarJsonInput input = new TinkarJsonInput(ms))
        //    {
        //        VertexDTO dtoEnd = VertexDTO.Make(input);
        //        Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
        //    }
        //}
    }
}
