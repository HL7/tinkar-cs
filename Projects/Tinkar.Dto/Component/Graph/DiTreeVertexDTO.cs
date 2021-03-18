﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    /// <summary>
    /// Directed tree vertex class
    /// </summary>
    public record DiTreeVertexDTO : GraphVertexDTO, IGraphVertex
    {
        /// <summary>
        /// This is builder class for creating DiTreeVertexDTO items.
        /// </summary>
        public new sealed class Builder : Builder<Builder>
        {
        }

        /// <summary>
        /// This is builder class for creating Builder derived classes.
        /// This should never be used directly, it only should be inherited from.
        /// </summary>
        /// <typeparam name="TBuilder">Derived builder type</typeparam>
        public abstract new class Builder<TBuilder> : GraphVertexDTO.Builder<TBuilder>
            where TBuilder : Builder<TBuilder>, new()
        {
            TBuilder predecessor = default(TBuilder);

            public TBuilder SetPredecessor(TBuilder value)
            {
                this.predecessor = value;
                return (TBuilder)this;
            }

            public DiTreeVertexDTO Create()
            {
                ImmutableDictionary<IConcept, Object>.Builder propBldr = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                propBldr.AddRange(this.properties);

                return new DiTreeVertexDTO(this.VertexId,
                    this.VertexIndex,
                    this.meaning,
                    propBldr.ToImmutableDictionary(),
                    this.predecessor.VertexIndex,
                    this.successors.ToImmutableList());
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

        /// <summary>
        /// Marshal class data to binary stream.
        /// </summary>
        /// <param name="output">binary output stream.</param>
        public override void Marshal(TinkarOutput output)
        {
            base.Marshal(output);
            output.WriteInt32(this.Predecessor);
        }

        /// <summary>
        /// Create Vertex from binary input stream.
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>new Vertex item</returns>
        public new static DiTreeVertexDTO Make(TinkarInput input)
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
            Int32 predecessor = input.GetInt32();
            return new DiTreeVertexDTO(new VertexId(vertexUuid),
                vertexIndex,
                new ConceptDTO(meaningId),
                properties.ToImmutable(),
                predecessor,
                successors.ToImmutableList());
        }

        public override bool IsEquivalent(Object o)
        {
            DiTreeVertexDTO other = o as DiTreeVertexDTO;
            if (other == null)
                return false;
            if (base.IsEquivalent(o) == false)
                return false;
            if (this.Predecessor != other.Predecessor)
                return false;
            return true;
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
    }
}
