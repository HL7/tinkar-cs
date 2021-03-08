using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace Tinkar.XUnitTests
{
    public unsafe class PublicIdTests
    {

#if Never
        Int32 CompareTo(Int64 a, Int64 b, Int64 c, Int64 d)
        {
            Int32 cmpVal = a.CompareTo(c);
            if (cmpVal != 0)
                return cmpVal;
            return b.CompareTo(d);
        }

        bool IsEqualD(Int64[] aSet, Int64[] bSet)
        {
            switch (bSet.Length)
            {
                case 2:
                    Int32 cmpVal = aSet[0].CompareTo(bSet[0]);
                    if (cmpVal == 0)
                        cmpVal = aSet[1].CompareTo(bSet[1]);
                    return cmpVal == 0;
                default:
                    throw new Exception("");
            }
        }

        bool IsEqualE(Int64[] aSet, Int64[] bSet)
        {
            if (bSet.Length == 2)
            {
                Int32 cmpVal = aSet[0].CompareTo(bSet[0]);
                if (cmpVal == 0)
                    cmpVal = aSet[1].CompareTo(bSet[1]);
                return cmpVal == 0;
            }
            else
                throw new NotImplementedException();
        }

        bool IsEqualF(Int64[] aSet, Int64[] bSet)
        {
            Int32 j = 0;
            Int32 k = 0;
            while (true)
            {
                Int32 cmpVal = aSet[j].CompareTo(bSet[k]);
                if (cmpVal == 0)
                    cmpVal = aSet[j + 1].CompareTo(bSet[k + 1]);
                if (cmpVal < 0)
                {
                    j += 2;
                    if (j >= aSet.Length)
                        return false;
                }
                else if (cmpVal > 0)
                {
                    k += 2;
                    if (k >= bSet.Length)
                        return false;
                }
                else
                {
                    return true;
                }
            }
        }

        [Fact]
        public void SpeedIntersectTest()
        {
            const Int32 iterations = 0x7ffff;
            Guid a = new Guid(2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Guid b = new Guid(3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            StringBuilder sb = new StringBuilder();


            byte[] ab = a.ToByteArray();
            long al1 = BitConverter.ToInt64(ab, 0);
            long al2 = BitConverter.ToInt64(ab, 8);

            byte[] bb = b.ToByteArray();
            long bl1 = BitConverter.ToInt64(bb, 0);
            long bl2 = BitConverter.ToInt64(bb, 8);


            {
                DateTime start = DateTime.Now;
                for (Int32 i = 0; i < iterations; i++)
                    CompareTo(al1, al2, bl1, bl2);
                TimeSpan time = DateTime.Now - start;

                sb.AppendLine($"Baseline Int64 comparisons {time}");
            }

            {
                Int64[] aSet = new Int64[] { al1, al2 };
                Int64[] bSet = new Int64[] { bl1, bl2 };
                DateTime start = DateTime.Now;
                for (Int32 i = 0; i < iterations; i++)
                    IsEqualD(aSet, bSet);
                TimeSpan time = DateTime.Now - start;

                sb.AppendLine($"Use Switch. {time}");
            }

            {
                Int64[] aSet = new Int64[] { al1, al2 };
                Int64[] bSet = new Int64[] { bl1, bl2 };
                DateTime start = DateTime.Now;
                for (Int32 i = 0; i < iterations; i++)
                    IsEqualE(aSet, bSet);
                TimeSpan time = DateTime.Now - start;

                sb.AppendLine($"Use if statements {time}");
            }
            {
                Int64[] aSet = new Int64[] { al1, al2 };
                Int64[] bSet = new Int64[] { bl1, bl2 };
                DateTime start = DateTime.Now;

                for (Int32 i = 0; i < iterations; i++)
                    IsEqualF(aSet, bSet);
                TimeSpan time = DateTime.Now - start;

                sb.AppendLine($"Use array indices {time}");
            }

            File.WriteAllText(@"c:\Temp\f.txt", sb.ToString());
        }
#endif

        [DoNotParallelize]
        [Fact]
        public void Insert()
        {
            GuidUnion alpha = new GuidUnion(1L, 0L);
            GuidUnion beta = new GuidUnion(2L, 0L);
            GuidUnion delta = new GuidUnion(3L, 0L);
            {
                PublicId pid = new PublicId(alpha.Uuid);
                Guid[] guids = pid.AsUuidArray;
                Assert.True(guids.Length == 1);
                Assert.True(guids[0].CompareTo(alpha.Uuid) == 0);
            }

            {
                PublicId pid = new PublicId(alpha.Uuid, beta.Uuid, delta.Uuid);
                Guid[] guids = pid.AsUuidArray;
                Assert.True(guids.Length == 3);
                Assert.True(guids[0].CompareTo(alpha.Uuid) == 0);
                Assert.True(guids[1].CompareTo(beta.Uuid) == 0);
                Assert.True(guids[2].CompareTo(delta.Uuid) == 0);
            }

            {
                PublicId pid = new PublicId(beta.Uuid, delta.Uuid, alpha.Uuid);
                Guid[] guids = pid.AsUuidArray;
                Assert.True(guids.Length == 3);
                Assert.True(guids[0].CompareTo(alpha.Uuid) == 0);
                Assert.True(guids[1].CompareTo(beta.Uuid) == 0);
                Assert.True(guids[2].CompareTo(delta.Uuid) == 0);
            }

            {
                PublicId pid = new PublicId(alpha.Uuid, delta.Uuid, beta.Uuid);
                Guid[] guids = pid.AsUuidArray;
                Assert.True(guids.Length == 3);
                Assert.True(guids[0].CompareTo(alpha.Uuid) == 0);
                Assert.True(guids[1].CompareTo(beta.Uuid) == 0);
                Assert.True(guids[2].CompareTo(delta.Uuid) == 0);
            }

            {
                PublicId pid = new PublicId(alpha.Uuid, beta.Uuid, alpha.Uuid, delta.Uuid, alpha.Uuid, alpha.Uuid);
                Guid[] guids = pid.AsUuidArray;
                Assert.True(guids.Length == 3);
                Assert.True(guids[0].CompareTo(alpha.Uuid) == 0);
                Assert.True(guids[1].CompareTo(beta.Uuid) == 0);
                Assert.True(guids[2].CompareTo(delta.Uuid) == 0);
            }
        }

        [DoNotParallelize]
        [Fact]
        public void Verify()
        {
            Guid guid = Guid.NewGuid();
            GuidUnion guidUnion = new GuidUnion(guid);
            Assert.True(sizeof(GuidUnion) == sizeof(Guid));
            Assert.True(guidUnion.Uuid == guid);

            byte[] gb = guid.ToByteArray();
            Int64 la = BitConverter.ToInt64(gb, 0);
            Int64 lb = BitConverter.ToInt64(gb, 8);

            Assert.True(guidUnion.MostSignificantBits == la);
            Assert.True(guidUnion.LeastSignificantBits == lb);
        }
    }
}
