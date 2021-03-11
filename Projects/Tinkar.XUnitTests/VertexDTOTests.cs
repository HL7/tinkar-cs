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
            VertexDTO dto = Misc.CreateVertexDTO();
            Assert.True(dto.VertexId.Uuid == Misc.GID(1));
            Assert.True(dto.VertexIndex == 123);
            Assert.True(dto.Meaning.CompareTo(new ConceptDTO(Misc.PublicIdH)) == 0);
            Assert.True(dto.Properties.Keys.Count() == 7);

            Assert.True(dto.Property<Int32>(new ConceptDTO(Misc.GID(0x1))) == 1);
            Assert.True(dto.Property<Int64>(new ConceptDTO(Misc.GID(0x2))) == 2);
            Assert.True(dto.Property<Single>(new ConceptDTO(Misc.GID(0x3))) == 3);
            Assert.True(dto.Property<Double>(new ConceptDTO(Misc.GID(0x4))) == 4);
            Assert.True(dto.Property<String>(new ConceptDTO(Misc.GID(0x5))) == "abcdef");
            Assert.True(dto.Property<bool>(new ConceptDTO(Misc.GID(0x6))) == true);
            Assert.True(dto.Property<DateTime>(new ConceptDTO(Misc.GID(0x7))) == new DateTime(2000, 1, 1));
            var keys = dto.PropertyKeys.ToList();
            keys.Sort();
            Assert.True(keys.Count == 7);
            Assert.True(keys[0].CompareTo(new ConceptDTO(Misc.GID(0x1))) == 0);
            Assert.True(keys[1].CompareTo(new ConceptDTO(Misc.GID(0x2))) == 0);
            Assert.True(keys[2].CompareTo(new ConceptDTO(Misc.GID(0x3))) == 0);
            Assert.True(keys[3].CompareTo(new ConceptDTO(Misc.GID(0x4))) == 0);
            Assert.True(keys[4].CompareTo(new ConceptDTO(Misc.GID(0x5))) == 0);
            Assert.True(keys[5].CompareTo(new ConceptDTO(Misc.GID(0x6))) == 0);
            Assert.True(keys[6].CompareTo(new ConceptDTO(Misc.GID(0x7))) == 0);
        }

        [DoNotParallelize]
        [Fact]
        public void VertexDTOIsEquivalentTest()
        {
            {
                VertexDTO a = Misc.CreateVertexDTO();
                VertexDTO b = Misc.CreateVertexDTO();
                Assert.True(a.IsEquivalent(b));
            }

            {
                VertexDTO a = Misc.CreateVertexDTO();
                VertexDTO b = Misc.CreateVertexDTO()
                with
                {
                    VertexId = new VertexId(Misc.h1)
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                VertexDTO a = Misc.CreateVertexDTO();
                VertexDTO b = Misc.CreateVertexDTO()
                with
                {
                    VertexIndex = 55
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                VertexDTO a = Misc.CreateVertexDTO();
                VertexDTO b = Misc.CreateVertexDTO()
                with
                {
                    Meaning = new ConceptDTO(a.Meaning.PublicId.AsUuidArray[0], Misc.other)
                };
                Assert.True(a.IsEquivalent(b));
            }

            {
                VertexDTO a = Misc.CreateVertexDTO();
                VertexDTO b = Misc.CreateVertexDTO()
                with
                {
                    Meaning = new ConceptDTO(Misc.other)
                };
                Assert.False(a.IsEquivalent(b));
            }

            {
                VertexDTO a = Misc.CreateVertexDTO();
                VertexDTO b = Misc.CreateVertexDTO()
                with
                {
                    Properties = new KeyValuePair<IConcept, Object>[]
                    {
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x1)), (Int32) 1),
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x2)), (Int64) 2),
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x3)), (Single) 3),
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x4)), (Double) 4),
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x5)), "abcdef"),
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x6)), true),
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x7)), new DateTime(2000, 12, 31))
                    }.ToImmutableDict()
                };
                Assert.False(a.IsEquivalent(b));
            }
        }


        [DoNotParallelize]
        [Fact]
        public void VertexDTOCompareToTest()
        {
            {
                VertexDTO a = Misc.CreateVertexDTO();
                VertexDTO b = Misc.CreateVertexDTO();
                Assert.True(a.CompareTo(b) == 0);
            }

            {
                VertexDTO a = Misc.CreateVertexDTO();
                VertexDTO b = Misc.CreateVertexDTO()
                with
                {
                    VertexId = new VertexId(Misc.h1)
                };
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                VertexDTO a = Misc.CreateVertexDTO();
                VertexDTO b = Misc.CreateVertexDTO()
                with
                {
                    VertexIndex = 55
                };
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                VertexDTO a = Misc.CreateVertexDTO();
                VertexDTO b = Misc.CreateVertexDTO()
                with
                {
                    Meaning = new ConceptDTO(a.Meaning.PublicId.AsUuidArray[0], Misc.other)
                };
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                VertexDTO a = Misc.CreateVertexDTO();
                VertexDTO b = Misc.CreateVertexDTO()
                with
                {
                    Meaning = new ConceptDTO(Misc.other)
                };
                Assert.False(a.CompareTo(b) == 0);
            }

            {
                VertexDTO a = Misc.CreateVertexDTO();
                VertexDTO b = Misc.CreateVertexDTO()
                with
                {
                    Properties = new KeyValuePair<IConcept, Object>[]
                    {
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x1)), (Int32) 1),
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x2)), (Int64) 2),
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x3)), (Single) 3),
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x4)), (Double) 4),
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x5)), "abcdef"),
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x6)), true),
                        KeyValuePair.Create<IConcept, Object>(new ConceptDTO(Misc.GID(0x7)), new DateTime(2000, 12, 31))
                    }.ToImmutableDict()
                };
                Assert.False(a.CompareTo(b) == 0);
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
