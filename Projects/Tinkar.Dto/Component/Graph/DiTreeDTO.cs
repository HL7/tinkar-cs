using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    public record DiTreeDTO : GraphDTO<DiTreeVertexDTO>,
        IDiTree<DiTreeVertexDTO>,
        IJsonMarshalable,
        IMarshalable
    {
        public new sealed class Builder : Builder<Builder, DiTreeVertexDTO.Builder>
        {
            public DiTreeDTO Create()
            {
                List<DiTreeVertexDTO> vertexMap = this.vertexMap.Select((a) => a.Create()).ToList();
                return new DiTreeDTO(root.Create(), vertexMap.ToImmutableList());
            }
        }

        public new abstract class Builder<TBuilder, TVertexBuilder> : 
            GraphDTO<DiTreeVertexDTO>.Builder<TBuilder, TVertexBuilder>
            where TBuilder : Builder<TBuilder, TVertexBuilder>
            where TVertexBuilder : DiTreeVertexDTO.Builder<TVertexBuilder>, new()
        {

            protected DiTreeVertexDTO.Builder root = default(DiTreeVertexDTO.Builder);

            public TBuilder SetRoot(DiTreeVertexDTO.Builder root)
            {
                this.root = root;
                return (TBuilder)this;
            }
        }

        public FieldDataType FieldDataType => FieldDataType.DiGraphType;

        public DiTreeVertexDTO Root { get; init; }

        public DiTreeDTO(DiTreeVertexDTO root,
                        ImmutableList<DiTreeVertexDTO> vertexMap) : base(vertexMap)
        {
            this.Root = root;
        }

        public DiTreeVertexDTO Predecessor(DiTreeVertexDTO vertex) =>
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
            //$ImmutableList<DiTreeVertexDTO> vertexMap = GraphDTO.UnmarshalVertexMap(input);
            //var successorMap = GraphDTO.UnmarshalSuccessorMap(input, vertexMap);
            //DiTreeVertexDTO root = vertexMap[input.GetInt32()];
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
