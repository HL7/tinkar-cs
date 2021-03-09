using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    public class Graph<V> : IGraph<V>
        where V : IVertex
    {
        public IEnumerable<V> VertexMap => throw new NotImplementedException("xxyyz");

        /// <summary>
        /// Gets the vertex associated with the identifier
        /// </summary>
        /// <param name="vertexId">a universally unique identifier for a vertex</param>
        /// <returns>Vertex associated with the identifier</returns>
        public V Vertex(Guid vertexId) => throw new NotImplementedException("xxyyz");

        public V Vertex(int vertexSequence) => throw new NotImplementedException("xxyyz");

        //ImmutableDictionary<>
        //ImmutableIntObjectMap<ImmutableIntList> successorMap();

        /// <summary>
        /// Gets the successors for the provided vertex
        /// </summary>
        /// <param name="vertex">vertex a vertex to retrieve the successors of</param>
        /// <returns>Successofs of indicated vertex</returns>
        public IEnumerable<V> Successors(V vertex) => throw new NotImplementedException("xxyyz");
    }
}
