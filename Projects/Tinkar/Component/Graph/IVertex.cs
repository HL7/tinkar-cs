using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public interface IVertex
    {
        /// <summary>
        /// Gets universally unique identifier for this vertex
        /// 
        /// </summary>
        IVertexId VertexId { get; }

        /// <summary>
        /// Gets tindex of this vertex within its graph. The index is locally
        /// unique within a graph, but not across graphs, or different versions of the same graph.
        /// Vertex index is not used in equality or hash calculations.
        /// </summary>
        int VertexIndex { get; }

        /// <summary>
        /// Concept that represents the meaning of this vertex.
        /// </summary>
        IConcept Meaning { get; }

        /// <summary>
        /// Gets optional object that is associated with the properly concept.
        /// </summary>
        /// <typeparam name="T">Type of the property object</typeparam>
        /// <param name="propertyConcept">Property Concept</param>
        /// <returns>Property associated with concept</returns>
        T Property<T>(IConcept propertyConcept);

        /// <summary>
        /// Gets optional object that is associated with the properly concept.
        /// </summary>
        /// <typeparam name="T">Type of the property object</typeparam>
        /// <param name="propertyConcept">Property Concept</param>
        /// <returns>Property associated with concept</returns>
        T PropertyFast<T>(IConcept propertyConcept);

        //CEnum PropertyKeys<CEnum> { get; }
        /// <summary>
        /// Gets keys for the populated properties
        /// </summary>
        /// <returns>keys</returns>
        IEnumerable<IConcept> PropertyKeys { get; }


        ImmutableDictionary<IConcept, Object> Properties { get;}
    }
}
