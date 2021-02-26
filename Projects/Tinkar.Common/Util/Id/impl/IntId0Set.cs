using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public class IntId0Set : IntId0, IIntIdSet
    {
        public static IntId0Set INSTANCE { get; }  = new IntId0Set();
    }
}
