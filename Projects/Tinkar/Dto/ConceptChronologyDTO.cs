/*
 * Copyright 2020 HL7.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;

namespace Tinkar
{
	/**
	 *
	 * @author kec
	 */
	public record ConceptChronologyDTO : IChangeSetThing,
		IJsonMarshalable,
		IMarshalable,
		IConceptChronology
	{
		private const int marshalVersion = 1;

        public IIdentifiedThing ChronologySet {get; init; }

        public IEnumerable<ConceptVersion> Versions {get; init; }

        public IEnumerable<Guid> ComponentUuids {get; init; }


        //$@Override
        //public Concept chronologySet() {
        //    return new ConceptDTO(chronologySetUuids);
        //}

        //@Override
        //public ImmutableList<ConceptVersion> versions() {
        //    return conceptVersions.collect(conceptVersionDTO -> (ConceptVersion) conceptVersionDTO);
        //}

        //@Override
        //public void jsonMarshal(Writer writer) {
        //    final JSONObject json = new JSONObject();
        //    json.put(ComponentFieldForJson.CLASS, this.getClass().getCanonicalName());
        //    json.put(ComponentFieldForJson.COMPONENT_UUIDS, componentUuids);
        //    json.put(ComponentFieldForJson.CHRONOLOGY_SET_UUIDS, chronologySetUuids);
        //    json.put(ComponentFieldForJson.CONCEPT_VERSIONS, conceptVersions);
        //    json.writeJSONString(writer);
        //}

        //@JsonChronologyUnmarshaler
        //public static ConceptChronologyDTO make(JSONObject jsonObject) {
        //    ImmutableList<UUID> componentUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.COMPONENT_UUIDS);
        //    return new ConceptChronologyDTO(componentUuids,
        //                    jsonObject.asImmutableUuidList(ComponentFieldForJson.CHRONOLOGY_SET_UUIDS),
        //                    jsonObject.asConceptVersionList(ComponentFieldForJson.CONCEPT_VERSIONS, componentUuids));
        //}

        //@Unmarshaler
        //public static ConceptChronologyDTO make(TinkarInput in) {
        //    try {
        //        int objectMarshalVersion = in.readInt();
        //        if (objectMarshalVersion == marshalVersion) {
        //            ImmutableList<UUID> componentUuids = in.readImmutableUuidList();
        //            return new ConceptChronologyDTO(
        //                    componentUuids, in.readImmutableUuidList(), in.readConceptVersionList(componentUuids));
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
        //        out.writeUuidList(chronologySetUuids);
        //        // Note that the componentIds are not written redundantly
        //        // in writeConceptVersionList...
        //        out.writeConceptVersionList(conceptVersions);
        //    } catch (IOException ex) {
        //        throw new MarshalExceptionUnchecked(ex);
        //    }
        //}
    }
}
