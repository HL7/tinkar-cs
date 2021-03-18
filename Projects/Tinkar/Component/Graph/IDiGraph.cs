using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public interface IDiGraph<V> : IGraph<V>,
        IComparable,
        IEquivalent
        where V : IVertex
    {
        /// <summary>
        /// Gets the roots of this item.
        /// A graph can have multiple roots.
        /// </summary>
        ImmutableList<V> Roots { get; }

        /// <summary>
        /// Get predecessors of the indicated vertex.
        /// A directed graph can have multiple predecessors.
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns>predecessors of the provided vertex.Empty list if a root node.</returns>
        ImmutableList<V> Predecessors(V vertex);
    }
}
