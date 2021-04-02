using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    public record DiTreeDTO : GraphDTO,
        IDiTree<VertexDTO>,
        IJsonMarshalable,
        IMarshalable
    {
        public VertexDTO Root { get; init; }

        public ImmutableDictionary<Int32, Int32> PredecessorMap { get; init; }

        public FieldDataType FieldDataType => FieldDataType.DiGraphType;

        public DiTreeDTO(VertexDTO root,
                        ImmutableDictionary<Int32, Int32> predecessorMap,
                        ImmutableList<VertexDTO> vertexMap,
                        ImmutableDictionary<Int32, ImmutableList<Int32>> successorMap) : base(vertexMap, successorMap)
        {
            this.Root = root;
            this.PredecessorMap = predecessorMap;
        }

        public VertexDTO Predecessor(VertexDTO vertex)
        {
            if (this.PredecessorMap.TryGetValue(vertex.VertexIndex, out int predecessorIndex) == false)
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

            cmpVal = FieldCompare.CompareMap(this.PredecessorMap, other.PredecessorMap, (a, b) => a.CompareTo(b));
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

            if (FieldCompare.CompareMap(this.PredecessorMap, other.PredecessorMap, (a, b) => a.CompareTo(b)) != 0)
                return false;

            return true;
        }

        public void Marshal(TinkarJsonOutput output) => throw new NotImplementedException();

        public static DiTreeDTO Make(TinkarInput input)
        {
            ImmutableList<VertexDTO> vertexMap = GraphDTO.UnmarshalVertexMap(input);
            var successorMap = GraphDTO.UnmarshalSuccessorMap(input, vertexMap);
            VertexDTO root = vertexMap[input.GetInt32()];
            int predecessorMapSize = input.GetInt32();
            ImmutableDictionary<Int32, Int32>.Builder predecessorMap = ImmutableDictionary<Int32, Int32>.Empty.ToBuilder();
            for (int i = 0; i < predecessorMapSize; i++)
                predecessorMap.Add(input.GetInt32(), input.GetInt32());
            return new DiTreeDTO(root, predecessorMap.ToImmutable(), vertexMap, successorMap);
        }

        public void Marshal(TinkarOutput output)
        {
            this.MarshalVertexMap(output);
            this.MarshalSuccessorMap(output);

            output.WriteInt32(this.Root.VertexIndex);
            output.WriteInt32(this.PredecessorMap.Count);
            foreach (KeyValuePair<Int32, Int32> item in this.PredecessorMap)
            {
                output.WriteInt32(item.Key);
                output.WriteInt32(item.Value);
            }
        }
    }
}
