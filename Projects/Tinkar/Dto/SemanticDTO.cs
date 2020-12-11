using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Tinkar
{
    public record SemanticDTO : BaseDTO<SemanticDTO>,
        IJsonMarshalable,
        IMarshalable,
        ISemantic
    {
        /// <summary>
        /// Version of marshalling code.
        /// If code is modified in a way that renders old serialized data
        /// non-conformant, then this number should be incremented.
        /// </summary>
        private const int MarshalVersion = 1;

        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        private const String JsonClassName = "SemanticDTO";

        /// <summary>
        /// Implementation of IIentifiedThing.ComponentUuids
        /// </summary>
        public IEnumerable<Guid> ComponentUuids { get; init; }

        /// <summary>
        /// Implementation of ISemantic.ReferencedComponent
        /// </summary>
        public IIdentifiedThing ReferencedComponent => new IdentifiedThingDTO(this.ReferencedComponentUuids);

        /// <summary>
        /// Implementation of ISemantic.DefinitionForSemantic
        /// </summary>
        public IDefinitionForSemantic DefinitionForSemantic => new DefinitionForSemanticDTO(this.DefinitionForSemanticUuids);

        /// <summary>
        /// Guids for DefinitionForSemantic
        /// </summary>
        public IEnumerable<Guid> DefinitionForSemanticUuids { get; init; }

        /// <summary>
        /// Guids for ReferencedComponent
        /// </summary>
        public IEnumerable<Guid> ReferencedComponentUuids { get; init; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="componentUuids">ComponentUuids</param>
        /// <param name="definitionForSemanticUuids">DefinitionForSemanticUuids</param>
        /// <param name="referencedComponentUuids">ReferencedComponentUuids</param>
        public SemanticDTO(
            IEnumerable<Guid> componentUuids,
            IEnumerable<Guid> definitionForSemanticUuids,
            IEnumerable<Guid> referencedComponentUuids
        )
        {
            this.ComponentUuids = componentUuids;
            this.DefinitionForSemanticUuids = definitionForSemanticUuids;
            this.ReferencedComponentUuids = referencedComponentUuids;
        }

        /// <summary>
        /// Create item from binary stream
        /// </summary>
        public SemanticDTO(TinkarInput input)
        {
            input.CheckMarshalVersion(MarshalVersion);
            this.ComponentUuids = input.ReadUuids();
            this.DefinitionForSemanticUuids = input.ReadUuids();
            this.ReferencedComponentUuids = input.ReadUuids();
        }

        /// <summary>
        /// Create item from json stream
        /// </summary>
        public SemanticDTO(JObject jObj)
        {
            this.ComponentUuids = jObj.ReadUuids(ComponentFieldForJson.COMPONENT_UUIDS);
            this.DefinitionForSemanticUuids = jObj.ReadUuids(ComponentFieldForJson.DEFINITION_FOR_SEMANTIC_UUIDS);
            this.ReferencedComponentUuids = jObj.ReadUuids(ComponentFieldForJson.REFERENCED_COMPONENT_PURPOSE_UUIDS);
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="other">Item to compare to</param>
        /// <returns>-1, 0, or 1</returns>
        public override Int32 CompareTo(SemanticDTO other)
        {
            Int32 cmp = FieldCompare.CompareGuids(this.ComponentUuids, other.ComponentUuids);
            if (cmp != 0)
                return cmp;
            cmp = FieldCompare.CompareGuids(this.DefinitionForSemanticUuids, other.DefinitionForSemanticUuids);
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
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static SemanticDTO Make(TinkarInput input) =>
            new SemanticDTO(input);

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream</param>
        public void Marshal(TinkarOutput output)
        {
            output.WriteMarshalVersion(MarshalVersion);
            output.WriteUuids(this.ComponentUuids);
            output.WriteUuids(this.DefinitionForSemanticUuids);
            output.WriteUuids(this.ReferencedComponentUuids);
        }

        /// <summary>
        /// Static method to Create DTO item from json .
        /// </summary>
        public static SemanticDTO Make(JObject jObj) => new SemanticDTO(jObj);

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream</param>
        public void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(JsonClassName);
            output.WriteUuids(ComponentFieldForJson.COMPONENT_UUIDS, this.ComponentUuids);
            output.WriteUuids(ComponentFieldForJson.DEFINITION_FOR_SEMANTIC_UUIDS, this.DefinitionForSemanticUuids);
            output.WriteUuids(ComponentFieldForJson.REFERENCED_COMPONENT_PURPOSE_UUIDS, this.ReferencedComponentUuids);
            output.WriteEndObject();
        }
    }
}