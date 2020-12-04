using System;
using System.IO;
using Xunit;
using Tinkar;

namespace Tinkar.XUnitTests
{
    public class TinkarOutputTests
    {
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
                to.WriteInt(value);
                ms.Position = 0;

                Assert.True(ms.Length == 4);
                Assert.True(ms.ReadByte() == 1);
                Assert.True(ms.ReadByte() == 2);
                Assert.True(ms.ReadByte() == 3);
                Assert.True(ms.ReadByte() == 4);
            }

            {
                MemoryStream ms = new MemoryStream();
                TinkarOutput to = new TinkarOutput(ms);
                Int32 value = (0xf1
                        | 0xf2 * 0x100
                        | 0xf3 * 0x10000
                        | 0x74 * 0x1000000);
                to.WriteInt(value);
                ms.Position = 0;

                Assert.True(ms.Length == 4);
                Assert.True(ms.ReadByte() == 0xf1);
                Assert.True(ms.ReadByte() == 0xf2);
                Assert.True(ms.ReadByte() == 0xf3);
                Assert.True(ms.ReadByte() == 0x74);
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
                ti.WriteLong(value);
                ms.Position = 0;

                Assert.True(ms.Length == 8);
                Assert.True(ms.ReadByte() == 1);
                Assert.True(ms.ReadByte() == 2);
                Assert.True(ms.ReadByte() == 3);
                Assert.True(ms.ReadByte() == 4);
                Assert.True(ms.ReadByte() == 5);
                Assert.True(ms.ReadByte() == 6);
                Assert.True(ms.ReadByte() == 7);
                Assert.True(ms.ReadByte() == 8);
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
                ti.WriteLong(value);
                ms.Position = 0;

                Assert.True(ms.Length == 8);
                Assert.True(ms.ReadByte() == 0xf1);
                Assert.True(ms.ReadByte() == 0xf2);
                Assert.True(ms.ReadByte() == 0xf3);
                Assert.True(ms.ReadByte() == 0xf4);
                Assert.True(ms.ReadByte() == 0xf5);
                Assert.True(ms.ReadByte() == 0xf6);
                Assert.True(ms.ReadByte() == 0xf7);
                Assert.True(ms.ReadByte() == 0x78);
            }
        }

        [Fact]
        public void WriteInstant()
        {
            throw new NotImplementedException();
        }


        [Fact]
        public void WriteUuidArrayTest()
        {
            {
                MemoryStream ms = new MemoryStream();
                TinkarOutput ti = new TinkarOutput(ms);
                ti.WriteUuidArray(new Guid[0]);
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
                Guid g1 = new Guid(new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16});
                ti.WriteUuidArray(new Guid[] {g1});
                ms.Position = 0;
                Assert.True(ms.Length == 20);
                Assert.True(ms.ReadByte() == 1);
                Assert.True(ms.ReadByte() == 0);
                Assert.True(ms.ReadByte() == 0);
                Assert.True(ms.ReadByte() == 0);

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
                Guid g1 = new Guid(new byte[] { 0xf0, 0xf1, 0xf2, 0xf3, 0xf4, 0xf5, 0xf6, 0xf7, 0xf8, 0xf9, 0xfa, 0xfb, 0xfc, 0xfd, 0xfe, 0xff});
                ti.WriteUuidArray(new Guid[] { g1 });
                ms.Position = 0;
                Assert.True(ms.Length == 20);
                Assert.True(ms.ReadByte() == 1);
                Assert.True(ms.ReadByte() == 0);
                Assert.True(ms.ReadByte() == 0);
                Assert.True(ms.ReadByte() == 0);

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
                ti.WriteUuidArray(new Guid[] { g1, g2 });
                ms.Position = 0;
                Assert.True(ms.Length == 36);
                Assert.True(ms.ReadByte() == 2);
                Assert.True(ms.ReadByte() == 0);
                Assert.True(ms.ReadByte() == 0);
                Assert.True(ms.ReadByte() == 0);

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
