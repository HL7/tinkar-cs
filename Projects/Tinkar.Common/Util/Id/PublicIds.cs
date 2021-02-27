using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public class PublicIds
    {
        public static  PublicIdListFactory list = PublicIdListFactory.INSTANCE;
    public static  PublicIdSetFactory set = PublicIdSetFactory.INSTANCE;

    public static  PublicId newRandom()
        {
            return new PublicId1(Guid.randomUUID());
        }

        public static  PublicId of(long msb, long lsb)
        {
            return new PublicId1(msb, lsb);
        }
        public static  PublicId of(long msb, long lsb, long msb2, long lsb2)
        {
            return new PublicId2(msb, lsb, msb2, lsb2);
        }
        public static  PublicId of(long msb, long lsb, long msb2, long lsb2, long msb3, long lsb3)
        {
            return new PublicId3(msb, lsb, msb2, lsb2, msb3, lsb3);
        }
        public static  PublicId of(List<Guid> list)
        {
            return of(list.toArray(new Guid[list.size()]));
        }
        public static  PublicId of(ImmutableList<Guid> list)
        {
            return of(list.toArray(new Guid[list.size()]));
        }
        public static  PublicId of(Guid...uuids)
        {
            if (uuids.length == 1)
            {
                return new PublicId1(uuids[0]);
            }
            if (uuids.length == 2)
            {
                return new PublicId2(uuids[0], uuids[1]);
            }
            if (uuids.length == 3)
            {
                return new PublicId3(uuids[0], uuids[1], uuids[3]);
            }
            return new PublicIdN(uuids);
        }
        public static  PublicId of(long... uuidParts)
        {
            if (uuidParts.length == 2)
            {
                return new PublicId1(uuidParts[0], uuidParts[1]);
            }
            if (uuidParts.length == 4)
            {
                return new PublicId2(uuidParts[0], uuidParts[1], uuidParts[2], uuidParts[3]);
            }
            if (uuidParts.length == 6)
            {
                return new PublicId3(uuidParts[0], uuidParts[1], uuidParts[2], uuidParts[3], uuidParts[4], uuidParts[5]);
            }
            return new PublicIdN(uuidParts);
        }
    }
}
