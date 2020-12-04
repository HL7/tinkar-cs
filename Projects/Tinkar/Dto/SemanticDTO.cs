using System;
using System.Collections.Generic;

namespace Tinkar
{
    public record SemanticDTO : BaseDTO, 
        IJsonMarshalable, 
        IMarshalable, 
        ISemantic
    {
        protected override int MarshalVersion => 1;

        public IIdentifiedThing ReferencedComponent { get; init; }

        public IDefinitionForSemantic DefinitionForSemantic { get; init; }

        public IEnumerable<Guid> ComponentUuids { get; init; }

        //$@Override
        //public IdentifiedThing referencedComponent() {
        //    return new IdentifiedThingDTO(referencedComponentUuids);
        //}

        //$@Override
        //public DefinitionForSemantic definitionForSemantic() {
        //    return new DefinitionForSemanticDTO(definitionForSemanticUuids);
        //}

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
        //public static SemanticDTO make(JSONObject jsonObject) {
        //    IEnumerable<Guid> componentUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.COMPONENT_UUIDS);
        //    IEnumerable<Guid> definitionForSemanticUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.DEFINITION_FOR_SEMANTIC_UUIDS);
        //    IEnumerable<Guid> referencedComponentUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.REFERENCED_COMPONENT_UUIDS);
        //    return new SemanticDTO(componentUuids, definitionForSemanticUuids, referencedComponentUuids);
        //}

        //$@Unmarshaler
        public static SemanticDTO Make(TinkarInput input)
        {
            try
            {
                int objectMarshalVersion = input.ReadInt();
                if (objectMarshalVersion != marshalVersion)
                    throw new UnsupportedOperationException("Unsupported version: " + objectMarshalVersion);

                IEnumerable<Guid> componentUuids = input.ReadImmutableUuidList();
                IEnumerable<Guid> definitionForSemanticUuids = input.ReadImmutableUuidList();
                IEnumerable<Guid> referencedComponentUuids = input.ReadImmutableUuidList();
                return new SemanticDTO(componentUuids, definitionForSemanticUuids, referencedComponentUuids);
            }
            catch (Exception ex)
            {
                throw new MarshalExceptionUnchecked(ex);
            }
        }

        //$@Override
        //@Marshaler
        //public void marshal(TinkarOutput out) {
        //    try {
        //        out.writeInt(marshalVersion);
        //        out.writeUuidList(componentUuids);
        //        out.writeUuidList(definitionForSemanticUuids);
        //        out.writeUuidList(referencedComponentUuids);
        //    } catch (Exception ex) {
        //        throw new MarshalExceptionUnchecked(ex);
        //    }
        //}
    }
}