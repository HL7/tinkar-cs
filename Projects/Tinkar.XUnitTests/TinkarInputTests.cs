using System;
using System.IO;
using Xunit;

namespace Tinkar.XUnitTests
{
    public class TinkarInputTests
    {
        [Fact]
        public void ReadIntTest()
        {
            {
                MemoryStream ms = new MemoryStream(new byte[] { 1, 2, 3, 4 });
                Tinkar.TinkarInput ti = new Tinkar.TinkarInput(ms);
                Int32 value = ti.ReadInt();
                Assert.True(value == (1
                                      | 2 * 0x100
                                      | 3 * 0x10000
                                      | 4 * 0x1000000));
            }

            {
                MemoryStream ms = new MemoryStream(new byte[] { 0xf1, 0xf2, 0xf3, 0x74 });
                Tinkar.TinkarInput ti = new Tinkar.TinkarInput(ms);
                Int32 value = ti.ReadInt();
                Assert.True(value == (0xf1
                                      | 0xf2 * 0x100
                                      | 0xf3 * 0x10000
                                      | 0x74 * 0x1000000));
            }
        }

        [Fact]
        public void ReadLongTest()
        {
            {
                MemoryStream ms = new MemoryStream(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });
                Tinkar.TinkarInput ti = new Tinkar.TinkarInput(ms);
                Int64 value = ti.ReadLong();
                Int64 compare = (1L
                                 | 2L * 0x100
                                 | 3L * 0x10000
                                 | 4L * 0x1000000
                                 | 5L * 0x100000000
                                 | 6L * 0x10000000000
                                 | 7L * 0x1000000000000
                                 | 8L * 0x100000000000000);
                Assert.True(value == compare);
            }

            {
                MemoryStream ms = new MemoryStream(new byte[] { 0xf1, 0xf2, 0xf3, 0xf4, 0xf5, 0xf6, 0xf7, 0x78 });
                Tinkar.TinkarInput ti = new Tinkar.TinkarInput(ms);
                Int64 value = ti.ReadLong();
                Int64 compare = (0xf1L
                                 | 0xf2L * 0x100
                                 | 0xf3L * 0x10000
                                 | 0xf4L * 0x1000000
                                 | 0xf5L * 0x100000000
                                 | 0xf6L * 0x10000000000
                                 | 0xf7L * 0x1000000000000
                                 | 0x78L * 0x100000000000000);
                Assert.True(value == compare);
            }
        }

        [Fact]
        public void ReadInstantTest()
        {
            throw new NotImplementedException();
        }

        MemoryStream MSCreate(params byte[][] blocks)
        {
            MemoryStream ms = new MemoryStream();
            foreach (byte[] block in blocks)
                ms.Write(block);
            ms.Position = 0;
            return ms;
        }

        [Fact]
        public void ReadUuidArrayTest()
        {
            byte[] lenZero = new byte[] { 0, 0, 0, 0 };
            byte[] lenOne = new byte[] { 1, 0, 0, 0 };
            byte[] lenTwo = new byte[] { 2, 0, 0, 0 };
            byte[] guidBytes1 = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] guidBytes2 = new byte[] { 0xf0, 0xf1, 0xf2, 0xf3, 0xf4, 0xf5, 0xf6, 0xf7, 0xf8, 0xf9, 0xfa, 0xfb, 0xfc, 0xfd, 0xfe, 0xff };

            {
                MemoryStream ms = MSCreate(lenZero);
                Tinkar.TinkarInput ti = new Tinkar.TinkarInput(ms);
                Guid[] value = ti.ReadUuidArray();
                Assert.True(value.Length == 0);
            }
            {
                MemoryStream ms = MSCreate(lenOne, guidBytes1);
                TinkarInput ti = new TinkarInput(ms);
                Guid[] gArr = ti.ReadUuidArray();

                Assert.True(gArr.Length == 1);
                Assert.True(gArr[0] == new Guid(guidBytes1));
            }

            {
                MemoryStream ms = MSCreate(lenOne, guidBytes2);
                TinkarInput ti = new TinkarInput(ms);
                Guid[] gArr = ti.ReadUuidArray();

                Assert.True(gArr.Length == 1);
                Assert.True(gArr[0] == new Guid(guidBytes2));
            }

            {
                MemoryStream ms = MSCreate(lenTwo, guidBytes1, guidBytes2);
                TinkarInput ti = new TinkarInput(ms);
                Guid[] gArr = ti.ReadUuidArray();

                Assert.True(gArr.Length == 2);
                Assert.True(gArr[0] == new Guid(guidBytes1));
                Assert.True(gArr[1] == new Guid(guidBytes2));
            }
        }
    }
}
