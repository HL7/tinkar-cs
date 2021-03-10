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
    public class VertexDTOTests
    {
        [DoNotParallelize]
        [Fact]
        public void VertexDTOFieldsTest()
        {
            VertexDTO dtoStart = Misc.CreateVertexDTO();
            Assert.True(dtoStart.VertexId.Uuid == Misc.GID(1));
            Assert.True(dtoStart.VertexIndex == 123);
            Assert.True(dtoStart.Meaning.CompareTo(new ConceptDTO(Misc.PublicIdH)) == 0);
            Assert.True(dtoStart.Properties.Keys.Count() == 7);

            Assert.True(dtoStart.Property<Int32>(new ConceptDTO(Misc.GID(0x1))) == 1);
            Assert.True(dtoStart.Property<Int64>(new ConceptDTO(Misc.GID(0x2))) == 2);
            Assert.True(dtoStart.Property<Single>(new ConceptDTO(Misc.GID(0x3))) == 3);
            Assert.True(dtoStart.Property<Double>(new ConceptDTO(Misc.GID(0x4))) == 4);
            Assert.True(dtoStart.Property<String>(new ConceptDTO(Misc.GID(0x5))) == "abcdef");
            Assert.True(dtoStart.Property<bool>(new ConceptDTO(Misc.GID(0x6))) == true);
            Assert.True(dtoStart.Property<DateTime>(new ConceptDTO(Misc.GID(0x7))) == new DateTime(2000, 1, 1));
        }

        //[DoNotParallelize]
        //[Fact]
        //public void VertexDTOIsEquivalentTest()
        //{
        //    {
        //        VertexDTO a = Misc.CreateVertexDTO;
        //        VertexDTO b = Misc.CreateVertexDTO;
        //        Assert.True(a.IsEquivalent(b));
        //    }

        //    {
        //        VertexDTO a = Misc.CreateVertexDTO;
        //        VertexDTO b = Misc.CreateVertexDTO
        //        with
        //        {
        //            PublicId = new PublicId(Misc.other)
        //        };
        //        Assert.False(a.IsEquivalent(b));
        //    }

        //    {
        //        VertexDTO a = Misc.CreateVertexDTO;
        //        VertexDTO b = Misc.CreateVertexDTO
        //        with
        //        {
        //            DefinitionForSemanticPublicId = new PublicId(Misc.other)
        //        };
        //        Assert.False(a.IsEquivalent(b));
        //    }

        //    {
        //        VertexDTO a = Misc.CreateVertexDTO;
        //        VertexDTO b = Misc.CreateVertexDTO
        //        with
        //        {
        //            ReferencedComponentPublicId = new PublicId(Misc.other)
        //        };
        //        Assert.False(a.IsEquivalent(b));
        //    }

        //    {
        //        VertexDTO a = Misc.CreateVertexDTO;
        //        VertexDTO b = Misc.CreateVertexDTO
        //        with
        //        {
        //            StampDTO = Misc.CreateStampDTO with { StatusPublicId = new PublicId(Misc.g2 ) }
        //        };
        //        Assert.False(a.IsEquivalent(b));
        //    }

        //    {
        //        VertexDTO a = Misc.CreateVertexDTO;
        //        VertexDTO b = Misc.CreateVertexDTO
        //        with
        //        {
        //            Fields = new Object[] { 1, "abcdef" }
        //        };
        //        Assert.False(a.IsEquivalent(b));
        //    }
        //}


        //[DoNotParallelize]
        //[Fact]
        //public void VertexDTOIsSameTest()
        //{
        //    {
        //        VertexDTO a = Misc.CreateVertexDTO;
        //        VertexDTO b = Misc.CreateVertexDTO;
        //        Assert.True(a.CompareTo(b) == 0);
        //    }

        //    {
        //        VertexDTO a = Misc.CreateVertexDTO;
        //        VertexDTO b = Misc.CreateVertexDTO
        //        with
        //        {
        //            PublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
        //        };
        //        Assert.False(a.CompareTo(b) == 0);
        //    }

        //    {
        //        VertexDTO a = Misc.CreateVertexDTO;
        //        VertexDTO b = Misc.CreateVertexDTO
        //        with
        //        {
        //            DefinitionForSemanticPublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
        //        };
        //        Assert.False(a.CompareTo(b) == 0);
        //    }

        //    {
        //        VertexDTO a = Misc.CreateVertexDTO;
        //        VertexDTO b = Misc.CreateVertexDTO
        //        with
        //        {
        //            ReferencedComponentPublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
        //        };
        //        Assert.False(a.CompareTo(b) == 0);
        //    }

        //    {
        //        VertexDTO a = Misc.CreateVertexDTO;
        //        VertexDTO b = Misc.CreateVertexDTO
        //        with
        //        {
        //            StampDTO = Misc.CreateStampDTO with { StatusPublicId = new PublicId(Misc.g2) }
        //        };
        //        Assert.False(a.CompareTo(b) == 0);
        //    }

        //    {
        //        VertexDTO a = Misc.CreateVertexDTO;
        //        VertexDTO b = Misc.CreateVertexDTO
        //        with
        //        {
        //            Fields = new Object[] { 1, "abcdef" }
        //        };
        //        Assert.False(a.CompareTo(b) == 0);
        //    }
        //}



        //[DoNotParallelize]
        //[Fact]
        //public void VertexDTOMarshalTest()
        //{
        //    VertexDTO dtoStart = Misc.CreateVertexDTO;

        //    MemoryStream ms = new MemoryStream();
        //    using (TinkarOutput output = new TinkarOutput(ms))
        //    {
        //        dtoStart.Marshal(output);
        //    }

        //    ms.Position = 0;
        //    using (TinkarInput input = new TinkarInput(ms))
        //    {
        //        VertexDTO dtoRead = VertexDTO.Make(input,
        //            dtoStart.PublicId,
        //            dtoStart.DefinitionForSemanticPublicId,
        //            dtoStart.ReferencedComponentPublicId);
        //        Assert.True(dtoStart.CompareTo(dtoRead) == 0);
        //    }
        //}
        //[DoNotParallelize]
        //[Fact]
        //public void VertexDTOJsonMarshal()
        //{
        //    VertexDTO dtoStart = Misc.CreateVertexDTO;
        //    MemoryStream ms = new MemoryStream();
        //    using (TinkarJsonOutput output = new TinkarJsonOutput(ms, true))
        //    {
        //        dtoStart.Marshal(output);
        //    }

        //    ms.Dump();
        //    ms.Position = 0;
        //    using (TinkarJsonInput input = new TinkarJsonInput(ms))
        //    {
        //        VertexDTO dtoEnd = VertexDTO.Make(
        //            input.ReadJsonObject(),
        //            dtoStart.PublicId,
        //            dtoStart.DefinitionForSemanticPublicId,
        //            dtoStart.ReferencedComponentPublicId);
        //        Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
        //    }
        //}

    }
}
