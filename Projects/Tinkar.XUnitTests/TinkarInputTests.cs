using System;
using System.IO;
using Xunit;

namespace Tinkar.XUnitTests
{
    public class TinkarInputTests
    {
        Int32 MakeInt32(Int32 b1, Int32 b2, Int32 b3, Int32 b4)
        {
            return (b4
                   | b3 * 0x100
                   | b2 * 0x10000
                   | b1 * 0x1000000);
        }

        Int64 MakeInt64(Int64 b1, Int64 b2, Int64 b3, Int64 b4, Int64 b5, Int64 b6, Int64 b7, Int64 b8)
        {
            return (
                    b8
                    | b7 * 0x100L
                    | b6 * 0x10000L
                    | b5 * 0x1000000L
                    | b4 * 0x100000000L
                    | b3 * 0x10000000000L
                    | b2 * 0x1000000000000L
                    | b1 * 0x100000000000000L);
        }

        [Fact]
        public void ReadIntTest()
        {
            {
                MemoryStream ms = new MemoryStream(new byte[] { 1, 2, 3, 4 });
                Tinkar.TinkarInput ti = new Tinkar.TinkarInput(ms);
                Int32 value = ti.ReadInt();
                Assert.True(value == this.MakeInt32(1, 2, 3, 4));
            }

            {
                MemoryStream ms = new MemoryStream(new byte[] { 0xf1, 0xf2, 0xf3, 0xf4 });
                Tinkar.TinkarInput ti = new Tinkar.TinkarInput(ms);
                Int32 value = ti.ReadInt();
                Assert.True(value == this.MakeInt32(0xf1, 0xf2, 0xf3, 0xf4));
            }
        }

        [Fact]
        public void ReadLongTest()
        {
            {
                MemoryStream ms = new MemoryStream(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });
                Tinkar.TinkarInput ti = new Tinkar.TinkarInput(ms);
                Int64 value = ti.ReadLong();
                Int64 compare = MakeInt64(1, 2, 3, 4, 5, 6, 7, 8);
                Assert.True(value == compare);
            }

            {
                MemoryStream ms = new MemoryStream(new byte[] { 0xf1, 0xf2, 0xf3, 0xf4, 0xf5, 0xf6, 0xf7, 0xf8 });
                Tinkar.TinkarInput ti = new Tinkar.TinkarInput(ms);
                Int64 value = ti.ReadLong();
                Int64 compare = MakeInt64(0xf1, 0xf2, 0xf3, 0xf4, 0xf5, 0xf6, 0xf7, 0xf8);
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
            byte[] lenOne = new byte[] { 0, 0, 0, 1 };
            byte[] lenTwo = new byte[] { 0, 0, 0, 2 };
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
