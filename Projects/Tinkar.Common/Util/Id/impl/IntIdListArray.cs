using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public sealed class IntIdListArray : IIntIdList
    {
        private readonly int[] elements;

        public IntIdListArray(params int[] newElements)
        {
            this.elements = newElements;
        }

        public int Size => elements.Length;

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

        public int[] ToArray() => elements;

        public bool Contains(int value) => elements.Contains(value);

        public int Get(int index) => elements[index];
    }
}
