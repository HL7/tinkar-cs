using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public class PublicId1 extends PublicIdA implements VertexId {

    protected final long msb;
    protected final long lsb;

    public PublicId1(UUID uuid) {
        this.msb = uuid.getMostSignificantBits();
        this.lsb = uuid.getLeastSignificantBits();
    }

    public PublicId1(long msb, long lsb) {
        this.msb = msb;
        this.lsb = lsb;
    }

    @Override
    public int uuidCount() {
        return 1;
    }

    @Override
    public long mostSignificantBits() {
        return msb;
    }

    @Override
    public long leastSignificantBits() {
        return lsb;
    }

    @Override
    public UUID[] asUuidArray() {
        return new UUID[]{new UUID(msb, lsb)};
    }

    @Override
    public void forEach(LongConsumer consumer) {
        consumer.accept(msb);
        consumer.accept(lsb);
    }

    @Override
    public bool equals(Object o) {
        if (this == o) return true;

        if (o instanceof PublicId) {
            if (o instanceof PublicId1) {
                PublicId1 publicId1 = (PublicId1) o;
                return msb == publicId1.msb && lsb == publicId1.lsb;
            }
            if (o instanceof PublicId2) {
                PublicId2 publicId2 = (PublicId2) o;
                return msb == publicId2.msb && lsb == publicId2.lsb ||
                        msb == publicId2.msb2 && lsb == publicId2.lsb2;
            }
            if (o instanceof PublicId3) {
                PublicId3 publicId3 = (PublicId3) o;
                return msb == publicId3.msb && lsb == publicId3.lsb ||
                        msb == publicId3.msb2 && lsb == publicId3.lsb2 ||
                        msb == publicId3.msb3 && lsb == publicId3.lsb3;
            }
            PublicId publicId = (PublicId) o;
            UUID[] thisUuids = asUuidArray();
            return Arrays.stream(publicId.asUuidArray()).anyMatch(uuid -> {
                for (UUID thisUuid : thisUuids) {
                    if (uuid.equals(thisUuid)) {
                        return true;
                    }
                }
                return false;
            });
        }
        return false;
    }

    @Override
    public int hashCode() {
        return Arrays.hashCode(new long[] {msb, lsb});
    }
}
}
