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
using System.Collections.Immutable;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Tinkar.Dto
{
    /// <summary>
    /// Tinkar PatternForSemanticVersion record.
    /// </summary>
    public record TypePatternVersionDTO :
        VersionDTO,
        ITypePatternVersion<FieldDefinitionDTO>,
        IJsonMarshalable,
        IMarshalable
    {
        // <summary>
        // Name of this class in JSON serialization.
        // This must be consistent with Java implementation.
        // </summary>
        //public const String JSONCLASSNAME = "PatternForSemanticVersionDTO";

        /// <summary>
        /// Unique ID for binary marshal of this item.
        /// </summary>
        public FieldDataType FieldDataType => FieldDataType.TypePatternVersionType;

        public IPublicId ReferencedComponentPurposePublicId { get; init; }
        public IPublicId ReferencedComponentMeaningPublicId { get; init; }
        public ImmutableArray<FieldDefinitionDTO> FieldDefinitionDTOs { get; init; }

        public IConcept ReferencedComponentPurpose =>
                new ConceptDTO(this.ReferencedComponentPurposePublicId);

        public IConcept ReferencedComponentMeaning =>
                new ConceptDTO(this.ReferencedComponentMeaningPublicId);

        /// <summary>
        /// Gets FieldDefinitions.
        /// </summary>
        public ImmutableArray<FieldDefinitionDTO> FieldDefinitions => this.FieldDefinitionDTOs;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypePatternVersionDTO"/> class.
        /// </summary>
        /// <param name = "componentPublicId" > Public id(component ids).</param>
        /// <param name="stamp">StampDTO.</param>
        /// <param name="referencedComponentPurposePublicId">Referenced Component Purpose PublicIc.</param>
        /// <param name="referencedComponentMeaningPublicId">Referenced Component Meaning PublicIc.</param>
        /// <param name="fieldDefinitionDTOs">FieldDefinitions.</param>
        public TypePatternVersionDTO(
            IPublicId componentPublicId,
            StampDTO stamp,
            IPublicId referencedComponentPurposePublicId,
            IPublicId referencedComponentMeaningPublicId,
            ImmutableArray<FieldDefinitionDTO> fieldDefinitionDTOs) : base(componentPublicId, stamp)
        {
            this.ReferencedComponentPurposePublicId = referencedComponentPurposePublicId;
            this.ReferencedComponentMeaningPublicId = referencedComponentMeaningPublicId;
            this.FieldDefinitionDTOs = fieldDefinitionDTOs;
        }

        /// <summary>
        /// Returns zero of the two items are equivalent. This is not necessarily the
        /// same is same.
        /// </summary>
        /// <param name="otherObject"></param>
        /// <returns></returns>
        public override Boolean IsEquivalent(Object otherObject)
        {
            if (base.IsEquivalent(otherObject) == false)
                return false;

            TypePatternVersionDTO other = otherObject as TypePatternVersionDTO;
            if (other == null)
                return false;

            if (this.ReferencedComponentPurposePublicId.IsEquivalent(other.ReferencedComponentPurposePublicId) == false)
                return false;
            if (this.ReferencedComponentMeaningPublicId.IsEquivalent(other.ReferencedComponentMeaningPublicId) == false)
                return false;
            if (FieldCompare.IsEquivalentSequence(this.FieldDefinitionDTOs, other.FieldDefinitionDTOs) == false)
                return false;
            return true;
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="otherObject">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public override Int32 CompareTo(Object otherObject)
        {
            TypePatternVersionDTO other = otherObject as TypePatternVersionDTO;
            if (other == null)
                return -1;

            Int32 cmp = base.CompareTo(other);
            if (cmp != 0)
                return cmp;

            cmp = this.ReferencedComponentPurposePublicId.CompareTo(other.ReferencedComponentPurposePublicId);
            if (cmp != 0)
                return cmp;

            cmp = this.ReferencedComponentMeaningPublicId.CompareTo(other.ReferencedComponentMeaningPublicId);
            if (cmp != 0)
                return cmp;

            cmp = FieldCompare.CompareSequence(this.FieldDefinitionDTOs, other.FieldDefinitionDTOs);
            if (cmp != 0)
                return cmp;
            return 0;
        }

        public static TypePatternVersionDTO Make(ITypePatternVersion<FieldDefinitionDTO> definitionForSemanticVersion)
        {

            List<FieldDefinitionDTO> fields = new List<FieldDefinitionDTO>();
            foreach (IFieldDefinition fieldDefinition in definitionForSemanticVersion.FieldDefinitions)
                fields.Add(FieldDefinitionDTO.Make(fieldDefinition));

            return new TypePatternVersionDTO(
                    definitionForSemanticVersion.PublicId,
                    StampDTO.Make(definitionForSemanticVersion.Stamp),
                    definitionForSemanticVersion.ReferencedComponentPurpose.PublicId,
                    definitionForSemanticVersion.ReferencedComponentMeaning.PublicId,
                    fields.ToImmutableArray());
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        /// <param name="componentPublicId">Public id (component ids).</param>
        /// <returns>new DTO item.</returns>
        public static TypePatternVersionDTO Make(TinkarInput input, IPublicId componentPublicId) =>
            new TypePatternVersionDTO(
                componentPublicId,
                StampDTO.Make(input),
                input.GetPublicId(),
                input.GetPublicId(),
                input.GetFieldDefinitionList().ToImmutableArray());

        /// <summary>
        /// Static method to Create DTO item from json .
        /// </summary>
        /// <param name="jsonObject">JSON parent container to read from.</param>
        /// <param name="componentPublicId">Public id (component ids).</param>
        /// <returns>Deserialized PatternForSemanticVersion record.</returns>
        public static TypePatternVersionDTO Make(
            JObject jsonObject,
            IPublicId componentPublicId) =>
                new TypePatternVersionDTO(componentPublicId,
                    StampDTO.Make((JObject)jsonObject[ComponentFieldForJson.STAMP]),
                    jsonObject.AsPublicId(ComponentFieldForJson.REFERENCED_COMPONENT_PURPOSE_PUBLIC_ID),
                    jsonObject.AsPublicId(ComponentFieldForJson.REFERENCED_COMPONENT_MEANING_PUBLIC_ID),
                    jsonObject.AsFieldDefinitionList(ComponentFieldForJson.FIELD_DEFINITIONS).ToImmutableArray());

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public virtual void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WritePropertyName(ComponentFieldForJson.STAMP);
            this.StampDTO.Marshal(output);
            output.Put(ComponentFieldForJson.REFERENCED_COMPONENT_PURPOSE_PUBLIC_ID, this.ReferencedComponentPurposePublicId);
            output.Put(ComponentFieldForJson.REFERENCED_COMPONENT_MEANING_PUBLIC_ID, this.ReferencedComponentMeaningPublicId);
            output.WriteMarshalableList(ComponentFieldForJson.FIELD_DEFINITIONS, this.FieldDefinitionDTOs);
            output.WriteEndObject();
        }

        public virtual void Marshal(TinkarOutput output)
        {
            this.StampDTO.Marshal(output);
            output.PutPublicId(this.ReferencedComponentPurposePublicId);
            output.PutPublicId(this.ReferencedComponentMeaningPublicId);
            output.WriteMarshalableList(this.FieldDefinitionDTOs);
        }
    }
}
