using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public class PublicId2 : PublicIdA
    {
        public long msb { get; }
        public long lsb { get; }
        public long msb2 { get; }
        public long lsb2 { get; }

        public PublicId2(Guid uuid, Guid uuid2)
        {
            this.msb = uuid.MostSignificantBits();
            this.lsb = uuid.LeastSignificantBits();
            this.msb2 = uuid2.MostSignificantBits();
            this.lsb2 = uuid2.LeastSignificantBits();
        }

        public PublicId2(long msb, long lsb, long msb2, long lsb2)
        {
            this.msb = msb;
            this.lsb = lsb;
            this.msb2 = msb2;
            this.lsb2 = lsb2;
        }

        public override int UuidCount => 2;

        //$public void forEach(LongConsumer consumer) {
        //    consumer.accept(msb);
        //    consumer.accept(lsb);
        //    consumer.accept(msb2);
        //    consumer.accept(lsb2);
        //}

        public override Guid[] AsUuidArray() => new Guid[] { GuidUtil.AsUuid(msb, lsb), GuidUtil.AsUuid(msb2, lsb2) };

        public bool equals(Object other)
        {
            if (this == other) return true;

            switch (other)
            {
                case PublicId1 publicId1:
                    return publicId1.Equals(this);
                case PublicId2 publicId2:
                    return msb == publicId2.msb && lsb == publicId2.lsb ||
                            msb == publicId2.msb2 && lsb == publicId2.lsb2 ||
                            msb2 == publicId2.msb && lsb2 == publicId2.lsb ||
                            msb2 == publicId2.msb2 && lsb2 == publicId2.lsb2;
                case PublicId3 publicId3:
                    return msb == publicId3.msb && lsb == publicId3.lsb ||
                            msb == publicId3.msb2 && lsb == publicId3.lsb2 ||
                            msb == publicId3.msb3 && lsb == publicId3.lsb3 ||

                            msb2 == publicId3.msb && lsb2 == publicId3.lsb ||
                            msb2 == publicId3.msb2 && lsb2 == publicId3.lsb2 ||
                            msb2 == publicId3.msb3 && lsb2 == publicId3.lsb3;
                case IPublicId publicId1:
                    return GuidUtil.AreEqual(this.AsUuidArray(), publicId1.AsUuidArray());

                default:
                    return false;
            }
        }

        public int HashCode() => (new long[] { msb, lsb, msb2, lsb2 }).GetHashCode();
    }
}
