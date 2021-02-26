using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public class IntId1Set : IntId1, IIntIdSet
    {
        public IntId1Set(int element) : base(element)
        {
        }
    }
}
