using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public class IntId0List : IntId0, IIntIdList
    {
        public static IntId0List INSTANCE { get; } = new IntId0List();
    }
}
