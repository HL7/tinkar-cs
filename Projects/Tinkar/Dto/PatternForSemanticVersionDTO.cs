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
        ComponentDTO,
        IPatternForSemanticVersion<FieldDefinitionDTO>,
        IDTO,
        IJsonMarshalable,
        IMarshalable
    {
        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public const String JSONCLASSNAME = "PatternForSemanticVersionDTO";

        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public override String JsonClassName => JSONCLASSNAME;

        /// <summary>
        /// Gets Stamp.
        /// </summary>
        public IStamp Stamp => this.StampDTO;

        public IPublicId ReferencedComponentPurposePublicId { get; init; }
        public IPublicId ReferencedComponentMeaningPublicId { get; init; }
        public IEnumerable<FieldDefinitionDTO> FieldDefinitionDTOs { get; init; }

        public IConcept ReferencedComponentPurpose =>
                new ConceptDTO(this.ReferencedComponentPurposePublicId);

        /// <summary>
        /// Gets FieldDefinitions.
        /// </summary>
        public IEnumerable<FieldDefinitionDTO> FieldDefinitions => this.FieldDefinitionDTOs;

        /// <summary>
        /// Gets Stamp record.
        /// </summary>
        public StampDTO StampDTO { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatternForSemanticVersionDTO"/> class.
        /// </summary>
        /// <param name = "publicId" > Public id(component ids).</param>
        /// <param name="stampDTO">StampDTO.</param>
        /// <param name="referencedComponentPurposePublicId">Referenced Component Purpose PublicIc.</param>
        /// <param name="referencedComponentMeaningPublicId">Referenced Component Meaning PublicIc.</param>
        /// <param name="fieldDefinitionDTOs">FieldDefinitions.</param>
        public PatternForSemanticVersionDTO(
            IPublicId publicId,
            StampDTO stampDTO,
            IPublicId referencedComponentPurposePublicId,
            IPublicId referencedComponentMeaningPublicId,
            IEnumerable<FieldDefinitionDTO> fieldDefinitionDTOs) : base(publicId)
        {
            this.StampDTO = stampDTO;
            this.ReferencedComponentPurposePublicId = referencedComponentPurposePublicId;
            this.ReferencedComponentMeaningPublicId = referencedComponentMeaningPublicId;
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
            IPublicId publicId) : base(jObj, publicId)
        {
            this.StampDTO = new StampDTO(jObj.ReadToken<JObject>(ComponentFieldForJson.STAMP));
            this.ReferencedComponentPurposePublicId = jObj.ReadPublicId(ComponentFieldForJson.REFERENCED_COMPONENT_PURPOSE_PUBLIC_ID);
            this.ReferencedComponentMeaningPublicId = jObj.ReadPublicId(ComponentFieldForJson.REFERENCED_COMPONENT_MEANING_PUBLIC_ID);
            this.FieldDefinitionDTOs = jObj.ReadFieldDefinitionList();
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="otherObject">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public override Int32 CompareTo(Object otherObject)
        {
            PatternForSemanticVersionDTO other = otherObject as PatternForSemanticVersionDTO;
            if (other == null)
                return -1;

            Int32 cmp = base.CompareTo(other);
            if (cmp != 0)
                return cmp;

            cmp = this.StampDTO.CompareTo(other.StampDTO);
            if (cmp != 0)
                return cmp;

            cmp = this.ReferencedComponentPurposePublicId.CompareTo(other.ReferencedComponentPurposePublicId);
            if (cmp != 0)
                return cmp;

            cmp = this.ReferencedComponentMeaningPublicId.CompareTo(other.ReferencedComponentMeaningPublicId);
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
            new PatternForSemanticVersionDTO(
                publicId,
                StampDTO.Make(input),
                input.GetPublicId(),
                input.GetPublicId(),
                input.GetFieldDefinitionList());

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public override void MarshalFields(TinkarOutput output)
        {
            base.MarshalFields(output);
            this.StampDTO.Marshal(output);
            output.WritePublicId(this.ReferencedComponentPurposePublicId);
            output.WritePublicId(this.ReferencedComponentPurposePublicId);
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
        public override void MarshalFields(TinkarJsonOutput output)
        {
            base.MarshalFields(output);
            output.WritePropertyName(ComponentFieldForJson.STAMP);
            this.StampDTO.Marshal(output);
            output.WritePublicId(
                ComponentFieldForJson.REFERENCED_COMPONENT_PURPOSE_PUBLIC_ID,
                this.ReferencedComponentPurposePublicId);
            output.WritePublicId(
                ComponentFieldForJson.REFERENCED_COMPONENT_MEANING_PUBLIC_ID,
                this.ReferencedComponentMeaningPublicId);
            output.WriteMarshalableList(
                ComponentFieldForJson.FIELD_DEFINITIONS,
                this.FieldDefinitionDTOs);
        }
    }
}
