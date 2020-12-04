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
using System.Collections.Generic;

namespace Tinkar
{
    public record StampDTO : BaseDTO, ,
        IChangeSetThing, 
        IJsonMarshalable, 
        IMarshalable, 
        IStamp
    {
        protected override int MarshalVersion => 1;
        public IEnumerable<Guid> StatusUuids { get; init; }
        public DateTime Time { get; init; }
        public IEnumerable<Guid> AuthorUuids { get; init; }
        public IEnumerable<Guid> ModuleUuids { get; init; }
        public IEnumerable<Guid> PathUuids { get; init; }

        public IConcept Status => new ConceptDTO { ComponentUuids = StatusUuids };
        public IConcept Author => new ConceptDTO { ComponentUuids = AuthorUuids };
        public IConcept Module() => new ConceptDTO { ComponentUuids = ModuleUuids };
        public IConcept Path() => new ConceptDTO { ComponentUuids = PathUuids };

        //@Override
        //public void jsonMarshal(Writer writer)
        //{
        //	final JSONObject json = new JSONObject();
        //	json.put(STATUS_UUIDS, statusUuids);
        //	json.put(ComponentFieldForJson.TIME, time);
        //	json.put(ComponentFieldForJson.AUTHOR_UUIDS, authorUuids);
        //	json.put(ComponentFieldForJson.MODULE_UUIDS, moduleUuids);
        //	json.put(ComponentFieldForJson.PATH_UUIDS, pathUuids);
        //	json.writeJSONString(writer);
        //}

        //@JsonChronologyUnmarshaler
        //public static StampDTO make(JSONObject jsonObject)
        //{
        //	return new StampDTO(jsonObject.asImmutableUuidList(STATUS_UUIDS),
        //			jsonObject.asInstant(TIME),
        //			jsonObject.asImmutableUuidList(AUTHOR_UUIDS),
        //			jsonObject.asImmutableUuidList(MODULE_UUIDS),
        //			jsonObject.asImmutableUuidList(PATH_UUIDS));
        //}

        //@Unmarshaler
        public static StampDTO Make(TinkarInput input)
        {
            try
            {
                int objectMarshalVersion = input.ReadInt();
                if (objectMarshalVersion != marshalVersion)
                    throw new UnsupportedOperationException("Unsupported version: " + objectMarshalVersion);

                return new StampDTO
                {
                        input.ReadImmutableUuidList(),
                        input.ReadInstant(),
                        input.ReadImmutableUuidList(),
                        input.ReadImmutableUuidList(),
                        input.ReadImmutableUuidList()
                }
            }
            catch (Exception ex)
            {
                throw new UncheckedIOException(ex);
            }
        }

        //@Override
        //@Marshaler

        //public void marshal(TinkarOutput out)
        //{
        //	try
        //	{
        //           out.writeInt(marshalVersion);
        //           out.writeUuidList(statusUuids);
        //           out.writeInstant(time);
        //           out.writeUuidList(authorUuids);
        //           out.writeUuidList(moduleUuids);
        //           out.writeUuidList(pathUuids);
        //	}
        //	catch (Exception ex)
        //	{
        //		throw new UncheckedIOException(ex);
        //	}
        //}
    }
}