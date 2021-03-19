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
    public sealed record DiGraphDTO : DiGraphDTO<DiGraphVertexDTO>,
        IJsonMarshalable,
        IMarshalable
    {
        #warning are successor and predecessor alawys paired, or can a node havea predecessor thsi it is not a successor of of vice versa.
        /// <summary>
        /// This is builder class for creating DiGraphDTO items.
        /// </summary>
        public sealed class Builder : DiGraphDTO<DiGraphVertexDTO>.Builder<Builder, DiGraphVertexDTO.Builder>
        {
            public DiGraphDTO Create()
            {
                List<Int32> roots = this.roots.Select((a) => a.VertexIndex).ToList();
                List<DiGraphVertexDTO> vertexMap = this.vertexMap.Select((a) => a.Create()).ToList();
                return new DiGraphDTO(roots.ToImmutableArray(), vertexMap.ToImmutableArray());
            }
        }

        public DiGraphDTO(ImmutableArray<Int32> roots,
            ImmutableArray<DiGraphVertexDTO> vertexMap) : base(roots, vertexMap)
        {
        }

        public static DiGraphDTO Make(TinkarInput input)
        {
            Int32 vertexCount = input.GetInt32();
            DiGraphVertexDTO[] vertexMap = new DiGraphVertexDTO[vertexCount];
            for (Int32 i = 0; i < vertexCount; i++)
                vertexMap[i] = DiGraphVertexDTO.Make(input);

            Int32 rootsCount = input.GetInt32();
            Int32[] roots = new Int32[rootsCount];
            for (Int32 i = 0; i < rootsCount; i++)
                roots[i] = input.GetInt32();

            return new DiGraphDTO(roots.ToImmutableArray(), vertexMap.ToImmutableArray());
        }

        /// <summary>
        /// Marshal class data to binary stream.
        /// </summary>
        /// <param name="output">binary output stream.</param>
        public void Marshal(TinkarOutput output)
        {
            output.WriteInt32(this.VertexMap.Length);
            for (Int32 i = 0; i < this.VertexMap.Length; i++)
                this.VertexMap[i].Marshal(output);
            output.WriteInt32(this.roots.Length);
            for (Int32 i = 0; i < this.roots.Length; i++)
                output.WriteInt32(this.roots[i]);
        }

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public void Marshal(TinkarJsonOutput output) => throw new NotImplementedException("xxyyz");
    }

    /// <summary>
    /// Directed Graph abstract base class.
    /// This class is intended to be used only for inheritance..
    /// </summary>
    /// <typeparam name="TVertex">Type of vertex class that child class implements</typeparam>
    public abstract record DiGraphDTO<TVertex> : GraphDTO<TVertex>,
        IDiGraph<TVertex>
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

            public TBuilder ClearRoots()
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
        public ImmutableArray<TVertex> Roots => this.roots.Select((a) => this.VertexMap[a]).ToImmutableArray();

        /// <summary>
        /// Gets the roots of this item.
        /// A graph can have multiple roots.
        /// </summary>
        protected ImmutableArray<Int32> roots;

        public DiGraphDTO(ImmutableArray<Int32> roots,
                        ImmutableArray<TVertex> vertexMap) : base(vertexMap)
        {
            this.roots = roots;
        }


        /// <summary>
        /// Get predecessors of the indicated vertex.
        /// A directed graph can have multiple predecessors.
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns>predecessors of the provided vertex.Empty list if a root node.</returns>
        public ImmutableArray<TVertex> Predecessors(TVertex vertex)
        {
            TVertex[] retVal = new TVertex[vertex.Predecessors.Length];
            for (Int32 i = 0; i < vertex.Predecessors.Length; i++)
                retVal[i] = this.VertexMap[vertex.Predecessors[i]];
            return retVal.ToImmutableArray();
        }
    }
}
