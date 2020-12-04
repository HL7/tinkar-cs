using System;
using System.Collections.Generic;

namespace Tinkar
{
	public record DefinitionForSemanticDTO : BaseDTO, 
        IDefinitionForSemantic,
		IJsonMarshalable,
		IMarshalable
	{
		protected override int MarshalVersion => 1;

        public IEnumerable<Guid> ComponentUuids {get; init; }

        //$@Override
        //public void jsonMarshal(Writer writer) {
        //    final JSONObject json = new JSONObject();
        //    json.put(ComponentFieldForJson.CLASS, this.getClass().getCanonicalName());
        //    json.put(ComponentFieldForJson.COMPONENT_UUIDS, componentUuids);
        //    json.writeJSONString(writer);
        //}

        //@JsonChronologyUnmarshaler
        //public static DefinitionForSemanticDTO make(JSONObject jsonObject) {
        //    IEnumerable<Guid> componentUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.COMPONENT_UUIDS);
        //    return new DefinitionForSemanticDTO(componentUuids);
        //}

        //@Unmarshaler
        //public static DefinitionForSemanticDTO make(TinkarInput input) {
        //    try {
        //        int objectMarshalVersion = input.ReadInt();
        //        if (objectMarshalVersion == marshalVersion) {
        //            IEnumerable<Guid> componentUuids = input.ReadImmutableUuidList();
        //            return new DefinitionForSemanticDTO(componentUuids);
        //        } else {
        //            throw new UnsupportedOperationException("Unsupported version: " + objectMarshalVersion);
        //        }
        //    } catch (Exception ex) {
        //        throw new MarshalExceptionUnchecked(ex);
        //    }
        //}

        //@Override
        //@Marshaler
        //public void marshal(TinkarOutput out) {
        //    try {
        //        out.writeInt(marshalVersion);
        //        out.writeUuidList(componentUuids);
        //    } catch (Exception ex) {
        //        throw new MarshalExceptionUnchecked(ex);
        //    }
        //}
    }
}
