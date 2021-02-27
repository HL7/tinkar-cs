using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Tinkar
{
    /// <summary>
    /// Semantic record.
    /// </summary>
    public record SemanticDTO : ComponentDTO<SemanticDTO>,
        IJsonMarshalable,
        IMarshalable,
        ISemantic
    {
        /// <summary>
        /// Version of marshalling code.
        /// If code is modified in a way that renders old serialized data
        /// non-conformant, then this number should be incremented.
        /// </summary>
        private const int LocalMarshalVersion = 3;

        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public const String JsonClassName = "SemanticDTO";

        /// <summary>
        /// Gets public id.
        /// </summary>
        public IPublicId PublicId { get; init; }

        /// <summary>
        /// Gets ReferencedComponent.
        /// </summary>
        public IComponent ReferencedComponent => new ComponentDTO(this.ReferencedComponentUuids);

        /// <summary>
        /// Gets PatternForSemantic.
        /// </summary>
        public IPatternForSemantic PatternForSemantic => new PatternForSemanticDTO(this.PatternForSemanticUuids);

        /// <summary>
        /// Gets PatternForSemantic UUIDs.
        /// </summary>
        public IEnumerable<Guid> PatternForSemanticUuids { get; init; }

        /// <summary>
        /// Gets ReferencedComponent UUIDs.
        /// </summary>
        public IEnumerable<Guid> ReferencedComponentUuids { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticDTO"/> class.
        /// </summary>
        /// <param name = "publicId" > Public id(component ids).</param>
        /// <param name="definitionForSemanticUuids">PatternForSemanticUuids.</param>
        /// <param name="referencedComponentUuids">ReferencedComponentUuids.</param>
        public SemanticDTO(
            IPublicId publicId,
            IEnumerable<Guid> definitionForSemanticUuids,
            IEnumerable<Guid> referencedComponentUuids)
        {
            this.PublicId = publicId;
            this.PatternForSemanticUuids = definitionForSemanticUuids;
            this.ReferencedComponentUuids = referencedComponentUuids;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticDTO"/> class
        /// from binary stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        public SemanticDTO(TinkarInput input)
        {
            input.CheckMarshalVersion(LocalMarshalVersion);
            this.PublicId = input.ReadPublicId();
            this.PatternForSemanticUuids = input.ReadUuids();
            this.ReferencedComponentUuids = input.ReadUuids();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticDTO"/> class
        /// from json stream.
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        public SemanticDTO(JObject jObj)
        {
            this.PublicId  = jObj.ReadPublicId(ComponentFieldForJson.COMPONENT_PUBLIC_ID);
            this.PatternForSemanticUuids = jObj.ReadUuids(ComponentFieldForJson.DEFINITION_FOR_SEMANTIC_UUIDS);
            this.ReferencedComponentUuids = jObj.ReadUuids(ComponentFieldForJson.REFERENCED_COMPONENT_PURPOSE_UUIDS);
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="other">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public override Int32 CompareTo(SemanticDTO other)
        {
            Int32 cmp = base.CompareTo(other);
            if (cmp != 0)
                return cmp;
            cmp = FieldCompare.CompareGuids(this.PatternForSemanticUuids, other.PatternForSemanticUuids);
            if (cmp != 0)
                return cmp;
            cmp = FieldCompare.CompareGuids(this.ReferencedComponentUuids, other.ReferencedComponentUuids);
            if (cmp != 0)
                return cmp;
            return 0;
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        /// <returns>new DTO item.</returns>
        public static SemanticDTO Make(TinkarInput input) =>
            new SemanticDTO(input);

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public void Marshal(TinkarOutput output)
        {
            output.CheckMarshalVersion(LocalMarshalVersion);;
            output.WriteUuids(this.PublicId);
            output.WriteUuids(this.PatternForSemanticUuids);
            output.WriteUuids(this.ReferencedComponentUuids);
        }

        /// <summary>
        /// Static method to Create DTO item from json .
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        /// <returns>Deserialized Semantic record.</returns>
        public static SemanticDTO Make(JObject jObj) => new SemanticDTO(jObj);

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(JsonClassName);
            output.WriteUuids(ComponentFieldForJson.COMPONENT_PUBLIC_ID, this.PublicId);
            output.WriteUuids(ComponentFieldForJson.DEFINITION_FOR_SEMANTIC_UUIDS, this.PatternForSemanticUuids);
            output.WriteUuids(ComponentFieldForJson.REFERENCED_COMPONENT_PURPOSE_UUIDS, this.ReferencedComponentUuids);
            output.WriteEndObject();
        }
    }
}