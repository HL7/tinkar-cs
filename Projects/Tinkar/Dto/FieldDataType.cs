using System;

namespace Tinkar
{
    /*
     * Note that Double objects will be converted to Float objects by the serialization mechanisms.
     *
     * The underlying intent is to keep the implementation simple by using the common types,
     * with precision dictated by domain of use, and that long and double are more granular than
     * typically required, and they waste more memory/bandwidth.
     *
     * If there is compelling use for a more precise data type (such as Instant), they can be added when a
     * agreed business need and use case are identified..
     */

    /// <summary>
    /// Field data type enumeration. Unique value for each
    /// data type that can be serializzed.
    /// </summary>
    public enum FieldDataType : byte
    {
        /// <summary>
        /// String type.
        /// </summary>
        StringType = 0,

        /// <summary>
        /// Integer Type.
        /// </summary>
        IntegerType = 1,

        /// <summary>
        /// Float Type.
        /// </summary>
        FloatType = 2,

        /// <summary>
        /// Boolean Type.
        /// </summary>
        BooleanType = 3,

        /// <summary>
        /// ByteArray Type.
        /// </summary>
        ByteArrayType = 4,

        /// <summary>
        /// ObjectArray Type.
        /// </summary>
        ObjectArrayType = 5,

        /// <summary>
        /// DiGraph Type.
        /// </summary>
        DiGraphType = 6,

        /// <summary>
        /// Instant Type.
        /// </summary>
        InstantType = 7,

        /// <summary>
        /// ConceptChronology Type.
        /// </summary>
        ConceptChronologyType = 8,

        /// <summary>
        /// Concept Type.
        /// </summary>
        ConceptType = 9,

        /// <summary>
        /// DefinitionForSemanticChronology Type.
        /// </summary>
        DefinitionForSemanticChronologyType = 10,

        /// <summary>
        /// DefinitionForSemantic Type.
        /// </summary>
        DefinitionForSemanticType = 11,

        /// <summary>
        /// SemanticChronology Type.
        /// </summary>
        SemanticChronologyType = 12,

        /// <summary>
        /// Semantic Type.
        /// </summary>
        SemanticType = 13,

        /// <summary>
        /// IdentifiedThing Type.
        /// </summary>
        // Identified thing needs to go last...
        IdentifiedThingType = Byte.MaxValue,
    }
}
