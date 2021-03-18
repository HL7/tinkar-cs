using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    /// <summary>
    /// Directed Graph abstract base class.
    /// This class is instantiable.
    /// </summary>
    public sealed record DiGraphDTO : DiGraphDTO<DiGraphVertexDTO>
    {
        /// <summary>
        /// This is builder class for creating DiGraphDTO items.
        /// </summary>
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
    /// Directed Graph abstract base class.
    /// This class is intended to be used only for inheritance..
    /// </summary>
    /// <typeparam name="TVertex">Type of vertex class that child class implements</typeparam>
    public abstract record DiGraphDTO<TVertex> : GraphDTO<TVertex>,
        IDiGraph<TVertex>,
        IJsonMarshalable,
        IMarshalable
        where TVertex : DiGraphVertexDTO
    {
        /// <summary>
        /// abstract builder class.
        /// This is the class to inherit from when creating child classes.
        /// This class should never be directly instantiated.
        /// </summary>
        /// <typeparam name="TBuilder">Child builder class</typeparam>
        /// <typeparam name="TVertexBuilder">Child vertex builder class</typeparam>
        public new abstract class Builder<TBuilder, TVertexBuilder> :
            GraphDTO<TVertex>.Builder<TBuilder, TVertexBuilder>
            where TBuilder : Builder<TBuilder, TVertexBuilder>
            where TVertexBuilder : DiGraphVertexDTO.Builder<TVertexBuilder>, new()
        {
            protected List<TVertexBuilder> roots { get; } = new List<TVertexBuilder>();

            public TBuilder ClearRoot()
            {
                this.roots.Clear();
                return (TBuilder)this;
            }

            public TBuilder AppendRoots(params TVertexBuilder[] roots)
            {
                this.roots.AddRange(roots);
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
