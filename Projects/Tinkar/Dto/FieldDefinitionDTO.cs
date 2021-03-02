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
    public record FieldDefinitionDTO :
        ComponentDTO<FieldDefinitionDTO>,
        IFieldDefinition,
        IDTO,
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
        public PublicId DataTypePublicId { get; init; }

        /// <summary>
        /// Gets Purpose UUIDs.
        /// </summary>
        public PublicId PurposePublicId { get; init; }

        /// <summary>
        /// Gets Meaning public id.
        /// </summary>
        public PublicId MeaningPublicId { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldDefinitionDTO"/> class.
        /// </summary>
        /// <param name="dataTypePublicId">dataTypePublicId.</param>
        /// <param name="purposePublicId">purposePublicId.</param>
        /// <param name="meaningPublicId">meaningPublicId.</param>
        public FieldDefinitionDTO(
            PublicId dataTypePublicId,
            PublicId purposePublicId,
            PublicId meaningPublicId)
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
        protected FieldDefinitionDTO(TinkarInput input)
        {
            input.CheckMarshalVersion(LocalMarshalVersion);
            this.DataTypePublicId = input.ReadPublicId();
            this.PurposePublicId = input.ReadPublicId();
            this.MeaningPublicId = input.ReadPublicId();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldDefinitionDTO"/> class
        /// from json stream.
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        public FieldDefinitionDTO(JObject jObj)
        {
            this.DataTypePublicId = jObj.ReadPublicId(ComponentFieldForJson.DATATYPE_PUBLIC_ID);
            this.PurposePublicId = jObj.ReadPublicId(ComponentFieldForJson.PURPOSE_PUBLIC_ID);
            this.MeaningPublicId = jObj.ReadPublicId(ComponentFieldForJson.MEANING_PUBLIC_ID);
        }

        /// <summary>
        /// Compare this with another item of same type.
        /// </summary>
        /// <param name="other">Item to compare to for equality.</param>
        /// <returns> -1, 0, or 1.</returns>
        public override Int32 CompareTo(FieldDefinitionDTO other)
        {
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
            output.CheckMarshalVersion(LocalMarshalVersion);
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
