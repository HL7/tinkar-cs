using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    public class DiGraphDTO<V> :  GraphDTO<V>, 
        IDiGraph<V>,
        IJsonMarshalable,
        IMarshalable
        where V : IVertex
    {
        /// <summary>
        /// Unique id for this data field.
        /// </summary>
        public FieldDataType FieldDataType => FieldDataType.DiGraphType;

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

        /// <summary>
        /// Marshal class data to binary stream.
        /// </summary>
        /// <param name="output">binary output stream.</param>
        public void Marshal(TinkarOutput output) => throw new NotImplementedException("xxyyz");

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public void Marshal(TinkarJsonOutput output) => throw new NotImplementedException("xxyyz");
    }
}
