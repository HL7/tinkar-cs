using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    /// <summary>
    /// Graph vertex class
    /// </summary>
    public record GraphVertexDTO : VertexDTO, IGraphVertex
    {
        /// <summary>
        /// This is builder class for creating GraphVertexDTO items.
        /// </summary>
        public new sealed class Builder : Builder<Builder>
        {
            public GraphVertexDTO Create()
            {
                ImmutableDictionary<IConcept, Object>.Builder propBldr = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                propBldr.AddRange(this.properties);

                return new GraphVertexDTO(this.VertexId,
                    this.VertexIndex,
                    this.meaning,
                    propBldr.ToImmutableDictionary(),
                    this.successors.ToImmutableArray());
            }
        }

        /// <summary>
        /// This is builder class for creating Builder derived classes.
        /// This should never be used directly, it only should be inherited from.
        /// </summary>
        /// <typeparam name="TBuilder">Derived builder type</typeparam>
        public abstract new class Builder<TBuilder> : VertexDTO.Builder<TBuilder>
            where TBuilder : Builder<TBuilder>
        {
            protected List<Int32> successors = new List<int>();

            public Int32 SuccessorCount => this.successors.Count();

            public TBuilder ClearSuccessors()
            {
                this.successors.Clear();
                return (TBuilder)this;
            }

            public TBuilder AppendSuccessors(params VertexDTO.Builder<TBuilder>[] children)
            {
                successors.AddRange(children.Select((a) => a.VertexIndex));
                return (TBuilder)this;
            }
        }

        public ImmutableArray<Int32> Successors { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphVertexDTO"/> class.
        /// </summary>
        protected GraphVertexDTO(VertexId vertexId,
            int vertexIndex,
            ConceptDTO meaning,
            ImmutableDictionary<IConcept, Object> properties,
            ImmutableArray<Int32> successors) :
            base(vertexId, vertexIndex, meaning, properties)
        {
            this.Successors = successors;
        }

        /// <summary>
        /// Marshal class data to binary stream.
        /// </summary>
        /// <param name="output">binary output stream.</param>
        public override void Marshal(TinkarOutput output)
        {
            base.Marshal(output);
            output.WriteInt32(this.Successors.Length);
            for (Int32 i = 0; i < this.Successors.Length; i++)
                output.WriteInt32(this.Successors[i]);
        }

        /// <summary>
        /// Create Vertex from binary input stream.
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>new Vertex item</returns>
        public new static GraphVertexDTO Make(TinkarInput input)
        {
            Guid vertexUuid = input.GetUuid();
            int vertexIndex = input.GetInt32();
            IPublicId meaningId = input.GetPublicId();

            int propertyCount = input.GetInt32();
            ImmutableDictionary<IConcept, Object>.Builder properties =
                ImmutableDictionary<IConcept, object>.Empty.ToBuilder();

            for (int i = 0; i < propertyCount; i++)
            {
                ConceptDTO conceptKey = new ConceptDTO(input.GetPublicId());
                Object obj = input.GetField();
                properties.Add(conceptKey, obj);
            }
            Int32 successorCount = input.GetInt32();
            Int32[] successors = new int[successorCount];
            for (Int32 i = 0; i < successorCount; i++)
                successors[i] = input.GetInt32();
            return new GraphVertexDTO(new VertexId(vertexUuid),
                vertexIndex,
                new ConceptDTO(meaningId),
                properties.ToImmutable(),
                successors.ToImmutableArray());
        }

        public override bool IsEquivalent(Object o)
        {
            GraphVertexDTO other = o as GraphVertexDTO;
            if (other == null)
                return false;
            if (base.IsEquivalent(o) == false)
                return false;
            if (FieldCompare.CompareSequence(this.Successors, other.Successors) != 0)
                return false;
            return true;
        }


        public override Int32 CompareTo(Object o)
        {
            GraphVertexDTO other = o as GraphVertexDTO;
            if (other == null)
                return this.GetType().FullName.CompareTo(o.GetType().FullName);

            Int32 cmpVal = base.CompareTo(o);
            if (cmpVal != 0)
                return cmpVal;
            cmpVal = FieldCompare.CompareSequence(this.Successors, other.Successors);
            if (cmpVal != 0)
                return cmpVal;
            return 0;
        }
    }
}
