using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public interface IGraph<V>:
        IComparable, IComparable<IGraph<V>>,
        IEquivalent, IEquivalent<IGraph<V>>
        where V : IVertex
    {
        ImmutableList<V> VertexMap { get; }

        /// <summary>
        /// Gets the vertex associated with the identifier
        /// </summary>
        /// <param name="vertexId">a universally unique identifier for a vertex</param>
        /// <returns>Vertex associated with the identifier</returns>
        V Vertex(Guid vertexId);

        V Vertex(int vertexSequence);

        ImmutableDictionary<Int32, ImmutableList<Int32>> SuccessorMap { get; }

        /// <summary>
        /// Gets the successors for the provided vertex
        /// </summary>
        /// <param name="vertex">vertex a vertex to retrieve the successors of</param>
        /// <returns>Successofs of indicated vertex</returns>
        IEnumerable<V> Successors(V vertex);
    }
}
