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
    public record StampDTO : BaseDTO<StampDTO>,
        IChangeSetThing,
        IJsonMarshalable,
        IMarshalable,
        IStamp
    {
        private const int MarshalVersion = 1;

        /// <summary>
        /// Status guids
        /// </summary>
        public IEnumerable<Guid> StatusUuids { get; init; }

        /// <summary>
        /// Author guids
        /// </summary>
        public IEnumerable<Guid> AuthorUuids { get; init; }

        /// <summary>
        /// Module guids
        /// </summary>
        public IEnumerable<Guid> ModuleUuids { get; init; }

        /// <summary>
        /// Path guids
        /// </summary>
        public IEnumerable<Guid> PathUuids { get; init; }

        /// <summary>
        /// Implementation of IStamp.Time
        /// </summary>
        public DateTime Time { get; init; }

        /// <summary>
        /// Implementation of IStamp.Status
        /// </summary>
        public IConcept Status => new ConceptDTO(this.StatusUuids);

        /// <summary>
        /// Implementation of IStamp.Author
        /// </summary>
        public IConcept Author => new ConceptDTO(this.AuthorUuids);

        /// <summary>
        /// Implementation of IStamp.Module
        /// </summary>
        public IConcept Module => new ConceptDTO(this.ModuleUuids);

        /// <summary>
        /// Implementation of IStamp.Path
        /// </summary>
        public IConcept Path => new ConceptDTO(this.PathUuids);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="statusUuids">StatusUuids</param>
        /// <param name="time">Time</param>
        /// <param name="authorUuids">AuthorUuids</param>
        /// <param name="moduleUuids">ModuleUuids</param>
        /// <param name="pathUuids">PathUuids</param>
        public StampDTO(IEnumerable<Guid> statusUuids,
                        DateTime time,
                        IEnumerable<Guid> authorUuids,
                        IEnumerable<Guid> moduleUuids,
                        IEnumerable<Guid> pathUuids)
        {
            this.StatusUuids = statusUuids;
            this.Time = time;
            this.AuthorUuids = authorUuids;
            this.ModuleUuids = moduleUuids;
            this.PathUuids = pathUuids;
        }

        /// <summary>
        /// Compare this with another item of same type.
        /// </summary>
        /// <param name="other">Item to compare to for equality</param>
        /// <returns> -1, 0, or 1</returns>
        public override Int32 CompareTo(StampDTO other)
        {
            Int32 cmp = this.CompareGuids(this.StatusUuids, other.StatusUuids);
            if (cmp != 0)
                return cmp;
            cmp = this.Time.CompareTo(other.Time);
            if (cmp != 0)
                return cmp;
            cmp = this.CompareGuids(this.AuthorUuids, other.AuthorUuids);
            if (cmp != 0)
                return cmp;
            cmp = this.CompareGuids(this.ModuleUuids, other.ModuleUuids);
            if (cmp != 0)
                return cmp;
            cmp = this.CompareGuids(this.PathUuids, other.PathUuids);
            if (cmp != 0)
                return cmp;
            return 0;
        }


        /// <summary>
        /// Override of default hashcode. Must provide if Equals overridden.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() =>
            this.StatusUuids.GetHashCode() ^
            this.Time.GetHashCode() ^
            this.AuthorUuids.GetHashCode() ^
            this.ModuleUuids.GetHashCode() ^
            this.PathUuids.GetHashCode()
            ;

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
        //public static StampDTO Make(JSONObject jsonObject)
        //{
        //	return new StampDTO(jsonObject.asImmutableUuidList(STATUS_UUIDS),
        //			jsonObject.asInstant(TIME),
        //			jsonObject.asImmutableUuidList(AUTHOR_UUIDS),
        //			jsonObject.asImmutableUuidList(MODULE_UUIDS),
        //			jsonObject.asImmutableUuidList(PATH_UUIDS));
        //}

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static StampDTO Make(TinkarInput input)
        {
            //$NotTested
            CheckMarshalVersion(input, MarshalVersion);
            return new StampDTO(
                input.ReadImmutableUuidList(),
                input.ReadInstant(),
                input.ReadImmutableUuidList(),
                input.ReadImmutableUuidList(),
                input.ReadImmutableUuidList()
                );
        }

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream</param>
        public void Marshal(TinkarOutput output)
        {
            //$NotTested
            WriteMarshalVersion(output, MarshalVersion);
            output.WriteUuidList(this.StatusUuids);
            output.WriteInstant(this.Time);
            output.WriteUuidList(this.AuthorUuids);
            output.WriteUuidList(this.ModuleUuids);
            output.WriteUuidList(this.PathUuids);
        }
    }
}