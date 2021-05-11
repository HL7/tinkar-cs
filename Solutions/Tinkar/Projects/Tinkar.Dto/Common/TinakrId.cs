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

        [FieldOffset(0)] public UInt32 id1;
        [FieldOffset(4)] public UInt32 id2;
        [FieldOffset(8)] public UInt32 id3;
        [FieldOffset(12)] public UInt32 id4;

        public Guid Uuid => this.uuid;
        public Int64 MostSignificantBits => this.mostSignificantBits;
        public Int64 LeastSignificantBits => this.leastSignificantBits;

        public UInt32 Id1 => this.id1;
        public UInt32 Id2 => this.id2;
        public UInt32 Id3 => this.id3;
        public UInt32 Id4 => this.id4;

        public TinkarId(Guid uuid)
        {
            this.id1 = 0;
            this.id2 = 0;
            this.id3 = 0;
            this.id4 = 0;
            this.mostSignificantBits = 0;
            this.leastSignificantBits = 0;
            this.uuid = uuid;
        }

        public TinkarId(UInt32 id1, UInt32 id2, UInt32 id3, UInt32 id4)
        {
            this.uuid = Guid.Empty;
            this.mostSignificantBits = 0;
            this.leastSignificantBits = 0;
            this.id1 = id1;
            this.id2 = id2;
            this.id3 = id3;
            this.id4 = id4;
        }

        public TinkarId(Int64 mostSignificantBits, Int64 leastSignificantBits)
        {
            this.id1 = 0;
            this.id2 = 0;
            this.id3 = 0;
            this.id4 = 0;
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
