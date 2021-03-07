using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Xunit;
using Assert = Xunit.Assert;

namespace Tinkar.XUnitTests
{
    public class FieldCompareTests
    {
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

        [DoNotParallelize]
        [Fact]
        public void FieldCompareTest()
        {
            {
                Assert.True(FieldCompare.Same("abc", "abc"));
                Assert.False(FieldCompare.Same("abc", "abcd"));
            }
            {
                Assert.True(FieldCompare.Same(0, 0));
                Assert.False(FieldCompare.Same(0, 1));
            }
            {
                Assert.True(FieldCompare.Same(0.1F, 0.1F));
                Assert.False(FieldCompare.Same(0.0F, 1.0F));
            }
            {
                Assert.True(FieldCompare.Same(true, true));
                Assert.False(FieldCompare.Same(true, false));
            }
            {
                Assert.True(FieldCompare.Same(new DateTime(2020, 1, 1), new DateTime(2020, 1, 1)));
                Assert.False(FieldCompare.Same(new DateTime(2020, 1, 1), new DateTime(2020, 2, 1)));
            }
            {
                Assert.True(FieldCompare.Same(new byte[] { 1, 2, 3 },
                                                    new byte[] { 1, 2, 3 }));
                Assert.False(FieldCompare.Same(new byte[] { 1, 2, 3 },
                                                    new byte[] { 1, 3, 2 }));
            }
            {
                Assert.True(FieldCompare.Same(Misc.CreateConceptChronologyDTO,
                    Misc.CreateConceptChronologyDTO));
                Assert.False(FieldCompare.Same(
                    Misc.CreateConceptChronologyDTO,
                    Misc.CreateConceptChronologyDTO
                    with
                    {
                        PublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
                    }));
            }
            {
                Assert.True(FieldCompare.Same(
                    Misc.CreateConceptDTO,
                    Misc.CreateConceptDTO));
                Assert.False(FieldCompare.Same(
                    Misc.CreateConceptDTO,
                    Misc.CreateConceptDTO
                    with
                    {
                        PublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
                    }));
            }
            {
                Assert.True(FieldCompare.Same(Misc.CreatePatternForSemanticChronologyDTO,
                    Misc.CreatePatternForSemanticChronologyDTO));
                Assert.False(FieldCompare.Same(
                    Misc.CreatePatternForSemanticChronologyDTO,
                    Misc.CreatePatternForSemanticChronologyDTO
                with
                    {
                        PublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
                    }));
            }
            {
                Assert.True(FieldCompare.Same(
                    Misc.CreatePatternForSemanticDTO,
                    Misc.CreatePatternForSemanticDTO));
                Assert.False(FieldCompare.Same(
                    Misc.CreatePatternForSemanticDTO,
                    Misc.CreatePatternForSemanticDTO
                with
                    {
                        PublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
                    }));
            }
            {
                Assert.True(FieldCompare.Same(
                    Misc.CreateSemanticChronologyDTO,
                    Misc.CreateSemanticChronologyDTO));
                Assert.False(FieldCompare.Same(
                    Misc.CreateSemanticChronologyDTO,
                    Misc.CreateSemanticChronologyDTO
                with
                    {
                        PublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
                    }));
            }
            {
                Assert.True(FieldCompare.Same(
                    Misc.CreateSemanticDTO,
                    Misc.CreateSemanticDTO));
                Assert.False(FieldCompare.Same(
                    Misc.CreateSemanticDTO,
                    Misc.CreateSemanticDTO
                with
                    {
                        PublicId = new PublicId(Misc.g2, Misc.g2, Misc.g3, Misc.g4)
                    }));
            }
            {
                Object[] AArr() => new object[] { "a", 2, 0.1F };
                Object[] BArr() => new object[] { "a", 2, "ggg" };

                Assert.True(FieldCompare.Same(AArr(), AArr()));
                Assert.False(FieldCompare.Same(AArr(), BArr()));
            }
        }
    }
}
