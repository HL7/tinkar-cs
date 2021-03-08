using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public interface IPublicId : IEquivalent<IPublicId>, IComparable<IPublicId>
    {
        GuidUnion this[Int32 index] { get; }

        Guid[] AsUuidArray { get; }
        IEnumerable<Guid> AsUuidList { get; }
        Int32 UuidCount { get; }
    }
}
