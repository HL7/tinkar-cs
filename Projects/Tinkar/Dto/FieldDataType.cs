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
    public enum FieldDataType : byte
    {
        StringType = 0,
        IntegerType = 1,
        FloatType = 2,
        BooleanType = 3,
        ByteArrayType = 4,
        ObjectArrayType = 5,
        DiGraphType = 6,
        InstantType = 7,
        ConceptChronologyType = 8,
        ConceptType = 9,
        DefinitionForSemanticChronologyType = 10,
        DefinitionForSemanticType = 11,
        SemanticChronologyType = 12,
        SemanticType = 13,

        // Identified thing needs to go last...
        IdentifiedThingType = Byte.MaxValue,
    }
}
