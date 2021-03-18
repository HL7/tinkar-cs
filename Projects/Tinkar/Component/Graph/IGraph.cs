using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public interface IGraph<TVertex>:
        IComparable,
        IEquivalent
        where TVertex : IVertex
    {
        /// <summary>
        /// Gets the vertex associated with the identifier
        /// </summary>
        /// <param name="vertexId">a universally unique identifier for a vertex</param>
        /// <returns>Vertex associated with the identifier</returns>
        TVertex Vertex(Guid vertexId);

        TVertex Vertex(int vertexSequence);
    }
}
