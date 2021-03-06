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
        [FieldOffset(0)] public Int64 Int64A;
        [FieldOffset(8)] public Int64 Int64B;

        public GuidUnion(Guid uuid)
        {
            this.Int64A = 0;
            this.Int64B = 0;
            this.Uuid = uuid;
        }

        public GuidUnion(Int64 a, Int64 b)
        {
            this.Uuid = Guid.Empty;
            this.Int64A = a;
            this.Int64B = b;
        }

        /// <summary>
        /// Implementation if IComparable
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Int32 CompareTo(GuidUnion other)
        {
            Int32 cmpVal = this.Int64A.CompareTo(other.Int64A);
            if (cmpVal != 0)
                return cmpVal;
            return this.Int64B.CompareTo(other.Int64B);
        }
    }
}
