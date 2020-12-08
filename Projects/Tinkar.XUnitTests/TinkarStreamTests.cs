using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Tinkar.XUnitTests
{
    public class TinkarStreamTests
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
                Int32 value = ti.ReadInt32();
                Assert.True(value == this.MakeInt32(1, 2, 3, 4));
            }

            {
                MemoryStream ms = new MemoryStream(new byte[] { 0xf1, 0xf2, 0xf3, 0xf4 });
                Tinkar.TinkarInput ti = new Tinkar.TinkarInput(ms);
                Int32 value = ti.ReadInt32();
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
        public void InstantTest()
        {
            void Test(DateTime start)
            {
                MemoryStream ms = new MemoryStream();
                TinkarOutput output = new TinkarOutput(ms);
                output.WriteField(start);

                ms.Position = 0;
                TinkarInput input = new TinkarInput(ms);
                DateTime value = (DateTime) input.ReadField();
                Assert.True(start == value);
            }

            Test(new DateTime(1900, 1, 1));
            Test(new DateTime(2020, 1, 2, 12, 30, 30, 100));
        }

        [Fact]
        public void Int32Test()
        {
            void Test(Int32 start)
            {
                MemoryStream ms = new MemoryStream();
                TinkarOutput output = new TinkarOutput(ms);
                output.WriteField(start);

                ms.Position = 0;
                TinkarInput input = new TinkarInput(ms);
                Int32 value = (Int32) input.ReadField();
                Assert.True(start == value);
            }

            Test(Int32.MinValue);
            Test(Int32.MaxValue);
            Test(0);
            Test(-1);
            Test(100);
            Test((Int32) 0x7fe1deab);
        }

        [Fact]
        public void Int64Test()
        {
            void Test(Int64 start)
            {
                MemoryStream ms = new MemoryStream();
                TinkarOutput output = new TinkarOutput(ms);
                output.WriteField(start);

                ms.Position = 0;
                TinkarInput input = new TinkarInput(ms);
                Int32 value = (Int32) input.ReadField();
                Assert.True(start == value);
            }

            Test(0);
            Test(-1);
            Test(Int32.MinValue);
            Test(Int32.MaxValue);
            Test(100);
        }

        [Fact]
        public void StringTest()
        {
            void Test(String start)
            {
                MemoryStream ms = new MemoryStream();
                TinkarOutput output = new TinkarOutput(ms);
                output.WriteField(start);

                ms.Position = 0;
                TinkarInput input = new TinkarInput(ms);
                String value = (String) input.ReadField();
                Assert.True(start == value);
            }
            Test(String.Empty);
            Test("a");
            Test("This is a test string");
        }

        [Fact]
        public void ByteArrayTest()
        {
            void Test(byte[] start)
            {
                MemoryStream ms = new MemoryStream();
                TinkarOutput output = new TinkarOutput(ms);
                output.WriteField(start);

                ms.Position = 0;
                TinkarInput input = new TinkarInput(ms);
                byte[] value = (byte[]) input.ReadField();
                Assert.True(start.SequenceEqual(value));
            }
            Test(new byte[] {});
            Test(new byte[] { 1 });
            Test(new byte[] { 1, 30, 255});
        }

        [Fact]
        public void BoolTest()
        {
            void Test(bool start)
            {
                MemoryStream ms = new MemoryStream();
                TinkarOutput output = new TinkarOutput(ms);
                output.WriteField(start);

                ms.Position = 0;
                TinkarInput input = new TinkarInput(ms);
                Boolean value = (Boolean) input.ReadField();
                Assert.True(start == value);
            }
            Test(true);
            Test(false);
        }

        [Fact]
        public void FloatTest()
        {
            void Test(Single start)
            {
                MemoryStream ms = new MemoryStream();
                TinkarOutput output = new TinkarOutput(ms);
                output.WriteField(start);

                ms.Position = 0;
                TinkarInput input = new TinkarInput(ms);
                Single value = (Single) input.ReadField();
                Assert.True(start == value);
            }

            Test(Single.MinValue);
            Test(Single.MaxValue);
            Test(0.1F);
            Test(-1.234F);
            Test(100.987F);
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
                Guid[] value = ti.ReadUuids();
                Assert.True(value.Length == 0);
            }
            {
                MemoryStream ms = MSCreate(lenOne, guidBytes1);
                TinkarInput ti = new TinkarInput(ms);
                Guid[] gArr = ti.ReadUuids();

                Assert.True(gArr.Length == 1);
                Assert.True(gArr[0] == new Guid(guidBytes1));
            }

            {
                MemoryStream ms = MSCreate(lenOne, guidBytes2);
                TinkarInput ti = new TinkarInput(ms);
                Guid[] gArr = ti.ReadUuids();

                Assert.True(gArr.Length == 1);
                Assert.True(gArr[0] == new Guid(guidBytes2));
            }

            {
                MemoryStream ms = MSCreate(lenTwo, guidBytes1, guidBytes2);
                TinkarInput ti = new TinkarInput(ms);
                Guid[] gArr = ti.ReadUuids();

                Assert.True(gArr.Length == 2);
                Assert.True(gArr[0] == new Guid(guidBytes1));
                Assert.True(gArr[1] == new Guid(guidBytes2));
            }
        }

        [Fact]
        public void WriteIntTest()
        {
            {
                MemoryStream ms = new MemoryStream();
                TinkarOutput to = new TinkarOutput(ms);
                Int32 value = (1
                              | 2 * 0x100
                              | 3 * 0x10000
                              | 4 * 0x1000000);
                to.WriteInt32(value);
                ms.Position = 0;

                Assert.True(ms.Length == 4);
                Assert.True(ms.ReadByte() == 4);
                Assert.True(ms.ReadByte() == 3);
                Assert.True(ms.ReadByte() == 2);
                Assert.True(ms.ReadByte() == 1);
            }

            {
                MemoryStream ms = new MemoryStream();
                TinkarOutput to = new TinkarOutput(ms);
                Int32 value = (0xf1
                        | 0xf2 * 0x100
                        | 0xf3 * 0x10000
                        | 0x74 * 0x1000000);
                to.WriteInt32(value);
                ms.Position = 0;

                Assert.True(ms.Length == 4);
                Assert.True(ms.ReadByte() == 0x74);
                Assert.True(ms.ReadByte() == 0xf3);
                Assert.True(ms.ReadByte() == 0xf2);
                Assert.True(ms.ReadByte() == 0xf1);
            }
        }

        [Fact]
        public void WriteLongTest()
        {
            {
                MemoryStream ms = new MemoryStream();
                TinkarOutput ti = new TinkarOutput(ms);
                Int64 value = (1L
                               | 2L * 0x100
                               | 3L * 0x10000
                               | 4L * 0x1000000
                               | 5L * 0x100000000
                               | 6L * 0x10000000000
                               | 7L * 0x1000000000000
                               | 8L * 0x100000000000000);
                ti.WriteInt64(value);
                ms.Position = 0;

                Assert.True(ms.Length == 8);
                Assert.True(ms.ReadByte() == 8);
                Assert.True(ms.ReadByte() == 7);
                Assert.True(ms.ReadByte() == 6);
                Assert.True(ms.ReadByte() == 5);
                Assert.True(ms.ReadByte() == 4);
                Assert.True(ms.ReadByte() == 3);
                Assert.True(ms.ReadByte() == 2);
                Assert.True(ms.ReadByte() == 1);
            }

            {
                MemoryStream ms = new MemoryStream();
                TinkarOutput ti = new TinkarOutput(ms);
                Int64 value = (0xf1L
                                 | 0xf2L * 0x100
                                 | 0xf3L * 0x10000
                                 | 0xf4L * 0x1000000
                                 | 0xf5L * 0x100000000
                                 | 0xf6L * 0x10000000000
                                 | 0xf7L * 0x1000000000000
                                 | 0x78L * 0x100000000000000);
                ti.WriteInt64(value);
                ms.Position = 0;

                Assert.True(ms.Length == 8);
                Assert.True(ms.ReadByte() == 0x78);
                Assert.True(ms.ReadByte() == 0xf7);
                Assert.True(ms.ReadByte() == 0xf6);
                Assert.True(ms.ReadByte() == 0xf5);
                Assert.True(ms.ReadByte() == 0xf4);
                Assert.True(ms.ReadByte() == 0xf3);
                Assert.True(ms.ReadByte() == 0xf2);
                Assert.True(ms.ReadByte() == 0xf1);
            }
        }


        [Fact]
        public void WriteUuidArrayTest()
        {
            {
                MemoryStream ms = new MemoryStream();
                TinkarOutput ti = new TinkarOutput(ms);
                ti.WriteUuids(new Guid[0]);
                ms.Position = 0;
                Assert.True(ms.Length == 4);
                Assert.True(ms.ReadByte() == 0);
                Assert.True(ms.ReadByte() == 0);
                Assert.True(ms.ReadByte() == 0);
                Assert.True(ms.ReadByte() == 0);
            }

            {
                MemoryStream ms = new MemoryStream();
                TinkarOutput ti = new TinkarOutput(ms);
                Guid g1 = new Guid(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
                ti.WriteUuids(new Guid[] { g1 });
                ms.Position = 0;
                Assert.True(ms.Length == 20);
                Assert.True(ms.ReadByte() == 0);
                Assert.True(ms.ReadByte() == 0);
                Assert.True(ms.ReadByte() == 0);
                Assert.True(ms.ReadByte() == 1);

                Assert.True(ms.ReadByte() == 1);
                Assert.True(ms.ReadByte() == 2);
                Assert.True(ms.ReadByte() == 3);
                Assert.True(ms.ReadByte() == 4);
                Assert.True(ms.ReadByte() == 5);
                Assert.True(ms.ReadByte() == 6);
                Assert.True(ms.ReadByte() == 7);
                Assert.True(ms.ReadByte() == 8);
                Assert.True(ms.ReadByte() == 9);
                Assert.True(ms.ReadByte() == 10);
                Assert.True(ms.ReadByte() == 11);
                Assert.True(ms.ReadByte() == 12);
                Assert.True(ms.ReadByte() == 13);
                Assert.True(ms.ReadByte() == 14);
                Assert.True(ms.ReadByte() == 15);
                Assert.True(ms.ReadByte() == 16);
            }

            {
                MemoryStream ms = new MemoryStream();
                TinkarOutput ti = new TinkarOutput(ms);
                Guid g1 = new Guid(new byte[] { 0xf0, 0xf1, 0xf2, 0xf3, 0xf4, 0xf5, 0xf6, 0xf7, 0xf8, 0xf9, 0xfa, 0xfb, 0xfc, 0xfd, 0xfe, 0xff });
                ti.WriteUuids(new Guid[] { g1 });
                ms.Position = 0;
                Assert.True(ms.Length == 20);
                Assert.True(ms.ReadByte() == 0);
                Assert.True(ms.ReadByte() == 0);
                Assert.True(ms.ReadByte() == 0);
                Assert.True(ms.ReadByte() == 1);

                Assert.True(ms.ReadByte() == 0xf0);
                Assert.True(ms.ReadByte() == 0xf1);
                Assert.True(ms.ReadByte() == 0xf2);
                Assert.True(ms.ReadByte() == 0xf3);
                Assert.True(ms.ReadByte() == 0xf4);
                Assert.True(ms.ReadByte() == 0xf5);
                Assert.True(ms.ReadByte() == 0xf6);
                Assert.True(ms.ReadByte() == 0xf7);
                Assert.True(ms.ReadByte() == 0xf8);
                Assert.True(ms.ReadByte() == 0xf9);
                Assert.True(ms.ReadByte() == 0xfa);
                Assert.True(ms.ReadByte() == 0xfb);
                Assert.True(ms.ReadByte() == 0xfc);
                Assert.True(ms.ReadByte() == 0xfd);
                Assert.True(ms.ReadByte() == 0xfe);
                Assert.True(ms.ReadByte() == 0xff);
            }

            {
                MemoryStream ms = new MemoryStream();
                TinkarOutput ti = new TinkarOutput(ms);
                Guid g1 = new Guid(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
                Guid g2 = new Guid(new byte[] { 0xf0, 0xf1, 0xf2, 0xf3, 0xf4, 0xf5, 0xf6, 0xf7, 0xf8, 0xf9, 0xfa, 0xfb, 0xfc, 0xfd, 0xfe, 0xff });
                ti.WriteUuids(new Guid[] { g1, g2 });
                ms.Position = 0;
                Assert.True(ms.Length == 36);
                Assert.True(ms.ReadByte() == 0);
                Assert.True(ms.ReadByte() == 0);
                Assert.True(ms.ReadByte() == 0);
                Assert.True(ms.ReadByte() == 2);

                Assert.True(ms.ReadByte() == 1);
                Assert.True(ms.ReadByte() == 2);
                Assert.True(ms.ReadByte() == 3);
                Assert.True(ms.ReadByte() == 4);
                Assert.True(ms.ReadByte() == 5);
                Assert.True(ms.ReadByte() == 6);
                Assert.True(ms.ReadByte() == 7);
                Assert.True(ms.ReadByte() == 8);
                Assert.True(ms.ReadByte() == 9);
                Assert.True(ms.ReadByte() == 10);
                Assert.True(ms.ReadByte() == 11);
                Assert.True(ms.ReadByte() == 12);
                Assert.True(ms.ReadByte() == 13);
                Assert.True(ms.ReadByte() == 14);
                Assert.True(ms.ReadByte() == 15);
                Assert.True(ms.ReadByte() == 16);

                Assert.True(ms.ReadByte() == 0xf0);
                Assert.True(ms.ReadByte() == 0xf1);
                Assert.True(ms.ReadByte() == 0xf2);
                Assert.True(ms.ReadByte() == 0xf3);
                Assert.True(ms.ReadByte() == 0xf4);
                Assert.True(ms.ReadByte() == 0xf5);
                Assert.True(ms.ReadByte() == 0xf6);
                Assert.True(ms.ReadByte() == 0xf7);
                Assert.True(ms.ReadByte() == 0xf8);
                Assert.True(ms.ReadByte() == 0xf9);
                Assert.True(ms.ReadByte() == 0xfa);
                Assert.True(ms.ReadByte() == 0xfb);
                Assert.True(ms.ReadByte() == 0xfc);
                Assert.True(ms.ReadByte() == 0xfd);
                Assert.True(ms.ReadByte() == 0xfe);
                Assert.True(ms.ReadByte() == 0xff);
            }
        }
    }
}
