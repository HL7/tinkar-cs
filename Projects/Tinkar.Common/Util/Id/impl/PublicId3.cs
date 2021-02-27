using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public class PublicId3 : PublicIdA
    {

        public long msb { get; }
        public long lsb { get; }
        public long msb2 { get; }
        public long lsb2 { get; }
        public long msb3 { get; }
        public long lsb3 { get; }

        public PublicId3(Guid uuid, Guid uuid2, Guid uuid3)
        {
            this.msb = uuid.MostSignificantBits();
            this.lsb = uuid.LeastSignificantBits();
            this.msb2 = uuid2.MostSignificantBits();
            this.lsb2 = uuid2.LeastSignificantBits();
            this.msb3 = uuid3.MostSignificantBits();
            this.lsb3 = uuid3.LeastSignificantBits();
        }

        public PublicId3(long msb, long lsb, long msb2, long lsb2, long msb3, long lsb3)
        {
            this.msb = msb;
            this.lsb = lsb;
            this.msb2 = msb2;
            this.lsb2 = lsb2;
            this.msb3 = msb3;
            this.lsb3 = lsb3;
        }

        public override int UuidCount => 3;

        //$public void forEach(LongConsumer consumer) {
        //    consumer.accept(msb);
        //    consumer.accept(lsb);
        //    consumer.accept(msb2);
        //    consumer.accept(lsb2);
        //    consumer.accept(msb3);
        //    consumer.accept(lsb3);
        //}

        public override Guid[] AsUuidArray() => new Guid[] { GuidUtil.AsUuid(msb, lsb), GuidUtil.AsUuid(msb2, lsb2), GuidUtil.AsUuid(msb3, lsb3) };

        public override bool Equals(Object other)
        {
            if (this == other) return true;

            switch (other)
            {
                case PublicId1 publicId1:
                    return publicId1.Equals(this);
                case PublicId2 publicId2:
                    return publicId2.Equals(this);
                case PublicId3 publicId3:
                    return msb == publicId3.msb && lsb == publicId3.lsb ||
                            msb == publicId3.msb2 && lsb == publicId3.lsb2 ||
                            msb == publicId3.msb3 && lsb == publicId3.lsb3 ||

                            msb2 == publicId3.msb && lsb2 == publicId3.lsb ||
                            msb2 == publicId3.msb2 && lsb2 == publicId3.lsb2 ||
                            msb2 == publicId3.msb3 && lsb2 == publicId3.lsb3 ||

                            msb3 == publicId3.msb && lsb3 == publicId3.lsb ||
                            msb3 == publicId3.msb2 && lsb3 == publicId3.lsb2 ||
                            msb3 == publicId3.msb3 && lsb3 == publicId3.lsb3;
                case IPublicId publicId1:
                    return GuidUtil.AreEqual(this.AsUuidArray(), publicId1.AsUuidArray());
                default:
                    return false;
            }
        }


        public override int GetHashCode() => (new long[] { msb, lsb, msb2, lsb2, msb3, lsb3 }).GetHashCode();

    }
}