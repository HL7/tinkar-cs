using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public class PublicId1 : PublicIdA, IVertexId
    {

        protected long msb;
        protected long lsb;

        public PublicId1(Guid uuid)
        {
            this.msb = uuid.MostSignificantBits();
            this.lsb = uuid.LeastSignificantBits();
        }

        public PublicId1(long msb, long lsb)
        {
            this.msb = msb;
            this.lsb = lsb;
        }

        public override int UuidCount => 1;
        public long MostSignificantBits => msb;
        public long LeastSignificantBits => lsb;

        public override Guid[] AsUuidArray() => new Guid[] { GuidUtil.AsUuid(MostSignificantBits, LeastSignificantBits) };

        //$public void forEach(LongConsumer consumer)
        //{
        //    consumer.accept(msb);
        //    consumer.accept(lsb);
        //}

        public override bool Equals(Object other)
        {
            if (this == other) return true;

            switch (other)
            {
                case PublicId1 publicId1:
                    return msb == publicId1.msb && lsb == publicId1.lsb;
                case PublicId2 publicId2:
                    return msb == publicId2.msb && lsb == publicId2.lsb ||
                            msb == publicId2.msb2 && lsb == publicId2.lsb2;
                case PublicId3 publicId3:
                    return msb == publicId3.msb && lsb == publicId3.lsb ||
                            msb == publicId3.msb2 && lsb == publicId3.lsb2 ||
                            msb == publicId3.msb3 && lsb == publicId3.lsb3;
                case IPublicId publicId1:
                    return GuidUtil.AreEqual(this.AsUuidArray(), publicId1.AsUuidArray());
                default:
                    return false;
            }
        }

        public override int GetHashCode() => (new long[] { msb, lsb }).GetHashCode();
    }
}
