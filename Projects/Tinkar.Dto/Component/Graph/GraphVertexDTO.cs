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

                return new GraphVertexDTO(this.vertexId,
                    this.VertexIndex,
                    this.meaning,
                    propBldr.ToImmutableDictionary(),
                    this.successorList.ToImmutableList());
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
            protected List<Int32> successorList = new List<int>();

            public Int32 SuccessorCount => this.successorList.Count();

            public TBuilder AppendSuccessors(params VertexDTO.Builder<TBuilder>[] children)
            {
                successorList.AddRange(children.Select((a) => a.VertexIndex));
                return (TBuilder)this;
            }
        }

        public ImmutableList<Int32> Successors { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphVertexDTO"/> class.
        /// </summary>
        protected GraphVertexDTO(VertexId vertexId,
            int vertexIndex,
            ConceptDTO meaning,
            ImmutableDictionary<IConcept, Object> properties,
            ImmutableList<Int32> successors) :
            base(vertexId, vertexIndex, meaning, properties)
        {
            this.Successors = successors;
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
