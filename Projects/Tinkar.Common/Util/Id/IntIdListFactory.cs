using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Tinkar.Common
{
    public class IntIdListFactory
    {
        public static IntIdListFactory INSTANCE { get; } = new IntIdListFactory();

        public IIntIdList Empty() => IntId0List.INSTANCE;
        public IIntIdList Of() => this.Empty();
        public IIntIdList Of(int one) => new IntId1List(one);
        public IIntIdList Of(int one, int two) => new IntId2List(one, two);
        public IIntIdList Of(params Int32[] elements)
        {
            if (elements == null || elements.Length == 0)
                return this.Empty();
            if (elements.Length == 1)
                return new IntId1List(elements[0]);
            if (elements.Length == 2)
                return new IntId2List(elements[0], elements[1]);
            return new IntIdListArray(elements);
        }
    }
}
