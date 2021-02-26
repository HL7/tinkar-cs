using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class IntId2List : IntId2, IIntIdList
    {
        public IntId2List(int element, int element2) : base(element, element2)
        {
        }

        public int Get(int index)
        {
            if (index == 0)
                return element;
            if (index == 1)
                return element2;
            throw new IndexOutOfRangeException("Index: " + index);
        }
    }
}
