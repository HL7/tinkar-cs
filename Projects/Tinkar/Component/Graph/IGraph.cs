using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public interface IGraph<V>
        where V : IVertex
    {
        IEnumerable<V> VertexMap { get; }

        /// <summary>
        /// Gets the vertex associated with the identifier
        /// </summary>
        /// <param name="vertexId">a universally unique identifier for a vertex</param>
        /// <returns>Vertex associated with the identifier</returns>
        V Vertex(Guid vertexId);

        V Vertex(int vertexSequence);

        //ImmutableDictionary<>
        //ImmutableIntObjectMap<ImmutableIntList> successorMap();

        /// <summary>
        /// Gets the successors for the provided vertex
        /// </summary>
        /// <param name="vertex">vertex a vertex to retrieve the successors of</param>
        /// <returns>Successofs of indicated vertex</returns>
        IEnumerable<V> Successors(V vertex);
    }
}
