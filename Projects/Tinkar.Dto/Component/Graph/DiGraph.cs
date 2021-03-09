using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    public class DiGraph<V> :  Graph<V>, IDiGraph<V>
        where V : IVertex
    {
        /// <summary>
        /// Gets the roots of this item.
        /// A graph can have multiple roots.
        /// </summary>
        public IEnumerable<V> Roots => throw new NotImplementedException("XXYYZ");

        /// <summary>
        /// Get predecessors of the indicated vertex.
        /// A directed graph can have multiple predecessors.
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns>predecessors of the provided vertex.Empty list if a root node.</returns>
        public IEnumerable<V> Predecessors(V vertex) => 
            throw new NotImplementedException("XXYYZ");
    }
}
