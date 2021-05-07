using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public interface IPublicId : IComparable, IEquivalent
    {
        ITinkarId this[Int32 index] { get; }

        Guid[] AsUuidArray { get; }
        IEnumerable<Guid> AsUuidList { get; }
        Int32 UuidCount { get; }

        String ToUuidString();
    }
}
