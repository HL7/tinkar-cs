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
        /// Gets ReferencedComponent UUIDs.
        /// </summary>
        public IPublicId ReferencedComponentPublicId { get; init; }

        /// <summary>
        /// Gets PatternForSemantic UUIDs.
        /// </summary>
        public IPublicId DefinitionForSemanticPublicId { get; init; }

        /// <summary>
        /// Gets ReferencedComponent.
        /// </summary>
        public IComponent ReferencedComponent => new ComponentDTO(this.ReferencedComponentPublicId);

        /// <summary>
        /// Gets PatternForSemantic.
        /// </summary>
        public IPatternForSemantic PatternForSemantic => new PatternForSemanticDTO(this.DefinitionForSemanticPublicId);

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticDTO"/> class.
        /// </summary>
        /// <param name = "componentPublicId" > Public id(component ids).</param>
        /// <param name="definitionForSemanticPublicId">Definition ForSemantic PublicId.</param>
        /// <param name="referencedComponentPublicId">Referenced Component PublicId.</param>
        public SemanticDTO(
            IPublicId componentPublicId,
            IPublicId definitionForSemanticPublicId,
            IPublicId referencedComponentPublicId) : base(componentPublicId)
        {
            this.DefinitionForSemanticPublicId = definitionForSemanticPublicId;
            this.ReferencedComponentPublicId = referencedComponentPublicId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticDTO"/> class.
        /// </summary>
        /// <param name = "componentPublicId" > Public id(component ids).</param>
        /// <param name="patternForSemantic">PatternForSemanticUuids.</param>
        /// <param name="referencedComponentPublicId">ReferencedComponentUuids.</param>
        public SemanticDTO(
            IPublicId componentPublicId,
            IPatternForSemantic patternForSemantic,
            IPublicId referencedComponentPublicId) : this(componentPublicId, patternForSemantic.PublicId, referencedComponentPublicId)
        {
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
            cmp = this.DefinitionForSemanticPublicId.CompareTo(other.DefinitionForSemanticPublicId);
            if (cmp != 0)
                return cmp;
            cmp = this.ReferencedComponentPublicId.CompareTo(other.ReferencedComponentPublicId);
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
        /// Static method to Create DTO item from json .
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        /// <returns>Deserialized Semantic record.</returns>
        public static SemanticDTO Make(JObject jObj) => 
            new SemanticDTO(jObj.AsPublicId(ComponentFieldForJson.COMPONENT_PUBLIC_ID),
                jObj.AsPublicId(ComponentFieldForJson.PATTERN_FOR_SEMANTIC_PUBLIC_ID),
                jObj.AsPublicId(ComponentFieldForJson.REFERENCED_COMPONENT_PUBLIC_ID));


        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public virtual void Marshal(TinkarOutput output)
        {
            output.PutPublicId(this.PublicId);
            output.PutPublicId(this.DefinitionForSemanticPublicId);
            output.PutPublicId(this.ReferencedComponentPublicId);
        }

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public virtual void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(JSONCLASSNAME);
            output.Put(ComponentFieldForJson.COMPONENT_PUBLIC_ID, this.PublicId);
            output.Put(ComponentFieldForJson.PATTERN_FOR_SEMANTIC_PUBLIC_ID, this.DefinitionForSemanticPublicId);
            output.Put(ComponentFieldForJson.REFERENCED_COMPONENT_PUBLIC_ID, this.ReferencedComponentPublicId);
            output.WriteEndObject();
        }
    }
}