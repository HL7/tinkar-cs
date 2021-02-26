using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public class IntId1List : IntId1, IIntIdList
    {
        public IntId1List(int element) : base(element)
        {
        }
    }
}
