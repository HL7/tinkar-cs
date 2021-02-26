using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    /**
     * IntId0 is an optimization for {@link ImmutableIntList} of size 0.
     * This file was automatically generated from template file immutablePrimitiveEmptyList.stg.
     */
    public abstract class IntId0
    {
        public static int[] Elements { get; } = new int[0];

        public int Get(int index) => throw new IndexOutOfRangeException("Index: " + index + ", Size: 0");

        public int Size => 0;

        //$public void forEach(IntConsumer consumer) {
        //    // nothing to do...
        //}

        //$public IntStream intStream() {
        //    return IntStream.of(elements);
        //}

        public int[] ToArray() => Elements;

        public bool Contains(int value) => false;
    }
}
