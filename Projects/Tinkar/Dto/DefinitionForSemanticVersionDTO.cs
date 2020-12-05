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
    public record DefinitionForSemanticVersionDTO :
        BaseDTO<DefinitionForSemanticVersionDTO>,
        IDefinitionForSemanticVersion,
        IChangeSetThing,
        IJsonMarshalable,
        IMarshalable
    {
        private const int MarshalVersion = 1;

        /// <summary>
        /// Implementation of IIdentifiedThing.ComponentUuids
        /// </summary>
        public IEnumerable<Guid> ComponentUuids { get; init; }

        /// <summary>
        /// Implementation of IVersion.Stamp
        /// </summary>
        public IStamp Stamp => this.StampDTO;

        /// <summary>
        /// Implementation of IDefinitionForSemanticVersion.ReferencedComponentPurpose
        /// </summary>
        public IConcept ReferencedComponentPurpose =>
                new ConceptDTO(this.ReferencedComponentPurposeUuids);


        /// <summary>
        /// Implementation of IDefinitionForSemanticVersion.FieldDefinitions
        /// </summary>
        public IEnumerable<IFieldDefinition> FieldDefinitions =>
            this.FieldDefinitionDTOs.Select((dto) => (IFieldDefinition)dto);

        /// <summary>
        /// Backing DTO for Stamp
        /// </summary>
        public StampDTO StampDTO { get; init; }

        /// <summary>
        /// ReferencedComponentPurpose Guids
        /// </summary>
        public IEnumerable<Guid> ReferencedComponentPurposeUuids { get; init; }

        /// <summary>
        /// FieldDefinition Guids
        /// </summary>
        public IEnumerable<FieldDefinitionDTO> FieldDefinitionDTOs { get; init; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="componentUuids">ComponentUuids</param>
        /// <param name="stampDTO">StampDTO</param>
        /// <param name="referencedComponentPurposeUuids">ReferencedComponentPurposeUuids</param>
        /// <param name="fieldDefinitionDTOs">FieldDefinitionDTOs</param>
        public DefinitionForSemanticVersionDTO(
            IEnumerable<Guid> componentUuids,
            StampDTO stampDTO,
            IEnumerable<Guid> referencedComponentPurposeUuids,
            IEnumerable<FieldDefinitionDTO> fieldDefinitionDTOs)
        {
            this.ComponentUuids = componentUuids;
            this.StampDTO = stampDTO;
            this.ReferencedComponentPurposeUuids = referencedComponentPurposeUuids;
            this.FieldDefinitionDTOs = fieldDefinitionDTOs;
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="other">Item to compare to</param>
        /// <returns>-1, 0, or 1</returns>
        public override Int32 CompareTo(DefinitionForSemanticVersionDTO other)
        {
            Int32 cmp = this.CompareGuids(this.ComponentUuids, other.ComponentUuids);
            if (cmp != 0)
                return cmp;

            cmp = this.CompareItem(this.StampDTO, other.StampDTO);
            if (cmp != 0)
                return cmp;

            cmp = this.CompareGuids(this.ReferencedComponentPurposeUuids, other.ReferencedComponentPurposeUuids);
            if (cmp != 0)
                return cmp;

            cmp = this.CompareSequence(this.FieldDefinitionDTOs, other.FieldDefinitionDTOs);
            if (cmp != 0)
                return cmp;
            return 0;
        }

        ///**
        // * Marshal method for DefinitionForSemanticVersionDTO using JSON
        // * @param writer
        // */
        //@Override
        //public void jsonMarshal(Writer writer) {
        //    final JSONObject json = new JSONObject();
        //    json.put(ComponentFieldForJson.STAMP, stampDTO);
        //    json.put(REFERENCED_COMPONENT_PURPOSE_UUIDS, referencedComponentPurposeUuids);
        //    json.put(FIELD_DEFINITIONS, FieldDefinitionDTOs);
        //    json.writeJSONString(writer);
        //}

        ///**
        // * Unmarshal method for DefinitionForSemanticVersionDTO using JSON
        // * @param jsonObject
        // * @param componentUuids
        // * @return
        // */
        //@JsonVersionUnmarshaler
        //public static DefinitionForSemanticVersionDTO Make(JSONObject jsonObject, IEnumerable<Guid> componentUuids) {
        //    return new DefinitionForSemanticVersionDTO(componentUuids,
        //            StampDTO.Make((JSONObject) jsonObject.get(ComponentFieldForJson.STAMP)),
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
            CheckMarshalVersion(input, MarshalVersion);
            return new DefinitionForSemanticVersionDTO(componentUuids,
                    StampDTO.Make(input),
                    input.ReadImmutableUuidList(),
                    input.ReadFieldDefinitionList());
        }

        ///**
        // * Marshal method for DefinitionForSemanticVersionDTO
        // * @param out
        // */
        public void Marshal(TinkarOutput output)
        {
            WriteMarshalVersion(output, MarshalVersion);
            this.StampDTO.Marshal(output);
            output.WriteUuidList(this.ReferencedComponentPurposeUuids);
            output.WriteMarshalableList(this.FieldDefinitionDTOs);
        }
    }
}
