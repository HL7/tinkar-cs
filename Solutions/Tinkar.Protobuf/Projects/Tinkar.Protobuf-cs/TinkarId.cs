using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tinkar.ProtoBuf.CS;

namespace Tinkar.Protobuf.CS
{
    /// <summary>
    /// Defines a Union that can access a guid or 2 Int64's.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 16, CharSet = CharSet.Ansi)]
    public struct TinkarId
    {
        [FieldOffset(0)] public Guid Uuid;
        [FieldOffset(0)] public UInt32 Id1;
        [FieldOffset(4)] public UInt32 Id2;
        [FieldOffset(8)] public UInt32 Id3;
        [FieldOffset(12)] public UInt32 Id4;

        public TinkarId(Guid uuid)
        {
            this.Id1 = 0;
            this.Id2 = 0;
            this.Id3 = 0;
            this.Id4 = 0;
            this.Uuid = uuid;
        }
        public PBTinkarId ToPBTinkarId()
        {
            return new PBTinkarId
            {
                Id1 = this.Id1,
                Id2 = this.Id2,
                Id3 = this.Id3,
                Id4 = this.Id4
            };
        }
    }
}
