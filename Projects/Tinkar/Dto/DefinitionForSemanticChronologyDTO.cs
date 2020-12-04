/*
 * Copyright 2020 kec.
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
	public record DefinitionForSemanticChronologyDTO : BaseDTO, 
        IDefinitionForSemanticChronology,
		IChangeSetThing,
		IJsonMarshalable,
		IMarshalable
	{
		protected override int MarshalVersion => 1;

        public IIdentifiedThing ChronologySet {get; init; }

        public IEnumerable<IDefinitionForSemanticVersion> Versions {get; init; }

        public IEnumerable<Guid> ComponentUuids {get; init; }

        //$@Override
        //public IEnumerable<DefinitionForSemanticVersion> versions() {
        //    return definitionVersions.collect(definitionForSemanticVersionDTO -> (DefinitionForSemanticVersion) definitionForSemanticVersionDTO);
        //}

        //@Override
        //public Concept chronologySet() {
        //    return new ConceptDTO(chronologySetUuids);
        //}

        //@Override
        //public void jsonMarshal(Writer writer) {
        //    final JSONObject json = new JSONObject();
        //    json.put(ComponentFieldForJson.CLASS, this.getClass().getCanonicalName());
        //    json.put(ComponentFieldForJson.COMPONENT_UUIDS, componentUuids);
        //    json.put(ComponentFieldForJson.CHRONOLOGY_SET_UUIDS, chronologySetUuids);
        //    json.put(ComponentFieldForJson.DEFINITION_VERSIONS, definitionVersions);
        //    json.writeJSONString(writer);
        //}

        //@JsonChronologyUnmarshaler
        //public static DefinitionForSemanticChronologyDTO make(JSONObject jsonObject) {
        //    IEnumerable<Guid> componentUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.COMPONENT_UUIDS);
        //    return new DefinitionForSemanticChronologyDTO(componentUuids,
        //                    jsonObject.asImmutableUuidList(ComponentFieldForJson.CHRONOLOGY_SET_UUIDS),
        //                    jsonObject.asDefinitionForSemanticVersionList(ComponentFieldForJson.DEFINITION_VERSIONS, componentUuids));
        //}

        //@Unmarshaler
        //public static DefinitionForSemanticChronologyDTO make(TinkarInput input) {
        //    try {
        //        int objectMarshalVersion = input.ReadInt();
        //        if (objectMarshalVersion == marshalVersion) {
        //            IEnumerable<Guid> componentUuids = input.ReadImmutableUuidList();
        //            return new DefinitionForSemanticChronologyDTO(
        //                    componentUuids, input.ReadImmutableUuidList(), input.readDefinitionForSemanticVersionList(componentUuids));
        //        } else {
        //            throw new UnsupportedOperationException("Unsupported version: " + objectMarshalVersion);
        //        }
        //    } catch (Exception ex) {
        //        throw new UncheckedIOException(ex);
        //    }
        //}

        //@Override
        //@Marshaler
        //public void marshal(TinkarOutput out) {
        //    try {
        //        out.writeInt(marshalVersion);
        //        out.writeUuidList(componentUuids);
        //        out.writeUuidList(chronologySetUuids);
        //        out.writeDefinitionForSemanticVersionList(definitionVersions);
        //    } catch (Exception ex) {
        //        throw new UncheckedIOException(ex);
        //    }
        //}
    }
}
