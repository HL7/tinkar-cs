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
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Tinkar
{
    public record FieldDefinitionDTO :
        BaseDTO<FieldDefinitionDTO>,
        IFieldDefinition,
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
        public const String JsonClassName = "FieldDefinitionDTO";


        /// <summary>
        /// Implementation of IFieldDefinition.DataType
        /// </summary>
        public IConcept DataType => new ConceptDTO(this.DataTypeUuids);

        /// <summary>
        /// Implementation of IFieldDefinition.Purpose
        /// </summary>
        public IConcept Purpose => new ConceptDTO(this.PurposeUuids);

        /// <summary>
        /// Implementation of IFieldDefinition.Use
        /// </summary>
        public IConcept Use => new ConceptDTO(this.UseUuids);

        /// <summary>
        /// DataType uuids.
        /// </summary>
        public IEnumerable<Guid> DataTypeUuids { get; init; }

        /// <summary>
        /// Purpose uuids.
        /// </summary>
        public IEnumerable<Guid> PurposeUuids { get; init; }

        /// <summary>
        /// Use uuids.
        /// </summary>
        public IEnumerable<Guid> UseUuids { get; init; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataTypeUuids">DataTypeUuids</param>
        /// <param name="purposeUuids">PurposeUuids</param>
        /// <param name="useUuids">useUuids</param>
        public FieldDefinitionDTO(IEnumerable<Guid> dataTypeUuids,
            IEnumerable<Guid> purposeUuids,
            IEnumerable<Guid> useUuids)
        {
            this.DataTypeUuids = dataTypeUuids;
            this.PurposeUuids = purposeUuids;
            this.UseUuids = useUuids;
        }

        /// <summary>
        /// Create item from binary stream
        /// </summary>
        /// <param name="input">input data stream</param>
        public FieldDefinitionDTO(TinkarInput input)
        {
            input.CheckMarshalVersion(MarshalVersion);
            this.DataTypeUuids = input.ReadUuids();
            this.PurposeUuids = input.ReadUuids();
            this.UseUuids = input.ReadUuids();
        }

        /// <summary>
        /// Create item from json stream
        /// </summary>
        public FieldDefinitionDTO(JObject jObj)
        {
            this.DataTypeUuids = jObj.ReadUuids(ComponentFieldForJson.DATATYPE_UUIDS);
            this.PurposeUuids = jObj.ReadUuids(ComponentFieldForJson.PURPOSE_UUIDS);
            this.UseUuids = jObj.ReadUuids(ComponentFieldForJson.USE_UUIDS);
        }

        /// <summary>
        /// Compare this with another item of same type.
        /// </summary>
        /// <param name="other">Item to compare to for equality</param>
        /// <returns> -1, 0, or 1</returns>
        public override Int32 CompareTo(FieldDefinitionDTO other)
        {
            Int32 cmp = FieldCompare.CompareGuids(this.DataTypeUuids, other.DataTypeUuids);
            if (cmp != 0)
                return cmp;
            cmp = FieldCompare.CompareGuids(this.PurposeUuids, other.PurposeUuids);
            if (cmp != 0)
                return cmp;
            cmp = FieldCompare.CompareGuids(this.UseUuids, other.UseUuids);
            if (cmp != 0)
                return cmp;
            return 0;
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static FieldDefinitionDTO Make(TinkarInput input) =>
            new FieldDefinitionDTO(input);

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream</param>
        public void Marshal(TinkarOutput output)
        {
            output.WriteMarshalVersion(MarshalVersion);
            output.WriteUuids(this.DataTypeUuids);
            output.WriteUuids(this.PurposeUuids);
            output.WriteUuids(this.UseUuids);
        }

        /// <summary>
        /// Static method to Create DTO item from input json stream.
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static FieldDefinitionDTO Make(JObject input) =>
            new FieldDefinitionDTO(input);

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream</param>
        public void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(JsonClassName);
            output.WriteUuids(ComponentFieldForJson.DATATYPE_UUIDS, this.DataTypeUuids);
            output.WriteUuids(ComponentFieldForJson.PURPOSE_UUIDS, this.PurposeUuids);
            output.WriteUuids(ComponentFieldForJson.USE_UUIDS, this.UseUuids);
            output.WriteEndObject();
        }

    }
}
