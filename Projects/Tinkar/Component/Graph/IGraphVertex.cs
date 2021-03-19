using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public interface IGraphVertex : IVertex,
        IComparable,
        IEquivalent
    {
        /// <summary>
        /// Gets the successors for the provided vertex
        /// </summary>
        /// <returns>Successors of vertex</returns>
        ImmutableArray<Int32> Successors { get; }
    }
}
