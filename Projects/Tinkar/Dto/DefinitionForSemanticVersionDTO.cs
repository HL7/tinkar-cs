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

using Newtonsoft.Json.Linq;
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
        /// <summary>
        /// Version of marshalling code.
        /// If code is modified in a way that renders old serialized data
        /// non-conformant, then this number should be incremented.
        /// </summary>
        private const int MarshalVersion = 1;

        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        private const String JsonClassName = "DefinitionForSemanticVersionDTO";


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
        /// Create item from json stream
        /// </summary>
        public DefinitionForSemanticVersionDTO(JObject jObj,
            IEnumerable<Guid> componentUuids)
        {
            jObj.GetClass(JsonClassName);
            this.ComponentUuids = componentUuids;
            this.StampDTO = new StampDTO(jObj.ReadToken<JObject>(ComponentFieldForJson.STAMP));
            this.ReferencedComponentPurposeUuids = jObj.ReadUuids(ComponentFieldForJson.REFERENCED_COMPONENT_PURPOSE_UUIDS);
            this.FieldDefinitionDTOs = jObj.ReadFieldDefinitionList();
        }

        public DefinitionForSemanticVersionDTO(TinkarInput input,
                IEnumerable<Guid> componentUuids)
        {
            input.CheckMarshalVersion(MarshalVersion);
            this.ComponentUuids = componentUuids;
            this.StampDTO = new StampDTO(input);
            this.ReferencedComponentPurposeUuids = input.ReadUuids();
            this.FieldDefinitionDTOs = input.ReadFieldDefinitionList();
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="other">Item to compare to</param>
        /// <returns>-1, 0, or 1</returns>
        public override Int32 CompareTo(DefinitionForSemanticVersionDTO other)
        {
            Int32 cmp = FieldCompare.CompareGuids(this.ComponentUuids, other.ComponentUuids);
            if (cmp != 0)
                return cmp;

            cmp = FieldCompare.CompareItem(this.StampDTO, other.StampDTO);
            if (cmp != 0)
                return cmp;

            cmp = FieldCompare.CompareGuids(this.ReferencedComponentPurposeUuids, other.ReferencedComponentPurposeUuids);
            if (cmp != 0)
                return cmp;

            cmp = FieldCompare.CompareSequence(this.FieldDefinitionDTOs, other.FieldDefinitionDTOs);
            if (cmp != 0)
                return cmp;
            return 0;
        }

        public static DefinitionForSemanticVersionDTO Make(TinkarInput input,
            IEnumerable<Guid> componentUuids) =>
            new DefinitionForSemanticVersionDTO(input, componentUuids);

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream</param>
        public void Marshal(TinkarOutput output)
        {
            output.WriteMarshalVersion(MarshalVersion);
            this.StampDTO.Marshal(output);
            output.WriteUuids(this.ReferencedComponentPurposeUuids);
            output.WriteMarshalableList(this.FieldDefinitionDTOs);
        }

        /// <summary>
        /// Static method to Create DTO item from json .
        /// </summary>
        public static DefinitionForSemanticVersionDTO Make(JObject jObj,
            IEnumerable<Guid> componentUuids) =>
            new DefinitionForSemanticVersionDTO(jObj, componentUuids);

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream</param>
        public void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(JsonClassName);
            output.WriteUuids(ComponentFieldForJson.COMPONENT_UUIDS,
                this.ComponentUuids);
            output.WritePropertyName(ComponentFieldForJson.STAMP);
            this.StampDTO.Marshal(output);
            output.WriteUuids(ComponentFieldForJson.REFERENCED_COMPONENT_PURPOSE_UUIDS,
                this.ReferencedComponentPurposeUuids);
            output.WriteMarshalableList(ComponentFieldForJson.FIELD_DEFINITIONS,
                this.FieldDefinitionDTOs);
            output.WriteEndObject();
        }
    }
}
