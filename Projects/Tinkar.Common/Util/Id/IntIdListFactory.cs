using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Tinkar.Common
{
    public enum IntIdListFactory
    {
        INSTANCE;

    public IIntIdList empty()
    {
        return IntId0List.INSTANCE;
    }

    public IIntIdList of()
    {
        return this.empty();
    }

    public IIntIdList of(int one)
    {
        return new IntId1List(one);
    }

    public IIntIdList of(int one, int two)
    {
        return new IntId2List(one, two);
    }

    public IIntIdList of(params Int32[] elements)
    {
        if (elements == null || elements.length == 0)
        {
            return this.empty();
        }
        if (elements.length == 1)
        {
            return new IntId1List(elements[0]);
        }
        if (elements.length == 2)
        {
            return new IntId2List(elements[0], elements[1]);
        }
        return new IntIdListArray(elements);
    }
}
}
