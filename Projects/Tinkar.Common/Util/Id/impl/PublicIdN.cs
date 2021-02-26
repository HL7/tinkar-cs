using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public class PublicIdN implements PublicId {

    private final long[] uuidParts;

    public PublicIdN(UUID... uuids) {
        uuidParts = UuidUtil.asArray(uuids);
    }

    public PublicIdN(long... uuidParts) {
        this.uuidParts = uuidParts;
    }

    @Override
    public int uuidCount() {
        return uuidParts.length/2;
    }

    @Override
    public UUID[] asUuidArray() {
        return UuidUtil.toArray(uuidParts);
    }

    @Override
    public void forEach(LongConsumer consumer) {
        for (long uuidPart: uuidParts) {
            consumer.accept(uuidPart);
        }
    }

    @Override
    public bool equals(Object o) {
        if (this == o) return true;

        if (o instanceof PublicId) {
            PublicId publicId = (PublicId) o;
            UUID[] thisUuids = asUuidArray();
            return Arrays.stream(publicId.asUuidArray()).anyMatch(uuid -> {
                for (UUID thisUuid: thisUuids) {
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
        return Arrays.hashCode(uuidParts);
    }

}
}
