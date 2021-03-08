using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Common
{
    public class VertexId : IVertexId
    {
        GuidUnion guid;

        public Guid Uuid => guid.Uuid;

        public Int64 MostSignificantBits => guid.MostSignificantBits;

        public Int64 LeastSignificantBits => guid.LeastSignificantBits;


        public VertexId(Guid uuid)
        {
            this.guid = new GuidUnion(uuid); 
        }

        public VertexId(Int64 mostSignificantBits, Int64 leastSignificantBits)
        {
            this.guid = new GuidUnion(mostSignificantBits, leastSignificantBits);
        }

        public Int32 CompareTo(IVertexId other) => this.guid.CompareTo(((VertexId)other).guid);
    }
}
