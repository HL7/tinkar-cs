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
    public record ConceptVersionDTO : BaseDTO,
        IEquatable<ConceptVersionDTO>,
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
        /// Implementation of Equals.
        /// We manually create this rather than using the default
        /// record implementation because we want to compare to
        /// do a deep comparison, not just compare reference equality.
        /// </summary>
        /// <param name="other">Item to compare to for equality</param>
        /// <returns>true if equal</returns>
        public virtual bool Equals(ConceptVersionDTO other) =>
            this.CompareSequence(this.ComponentUuids, other.ComponentUuids) &&
            this.CompareItem<StampDTO>(this.StampDTO, other.StampDTO);

        /// <summary>
        /// Override of default hashcode. Must provide if Equals overridden.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => this.ComponentUuids.GetHashCode() ^
            this.StampDTO.GetHashCode();

        ///**
        // * Marshaler for ConceptVersionDTO using JSON
        // * @param writer
        // */
        //@JsonMarshaler
        //@Override
        //public void jsonMarshal(Writer writer) {
        //    final JSONObject json = new JSONObject();
        //    json.put(ComponentFieldForJson.STAMP, stampDTO);
        //    json.writeJSONString(writer);
        //}

        ///**
        // * Version unmarshaler for ConceptVersionDTO using JSON
        // * @param jsonObject
        // * @param componentUuids
        // * @return
        // */
        //@JsonVersionUnmarshaler
        //public static ConceptVersionDTO Make(JSONObject jsonObject, IEnumerable<Guid> componentUuids) {
        //    return new ConceptVersionDTO(
        //            componentUuids,
        //            StampDTO.Make((JSONObject) jsonObject.get(ComponentFieldForJson.STAMP)));
        //}

        ///**
        // * Version unmarshaler for ConceptVersionDTO.
        // * @param input
        // * @param componentUuids
        // * @return new instance of ConceptVersionDTO created from the input.
        // */
        //@VersionUnmarshaler
        public static ConceptVersionDTO Make(TinkarInput input, IEnumerable<Guid> componentUuids)
        {
            CheckMarshalVersion(input, MarshalVersion);
            return new ConceptVersionDTO(componentUuids, StampDTO.Make(input));
        }

        ///**
        // * Version marshaler for ConceptVersionDTO
        // * @param out
        // */
        public void Marshal(TinkarOutput output)
        {
            WriteMarshalVersion(output, MarshalVersion);
            // note that componentUuids are not written redundantly here,
            // they are written with the ConceptChronologyDTO...
            this.StampDTO.Marshal(output);
        }
    }
}
