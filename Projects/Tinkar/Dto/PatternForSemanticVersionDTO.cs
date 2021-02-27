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
using Newtonsoft.Json.Linq;

namespace Tinkar
{
    /// <summary>
    /// Tinkar PatternForSemanticVersion record.
    /// </summary>
    public record PatternForSemanticVersionDTO :
        ComponentDTO<PatternForSemanticVersionDTO>,
        IPatternForSemanticVersion,
        IChangeSetThing,
        IJsonMarshalable,
        IMarshalable
    {
        /// <summary>
        /// Version of marshalling code.
        /// If code is modified in a way that renders old serialized data
        /// non-conformant, then this number should be incremented.
        /// </summary>
        private const int LocalMarshalVersion = 3;

        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public const String JsonClassName = "PatternForSemanticVersionDTO";

        /// <summary>
        /// Gets public id.
        /// </summary>
        public IPublicId PublicId { get; init; }

        /// <summary>
        /// Gets Stamp.
        /// </summary>
        public IStamp Stamp => this.StampDTO;

        /// <summary>
        /// Gets ReferencedComponentPurpose.
        /// </summary>
        public IConcept ReferencedComponentPurpose =>
                new ConceptDTO(this.ReferencedComponentPurposeUuids);

        /// <summary>
        /// Gets FieldDefinitions.
        /// </summary>
        public IEnumerable<IFieldDefinition> FieldDefinitions =>
            this.FieldDefinitionDTOs.Select((dto) => (IFieldDefinition)dto);

        /// <summary>
        /// Gets Stamp record.
        /// </summary>
        public StampDTO StampDTO { get; init; }

        /// <summary>
        /// Gets ReferencedComponentPurpose UUIDs.
        /// </summary>
        public IEnumerable<Guid> ReferencedComponentPurposeUuids { get; init; }

        /// <summary>
        /// Gets FieldDefinition UUIDs.
        /// </summary>
        public IEnumerable<FieldDefinitionDTO> FieldDefinitionDTOs { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatternForSemanticVersionDTO"/> class.
        /// </summary>
        /// <param name = "publicId" > Public id(component ids).</param>
        /// <param name="stampDTO">StampDTO.</param>
        /// <param name="referencedComponentPurposeUuids">ReferencedComponentPurposeUuids.</param>
        /// <param name="fieldDefinitionDTOs">FieldDefinitionDTOs.</param>
        public PatternForSemanticVersionDTO(
            IPublicId publicId,
            StampDTO stampDTO,
            IEnumerable<Guid> referencedComponentPurposeUuids,
            IEnumerable<FieldDefinitionDTO> fieldDefinitionDTOs)
        {
            this.PublicId = publicId;
            this.StampDTO = stampDTO;
            this.ReferencedComponentPurposeUuids = referencedComponentPurposeUuids;
            this.FieldDefinitionDTOs = fieldDefinitionDTOs;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatternForSemanticVersionDTO"/> class
        /// from json stream.
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        /// <param name="publicId">Public id (component ids).</param>
        public PatternForSemanticVersionDTO(
            JObject jObj,
            IPublicId publicId)
        {
            jObj.GetClass(JsonClassName);
            this.PublicId = publicId;
            this.StampDTO = new StampDTO(jObj.ReadToken<JObject>(ComponentFieldForJson.STAMP));
            this.ReferencedComponentPurposeUuids = jObj.ReadUuids(ComponentFieldForJson.REFERENCED_COMPONENT_PURPOSE_UUIDS);
            this.FieldDefinitionDTOs = jObj.ReadFieldDefinitionList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatternForSemanticVersionDTO"/> class
        /// from binary stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        /// <param name="publicId">Public id (component ids).</param>
        public PatternForSemanticVersionDTO(TinkarInput input, IPublicId publicId)
        {
            input.CheckMarshalVersion(LocalMarshalVersion);
            this.PublicId = publicId;
            this.StampDTO = new StampDTO(input);
            this.ReferencedComponentPurposeUuids = input.ReadUuids();
            this.FieldDefinitionDTOs = input.ReadFieldDefinitionList();
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="other">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public override Int32 CompareTo(PatternForSemanticVersionDTO other)
        {
            Int32 cmp = FieldCompare.ComparePublicIds(this.PublicId, other.PublicId);
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

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        /// <param name="publicId">Public id (component ids).</param>
        /// <returns>new DTO item.</returns>
        public static PatternForSemanticVersionDTO Make(TinkarInput input, IPublicId publicId) =>
            new PatternForSemanticVersionDTO(input, publicId);

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public void Marshal(TinkarOutput output)
        {
            output.CheckMarshalVersion(LocalMarshalVersion);;
            this.StampDTO.Marshal(output);
            output.WriteUuids(this.ReferencedComponentPurposeUuids);
            output.WriteMarshalableList(this.FieldDefinitionDTOs);
        }

        /// <summary>
        /// Static method to Create DTO item from json .
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        /// <param name="publicId">Public id (component ids).</param>
        /// <returns>Deserialized PatternForSemanticVersion record.</returns>
        public static PatternForSemanticVersionDTO Make(
            JObject jObj,
            IPublicId publicId) =>
            new PatternForSemanticVersionDTO(jObj, publicId);

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(JsonClassName);
            output.WriteUuids(
                ComponentFieldForJson.COMPONENT_PUBLIC_ID,
                this.PublicId);
            output.WritePropertyName(ComponentFieldForJson.STAMP);
            this.StampDTO.Marshal(output);
            output.WriteUuids(
                ComponentFieldForJson.REFERENCED_COMPONENT_PURPOSE_UUIDS,
                this.ReferencedComponentPurposeUuids);
            output.WriteMarshalableList(
                ComponentFieldForJson.FIELD_DEFINITIONS,
                this.FieldDefinitionDTOs);
            output.WriteEndObject();
        }
    }
}
