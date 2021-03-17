﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    /// <summary>
    /// Directed graph vertex class
    /// </summary>
    public record DiGraphVertexDTO : GraphVertexDTO, IGraphVertex
    {
        /// <summary>
        /// This is builder class for creating DiGraphVertexDTO items.
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
            where TBuilder : Builder<TBuilder>
        {
            List<Int32> predecessors = new List<int>();

            public TBuilder AppendPredecessors(params TBuilder[] values)
            {
                this.predecessors.AddRange(values.Select((a) => a.VertexIndex));
                return (TBuilder)this;
            }

            public DiGraphVertexDTO Create()
            {
                ImmutableDictionary<IConcept, Object>.Builder propBldr = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
                propBldr.AddRange(this.properties);

                return new DiGraphVertexDTO(this.vertexId,
                    this.VertexIndex,
                    this.meaning,
                    propBldr.ToImmutableDictionary(),
                    this.predecessors.ToImmutableList(),
                    this.successorList.ToImmutableList());
            }
        }

        public ImmutableList<Int32> Predecessors { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiGraphVertexDTO"/> class.
        /// </summary>
        protected DiGraphVertexDTO(VertexId vertexId,
            int vertexIndex,
            ConceptDTO meaning,
            ImmutableDictionary<IConcept, Object> properties,
            ImmutableList<Int32> predecessors,
            ImmutableList<Int32> successors) : 
            base(vertexId, vertexIndex, meaning, properties, successors)
        {
            this.Predecessors = predecessors;
        }

        public override Int32 CompareTo(Object o)
        {
            DiGraphVertexDTO other = o as DiGraphVertexDTO;
            if (other == null)
                return this.GetType().FullName.CompareTo(o.GetType().FullName);

            Int32 cmpVal = base.CompareTo(o);
            if (cmpVal != 0)
                return cmpVal;

            cmpVal = FieldCompare.CompareSequence(this.Predecessors, other.Predecessors);
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
            output.WriteInt32(this.Predecessors.Count);
            foreach (Int32 predecessor in this.Predecessors)
                output.WriteInt32(predecessor);
            base.Marshal(output);
        }
    }
}
