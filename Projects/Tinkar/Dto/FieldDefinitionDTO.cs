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
    public record FieldDefinitionDTO : MarshalableDTO,
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
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public override String JsonClassName => JSONCLASSNAME;

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
        /// Initializes a new instance of the <see cref="FieldDefinitionDTO"/> class
        /// from binary stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        protected FieldDefinitionDTO(TinkarInput input) : base(input)
        {
            this.DataTypePublicId = input.ReadPublicId();
            this.PurposePublicId = input.ReadPublicId();
            this.MeaningPublicId = input.ReadPublicId();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldDefinitionDTO"/> class
        /// from json stream.
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        public FieldDefinitionDTO(JObject jObj) : base(jObj)
        {
            this.DataTypePublicId = jObj.ReadPublicId(ComponentFieldForJson.DATATYPE_PUBLIC_ID);
            this.PurposePublicId = jObj.ReadPublicId(ComponentFieldForJson.PURPOSE_PUBLIC_ID);
            this.MeaningPublicId = jObj.ReadPublicId(ComponentFieldForJson.MEANING_PUBLIC_ID);
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
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        /// <returns>new DTO item.</returns>
        public static FieldDefinitionDTO Make(TinkarInput input) =>
            new FieldDefinitionDTO(input);

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public override void MarshalFields(TinkarOutput output)
        {
            base.MarshalFields(output);
            output.WritePublicId(this.DataTypePublicId);
            output.WritePublicId(this.PurposePublicId);
            output.WritePublicId(this.MeaningPublicId);
        }

        /// <summary>
        /// Static method to Create DTO item from input json stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        /// <returns>new DTO item.</returns>
        public static FieldDefinitionDTO Make(JObject input) =>
            new FieldDefinitionDTO(input);

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public override void MarshalFields(TinkarJsonOutput output)
        {
            base.MarshalFields(output);
            output.WritePublicId(ComponentFieldForJson.DATATYPE_PUBLIC_ID, this.DataTypePublicId);
            output.WritePublicId(ComponentFieldForJson.PURPOSE_PUBLIC_ID, this.PurposePublicId);
            output.WritePublicId(ComponentFieldForJson.MEANING_PUBLIC_ID, this.MeaningPublicId);
        }
    }
}
