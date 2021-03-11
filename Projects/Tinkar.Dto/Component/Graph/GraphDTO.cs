using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    public record GraphDTO : IGraph<VertexDTO>
    {
        public ImmutableList<VertexDTO> VertexMap { get; init; }

        public ImmutableDictionary<Int32, ImmutableList<Int32>> SuccessorMap { get; init; }

        public GraphDTO(IEnumerable<VertexDTO> vertexMap,
                        ImmutableDictionary<Int32, ImmutableList<Int32>> successorMap)
        {
            this.VertexMap = vertexMap.ToImmutableList();
            this.SuccessorMap = successorMap;
        }

        public GraphDTO(IEnumerable<VertexDTO> vertexMap,
                        IEnumerable<KeyValuePair<Int32, ImmutableList<Int32>>> successorMapValuesPairs) :
                        this(vertexMap, successorMapValuesPairs.ToImmutableDict())
        {
        }


        /// <summary>
        /// Gets the vertex associated with the identifier
        /// </summary>
        /// <param name="vertexId">a universally unique identifier for a vertex</param>
        /// <returns>Vertex associated with the identifier</returns>
        public VertexDTO Vertex(Guid vertexId)
        {
            foreach (VertexDTO vertexDTO in this.VertexMap)
            {
                if (vertexDTO.VertexId.Uuid == vertexId)
                    return vertexDTO;
            }
            throw new Exception("No vertex for: " + vertexId);
        }

        public VertexDTO Vertex(int vertexSequence) => this.VertexMap.ElementAt(vertexSequence);


        /// <summary>
        /// Gets the successors for the provided vertex
        /// </summary>
        /// <param name="vertex">vertex a vertex to retrieve the successors of</param>
        /// <returns>Successofs of indicated vertex</returns>
        public IEnumerable<VertexDTO> Successors(VertexDTO vertex)
        {
            if (this.SuccessorMap.TryGetValue(vertex.VertexIndex, out ImmutableList<Int32> successorIndices) == false)
                return new VertexDTO[0];
            Int32[] successorIndicesArr = successorIndices.ToArray();

            VertexDTO[] retVal = new VertexDTO[successorIndices.Count()];
            for (Int32 i = 0; i < successorIndicesArr.Length; i++)
                retVal[i] = this.VertexMap.ElementAt(successorIndicesArr[i]);
            return retVal;
        }

        protected void MarshalVertexMap(TinkarOutput output)
        {
            output.WriteInt32(this.VertexMap.Count());
            foreach (VertexDTO vertexDTO in this.VertexMap)
                vertexDTO.Marshal(output);
        }

        protected static ImmutableList<VertexDTO> UnmarshalVertexMap(TinkarInput input)
        {
            int mapSize = input.GetInt32();
            ImmutableList<VertexDTO>.Builder vertexMap = ImmutableList<VertexDTO>.Empty.ToBuilder();
            for (int i = 0; i < mapSize; i++)
            {
                VertexDTO vertexDTO = VertexDTO.Make(input);
                vertexMap.Add(vertexDTO);
            }
            return vertexMap.ToImmutableList();
        }

        protected void MarshalSuccessorMap(TinkarOutput output)
        {
            output.WriteInt32(this.VertexMap.Count());
            foreach (KeyValuePair<Int32, ImmutableList<Int32>> keyValue in this.SuccessorMap)
            {
                output.WriteInt32(keyValue.Key);
                Int32[] successors = keyValue.Value.ToArray();
                output.WriteInt32(successors.Length);
                foreach (Int32 successor in successors)
                    output.WriteInt32(successor);
            }
        }

        protected static ImmutableDictionary<Int32, ImmutableList<Int32>> UnmarshalSuccessorMap(TinkarInput input,
            ImmutableList<VertexDTO> vertexMap)
        {
            int mapSize = input.GetInt32();
            ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder successorMap =
                ImmutableDictionary<Int32, ImmutableList<Int32>>.Empty.ToBuilder();

            for (int i = 0; i < mapSize; i++)
            {
                int vertexSequence = input.GetInt32();
                int successorListSize = input.GetInt32();
                ImmutableList<Int32>.Builder successorList =
                    ImmutableList<Int32>.Empty.ToBuilder();
                for (int j = 0; j < successorListSize; j++)
                    successorList.Add(input.GetInt32());
                successorMap.Add(vertexSequence, successorList.ToImmutable());
            }
            return successorMap.ToImmutable();
        }


        public Int32 CompareTo(Object other) => CompareTo(other as IGraph<IVertex>);
        public Int32 CompareTo(IGraph<IVertex> other)
        {
            Int32 cmpVal = FieldCompare.CompareSequence(this.VertexMap, other.VertexMap);
            if (cmpVal != 0)
                return cmpVal;
            cmpVal = FieldCompare.CompareMap(this.SuccessorMap, other.SuccessorMap);
            if (cmpVal != 0)
                return cmpVal;
            return 0;
        }

        public Boolean IsEquivalent(Object other) => IsEquivalent(other as IGraph<IVertex>);
        public Boolean IsEquivalent(IGraph<IVertex> other) => throw new NotImplementedException();
    }
}
