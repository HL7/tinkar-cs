using System;
using System.Collections.Generic;

namespace Tinkar
{
    public record DefinitionForSemanticDTO(IEnumerable<Guid> ComponentUuids) : BaseDTO,
        IDefinitionForSemantic,
        IJsonMarshalable,
        IMarshalable
    {
        private const int MarshalVersion = 1;

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
        public static DefinitionForSemanticDTO make(TinkarInput input)
        {
            CheckMarshallVersion(input, MarshalVersion);
            IEnumerable<Guid> componentUuids = input.ReadImmutableUuidList();
            return new DefinitionForSemanticDTO(componentUuids);
        }

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
