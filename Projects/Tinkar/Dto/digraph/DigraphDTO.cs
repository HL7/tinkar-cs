using System;
using System.Collections.Generic;

namespace Tinkar
{
    public record DigraphDTO
    {
        internal Guid[] nodes { get; init; }
        internal int[][] adjacencyList { get; init; }
    }
}