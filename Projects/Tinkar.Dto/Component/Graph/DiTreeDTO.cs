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
    public sealed record DiTreeDTO : DiTreeDTO<DiTreeVertexDTO>,
        //IJsonMarshalable,
        IMarshalable
    {
#warning are successor and predecessor alawys paired, or can a node havea predecessor thsi it is not a successor of of vice versa.
        /// <summary>
        /// This is builder class for creating DiTreeDTO items.
        /// </summary>
        public sealed class Builder : DiTreeDTO<DiTreeVertexDTO>.Builder<Builder, DiTreeVertexDTO.Builder>
        {
            public DiTreeDTO Create()
            {
                if (this.root == null)
                    throw new Exception($"No root set fpr DiTree instance");
                DiTreeVertexDTO root = this.root.Create();

                DiTreeVertexDTO[] vertexMap = new DiTreeVertexDTO[this.vertexMap.Count];
                for (Int32 i = 0; i < this.vertexMap.Count; i++)
                {
                    DiTreeVertexDTO.Builder vmBldr = this.vertexMap[i];
                    if ((vmBldr.Predecessor == null) && (vmBldr != this.root))
                        throw new Exception($"DiTree contains item '{vmBldr.VertexId}' with no predecessor that is not marked as a root");
                    vertexMap[i] = vmBldr.Create();
                }
                return new DiTreeDTO(root.VertexIndex, vertexMap.ToImmutableArray());
            }
        }

        public DiTreeDTO(Int32 rootIndex,
            ImmutableArray<DiTreeVertexDTO> vertexMap) : base(rootIndex, vertexMap)
        {
        }

        //public void Marshal(TinkarJsonOutput output) => throw new NotImplementedException();

        public static DiTreeDTO Make(TinkarInput input)
        {
            Int32 vertexCount = input.GetInt32();
            DiTreeVertexDTO[] vertexMap = new DiTreeVertexDTO[vertexCount];
            for (Int32 i = 0; i < vertexCount; i++)
                vertexMap[i] = DiTreeVertexDTO.Make(input);
            Int32 rootIndex = input.GetInt32();

            return new DiTreeDTO(rootIndex, vertexMap.ToImmutableArray());
        }

        public void Marshal(TinkarOutput output)
        {
            output.WriteInt32(this.VertexMap.Length);
            for (Int32 i = 0; i < this.VertexMap.Length; i++)
                this.VertexMap[i].Marshal(output);
            output.WriteInt32(this.Root.VertexIndex);
        }
    }

    /// <summary>
    /// This is builder class for creating Builder derived classes.
    /// This should never be used directly, it only should be inherited from.
    /// </summary>
    /// <typeparam name="TVertex">Vertex class</typeparam>
    public abstract record DiTreeDTO<TVertex> : GraphDTO<TVertex>
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

        public TVertex Root => this.VertexMap[this.rootIndex];
        Int32 rootIndex = -1;

        public DiTreeDTO(Int32 rootIndex,
                        ImmutableArray<TVertex> vertexMap) : base(vertexMap)
        {
            this.rootIndex = rootIndex;
        }

        public TVertex Predecessor(TVertex vertex)
        {
            Int32 predecessorIndex = vertex.Predecessor;
            if (predecessorIndex == vertex.VertexIndex)
                return null;
            return this.VertexMap[predecessorIndex];
        }

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
    }
}
