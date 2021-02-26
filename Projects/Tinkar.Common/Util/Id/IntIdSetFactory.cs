using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Tinkar.Common
{
    public enum IntIdSetFactory
    {
        INSTANCE;

    public IIntIdSet empty()
    {
        return IntId0Set.INSTANCE;
    }

    public IIntIdSet of()
    {
        return this.empty();
    }

    public IIntIdSet of(int one)
    {
        return new IntId1Set(one);
    }


    public IIntIdSet of(int one, int two)
    {
        return new IntId2Set(one, two);
    }

    public IIntIdSet of(params Int32[] elements)
    {
        if (elements == null || elements.length == 0)
        {
            return empty();
        }
        if (elements.length == 1)
        {
            return this.of(elements[0]);
        }
        if (elements.length == 2)
        {
            return this.of(elements[0], elements[1]);
        }
        if (elements.length < 1024)
        {
            return IntIdSetArray.newIntIdSet(elements);
        }
        return IntIdSetRoaring.newIntIdSet(elements);
    }

    public IIntIdSet ofAlreadySorted(params Int32[] elements)
    {
        if (elements == null || elements.length == 0)
        {
            return empty();
        }
        if (elements.length == 1)
        {
            return this.of(elements[0]);
        }
        if (elements.length == 2)
        {
            return this.of(elements[0], elements[1]);
        }
        if (elements.length < 1024)
        {
            return IntIdSetArray.newIntIdSetAlreadySorted(elements);
        }
        return IntIdSetRoaring.newIntIdSetAlreadySorted(elements);
    }
}
}
