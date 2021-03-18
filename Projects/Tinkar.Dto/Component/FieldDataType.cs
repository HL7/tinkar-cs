using System;

namespace Tinkar.Dto
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
    public enum FieldDataType : Byte
    {
        /// <summary>
        /// ConceptChronology Type.
        /// </summary>
        ConceptChronologyType = 1,

        /// <summary>
        /// PatternForConceptChronology Type.
        /// </summary>
        PatternForSemanticChronologyType = 2,

        /// <summary>
        /// SemanticChronology Type.
        /// </summary>
        SemanticChronologyType = 3,

        /// <summary>
        /// ConceptVersionType.
        /// </summary>
        ConceptVersionType = 4,

        /// <summary>
        /// PatternForSemanticVersion Type.
        /// </summary>
        PatternForSemanticVersionType = 5,

        /// <summary>
        /// SemanticVersion Type.
        /// </summary>
        SemanticVersionType = 6,

        /// <summary>
        /// Stamp Type.
        /// </summary>
        StampType = 7,

        /// <summary>
        /// String type.
        /// </summary>
        StringType = 8,

        /// <summary>
        /// Integer Type.
        /// </summary>
        IntegerType = 9,

        /// <summary>
        /// Float Type.
        /// </summary>
        FloatType = 10,

        /// <summary>
        /// Boolean Type.
        /// </summary>
        BooleanType = 11,

        /// <summary>
        /// ByteArray Type.
        /// </summary>
        ByteArrayType = 12,

        /// <summary>
        /// ObjectArray Type.
        /// </summary>
        ObjectArrayType = 13,

        /// <summary>
        /// DiGraph Type.
        /// </summary>
        DiGraphType = 14,

        /// <summary>
        /// Instant Type.
        /// </summary>
        InstantType = 15,

        /// <summary>
        /// Concept Type.
        /// </summary>
        ConceptType = 16,

        /// <summary>
        /// PatternForSemantic Type.
        /// </summary>
        PatternForSemanticType = 17,

        /// <summary>
        /// Semantic Type.
        /// </summary>
        SemanticType = 18,

        /// <summary>
        /// DiTree Type.
        /// </summary>
        DiTreeType = 19,

        /// <summary>
        /// Vertex Type.
        /// </summary>
        VertexType = 20,

        /// <summary>
        /// ComponentIdListType.
        /// </summary>
        ComponentIdList = 21,

        /// <summary>
        /// ComponentIdSetType.
        /// </summary>
        ComponentIdSet = 22,

        /// <summary>
        /// PlanarPoint Type.
        /// </summary>
        PlanarPoint = 23,

        /// <summary>
        /// SpatialPoint Type.
        /// </summary>
        SpatialPoint = 24,

        /// <summary>
        /// Component Type.
        /// </summary>
        // Component needs to go last...
        ComponentType = Byte.MaxValue,
    }
}
