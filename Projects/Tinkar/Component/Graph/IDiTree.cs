using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public interface IDiTree<V> : IGraph<V>,
        IComparable,
        IEquivalent
        where V : IVertex
    {
        /// <summary>
        /// Get root of tree. Tree can only have one root.
        /// </summary>
        V Root { get; }

        /// <summary>
        /// Get predecessor of vertex.
        /// </summary>
        /// <param name="vertex">Get predecessor of this vertex</param>
        /// <returns>Predecessor, or null if root</returns>
        V Predecessor(V vertex);

        /// <summary>
        /// Get dictionary of all predecessors
        /// </summary>
        ImmutableDictionary<Int32, Int32> PredecessorMap { get; }
    }
}
