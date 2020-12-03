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
	public record SemanticChronologyDTO : ISemanticChronology, 
        IChangeSetThing, 
        IJsonMarshalable, 
        IMarshalable
	{

		private const int marshalVersion = 1;

        public IIdentifiedThing ChronologySet {get; init; }

        public IEnumerable<ISemanticVersion> Versions {get; init; }

        public IIdentifiedThing ReferencedComponent {get; init; }

        public IDefinitionForSemantic DefinitionForSemantic {get; init; }

        public IEnumerable<Guid> ComponentUuids {get; init; }

        //$public SemanticChronologyDTO(ImmutableList<UUID> componentUuids, DefinitionForSemantic definitionForSemantic,
        //                          IdentifiedThing referencedComponent, ImmutableList<SemanticVersionDTO> semanticVersions) {
        //    this(componentUuids,
        //            definitionForSemantic.componentUuids(),
        //            referencedComponent.componentUuids(),
        //            semanticVersions);
        //}

        //@Override
        //public IdentifiedThing referencedComponent() {
        //    return new IdentifiedThingDTO(referencedComponentUuids);
        //}

        //@Override
        //public DefinitionForSemantic definitionForSemantic() {
        //    return new DefinitionForSemanticDTO(definitionForSemanticUuids);
        //}

        //@Override
        //public void jsonMarshal(Writer writer) {
        //    final JSONObject json = new JSONObject();
        //    json.put(ComponentFieldForJson.CLASS, this.getClass().getCanonicalName());
        //    json.put(ComponentFieldForJson.COMPONENT_UUIDS, componentUuids);
        //    json.put(ComponentFieldForJson.DEFINITION_FOR_SEMANTIC_UUIDS, definitionForSemanticUuids);
        //    json.put(ComponentFieldForJson.REFERENCED_COMPONENT_UUIDS, referencedComponentUuids);
        //    json.put(ComponentFieldForJson.VERSIONS, semanticVersions);
        //    json.writeJSONString(writer);
        //}

        //@JsonChronologyUnmarshaler
        //public static SemanticChronologyDTO make(JSONObject jsonObject) {
        //    ImmutableList<UUID> componentUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.COMPONENT_UUIDS);
        //    ImmutableList<UUID> definitionForSemanticUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.DEFINITION_FOR_SEMANTIC_UUIDS);
        //    ImmutableList<UUID> referencedComponentUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.REFERENCED_COMPONENT_UUIDS);
        //    return new SemanticChronologyDTO(componentUuids,
        //            definitionForSemanticUuids,
        //            referencedComponentUuids,
        //                    jsonObject.asSemanticVersionList(ComponentFieldForJson.VERSIONS,
        //                            componentUuids,
        //                            definitionForSemanticUuids,
        //                            referencedComponentUuids)
        //            );
        //}

        //@Unmarshaler
        //public static SemanticChronologyDTO make(TinkarInput in) {
        //    try {
        //        int objectMarshalVersion = in.readInt();
        //        if (objectMarshalVersion == marshalVersion) {
        //            ImmutableList<UUID> componentUuids = in.readImmutableUuidList();
        //            ImmutableList<UUID> definitionForSemanticUuids = in.readImmutableUuidList();
        //            ImmutableList<UUID> referencedComponentUuids = in.readImmutableUuidList();
        //            return new SemanticChronologyDTO(
        //                    componentUuids, definitionForSemanticUuids, referencedComponentUuids,
        //                    in.readSemanticVersionList(componentUuids, definitionForSemanticUuids, referencedComponentUuids));
        //        } else {
        //            throw new UnsupportedOperationException("Unsupported version: " + objectMarshalVersion);
        //        }
        //    } catch (IOException ex) {
        //        throw new UncheckedIOException(ex);
        //    }
        //}

        //@Override
        //@Marshaler
        //public void marshal(TinkarOutput out) {
        //    try {
        //        out.writeInt(marshalVersion);
        //        out.writeUuidList(componentUuids);
        //        out.writeUuidList(definitionForSemanticUuids);
        //        out.writeUuidList(referencedComponentUuids);
        //        out.writeSemanticVersionList(semanticVersions);
        //    } catch (IOException ex) {
        //        throw new UncheckedIOException(ex);
        //    }
        //}

        //@Override
        //public ImmutableList<SemanticVersion> versions() {
        //    return semanticVersions.collect(semanticVersionDTO ->  semanticVersionDTO);
        //}

        //@Override
        //public DefinitionForSemanticDTO chronologySet() {
        //    return new DefinitionForSemanticDTO(definitionForSemanticUuids);
        //}
    }
}