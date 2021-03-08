using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public interface IPlanarPoint : IComparable, IComparable<IPlanarPoint>
    {
        public Int32 X { get; init; }
        public Int32 Y { get; init; }
    }
}
