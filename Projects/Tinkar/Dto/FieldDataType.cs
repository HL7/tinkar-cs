using System;

namespace Tinkar
{
	/**
	 * Note that Double objects will be converted to Float objects by the serialization mechanisms.
	 *
	 * The underlying intent is to keep the implementation simple by using the common types,
	 * with precision dictated by domain of use, and that long and double are more granular than
	 * typically required, and they waste more memory/bandwidth.
	 *
	 * If there is compelling use for a more precise data type (such as Instant), they can be added when a
	 * agreed business need and use case are identified..
	 */
	public enum FieldDataType
	{
		//$STRING((byte) 0, String.class, UUID.fromString("601135f2-2bad-11eb-adc1-0242ac120002")),
		//INTEGER((byte) 1, Integer.class,  UUID.fromString("60113822-2bad-11eb-adc1-0242ac120002")),
		//FLOAT((byte) 2, Float.class,   UUID.fromString("6011391c-2bad-11eb-adc1-0242ac120002")),
		//BOOLEAN((byte) 3, Boolean.class, UUID.fromString("601139ee-2bad-11eb-adc1-0242ac120002")),
		//BYTE_ARRAY((byte) 4, byte[].class, UUID.fromString("60113aac-2bad-11eb-adc1-0242ac120002")),
		//OBJECT_ARRAY((byte) 5, Object[].class, UUID.fromString("60113b74-2bad-11eb-adc1-0242ac120002")),
		//DIGRAPH((byte) 6, DigraphDTO.class, UUID.fromString("60113dfe-2bad-11eb-adc1-0242ac120002")),
		//INSTANT((byte) 7, Instant.class, UUID.fromString("9cb1bd10-31b1-11eb-adc1-0242ac120002")),
		//CONCEPT_CHRONOLOGY((byte) 8, ConceptChronology.class, UUID.fromString("60965934-32a2-11eb-adc1-0242ac120002")),
		//CONCEPT((byte) 9, Concept.class, UUID.fromString("6882871c-32a2-11eb-adc1-0242ac120002")),
		//DEFINITION_FOR_SEMANTIC_CHRONOLOGY((byte) 10, DefinitionForSemanticChronology.class, UUID.fromString("6eaa968e-32a2-11eb-adc1-0242ac120002")),
		//DEFINITION_FOR_SEMANTIC((byte) 11, DefinitionForSemantic.class, UUID.fromString("74af5952-32a2-11eb-adc1-0242ac120002")),
		//SEMANTIC_CHRONOLOGY((byte) 12, SemanticChronology.class, UUID.fromString("7a01ea5a-32a2-11eb-adc1-0242ac120002")),
		//SEMANTIC((byte) 13, Semantic.class, UUID.fromString("7f21bbfa-32a2-11eb-adc1-0242ac120002")),

		//// Identified thing needs to go last...
		//IDENTIFIED_THING(Byte.MAX_VALUE, IdentifiedThingDTO.class, UUID.fromString("60113d36-2bad-11eb-adc1-0242ac120002"));

		//public final byte token;
		//public final Class<? extends Object> clazz;
		//public final UUID conceptUuid;

		//FieldDataType(byte token, Class<? extends Object> clazz, UUID conceptUuid) {
		//    this.token = token;
		//    this.clazz = clazz;
		//    this.conceptUuid = conceptUuid;
		//}

		//public static FieldDataType fromToken(byte token) {
		//    switch (token) {
		//        case 0: return STRING;
		//        case 1: return INTEGER;
		//        case 2: return FLOAT;
		//        case 3: return BOOLEAN;
		//        case 4: return BYTE_ARRAY;
		//        case 5: return OBJECT_ARRAY;
		//        case 6: return DIGRAPH;
		//        case 7: return INSTANT;
		//        case 8: return CONCEPT_CHRONOLOGY;
		//        case 9: return CONCEPT;
		//        case 10: return DEFINITION_FOR_SEMANTIC_CHRONOLOGY;
		//        case 11: return DEFINITION_FOR_SEMANTIC;
		//        case 12: return SEMANTIC_CHRONOLOGY;
		//        case 13: return SEMANTIC;

		//        // Identified thing needs to go last...
		//        case Byte.MAX_VALUE: return IDENTIFIED_THING;
		//        default:
		//            throw new UnsupportedOperationException("FieldDatatype.fromToken can't handle token: " +
		//                    token);
		//    }
		//}

		//public static FieldDataType getFieldDataType(Object obj) {
		//    for (FieldDataType  fieldDataType: FieldDataType.values()) {
		//        if (fieldDataType.clazz.isAssignableFrom(obj.getClass())) {
		//            return fieldDataType;
		//        }
		//    }
		//    if (obj instanceof Double) {
		//        return FLOAT;
		//    }
		//    if (obj instanceof Long) {
		//        return INTEGER;
		//    }
		//    throw new UnsupportedOperationException("getFieldDataType can't handle: " +
		//            obj.getClass().getSimpleName() + "\n" +  obj);
		//}
	}
}