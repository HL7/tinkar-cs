using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
#if NEVER
    public class DiTreeDTO : GraphDTO,
        IDiTree<VertexDTO>,
        IJsonMarshalable,
        IMarshalable
    {
        /// <summary>
        /// Unique id for this data field.
        /// </summary>
        public FieldDataType FieldDataType => FieldDataType.DiTreeType;

        /// <summary>
        /// Get root of tree. Tree can only have one root.
        /// </summary>
        public VertexDTO Root => throw new NotImplementedException("XXYYZ");

        /// <summary>
        /// Get predecessor of vertex.
        /// </summary>
        /// <param name="vertex">Get predecessor of this vertex</param>
        /// <returns>Predecessor, or null if root</returns>
        public VertexDTO Predecessor(VertexDTO vertex) => throw new NotImplementedException("XXYYZ");

        /// <summary>
        /// Get dictionary of all predecessors
        /// </summary>
        public ImmutableDictionary<Int32, Int32> PredecessorMap =>
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
#endif
}
