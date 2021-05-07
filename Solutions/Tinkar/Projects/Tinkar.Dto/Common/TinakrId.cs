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
    public struct TinkarId : ITinkarId
    {
        [FieldOffset(0)] public Guid uuid;
        [FieldOffset(0)] public Int64 mostSignificantBits;
        [FieldOffset(8)] public Int64 leastSignificantBits;

        public Guid Uuid => this.uuid;
        public Int64 MostSignificantBits => this.mostSignificantBits;
        public Int64 LeastSignificantBits => this.leastSignificantBits;

        public TinkarId(Guid uuid)
        {
            this.mostSignificantBits = 0;
            this.leastSignificantBits = 0;
            this.uuid = uuid;
        }

        public TinkarId(Int64 mostSignificantBits, Int64 leastSignificantBits)
        {
            this.uuid = Guid.Empty;
            this.mostSignificantBits = mostSignificantBits;
            this.leastSignificantBits = leastSignificantBits;
        }

        /// <summary>
        /// Implementation if IComparable
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Int32 CompareTo(Object other) => this.CompareTo(other as ITinkarId);

        /// <summary>
        /// Implementation if IComparable
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Int32 CompareTo(ITinkarId other)
        {
            Int32 cmpVal = this.MostSignificantBits.CompareTo(other.MostSignificantBits);
            if (cmpVal != 0)
                return cmpVal;
            return this.LeastSignificantBits.CompareTo(other.LeastSignificantBits);
        }
    }
}
