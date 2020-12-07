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

        Guid h1 = new Guid(0x11, 0, 0, zero);
        Guid h2 = new Guid(0x12, 0, 0, zero);
        Guid h3 = new Guid(0x13, 0, 0, zero);
        Guid h4 = new Guid(0x14, 0, 0, zero);


        Guid i1 = new Guid(0x21, 0, 0, zero);
        Guid i2 = new Guid(0x22, 0, 0, zero);
        Guid i3 = new Guid(0x23, 0, 0, zero);
        Guid i4 = new Guid(0x24, 0, 0, zero);


        Guid j1 = new Guid(0x31, 0, 0, zero);
        Guid j2 = new Guid(0x32, 0, 0, zero);
        Guid j3 = new Guid(0x33, 0, 0, zero);
        Guid j4 = new Guid(0x34, 0, 0, zero);

        Guid k1 = new Guid(0x41, 0, 0, zero);
        Guid k2 = new Guid(0x42, 0, 0, zero);
        Guid k3 = new Guid(0x43, 0, 0, zero);
        Guid k4 = new Guid(0x44, 0, 0, zero);
        Guid k5 = new Guid(0x45, 0, 0, zero);
        Guid k6 = new Guid(0x46, 0, 0, zero);

        Guid l1 = new Guid(0x51, 0, 0, zero);
        Guid l2 = new Guid(0x52, 0, 0, zero);
        Guid l3 = new Guid(0x53, 0, 0, zero);
        Guid l4 = new Guid(0x54, 0, 0, zero);
        Guid l5 = new Guid(0x55, 0, 0, zero);
        Guid l6 = new Guid(0x56, 0, 0, zero);

        protected FieldDefinitionDTO CreateFieldDefinition => 
            new FieldDefinitionDTO(
                new Guid[] { this.g1, this.g2, this.g3, this.g4 },
                new Guid[] { this.h1, this.h2, this.h3, this.h4 },
                new Guid[] { this.i1, this.i2, this.i3, this.i4 }
            );

        protected StampDTO CreateStampDTO => new StampDTO(
            new Guid[]
            {
                new Guid(0x80, 0, 0, zero),
                new Guid(0x81, 0, 0, zero)
            },
            new DateTime(1990, 3, 4),
            new Guid[]
            {
                new Guid(0x80, 1, 0, zero),
                new Guid(0x81, 1, 0, zero)
            },
            new Guid[] { new Guid(0x80, 2, 0, zero) },
            new Guid[] { new Guid(0x80, 3, 0, zero) }
            );

        void Compare(IEnumerable<Guid> inGuids, params Guid[] cmpGuids)
        {
            Guid[] guidArr = inGuids.ToArray();
            Assert.True(guidArr.Length == cmpGuids.Length);
            for (Int32 i = 0; i < guidArr.Length; i++)
                Assert.True(guidArr[i].CompareTo(cmpGuids[i]) == 0);
        }

        void Compare<T>(IEnumerable<IEquivalent<T>> inItems,
            IEnumerable<IEquivalent<T>> cmpItems)
        {
            IEquivalent<T>[] inArr = inItems.ToArray();
            IEquivalent<T>[] cmpArr = cmpItems.ToArray();
            Assert.True(inArr.Length == cmpArr.Length);
            for (Int32 i = 0; i < inArr.Length; i++)
                Assert.True(inArr[i].IsEquivalent((T)cmpArr[i]));
        }
    }
}
