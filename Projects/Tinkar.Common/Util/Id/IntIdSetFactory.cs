using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Tinkar.Common
{
    public class IntIdSetFactory
    {
        public static IntIdSetFactory INSTANCE { get; } = new IntIdSetFactory();

        public IIntIdSet Empty() => IntId0Set.INSTANCE;
        public IIntIdSet Of() => this.Empty();
        public IIntIdSet Of(int one) => new IntId1Set(one);
        public IIntIdSet Of(int one, int two) => new IntId2Set(one, two);
        public IIntIdSet Of(params Int32[] elements)
        {
            if (elements == null || elements.Length == 0)
                return Empty();
            if (elements.Length == 1)
                return this.Of(elements[0]);
            if (elements.Length == 2)
                return this.Of(elements[0], elements[1]);
            //$if (elements.Length < 1024)
            return IntIdSetArray.NewIntIdSet(elements);
            //$return IntIdSetRoaring.newIntIdSet(elements);
        }

        public IIntIdSet OfAlreadySorted(params Int32[] elements)
        {
            if (elements == null || elements.Length == 0)
                return Empty();
            if (elements.Length == 1)
                return this.Of(elements[0]);
            if (elements.Length == 2)
                return this.Of(elements[0], elements[1]);
            //$if (elements.Length < 1024)
            return IntIdSetArray.NewIntIdSetAlreadySorted(elements);
            //$return IntIdSetRoaring.newIntIdSetAlreadySorted(elements);
        }
    }
}
