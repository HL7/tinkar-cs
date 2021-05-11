using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    /// <summary>
    /// Base Tinkar Id.
    /// </summary>
    public interface ITinkarId : IComparable
    {
        Guid Uuid { get; }
        Int64 MostSignificantBits { get; }
        Int64 LeastSignificantBits { get; }
        UInt32 Id1 { get; }
        UInt32 Id2 { get; }
        UInt32 Id3 { get; }
        UInt32 Id4 { get; }
    }
}
