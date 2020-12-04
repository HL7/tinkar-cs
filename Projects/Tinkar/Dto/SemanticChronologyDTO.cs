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
using System.Linq;

namespace Tinkar
{
    /**
	 *
	 * @author kec
	 */
    public record SemanticChronologyDTO(
        IEnumerable<Guid> ComponentUuids,
        IEnumerable<Guid> DefinitionForSemanticUuids,
        IEnumerable<Guid> ReferencedComponentUuids,
        IEnumerable<SemanticVersionDTO> SemanticVersions) : BaseDTO,
        ISemanticChronology<DefinitionForSemanticDTO>,
        IChangeSetThing,
        IJsonMarshalable,
        IMarshalable
    {

        private const int MarshalVersion = 1;

        //$public SemanticChronologyDTO(IEnumerable<Guid> componentUuids, DefinitionForSemantic definitionForSemantic,
        //                          IdentifiedThing referencedComponent, IEnumerable<SemanticVersionDTO> semanticVersions) {
        //    this(componentUuids,
        //            definitionForSemantic.componentUuids(),
        //            referencedComponent.componentUuids(),
        //            semanticVersions);
        //}

        public IIdentifiedThing ReferencedComponent => new IdentifiedThingDTO(this.ReferencedComponentUuids);
        public IDefinitionForSemantic DefinitionForSemantic => new DefinitionForSemanticDTO(this.DefinitionForSemanticUuids);

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
        //    IEnumerable<Guid> componentUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.COMPONENT_UUIDS);
        //    IEnumerable<Guid> definitionForSemanticUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.DEFINITION_FOR_SEMANTIC_UUIDS);
        //    IEnumerable<Guid> referencedComponentUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.REFERENCED_COMPONENT_UUIDS);
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
        public static SemanticChronologyDTO Make(TinkarInput input)
        {
            CheckMarshallVersion(input, MarshalVersion);
            IEnumerable<Guid> componentUuids = input.ReadImmutableUuidList();
            IEnumerable<Guid> definitionForSemanticUuids = input.ReadImmutableUuidList();
            IEnumerable<Guid> referencedComponentUuids = input.ReadImmutableUuidList();
            return new SemanticChronologyDTO(
                    componentUuids, definitionForSemanticUuids, referencedComponentUuids,
                    input.ReadSemanticVersionList(componentUuids, definitionForSemanticUuids, referencedComponentUuids)
                    );
        }

        //@Override
        //@Marshaler
        //public void marshal(TinkarOutput out) {
        //    try {
        //        out.writeInt(marshalVersion);
        //        out.writeUuidList(componentUuids);
        //        out.writeUuidList(definitionForSemanticUuids);
        //        out.writeUuidList(referencedComponentUuids);
        //        out.writeSemanticVersionList(semanticVersions);
        //    } catch (Exception ex) {
        //        throw new UncheckedIOException(ex);
        //    }
        //}

        public IEnumerable<ISemanticVersion> Versions =>
            this.SemanticVersions.Select( (dto) => (ISemanticVersion) dto);

        public DefinitionForSemanticDTO ChronologySet => 
            new DefinitionForSemanticDTO(this.DefinitionForSemanticUuids);
    }
}