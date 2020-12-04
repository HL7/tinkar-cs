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
    public record DefinitionForSemanticVersionDTO(
            IEnumerable<Guid> ComponentUuids,
            StampDTO StampDTO,
            IEnumerable<Guid> ReferencedComponentPurposeUuids,
            IEnumerable<FieldDefinitionDTO> FieldDefinitionDTOs) : BaseDTO,
        IDefinitionForSemanticVersion,
        IChangeSetThing,
        IJsonMarshalable,
        IMarshalable
    {
        private const int MarshalVersion = 1;

        //$@Override
        public IConcept ReferencedComponentPurpose =>
            new ConceptDTO(this.ReferencedComponentPurposeUuids);

        //@Override
        public IStamp Stamp => this.StampDTO;

        //@Override
        public IEnumerable<IFieldDefinition> FieldDefinitions =>
            this.FieldDefinitionDTOs.Select((dto) => (IFieldDefinition)dto);

        ///**
        // * Marshal method for DefinitionForSemanticVersionDTO using JSON
        // * @param writer
        // */
        //@Override
        //public void jsonMarshal(Writer writer) {
        //    final JSONObject json = new JSONObject();
        //    json.put(ComponentFieldForJson.STAMP, stampDTO);
        //    json.put(REFERENCED_COMPONENT_PURPOSE_UUIDS, referencedComponentPurposeUuids);
        //    json.put(FIELD_DEFINITIONS, fieldDefinitionDTOS);
        //    json.writeJSONString(writer);
        //}

        ///**
        // * Unmarshal method for DefinitionForSemanticVersionDTO using JSON
        // * @param jsonObject
        // * @param componentUuids
        // * @return
        // */
        //@JsonVersionUnmarshaler
        //public static DefinitionForSemanticVersionDTO make(JSONObject jsonObject, IEnumerable<Guid> componentUuids) {
        //    return new DefinitionForSemanticVersionDTO(componentUuids,
        //            StampDTO.make((JSONObject) jsonObject.get(ComponentFieldForJson.STAMP)),
        //            jsonObject.asImmutableUuidList(REFERENCED_COMPONENT_PURPOSE_UUIDS),
        //            jsonObject.asFieldDefinitionList(FIELD_DEFINITIONS));
        //}

        ///**
        // * Unmarshal method for DefinitionForSemanticVersionDTO
        // * @param in
        // * @param componentUuids
        // * @return
        // */
        //@VersionUnmarshaler
        public static DefinitionForSemanticVersionDTO Make(TinkarInput input,
            IEnumerable<Guid> componentUuids)
        {
            CheckMarshallVersion(input, MarshalVersion);
            return new DefinitionForSemanticVersionDTO(componentUuids,
                    StampDTO.Make(input),
                    input.ReadImmutableUuidList(),
                    input.ReadFieldDefinitionList());
        }

        ///**
        // * Marshal method for DefinitionForSemanticVersionDTO
        // * @param out
        // */
        //@Override
        //@Marshaler
        //public void marshal(TinkarOutput out) {
        //    try {
        //        out.writeInt(marshalVersion);
        //        stampDTO.marshal(out);
        //        out.writeUuidList(referencedComponentPurposeUuids);
        //        out.writeFieldDefinitionList(fieldDefinitionDTOS);
        //    } catch (Exception ex) {
        //        throw new UncheckedIOException(ex);
        //    }
        //}
    }
}
