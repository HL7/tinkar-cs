using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public class IntId2 
    {
        protected int element;

        protected int element2;

        public IntId2(int element, int element2) 
        {
            if (element == element2)
                throw new ArgumentException("Duplicate values in set: " + element);
            this.element = element;
            this.element2 = element2;
        }

        public int Size => 2;

        //$public void forEach(IntConsumer consumer) {
        //    consumer.accept(element);
        //    consumer.accept(element2);
        //}

        //$public IntStream intStream() {
        //    return IntStream.of(element, element2);
        //}

        public int[] ToArray() => new int[] { element, element2 };

        public bool Contains(int value) => (value == element) || (value == element2);
    }
}
