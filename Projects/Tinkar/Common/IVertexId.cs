﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public interface IVertexId : ITinkarId, IComparable<IVertexId>
    {
    }
}
