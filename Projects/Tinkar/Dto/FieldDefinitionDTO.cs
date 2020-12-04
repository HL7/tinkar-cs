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

        public IConcept DataType => new ConceptDTO(DataTypeUuids);
        public IConcept Purpose => new ConceptDTO(PurposeUuids);
        public IConcept Use => new ConceptDTO(UseUuids);

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
        //public static FieldDefinitionDTO make(JSONObject jsonObject) {
        //    return new FieldDefinitionDTO(jsonObject.asImmutableUuidList(ComponentFieldForJson.DATATYPE_UUIDS),
        //            jsonObject.asImmutableUuidList(ComponentFieldForJson.PURPOSE_UUIDS),
        //            jsonObject.asImmutableUuidList(ComponentFieldForJson.USE_UUIDS));
        //}

        //@Unmarshaler
        public static FieldDefinitionDTO make(TinkarInput input)
        {
            try
            {
                CheckMarshallVersion(input, MarshalVersion);
                return new FieldDefinitionDTO(input.ReadImmutableUuidList(),
                    input.ReadImmutableUuidList(),
                    input.ReadImmutableUuidList());
            }
            catch (Exception ex)
            {
                throw new UncheckedIOException(ex);
            }
        }

        //@Override
        //@Marshaler
        //public void marshal(TinkarOutput out) {
        //    try {
        //        out.writeInt(marshalVersion);
        //        out.writeUuidList(dataTypeUuids);
        //        out.writeUuidList(purposeUuids);
        //        out.writeUuidList(useUuids);
        //    } catch (Exception ex) {
        //        throw new UncheckedIOException(ex);
        //    }
        //}
    }
}
