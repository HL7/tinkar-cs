using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public interface IPublicId
    {
        Guid[] AsUuidArray { get; }
        IEnumerable<Guid> AsUuidList { get; }
        int UuidCount { get; }
        Int32 CompareTo(IPublicId publicId);
    }
}
