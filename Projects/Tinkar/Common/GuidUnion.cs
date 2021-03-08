using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    /// <summary>
    /// Defines a Union that can access a guid or 2 Int64's.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 16, CharSet = CharSet.Ansi)]
    public struct GuidUnion : IComparable<GuidUnion>
    {
        [FieldOffset(0)] public Guid Uuid;
        [FieldOffset(0)] public Int64 MostSignificantBits;
        [FieldOffset(8)] public Int64 LeastSignificantBits;

        public GuidUnion(Guid uuid)
        {
            this.MostSignificantBits = 0;
            this.LeastSignificantBits = 0;
            this.Uuid = uuid;
        }

        public GuidUnion(Int64 mostSignificantBits, Int64 leastSignificantBits)
        {
            this.Uuid = Guid.Empty;
            this.MostSignificantBits = mostSignificantBits;
            this.LeastSignificantBits = leastSignificantBits;
        }

        /// <summary>
        /// Implementation if IComparable
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Int32 CompareTo(GuidUnion other)
        {
            Int32 cmpVal = this.MostSignificantBits.CompareTo(other.MostSignificantBits);
            if (cmpVal != 0)
                return cmpVal;
            return this.LeastSignificantBits.CompareTo(other.LeastSignificantBits);
        }
    }
}
