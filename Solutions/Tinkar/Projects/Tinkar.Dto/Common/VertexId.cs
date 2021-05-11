﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    public class VertexId : IVertexId
    {
        TinkarId tinkarId;

        public Guid Uuid => this.tinkarId.uuid;

        public Int64 MostSignificantBits => this.tinkarId.MostSignificantBits;

        public Int64 LeastSignificantBits => this.tinkarId.LeastSignificantBits;

        public UInt32 Id1 => this.tinkarId.id1;
        public UInt32 Id2 => this.tinkarId.id2;
        public UInt32 Id3 => this.tinkarId.id3;
        public UInt32 Id4 => this.tinkarId.id4;


        public VertexId(Guid uuid) =>
            this.tinkarId = new TinkarId(uuid);

        public VertexId(Int64 mostSignificantBits, Int64 leastSignificantBits) =>
            this.tinkarId = new TinkarId(mostSignificantBits, leastSignificantBits);

        public Int32 CompareTo(Object other) 
            => this.tinkarId.CompareTo(other);
        public Int32 CompareTo(IVertexId other) => 
            this.tinkarId.CompareTo(((VertexId)other).tinkarId);
        public Int32 CompareTo(ITinkarId other) => throw new NotImplementedException();
    }
}
