using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public class IntIdSetRoaring extends RoaringBitmap implements IntIdSet {
    private IntIdSetRoaring() {
    }

    public static IntIdSet newIntIdSet(params Int32[] newElements) {
        Arrays.sort(newElements);
        IntIdSetRoaring roaring = new IntIdSetRoaring();
        roaring.add(newElements);
        return roaring;
    }

    public static IntIdSet newIntIdSetAlreadySorted(params Int32[] newElements) {
        IntIdSetRoaring roaring = new IntIdSetRoaring();
        roaring.add(newElements);
        return roaring;
    }

    @Override
    public int size() {
        return this.getCardinality();
    }

    @Override
    public void forEach(IntConsumer consumer) {
        forEach(new ConsumerAdaptor(consumer));
    }

    @Override
    public IntStream intStream() {
        return stream();
    }

    private static class ConsumerAdaptor implements org.roaringbitmap.IntConsumer {
        java.util.function.IntConsumer adaptee;

        public ConsumerAdaptor(IntConsumer adaptee) {
            this.adaptee = adaptee;
        }

        @Override
        public void accept(int value) {
            this.adaptee.accept(value);
        }
    }
}
}
