using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Tinkar.XUnitTests
{
    public partial class DTOMarshalTests
    {
        static byte[] zero => new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };

        Guid g1 = new Guid(1, 0, 0, zero);
        Guid g2 = new Guid(2, 0, 0, zero);
        Guid g3 = new Guid(3, 0, 0, zero);
        Guid g4 = new Guid(4, 0, 0, zero);

        Guid h1 = new Guid(11, 0, 0, zero);
        Guid h2 = new Guid(12, 0, 0, zero);
        Guid h3 = new Guid(13, 0, 0, zero);
        Guid h4 = new Guid(14, 0, 0, zero);


        Guid i1 = new Guid(21, 0, 0, zero);
        Guid i2 = new Guid(22, 0, 0, zero);
        Guid i3 = new Guid(23, 0, 0, zero);
        Guid i4 = new Guid(24, 0, 0, zero);


        Guid j1 = new Guid(31, 0, 0, zero);
        Guid j2 = new Guid(32, 0, 0, zero);
        Guid j3 = new Guid(33, 0, 0, zero);
        Guid j4 = new Guid(34, 0, 0, zero);
        
        void Compare(IEnumerable<Guid> inGuids, params Guid[] cmpGuids)
        {
            Guid[] guidArr = inGuids.ToArray();
            Assert.True(guidArr.Length == cmpGuids.Length);
            for (Int32 i = 0; i < guidArr.Length; i++)
                Assert.True(guidArr[i].CompareTo(cmpGuids[i]) == 0);
        }
    }
}
