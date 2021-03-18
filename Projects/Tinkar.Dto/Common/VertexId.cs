using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    public class VertexId : IVertexId
    {
        TinkarId tinkarId;

        public Guid Uuid => tinkarId.uuid;

        public Int64 MostSignificantBits => tinkarId.MostSignificantBits;

        public Int64 LeastSignificantBits => tinkarId.LeastSignificantBits;


        public VertexId(Guid uuid) => 
            tinkarId = new TinkarId(uuid);

        public VertexId(Int64 mostSignificantBits, Int64 leastSignificantBits) =>
            tinkarId = new TinkarId(mostSignificantBits, leastSignificantBits);

        public Int32 CompareTo(Object other) 
            => this.tinkarId.CompareTo(other);
        public Int32 CompareTo(IVertexId other) => 
            this.tinkarId.CompareTo(((VertexId)other).tinkarId);
        public Int32 CompareTo(ITinkarId other) => throw new NotImplementedException();
    }
}
