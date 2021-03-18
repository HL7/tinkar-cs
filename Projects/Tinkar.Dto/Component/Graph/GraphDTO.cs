using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    /// <summary>
    /// Graph base class.
    /// This class is instantiable.
    /// </summary>
    public sealed record GraphDTO : GraphDTO<GraphVertexDTO>
    {
        /// <summary>
        /// This is builder class for creating GraphDTO items.
        /// </summary>
        public sealed class Builder : GraphDTO<GraphVertexDTO>.Builder<Builder, GraphVertexDTO.Builder>
        {
            public GraphDTO Create() =>
                new GraphDTO(this.vertexMap.Select( (a) => a.Create()).ToImmutableList());
        }

        public GraphDTO(ImmutableList<GraphVertexDTO> vertexMap) : base(vertexMap)
        {
        }
    }

    /// <summary>
    /// Graph abstract base class.
    /// This class is intended to be used only for inheritance..
    /// </summary>
    public abstract record GraphDTO<TVertex> : IGraph<TVertex>
        where TVertex : GraphVertexDTO
    {
        /// <summary>
        /// This is builder class for creating Builder derived classes.
        /// This should never be used directly, it only should be inherited from.
        /// </summary>
        /// <typeparam name="TBuilder">Derived builder type</typeparam>
        /// <typeparam name="TVertexBuilder">Derived vertex builder type</typeparam>
        public abstract class Builder<TBuilder, TVertexBuilder> : VertexDTO.Builder<TBuilder>
            where TBuilder : Builder<TBuilder, TVertexBuilder>
            where TVertexBuilder : VertexDTO.Builder<TVertexBuilder>, new()
        {
            protected List<TVertexBuilder> vertexMap = new List<TVertexBuilder>();

            public TVertexBuilder AppendVertex()
            {
                TVertexBuilder retVal = new TVertexBuilder()
                    .SetVertexIndex(vertexMap.Count)
                    ;
                vertexMap.Add(retVal);
                return retVal;
            }

            public TVertexBuilder Vertex(Guid vertexId)
            {
                foreach (TVertexBuilder bldr in this.vertexMap)
                {
                    if (bldr.VertexId.Uuid == vertexId)
                        return bldr;
                }
                throw new Exception($"Vertex '{vertexId}' not found");
            }

            public TVertexBuilder AppendVertex(Guid vertexId, ConceptDTO meaning)
            {
                TVertexBuilder retVal = this.AppendVertex();
                retVal.SetMeaning(meaning);
                retVal.SetVertexId(vertexId);
                return retVal;
            }

            public TVertexBuilder AppendVertex(Int64 vertexIdMsb, Int64 vertexIdLsb, ConceptDTO meaning)
            {
                TVertexBuilder retVal = this.AppendVertex();
                retVal.SetMeaning(meaning);
                retVal.SetVertexId(vertexIdMsb, vertexIdLsb);
                return retVal;
            }
        }

        public ImmutableList<TVertex> VertexMap { get; init; }

        public GraphDTO(ImmutableList<TVertex> vertexMap)
        {
            this.VertexMap = vertexMap;
        }

        /// <summary>
        /// Gets the vertex associated with the identifier
        /// </summary>
        /// <param name="vertexId">a universally unique identifier for a vertex</param>
        /// <returns>Vertex associated with the identifier</returns>
        public TVertex Vertex(Guid vertexId)
        {
            foreach (TVertex vertexDTO in this.VertexMap)
            {
                if (vertexDTO.VertexId.Uuid == vertexId)
                    return vertexDTO;
            }
            throw new Exception("No vertex for: " + vertexId);
        }

#warning 'Does java return null if not found or throw exception'
        public TVertex Vertex(int vertexSequence) => this.VertexMap[vertexSequence];

        /// <summary>
        /// Gets the successors for the provided vertex
        /// </summary>
        /// <param name="vertex">vertex a vertex to retrieve the successors of</param>
        /// <returns>Successofs of indicated vertex</returns>
        public IEnumerable<TVertex> Successors(TVertex vertex)
        {
            TVertex[] retVal = new TVertex[vertex.Successors.Count];
            for (Int32 i = 0; i < vertex.Successors.Count; i++)
                retVal[i] = this.VertexMap[vertex.Successors[i]];
            return retVal;
        }

        public void MarshalVertexMap(TinkarOutput output)
        {
            output.WriteInt32(this.VertexMap.Count());
            foreach (TVertex vertexDTO in this.VertexMap)
                vertexDTO.Marshal(output);
        }

        Int32 Comparer(ImmutableList<Int32> value1, ImmutableList<Int32> value2)
        {
            Int32 cmpVal = value1.Count.CompareTo(value2.Count);
            if (cmpVal != 0)
                return cmpVal;
            for (Int32 j = 0; j < value1.Count; j++)
            {
                cmpVal = value1[j].CompareTo(value2[j]);
                if (cmpVal != 0)
                    return cmpVal;
            }
            return 0;
        }

        public virtual Int32 CompareTo(Object o)
        {
            GraphDTO<TVertex> other = o as GraphDTO<TVertex>;
            if (o == null)
                return this.GetType().FullName.CompareTo(o.GetType().FullName);
            Int32 cmpVal = FieldCompare.CompareSequence(this.VertexMap, other.VertexMap);
            if (cmpVal != 0)
                return cmpVal;
            return 0;
        }

        public virtual Boolean IsEquivalent(Object o)
        {
            GraphDTO<TVertex> other = o as GraphDTO<TVertex>;
            if (other == null)
                return false;

            if (FieldCompare.CompareSequence(this.VertexMap, other.VertexMap) != 0)
                return false;

            return true;
        }
    }
}
