using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    public class DiTree<V> : Graph<V>, IDiTree<V>
        where V : IVertex
    {
        /// <summary>
        /// Get root of tree. Tree can only have one root.
        /// </summary>
        public V Root => throw new NotImplementedException("XXYYZ");

        /// <summary>
        /// Get predecessor of vertex.
        /// </summary>
        /// <param name="vertex">Get predecessor of this vertex</param>
        /// <returns>Predecessor, or null if root</returns>
        public V Predecessor(V vertex) => throw new NotImplementedException("XXYYZ");

        /// <summary>
        /// Get dictionary of all predecessors
        /// </summary>
        public ImmutableDictionary<Int32, Int32> PredecessorMap =>
            throw new NotImplementedException("XXYYZ");
    }
}
