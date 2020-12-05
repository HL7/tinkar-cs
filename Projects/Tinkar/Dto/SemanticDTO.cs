using System;
using System.Collections.Generic;

namespace Tinkar
{
    public record SemanticDTO : BaseDTO<SemanticDTO>,
        IJsonMarshalable,
        IMarshalable,
        ISemantic
    {
        private const int MarshalVersion = 1;

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
        /// Compares this to another item.
        /// </summary>
        /// <param name="other">Item to compare to</param>
        /// <returns>-1, 0, or 1</returns>
        public override Int32 CompareTo(SemanticDTO other)
        {
            Int32 cmp = this.CompareGuids(this.ComponentUuids, other.ComponentUuids);
            if (cmp != 0)
                return cmp;
            cmp = this.CompareGuids(this.DefinitionForSemanticUuids, other.DefinitionForSemanticUuids);
            if (cmp != 0)
                return cmp;
            cmp = this.CompareGuids(this.ReferencedComponentUuids, other.ReferencedComponentUuids);
            if (cmp != 0)
                return cmp;
            return 0;
        }

        /// <summary>
        /// Override of default hashcode. Must provide if Equals overridden.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => this.ComponentUuids.GetHashCode() ^
                                             this.DefinitionForSemanticUuids.GetHashCode() ^
                                             this.ReferencedComponentUuids.GetHashCode();

        //$@Override
        //public void jsonMarshal(Writer writer) {
        //    final JSONObject json = new JSONObject();
        //    json.put(ComponentFieldForJson.CLASS, this.getClass().getCanonicalName());
        //    json.put(ComponentFieldForJson.COMPONENT_UUIDS, componentUuids);
        //    json.put(ComponentFieldForJson.DEFINITION_FOR_SEMANTIC_UUIDS, definitionForSemanticUuids);
        //    json.put(ComponentFieldForJson.REFERENCED_COMPONENT_UUIDS, referencedComponentUuids);
        //    json.writeJSONString(writer);
        //}

        //$@JsonChronologyUnmarshaler
        //public static SemanticDTO Make(JSONObject jsonObject) {
        //    IEnumerable<Guid> componentUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.COMPONENT_UUIDS);
        //    IEnumerable<Guid> definitionForSemanticUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.DEFINITION_FOR_SEMANTIC_UUIDS);
        //    IEnumerable<Guid> referencedComponentUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.REFERENCED_COMPONENT_UUIDS);
        //    return new SemanticDTO(componentUuids, definitionForSemanticUuids, referencedComponentUuids);
        //}

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// $NotTested
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static SemanticDTO Make(TinkarInput input)
        {
            CheckMarshalVersion(input, MarshalVersion);
            IEnumerable<Guid> componentUuids = input.ReadImmutableUuidList();
            IEnumerable<Guid> definitionForSemanticUuids = input.ReadImmutableUuidList();
            IEnumerable<Guid> referencedComponentUuids = input.ReadImmutableUuidList();
            return new SemanticDTO(componentUuids, definitionForSemanticUuids, referencedComponentUuids);
        }

        /// <summary>
        /// Marshal DTO item to output stream.
        /// $NotTested
        /// </summary>
        /// <param name="output">output data stream</param>
        public void Marshal(TinkarOutput output)
        {
            WriteMarshalVersion(output, MarshalVersion);
            output.WriteUuidList(this.ComponentUuids);
            output.WriteUuidList(this.DefinitionForSemanticUuids);
            output.WriteUuidList(this.ReferencedComponentUuids);
        }
    }
}