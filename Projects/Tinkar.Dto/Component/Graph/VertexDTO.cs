using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    public class VertexDTO : IVertex,
        IJsonMarshalable,
        IMarshalable
    {
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
    public static ImmutableMap<ConceptDTO, Object> abstractProperties(ImmutableMap<ConceptDTO, Object> incoming) {
        MutableMap<ConceptDTO, Object> outgoing = Maps.mutable.ofInitialCapacity(incoming.size());
        incoming.forEachKeyValue((key, value) -> {
            outgoing.put(abstractObject(key), abstractObject(value));
        });
        return outgoing.toImmutable();
    }

    @Override
    public int hashCode() {
        return Objects.hash(vertexIdMsb, vertexIdLsb, meaning);
    }

    @Override
    public void jsonMarshal(Writer writer) {
        final JSONObject json = new JSONObject();
        json.put(ComponentFieldForJson.CLASS, this.getClass().getCanonicalName());
        json.put(ComponentFieldForJson.VERTEX_ID, vertexId());
        json.put(ComponentFieldForJson.VERTEX_INDEX, vertexIndex());
        json.put(ComponentFieldForJson.VERTEX_MEANING, meaning());
        final JSONObject jsonPropertyMap = new JSONObject();
        json.put(ComponentFieldForJson.VERTEX_PROPERTIES, jsonPropertyMap);
        properties.forEachKeyValue((conceptKey, value) -> {
            jsonPropertyMap.put(conceptKey.componentPublicId().toString(), value);
        });
        json.writeJSONString(writer);
    }

    @JsonChronologyUnmarshaler
    public static VertexDTO make(JSONObject jsonObject) {
        JSONArray idParts = (JSONArray) jsonObject.get(ComponentFieldForJson.VERTEX_ID);
        UUID vertexUuid = (UUID) idParts.get(0);
        return VertexDTO.builder().vertexIdMsb(vertexUuid.getMostSignificantBits())
                .vertexIdLsb(vertexUuid.getLeastSignificantBits())
                .vertexIndex(((Long) jsonObject.get(ComponentFieldForJson.VERTEX_INDEX)).intValue())
                .meaning(jsonObject.asConcept(ComponentFieldForJson.VERTEX_MEANING))
                .properties(jsonObject.getConceptObjectMap(ComponentFieldForJson.VERTEX_PROPERTIES)).build();
    }

    @Unmarshaler
    public static VertexDTO make(TinkarInput in) {
        if (localMarshalVersion == in.getTinkerFormatVersion()) {
            UUID vertexUuid = in.getUuid();
            int vertexSequence = in.getInt();
            PublicId meaningId = in.getPublicId();
            final ImmutableMap<ConceptDTO, Object> immutableProperties;
            int propertyCount = in.getInt();
            if (propertyCount > 0) {
                MutableMap<ConceptDTO, Object> mutableProperties = Maps.mutable.ofInitialCapacity(propertyCount);
                for (int i = 0; i < propertyCount; i++) {
                    ConceptDTO conceptKey = new ConceptDTO(in.getPublicId());
                    Object object = in.getTinkarNativeObject();
                    mutableProperties.put(conceptKey, object);
                }
                immutableProperties = mutableProperties.toImmutable();
            } else {
                immutableProperties = Maps.immutable.empty();
            }

            return VertexDTO.builder()
                    .vertexIdMsb(vertexUuid.getMostSignificantBits())
                    .vertexIdLsb(vertexUuid.getLeastSignificantBits())
                    .vertexIndex(vertexSequence)
                    .meaning(new ConceptDTO(meaningId))
                    .properties(immutableProperties).build();
        } else {
            throw new UnsupportedOperationException("Unsupported version: " + marshalVersion);
        }
    }

    @Override
    public RichIterable<ConceptDTO> propertyKeys() {
        return this.properties.keysView();
    }

    @Override
    @Marshaler
    public void marshal(TinkarOutput out) {
        out.putLong(vertexIdMsb);
        out.putLong(vertexIdLsb);
        out.putInt(vertexIndex);
        out.putPublicId(meaning.componentPublicId());
        out.putInt(properties.size());
        properties.forEachKeyValue((conceptKey, object) -> {
            out.putPublicId(conceptKey);
            out.putTinkarNativeObject(object);
        });

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
        public int VertexIndex { get; }

        /// <summary>
        /// Concept that represents the meaning of this vertex.
        /// </summary>
        public IConcept Meaning { get; }


        /// <summary>
        /// Initializes a new instance of the <see cref="VertexDTO"/> class.
        /// </summary>
        /// <param name="vertexIdMsb"></param>
        /// <param name="vertexIdLsb"></param>
        /// <param name="vertexIndex"></param>
        /// <param name="meaning"></param>
        /// <param name="properties"></param>

        public VertexDTO(long vertexIdMsb,
            long vertexIdLsb,
            int vertexIndex,
            ConceptDTO meaning,
            ImmutableDictionary<IConcept, Object> properties)
        {
            this.VertexId = new VertexId(vertexIdMsb, vertexIdLsb);
            this.VertexIndex = vertexIndex;
            this.Meaning = meaning;
            this.Properties = properties;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexDTO"/> class.
        /// </summary>
        /// <param name="vertexId"></param>
        /// <param name="vertexIndex"></param>
        /// <param name="meaning"></param>
        /// <param name="properties"></param>

        public VertexDTO(Guid vertexId,
            int vertexIndex,
            ConceptDTO meaning,
            ImmutableDictionary<IConcept, Object> properties)
        {
            this.VertexId = new VertexId(vertexId);
            this.VertexIndex = vertexIndex;
            this.Meaning = meaning;
            this.Properties = properties;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexDTO"/> class.
        /// </summary>
        /// <param name="vertexId"></param>
        /// <param name="vertexIndex"></param>
        /// <param name="meaning"></param>
        /// <param name="properties"></param>

        public VertexDTO(Guid vertexId,
            int vertexIndex,
            ConceptDTO meaning,
            IEnumerable<KeyValuePair<IConcept, Object>> properties) : 
                this(vertexId, vertexIndex, meaning, ToImmutableDict(properties))
        {
        }

        static ImmutableDictionary<IConcept, Object> ToImmutableDict(IEnumerable<KeyValuePair<IConcept, Object>> properties)
        {
            var builder = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
            builder.AddRange(properties);
            return builder.ToImmutable();
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
            return (T) value;
        }

        /// <summary>
        /// Gets optional object that is associated with the properly concept.
        /// </summary>
        /// <typeparam name="T">Type of the property object</typeparam>
        /// <param name="propertyConcept">Property Concept</param>
        /// <returns>Property associated with concept</returns>
        public T PropertyFast<T>(IConcept propertyConcept) => throw new NotImplementedException("xxyyz");

        //CEnum PropertyKeys<CEnum> { get; }
        /// <summary>
        /// Gets keys for the populated properties
        /// </summary>
        /// <returns>keys</returns>
        public IEnumerable<IConcept> PropertyKeys => throw new NotImplementedException("xxyyz");

        public Int32 CompareTo(Object other) => CompareTo(other as IVertex);
        public Int32 CompareTo(IVertex other)
        {
            if (this == other) return 0;
            Int32 cmpVal = this.VertexId.CompareTo(other.VertexId);
            if (cmpVal != 0)
                return cmpVal;
            cmpVal = this.Meaning.CompareTo(other.Meaning);
            if (cmpVal != 0)
                return cmpVal;
            cmpVal = FieldCompare.CompareMap(this.Properties, other.Properties);
            if (cmpVal != 0)
                return cmpVal;
            return 0;
        }

        /// <summary>
        /// Create Vertex from binary input stream.
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>new Vertex item</returns>
        public static VertexDTO Make(TinkarInput input)
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

            return new VertexDTO(vertexUuid, vertexIndex, new ConceptDTO(meaningId), properties.ToImmutable());
        }

        public bool IsEquivalent(IVertex other)
        {
            if (this == other) return true;
            if (this.VertexId.CompareTo(other.VertexId) != 0)
                return false;
            if (this.Meaning.IsEquivalent(other.Meaning) == false)
                return false;
            if (FieldCompare.CompareMap(this.Properties, other.Properties) != 0)
                return false;
            return true;
        }

        /// <summary>
        /// Marshal class data to binary stream.
        /// </summary>
        /// <param name="output">binary output stream.</param>
        public void Marshal(TinkarOutput output)
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
