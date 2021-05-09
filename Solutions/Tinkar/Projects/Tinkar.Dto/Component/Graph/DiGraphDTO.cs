using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    public record DiGraphDTO : GraphDTO,
        IDiGraph<VertexDTO>,
        IJsonMarshalable,
        IMarshalable
    {
        public ImmutableList<VertexDTO> Roots => this.RootSequence.Select((a) => this.VertexMap[a]).ToImmutableList();
        public ImmutableList<Int32> RootSequence { get; init; }

        public ImmutableDictionary<Int32, ImmutableList<Int32>> PredecessorMap { get; init; }

        public FieldDataType FieldDataType => FieldDataType.DiGraphType;

        public DiGraphDTO(ImmutableList<Int32> rootSequence,
                        ImmutableDictionary<Int32, ImmutableList<Int32>> predecessorMap,
                        ImmutableList<VertexDTO> vertexMap,
                        ImmutableDictionary<Int32, ImmutableList<Int32>> successorMap) : base(vertexMap, successorMap)
        {
            this.RootSequence = rootSequence;
            this.PredecessorMap = predecessorMap;
        }

        public IEnumerable<VertexDTO> Predecessors(VertexDTO vertex)
        {
            if (this.PredecessorMap.TryGetValue(vertex.VertexIndex, out ImmutableList<Int32> predecessors) == false)
                return null;
            VertexDTO[] retVal = new VertexDTO[predecessors.Count];
            for (Int32 i = 0; i < predecessors.Count; i++)
                retVal[i] = this.VertexMap[predecessors[i]];
            return retVal;
        }

        public override Int32 CompareTo(Object o)
        {
            DiGraphDTO other = o as DiGraphDTO;
            if (o == null)
                return this.GetType().FullName.CompareTo(o.GetType().FullName);
            Int32 cmpVal = base.CompareTo(other);
            if (cmpVal != 0)
                return cmpVal;

            cmpVal = FieldCompare.CompareSequence(this.Roots, other.Roots);
            if (cmpVal != 0)
                return cmpVal;

            cmpVal = FieldCompare.CompareMap(this.PredecessorMap, other.PredecessorMap, (a, b) => FieldCompare.CompareSequence(a, b));
            if (cmpVal != 0)
                return cmpVal;

            return 0;
        }

        public override bool IsEquivalent(Object o)
        {
            DiGraphDTO other = o as DiGraphDTO;
            if (o == null)
                return false;
            if (base.IsEquivalent(other) == false)
                return false;

            Int32 cmpVal = FieldCompare.CompareSequence(this.Roots, other.Roots);
            if (cmpVal != 0)
                return false;

            cmpVal = FieldCompare.CompareMap(this.PredecessorMap, other.PredecessorMap, (a, b) => FieldCompare.CompareSequence(a, b));
            if (cmpVal != 0)
                return false;

            return true;
        }

        public void Marshal(TinkarJsonOutput output) => throw new NotImplementedException();

        public static DiGraphDTO Make(TinkarInput input)
        {
            ImmutableList<VertexDTO> vertexMap = GraphDTO.UnmarshalVertexMap(input);
            ImmutableDictionary<Int32, ImmutableList<Int32>> successorMap = GraphDTO.UnmarshalMap(input);

            ImmutableList<Int32>.Builder roots = ImmutableList<Int32>.Empty.ToBuilder();
            Int32 rootCount = input.GetInt32();
            for (Int32 i = 0; i < rootCount; i++)
                roots.Add(input.GetInt32());
            ImmutableDictionary<Int32, ImmutableList<Int32>> predecessorMap = GraphDTO.UnmarshalMap(input);
            return new DiGraphDTO(roots.ToImmutable(),
                predecessorMap, 
                vertexMap, 
                successorMap);
        }

        public void Marshal(TinkarOutput output)
        {
            this.MarshalVertexMap(output);
            this.MarshalMap(output, this.SuccessorMap);

            output.WriteInt32(this.RootSequence.Count());
            for (Int32 i = 0; i < this.RootSequence.Count(); i++)
                output.WriteInt32(this.RootSequence[i]);
            this.MarshalMap(output, this.PredecessorMap);
        }
    }
}
