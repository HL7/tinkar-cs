using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    public record DiTreeVertexDTO : GraphVertexDTO, IGraphVertex
    {
        public new interface IBuilder<TBuilder> : GraphVertexDTO.IBuilder<TBuilder>
            where TBuilder : IBuilder<TBuilder>
        {
            TBuilder SetPredecessor(TBuilder value);

            new DiTreeVertexDTO Create();
        }

        /// <summary>
        /// This is builder class for creating DiTreeVertexDTO items.
        /// </summary>
        public new sealed class Builder : Builder<Builder>, IBuilder<Builder>
        {
        }

        /// <summary>
        /// This is builder class for creating DiTreeVertexDTO items.
        /// This should never be used directly, it only should be inherited from.
        /// </summary>
        /// <typeparam name="TBuilder"></typeparam>
        public abstract new class Builder<TBuilder> : GraphVertexDTO.Builder<TBuilder>, IBuilder<TBuilder>
            where TBuilder : IBuilder<TBuilder>, new()
        {
            TBuilder predecessor = default(TBuilder);

            public TBuilder SetPredecessor(TBuilder value)
            {
                this.predecessor = value;
                return (TBuilder)(IBuilder<TBuilder>)this;
            }

            public new DiTreeVertexDTO Create()
            {
                ImmutableDictionary<IConcept, Object>.Builder propBldr = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                propBldr.AddRange(this.properties);

                return new DiTreeVertexDTO(this.vertexId,
                    this.VertexIndex,
                    this.meaning,
                    propBldr.ToImmutableDictionary(),
                    this.predecessor.VertexIndex,
                    this.successorList.ToImmutableList());
            }
        }


        public Int32 Predecessor { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiTreeVertexDTO"/> class.
        /// </summary>
        protected DiTreeVertexDTO(VertexId vertexId,
            int vertexIndex,
            ConceptDTO meaning,
            ImmutableDictionary<IConcept, Object> properties,
            Int32 predecessor,
            ImmutableList<Int32> successors) : 
            base(vertexId, vertexIndex, meaning, properties, successors)
        {
            this.Predecessor = predecessor;
        }

        public override Int32 CompareTo(Object o)
        {
            DiTreeVertexDTO other = o as DiTreeVertexDTO;
            if (other == null)
                return this.GetType().FullName.CompareTo(o.GetType().FullName);

            Int32 cmpVal = base.CompareTo(o);
            if (cmpVal != 0)
                return cmpVal;

            cmpVal = this.Predecessor.CompareTo(other.Predecessor);
            if (cmpVal != 0)
                return cmpVal;
            return 0;
        }

        /// <summary>
        /// Marshal class data to binary stream.
        /// </summary>
        /// <param name="output">binary output stream.</param>
        public override void Marshal(TinkarOutput output)
        {
            base.Marshal(output);
            output.WriteInt32(this.Predecessor);
        }
    }
}
