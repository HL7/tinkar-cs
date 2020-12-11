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
using System.Collections.Generic;

namespace Tinkar
{
    public record ConceptVersionDTO : BaseDTO<ConceptVersionDTO>,
        IConceptVersion,
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
        public const String JsonClassName = "ConceptVersionDTO";

        /// <summary>
        /// Implementation of IIdentifiedThing.ComponentUuids.
        /// </summary>
        public IEnumerable<Guid> ComponentUuids { get; init; }

        /// <summary>
        /// Stamp DTO
        /// </summary>
        public StampDTO StampDTO { get; init; }

        /// <summary>
        /// Implementation of IStampComment.Stamp.
        /// </summary>
        public IStamp Stamp => this.StampDTO;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="componentUuids">ComponentUuids</param>
        public ConceptVersionDTO(IEnumerable<Guid> componentUuids,
            StampDTO stampDTO)
        {
            this.ComponentUuids = componentUuids;
            this.StampDTO = stampDTO;
        }

        /// <summary>
        /// Create item from binary stream
        /// </summary>
        /// <param name="input"></param>
        /// <param name="componentUuids"></param>
        public ConceptVersionDTO(TinkarInput input,
            IEnumerable<Guid> componentUuids)
        {
            input.CheckMarshalVersion(MarshalVersion);
            this.ComponentUuids = componentUuids;
            this.StampDTO = new StampDTO(input);
        }

        /// <summary>
        /// Create item from json stream
        /// </summary>
        public ConceptVersionDTO(JObject jObj,
            IEnumerable<Guid> componentUuids)
        {
            this.ComponentUuids = componentUuids;
            jObj.GetClass(JsonClassName);
            JObject jObjStamp = jObj.ReadToken<JObject>(ComponentFieldForJson.STAMP);
            this.StampDTO = new StampDTO(jObjStamp);
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="other">Item to compare to</param>
        /// <returns>-1, 0, or 1</returns>
        public override Int32 CompareTo(ConceptVersionDTO other)
        {
            Int32 cmp = FieldCompare.CompareGuids(this.ComponentUuids, other.ComponentUuids);
            if (cmp != 0)
                return cmp;
            cmp = FieldCompare.CompareItem(this.StampDTO, other.StampDTO);
            if (cmp != 0)
                return cmp;
            return 0;
        }


        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static ConceptVersionDTO Make(TinkarInput input,
            IEnumerable<Guid> componentUuids) =>
            new ConceptVersionDTO(input, componentUuids);

        /// <summary>
        /// Marshal all fields to binary output stream.
        /// </summary>
        /// <param name="output">Json output stream</param>
        public void Marshal(TinkarOutput output)
        {
            output.WriteMarshalVersion(MarshalVersion);
            // note that componentUuids are not written redundantly here,
            // they are written with the ConceptChronologyDTO...
            this.StampDTO.Marshal(output);
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static ConceptVersionDTO Make(JObject jObj,
            IEnumerable<Guid> componentUuids) =>
            new ConceptVersionDTO(jObj, componentUuids);

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream</param>
        public void Marshal(TinkarJsonOutput output)
        {
            // note that componentUuids are not written redundantly here,
            // they are written with the ConceptChronologyDTO...

            output.WriteStartObject();
            output.WriteClass(JsonClassName);
            output.WritePropertyName(ComponentFieldForJson.STAMP);
            this.StampDTO.Marshal(output);
            output.WriteEndObject();
        }
    }
}
