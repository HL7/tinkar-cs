using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public class PublicIdN : IPublicId
    {
        long[] uuidParts;

        public PublicIdN(params Guid[] uuids)
        {
            uuidParts = GuidUtil.AsArray(uuids);
        }

        public PublicIdN(params long[] uuidParts)
        {
            this.uuidParts = uuidParts;
        }

        public int UuidCount => uuidParts.Length / 2;

        public Guid[] AsUuidArray() => GuidUtil.ToArray(uuidParts);

        //$public void forEach(LongConsumer consumer)
        //{
        //    for (long uuidPart: uuidParts)
        //    {
        //        consumer.accept(uuidPart);
        //    }
        //}

        public override bool Equals(Object other)
        {
            if (this == other) return true;
            switch (other)
            {
                case IPublicId publicId:
                    return GuidUtil.AreEqual(this.AsUuidArray(), publicId.AsUuidArray());
                default:
                    return false;
            }
        }

        public override int GetHashCode() => uuidParts.GetHashCode();
    }
}
