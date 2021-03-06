/*
 * Copyright 2020-2021 HL7.
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
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Tinkar
{
    /// <summary>
    /// FieldDefinition record.
    /// </summary>
    public record FieldDefinitionDTO:
        IFieldDefinition,
        IJsonMarshalable,
        IMarshalable,
        IEquivalent,
        IComparable
    {
        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public const String JSONCLASSNAME = "FieldDefinitionDTO";

        /// <summary>
        /// Gets DataType record.
        /// </summary>
        public IConcept DataType => new ConceptDTO(this.DataTypePublicId);

        /// <summary>
        /// Gets Purpose concept.
        /// </summary>
        public IConcept Purpose => new ConceptDTO(this.PurposePublicId);

        /// <summary>
        /// Gets Meaning concept.
        /// </summary>
        public IConcept Meaning => new ConceptDTO(this.MeaningPublicId);

        /// <summary>
        /// Gets DataType uuids.
        /// </summary>
        public IPublicId DataTypePublicId { get; init; }

        /// <summary>
        /// Gets Purpose UUIDs.
        /// </summary>
        public IPublicId PurposePublicId { get; init; }

        /// <summary>
        /// Gets Meaning public id.
        /// </summary>
        public IPublicId MeaningPublicId { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldDefinitionDTO"/> class.
        /// </summary>
        /// <param name="dataTypePublicId">dataTypePublicId.</param>
        /// <param name="purposePublicId">purposePublicId.</param>
        /// <param name="meaningPublicId">meaningPublicId.</param>
        public FieldDefinitionDTO(
            IPublicId dataTypePublicId,
            IPublicId purposePublicId,
            IPublicId meaningPublicId)
        {
            this.DataTypePublicId = dataTypePublicId;
            this.PurposePublicId = purposePublicId;
            this.MeaningPublicId = meaningPublicId;
        }


        /// <summary>
        /// Implementation of IEquivalent.IsEquivalent
        /// We manually create this rather than using the default
        /// record implementation because we want to compare to
        /// do a deep comparison, not just compare reference equality.
        /// </summary>
        /// <param name="other">Item to compare to for equivalence.</param>
        /// <returns>true if equal.</returns>
        public Boolean IsEquivalent(Object other) => this.CompareTo(other) == 0;

        /// <summary>
        /// Compare this with another item of same type.
        /// </summary>
        /// <param name="otherObject">Item to compare to for equality.</param>
        /// <returns> -1, 0, or 1.</returns>
        public Int32 CompareTo(Object otherObject)
        {
            FieldDefinitionDTO other = otherObject as FieldDefinitionDTO;
            if (other == null)
                return -1;

            Int32 cmp = FieldCompare.ComparePublicIds(this.DataTypePublicId, other.DataTypePublicId);
            if (cmp != 0)
                return cmp;
            cmp = FieldCompare.ComparePublicIds(this.PurposePublicId, other.PurposePublicId);
            if (cmp != 0)
                return cmp;
            cmp = FieldCompare.ComparePublicIds(this.MeaningPublicId, other.MeaningPublicId);
            if (cmp != 0)
                return cmp;
            return 0;
        }

        /// <summary>
        /// Make a FieldDefinitionDTP instance from a IFieldDefinition instance.
        /// </summary>
        /// <param name="fieldDefinition"></param>
        /// <returns></returns>
        public static FieldDefinitionDTO Make(IFieldDefinition fieldDefinition) =>
            new FieldDefinitionDTO(fieldDefinition.DataType.PublicId,
                    fieldDefinition.Purpose.PublicId,
                    fieldDefinition.Meaning.PublicId);

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        /// <returns>new DTO item.</returns>
        public static FieldDefinitionDTO Make(TinkarInput input) =>
            new FieldDefinitionDTO(input.GetPublicId(), input.GetPublicId(), input.GetPublicId());

        /// <summary>
        /// Static method to Create DTO item from input json stream.
        /// </summary>
        /// <param name="jsonObject">input data stream.</param>
        /// <returns>new DTO item.</returns>
        public static FieldDefinitionDTO Make(JObject jsonObject) =>
            new FieldDefinitionDTO(jsonObject.AsPublicId(ComponentFieldForJson.DATATYPE_PUBLIC_ID),
                jsonObject.AsPublicId(ComponentFieldForJson.PURPOSE_PUBLIC_ID),
                jsonObject.AsPublicId(ComponentFieldForJson.MEANING_PUBLIC_ID));

        public void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(JSONCLASSNAME);
            output.Put(ComponentFieldForJson.DATATYPE_PUBLIC_ID, this.DataTypePublicId);
            output.Put(ComponentFieldForJson.PURPOSE_PUBLIC_ID, this.PurposePublicId);
            output.Put(ComponentFieldForJson.MEANING_PUBLIC_ID, this.MeaningPublicId);
            output.WriteEndObject();
        }

        public virtual void Marshal(TinkarOutput output)
        {
            output.PutPublicId(this.DataTypePublicId);
            output.PutPublicId(this.PurposePublicId);
            output.PutPublicId(this.MeaningPublicId);
        }
    }
}
