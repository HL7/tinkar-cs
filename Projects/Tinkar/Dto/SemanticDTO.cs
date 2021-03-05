using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Tinkar
{
    /// <summary>
    /// Semantic record.
    /// </summary>
    public record SemanticDTO : ComponentDTO,
        IJsonMarshalable,
        IMarshalable,
        ISemantic
    {
        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public const String JSONCLASSNAME = "SemanticDTO";

        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public override String JsonClassName => JSONCLASSNAME;

        /// <summary>
        /// Gets ReferencedComponent.
        /// </summary>
        public IComponent ReferencedComponent => new ComponentDTO(this.referencedComponentPublicId);

        /// <summary>
        /// Gets PatternForSemantic.
        /// </summary>
        public IPatternForSemantic PatternForSemantic => new PatternForSemanticDTO(this.definitionForSemanticPublicId);

        /// <summary>
        /// Gets PatternForSemantic UUIDs.
        /// </summary>
        IPublicId definitionForSemanticPublicId { get; init; }

        /// <summary>
        /// Gets ReferencedComponent UUIDs.
        /// </summary>
        IPublicId referencedComponentPublicId { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticDTO"/> class.
        /// </summary>
        /// <param name = "publicId" > Public id(component ids).</param>
        /// <param name="definitionForSemanticPublicId">PatternForSemanticUuids.</param>
        /// <param name="referencedComponentPublicId">ReferencedComponentUuids.</param>
        public SemanticDTO(
            IPublicId publicId,
            IPublicId definitionForSemanticPublicId,
            IPublicId referencedComponentPublicId) : base(publicId)
        {
            this.definitionForSemanticPublicId = definitionForSemanticPublicId;
            this.referencedComponentPublicId = referencedComponentPublicId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticDTO"/> class
        /// from json stream.
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        public SemanticDTO(JObject jObj) : base(jObj)
        {
            this.definitionForSemanticPublicId = jObj.ReadPublicId(ComponentFieldForJson.PATTERN_FOR_SEMANTIC_PUBLIC_ID);
            this.referencedComponentPublicId = jObj.ReadPublicId(ComponentFieldForJson.REFERENCED_COMPONENT_PUBLIC_ID);
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="otherObject">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public override Int32 CompareTo(Object otherObject)
        {
            SemanticDTO other = otherObject as SemanticDTO;
            if (other == null)
                return -1;

            Int32 cmp = base.CompareTo(other);
            if (cmp != 0)
                return cmp;
            cmp = this.definitionForSemanticPublicId.CompareTo(other.definitionForSemanticPublicId);
            if (cmp != 0)
                return cmp;
            cmp = this.referencedComponentPublicId.CompareTo(other.referencedComponentPublicId);
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
            new SemanticDTO(input.GetPublicId(),
                input.GetPublicId(),
                input.GetPublicId());

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public override void MarshalFields(TinkarOutput output)
        {
            base.MarshalFields(output);
            output.WritePublicId(this.definitionForSemanticPublicId);
            output.WritePublicId(this.referencedComponentPublicId);
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
        public override void MarshalFields(TinkarJsonOutput output)
        {
            base.MarshalFields(output);
            output.WritePublicId(ComponentFieldForJson.PATTERN_FOR_SEMANTIC_PUBLIC_ID, this.definitionForSemanticPublicId);
            output.WritePublicId(ComponentFieldForJson.REFERENCED_COMPONENT_PUBLIC_ID, this.referencedComponentPublicId);
        }
    }
}