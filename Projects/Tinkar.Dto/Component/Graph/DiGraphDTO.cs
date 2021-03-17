using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    /// <summary>
    /// DiGraphDTO sealed class.
    /// This is the class to use when creating DiGraph items.
    /// </summary>
    public sealed record DiGraphDTO : DiGraphDTO<DiGraphVertexDTO>
    {
        public sealed class Builder : DiGraphDTO<DiGraphVertexDTO>.Builder<Builder, DiGraphVertexDTO.Builder>
        {
            public DiGraphDTO Create()
            {
                List<DiGraphVertexDTO> roots = this.roots.Select((a) => a.Create()).ToList();
                List<DiGraphVertexDTO> vertexMap = this.vertexMap.Select((a) => a.Create()).ToList();
                return new DiGraphDTO(roots.ToImmutableList(), vertexMap.ToImmutableList());
            }
        }

        public DiGraphDTO(ImmutableList<DiGraphVertexDTO> roots,
            ImmutableList<DiGraphVertexDTO> vertexMap) : base(roots, vertexMap)
        {
        }
    }

    /// <summary>
    /// DiGraphDTO abstract class.
    /// This is the class to inherit from when creating child classes.
    /// This class should never be directly instantiated.
    /// </summary>
    public abstract record DiGraphDTO<TVertex> : GraphDTO<TVertex>,
        IDiGraph<TVertex>,
        IJsonMarshalable,
        IMarshalable
        where TVertex : DiGraphVertexDTO
    {
        public new abstract class Builder<TBuilder, TVertexBuilder> :
            GraphDTO<TVertex>.Builder<TBuilder, TVertexBuilder>
            where TBuilder : Builder<TBuilder, TVertexBuilder>
            where TVertexBuilder : DiGraphVertexDTO.Builder<TVertexBuilder>, new()
        {
            protected List<TVertexBuilder> roots { get; } = new List<TVertexBuilder>();

            public TBuilder AppendRoot(TVertexBuilder root)
            {
                this.roots.Add(root);
                return (TBuilder)this;
            }
        }

        /// <summary>
        /// Unique id for this data field.
        /// </summary>
        public FieldDataType FieldDataType => FieldDataType.DiGraphType;

        /// <summary>
        /// Gets the roots of this item.
        /// A graph can have multiple roots.
        /// </summary>
        public ImmutableList<TVertex> Roots { get; init; }

        public DiGraphDTO(ImmutableList<TVertex> roots,
                        ImmutableList<TVertex> vertexMap) : base(vertexMap)
        {
            this.Roots = roots;
        }


        /// <summary>
        /// Get predecessors of the indicated vertex.
        /// A directed graph can have multiple predecessors.
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns>predecessors of the provided vertex.Empty list if a root node.</returns>
        public ImmutableList<TVertex> Predecessors(TVertex vertex) => 
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
