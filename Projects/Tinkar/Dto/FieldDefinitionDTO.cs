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
    public record FieldDefinitionDTO(IEnumerable<Guid> DataTypeUuids,
                                    IEnumerable<Guid> PurposeUuids,
                                    IEnumerable<Guid> UseUuids) : BaseDTO,
        IFieldDefinition,
        IChangeSetThing,
        IJsonMarshalable,
        IMarshalable
    {
        private const int MarshalVersion = 1;

        public IConcept DataType => new ConceptDTO(this.DataTypeUuids);
        public IConcept Purpose => new ConceptDTO(this.PurposeUuids);
        public IConcept Use => new ConceptDTO(this.UseUuids);

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
        /// $NotTested
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static FieldDefinitionDTO Make(TinkarInput input)
        {
            try
            {
                CheckMarshalVersion(input, MarshalVersion);
                return new FieldDefinitionDTO(input.ReadImmutableUuidList(),
                    input.ReadImmutableUuidList(),
                    input.ReadImmutableUuidList());
            }
            catch (Exception ex)
            {
                throw new UncheckedIOException(ex);
            }
        }

        /// <summary>
        /// Marshal DTO item to output stream.
        /// $NotTested
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
