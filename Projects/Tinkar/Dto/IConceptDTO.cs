using System;
using System.Collections.Generic;

namespace Tinkar
{
public record ConceptDTO : IConcept, 
    IJsonMarshalable, 
    IMarshalable 
{
    private const int marshalVersion = 1;

        public IEnumerable<Guid> ComponentUuids {get; init; }

        //$@Override
        //public void jsonMarshal(Writer writer) {
        //    final JSONObject json = new JSONObject();
        //    json.put(ComponentFieldForJson.CLASS, this.getClass().getCanonicalName());
        //    json.put(ComponentFieldForJson.COMPONENT_UUIDS, componentUuids);
        //    json.writeJSONString(writer);
        //}

        //@JsonChronologyUnmarshaler
        //public static ConceptDTO make(JSONObject jsonObject) {
        //    ImmutableList<UUID> componentUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.COMPONENT_UUIDS);
        //    return new ConceptDTO(componentUuids);
        //}

        //@Unmarshaler
        //public static ConceptDTO make(TinkarInput in) {
        //    try {
        //        int objectMarshalVersion = in.readInt();
        //        if (objectMarshalVersion == marshalVersion) {
        //            ImmutableList<UUID> componentUuids = in.readImmutableUuidList();
        //            return new ConceptDTO(componentUuids);
        //        } else {
        //            throw new UnsupportedOperationException("Unsupported version: " + objectMarshalVersion);
        //        }
        //    } catch (IOException ex) {
        //        throw new MarshalExceptionUnchecked(ex);
        //    }
        //}

        //@Override
        //@Marshaler
        //public void marshal(TinkarOutput out) {
        //    try {
        //        out.writeInt(marshalVersion);
        //        out.writeUuidList(componentUuids);
        //    } catch (IOException ex) {
        //        throw new MarshalExceptionUnchecked(ex);
        //    }
        //}
    }
}
