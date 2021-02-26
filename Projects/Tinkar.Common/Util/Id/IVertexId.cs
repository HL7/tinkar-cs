using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public interface IVertexId : IComparable<IVertexId>
    {
        long MostSignificantBits { get; }

        long LeastSignificantBits { get; }

        /// <summary>
        /// Convert to Guid.
        /// </summary>
        /// <returns></returns>
#warning 'Compare this to Java implementation to insure compatability.'
        public Guid AsUuid()
        {
            byte[] guidData = new byte[16];
            Array.Copy(BitConverter.GetBytes(MostSignificantBits), guidData, 8);
            Array.Copy(BitConverter.GetBytes(LeastSignificantBits), 0, guidData, 8, 8);
            return new Guid(guidData);
        }


#nullable enable
        Int32 IComparable<IVertexId>.CompareTo(IVertexId? o)
        {
            if (o == null)
                return -1;

            Int64 compare = MostSignificantBits - o.MostSignificantBits;
            if (compare != 0)
                return compare < 0 ? -1 : 1;

            compare = LeastSignificantBits - o.LeastSignificantBits;
            if (compare != 0)
                return compare < 0 ? -1 : 1;
            return 0;
        }
#nullable disable
    }
}
