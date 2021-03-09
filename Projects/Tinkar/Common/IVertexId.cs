using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public interface IVertexId : IComparable<IVertexId>
    {
        public Guid Uuid { get; }
        public Int64 MostSignificantBits { get; }
        public Int64 LeastSignificantBits { get; }
    }
}
