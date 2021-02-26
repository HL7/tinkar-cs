using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public class IntIdSetArray : IIntIdSet
    {
        private List<int> elements;

        private IntIdSetArray(Int32[] newElements, bool sortFlag)
        {
            this.elements = new List<Int32>(newElements);
            if (sortFlag)
                this.elements.Sort();
        }

        public static IntIdSetArray NewIntIdSet(params Int32[] newElements) => new IntIdSetArray(newElements, true);

        public static IntIdSetArray NewIntIdSetAlreadySorted(params Int32[] newElements) => new IntIdSetArray(newElements, false);

        public int Size => elements.Count;

        //$public void forEach(IntConsumer consumer)
        //{
        //    for (int element: elements)
        //    {
        //        consumer.accept(element);
        //    }
        //}

        //$public IntStream intStream()
        //{
        //    return IntStream.of(elements);
        //}

        public int[] ToArray() => elements.ToArray();

        public bool Contains(int value)
        {
            // for small lists, iteration is faster search than binary search because of less branching.
            if (elements.Count < 32)
                return elements.Contains(value);
            return elements.BinarySearch(value) > 0 ? true : false;
        }
    }
}
