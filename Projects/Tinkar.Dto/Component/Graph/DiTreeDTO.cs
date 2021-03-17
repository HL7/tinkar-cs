using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    /// <summary>
    /// Instantiable sealed builder class.
    /// This is the class meant for use when directly instantiating a Builder
    /// </summary>
    public sealed record DiTreeDTO : DiTreeDTO<DiTreeVertexDTO>
    {
        /// <summary>
        /// This is builder class for creating DiTreeDTO items.
        /// </summary>
        public sealed class Builder : DiTreeDTO<DiTreeVertexDTO>.Builder<Builder, DiTreeVertexDTO.Builder>
        {
            public DiTreeDTO Create()
            {
                DiTreeVertexDTO root = this.root.Create();
                List<DiTreeVertexDTO> vertexMap = this.vertexMap.Select((a) => a.Create()).ToList();
                return new DiTreeDTO(root, vertexMap.ToImmutableList());
            }
        }

        public DiTreeDTO(DiTreeVertexDTO root,
            ImmutableList<DiTreeVertexDTO> vertexMap) : base(root, vertexMap)
        {
        }
    }

    /// <summary>
    /// This is builder class for creating Builder derived classes.
    /// This should never be used directly, it only should be inherited from.
    /// </summary>
    /// <typeparam name="TVertex">Vertex class</typeparam>
    public abstract record DiTreeDTO<TVertex> : GraphDTO<TVertex>,
        IJsonMarshalable,
        IMarshalable
        where TVertex : DiTreeVertexDTO
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
            where TVertexBuilder : DiTreeVertexDTO.Builder<TVertexBuilder>, new()
        {

            protected TVertexBuilder root = default(TVertexBuilder);

            public TBuilder SetRoot(TVertexBuilder root)
            {
                this.root = root;
                return (TBuilder)this;
            }
        }

        public FieldDataType FieldDataType => FieldDataType.DiTreeType;

        public TVertex Root { get; init; }

        public DiTreeDTO(TVertex root,
                        ImmutableList<TVertex> vertexMap) : base(vertexMap)
        {
            this.Root = root;
        }

        public TVertex Predecessor(TVertex vertex) =>
            this.VertexMap[vertex.Predecessor];

        public override Int32 CompareTo(Object o)
        {
            DiTreeDTO other = o as DiTreeDTO;
            if (o == null)
                return this.GetType().FullName.CompareTo(o.GetType().FullName);
            Int32 cmpVal = base.CompareTo(other);
            if (cmpVal != 0)
                return cmpVal;

            cmpVal = this.Root.CompareTo(other.Root);
            if (cmpVal != 0)
                return cmpVal;

            return 0;
        }

        public override bool IsEquivalent(Object o)
        {
            DiTreeDTO other = o as DiTreeDTO;
            if (o == null)
                return false;
            if (base.IsEquivalent(other) == false)
                return false;

            if (this.Root.IsEquivalent(other.Root) == false)
                return false;

            return true;
        }

        public void Marshal(TinkarJsonOutput output) => throw new NotImplementedException();

        public static DiTreeDTO Make(TinkarInput input)
        {
            throw new NotImplementedException("xxyyz");
            //$ImmutableList<TVertex> vertexMap = GraphDTO.UnmarshalVertexMap(input);
            //var successorMap = GraphDTO.UnmarshalSuccessorMap(input, vertexMap);
            //TVertex root = vertexMap[input.GetInt32()];
            //int predecessorMapSize = input.GetInt32();
            //ImmutableDictionary<Int32, Int32>.Builder predecessorMap = ImmutableDictionary<Int32, Int32>.Empty.ToBuilder();
            //for (int i = 0; i < predecessorMapSize; i++)
            //    predecessorMap.Add(input.GetInt32(), input.GetInt32());
            //return new DiTreeDTO(root, predecessorMap.ToImmutable(), vertexMap, successorMap);
        }

        public void Marshal(TinkarOutput output)
        {
            this.MarshalVertexMap(output);

            output.WriteInt32(this.Root.VertexIndex);
        }
    }
}
