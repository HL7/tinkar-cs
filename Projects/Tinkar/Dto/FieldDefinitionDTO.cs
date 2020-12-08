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

namespace Tinkar
{
    public record FieldDefinitionDTO :
        BaseDTO<FieldDefinitionDTO>,
        IFieldDefinition,
        IChangeSetThing,
        IJsonMarshalable,
        IMarshalable
    {
        private const int MarshalVersion = 1;

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

        //@Override
        //public void jsonMarshal(Writer writer) {
        //    final JSONObject json = new JSONObject();
        //    json.put(ComponentFieldForJson.CLASS, this.getClass().getCanonicalName());
        //    json.put(ComponentFieldForJson.DATATYPE_UUIDS, dataTypeUuids);
        //    json.put(ComponentFieldForJson.PURPOSE_UUIDS, purposeUuids);
        //    json.put(ComponentFieldForJson.USE_UUIDS, useUuids);
        //    json.writeJSONString(writer);
        //}

        //@JsonChronologyUnmarshaler
        //public static FieldDefinitionDTO Make(JSONObject jsonObject) {
        //    return new FieldDefinitionDTO(jsonObject.asImmutableUuidList(ComponentFieldForJson.DATATYPE_UUIDS),
        //            jsonObject.asImmutableUuidList(ComponentFieldForJson.PURPOSE_UUIDS),
        //            jsonObject.asImmutableUuidList(ComponentFieldForJson.USE_UUIDS));
        //}

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static FieldDefinitionDTO Make(TinkarInput input)
        {
            CheckMarshalVersion(input, MarshalVersion);
            return new FieldDefinitionDTO(input.ReadUuidArray(),
                input.ReadUuidArray(),
                input.ReadUuidArray());
        }

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream</param>
        public void Marshal(TinkarOutput output)
        {
            WriteMarshalVersion(output, MarshalVersion);
            output.WriteUuidList(this.DataTypeUuids);
            output.WriteUuidList(this.PurposeUuids);
            output.WriteUuidList(this.UseUuids);
        }
    }
}
