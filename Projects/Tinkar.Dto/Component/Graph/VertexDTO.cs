using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    public record VertexDTO : IVertex,
        IJsonMarshalable,
        IMarshalable
    {
        public interface IBuilder<TBuilder>
            where TBuilder : IBuilder<TBuilder>
        {
            /// <summary>
            /// Vertex Index. This should only be set by class that creates Builder().
            /// </summary>
            Int32 VertexIndex { get; set; }

            TBuilder SetMeaning(ConceptDTO meaning);

            TBuilder SetVertexId(long vertexIdMsb, long vertexIdLsb);

            TBuilder SetVertexId(Guid vertexId);

            TBuilder AppendProperty(IConcept concept, Object value);
        }

        public abstract class Builder<TBuilder> : IBuilder<TBuilder>
            where TBuilder : IBuilder<TBuilder>
        {
            protected List<KeyValuePair<IConcept, Object>> properties = new List<KeyValuePair<IConcept, Object>>();
            protected VertexId vertexId;
            protected ConceptDTO meaning;

            /// <summary>
            /// Vertex Index. This should only be set by class that creates Builder().
            /// </summary>
            public Int32 VertexIndex { get; set; }

            public TBuilder SetMeaning(ConceptDTO meaning)
            {
                this.meaning = meaning;
                return (TBuilder)(IBuilder<TBuilder>)this;
            }

            public TBuilder SetVertexId(long vertexIdMsb, long vertexIdLsb)
            {
                this.vertexId = new VertexId(vertexIdMsb, vertexIdLsb);
                return (TBuilder)(IBuilder<TBuilder>)this;
            }

            public TBuilder SetVertexId(Guid vertexId)
            {
                this.vertexId = new VertexId(vertexId);
                return (TBuilder)(IBuilder<TBuilder>)this;
            }

            public TBuilder AppendProperty(IConcept concept, Object value)
            {
                properties.Add(new KeyValuePair<IConcept, Object>(concept, value));
                return (TBuilder)(IBuilder<TBuilder>)this;
            }
        }

        /// <summary>
        /// Name of serialized json field for this item.
        /// </summary>
        public const String JSONCLASSNAME = "VertexDTO";

        /// <summary>
        /// Unique id for this data field.
        /// </summary>
        public FieldDataType FieldDataType => FieldDataType.VertexType;

        public ImmutableDictionary<IConcept, Object> Properties { get; init; }

#if Never
        /// <summary>
        /// Gets tindex of this vertex within its graph. The index is locally
        /// unique within a graph, but not across graphs, or different versions of the same graph.
        /// Vertex index is not used in equality or hash calculations.
        /// </summary>
        public int VertexIndex { get; init; }

        /// <summary>
        /// Concept that represents the meaning of this vertex.
        /// </summary>
        public IConcept Meaning { get; init; }

    public static ImmutableMap<ConceptDTO, Object> abstractProperties(ImmutableMap<ConceptDTO, Object> incoming) {
        MutableMap<ConceptDTO, Object> outgoing = Maps.mutable.ofInitialCapacity(incoming.size());
        incoming.forEachKeyValue((key, value) -> {
            outgoing.put(abstractObject(key), abstractObject(value));
        });
        return outgoing.toImmutable();
    }

    @Override
    public RichIterable<ConceptDTO> propertyKeys() {
        return this.properties.keysView();
    }
#endif

        /// <summary>
        /// Gets universally unique identifier for this vertex
        /// 
        /// </summary>
        public IVertexId VertexId { get; init; }

        /// <summary>
        /// Gets tindex of this vertex within its graph. The index is locally
        /// unique within a graph, but not across graphs, or different versions of the same graph.
        /// Vertex index is not used in equality or hash calculations.
        /// </summary>
        public int VertexIndex { get; init; }

        /// <summary>
        /// Concept that represents the meaning of this vertex.
        /// </summary>
        public IConcept Meaning { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexDTO"/> class.
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>new Vertex item</returns>
        public VertexDTO(TinkarInput input)
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

            this.VertexId = new VertexId(vertexUuid);
            this.VertexIndex = vertexIndex;
            this.Meaning = new ConceptDTO(meaningId);
            this.Properties = properties.ToImmutable();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexDTO"/> class.
        /// </summary>
        /// <param name="vertexId"></param>
        /// <param name="vertexIndex"></param>
        /// <param name="meaning"></param>
        /// <param name="properties"></param>
        protected VertexDTO(VertexId vertexId,
            int vertexIndex,
            ConceptDTO meaning,
            ImmutableDictionary<IConcept, Object> properties)
        {
            this.VertexId = vertexId;
            this.VertexIndex = vertexIndex;
            this.Meaning = meaning;
            this.Properties = properties;
        }

        /// <summary>
        /// Gets optional object that is associated with the properly concept.
        /// </summary>
        /// <typeparam name="T">Type of the property object</typeparam>
        /// <param name="propertyConcept">Property Concept</param>
        /// <returns>Property associated with concept</returns>
#warning "How does java version of this handle a) not finding item or b) item is different type"
        public T Property<T>(IConcept propertyConcept)
        {
            if (this.Properties.TryGetValue(propertyConcept, out Object value) == false)
                return default(T);
            return (T)value;
        }

        /// <summary>
        /// Gets optional object that is associated with the properly concept.
        /// </summary>
        /// <typeparam name="T">Type of the property object</typeparam>
        /// <param name="propertyConcept">Property Concept</param>
        /// <returns>Property associated with concept</returns>
#warning "How is this different from T Property() above"
        public T PropertyFast<T>(IConcept propertyConcept) => throw new NotImplementedException("xxyyz");

        //CEnum PropertyKeys<CEnum> { get; }
        /// <summary>
        /// Gets keys for the populated properties
        /// </summary>
        /// <returns>keys</returns>
        public IEnumerable<IConcept> PropertyKeys => this.Properties.Keys;

        public virtual Int32 CompareTo(Object o)
        {
            Int32 Comparer(Object a, Object b)
            {
                IComparable aCmp = (IComparable)a;
                IComparable bCmp = (IComparable)b;
                return aCmp.CompareTo(bCmp);
            }

            VertexDTO other = o as VertexDTO;
            if (other == null)
                return this.GetType().FullName.CompareTo(o.GetType().FullName);

            Int32 cmpVal = this.VertexId.CompareTo(other.VertexId);
            if (cmpVal != 0)
                return cmpVal;
            cmpVal = this.VertexIndex.CompareTo(other.VertexIndex);
            if (cmpVal != 0)
                return cmpVal;
            cmpVal = this.Meaning.CompareTo(other.Meaning);
            if (cmpVal != 0)
                return cmpVal;
            cmpVal = FieldCompare.CompareMap(this.Properties, other.Properties, Comparer);
            if (cmpVal != 0)
                return cmpVal;
            return 0;
        }

        /// <summary>
        /// Create Vertex from binary input stream.
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>new Vertex item</returns>
        public static VertexDTO Make(TinkarInput input) => new VertexDTO(input);

        public bool IsEquivalent(Object other) => this.IsEquivalent(other as IVertex);

        public bool IsEquivalent(IVertex other)
        {
            Int32 Comparer(Object a, Object b)
            {
                IComparable aCmp = (IComparable)a;
                IComparable bCmp = (IComparable)b;
                return aCmp.CompareTo(bCmp);
            }

            if (this.VertexId.CompareTo(other.VertexId) != 0)
                return false;
            if (this.VertexIndex.CompareTo(other.VertexIndex) != 0)
                return false;
            if (this.Meaning.IsEquivalent(other.Meaning) == false)
                return false;
            if (FieldCompare.CompareMap(this.Properties, other.Properties, Comparer) != 0)
                return false;
            return true;
        }

        /// <summary>
        /// Marshal class data to binary stream.
        /// </summary>
        /// <param name="output">binary output stream.</param>
        public virtual void Marshal(TinkarOutput output)
        {
            output.WriteUuid(this.VertexId.Uuid);
            output.WriteInt32(this.VertexIndex);
            output.PutPublicId(this.Meaning.PublicId);
            Int32 propertyCount = this.Properties.Keys.Count();
            output.WriteInt32(propertyCount);
            foreach (KeyValuePair<IConcept, Object> item in this.Properties)
            {
                output.PutPublicId(item.Key.PublicId);
                output.WriteField(item.Value);
            }
        }

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(JSONCLASSNAME);
            output.WriteUuid(ComponentFieldForJson.VERTEX_ID, this.VertexId.Uuid);
            output.Put(ComponentFieldForJson.VERTEX_ID, this.VertexIndex);
            output.Put(ComponentFieldForJson.VERTEX_MEANING, this.Meaning);
            {
                output.WriteStartObject();
                output.WritePropertyName(ComponentFieldForJson.VERTEX_PROPERTIES);

                foreach (KeyValuePair<IConcept, Object> item in this.Properties)
                {
                    output.WriteStartObject();
                    output.WritePropertyName(item.Key.PublicId.ToString());
                    output.WriteField(item.Value);
                    output.WriteEndObject();
                }
                output.WriteEndObject();
            }
            output.WriteEndObject();
        }
    }
}
