using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public interface ISpatialPoint : IComparable<ISpatialPoint>, IComparable
    {
        public Int32 X { get; init; }
        public Int32 Y { get; init; }
        public Int32 Z { get; init; }
    }
}
