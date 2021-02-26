using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public class IntId2Set : IntId2, IIntIdSet
    {
        public IntId2Set(int element, int element2) : base(element, element2)
        {
        }
    }
}
