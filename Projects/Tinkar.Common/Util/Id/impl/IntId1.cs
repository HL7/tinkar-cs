using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public class IntId1
    {
        private int element;

        public IntId1(int element)
        {
            this.element = element;
        }

        public int Size => 1;

        //$public void forEach(IntConsumer consumer) {
        //    consumer.accept(element);
        //}

        //$public IntStream intStream() {
        //    return IntStream.of(element);
        //}

        public int[] ToArray() => new int[] { element };

        public bool Contains(int value) => value == element;

        public int Get(int index)
        {
            if (index == 0)
                return element;
            throw new IndexOutOfRangeException();
        }
    }
}
