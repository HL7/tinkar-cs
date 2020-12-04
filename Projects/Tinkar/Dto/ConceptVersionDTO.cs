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
    public record ConceptVersionDTO(IEnumerable<Guid> ComponentUuids, 
            StampDTO StampDTO) : BaseDTO, 
        IConceptVersion,
        IChangeSetThing,
        IJsonMarshalable,
        IMarshalable
    {
        private const int MarshalVersion = 1;

        public IStamp Stamp => this.StampDTO;

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
        //public static ConceptVersionDTO make(JSONObject jsonObject, IEnumerable<Guid> componentUuids) {
        //    return new ConceptVersionDTO(
        //            componentUuids,
        //            StampDTO.make((JSONObject) jsonObject.get(ComponentFieldForJson.STAMP)));
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
            try
            {
                CheckMarshallVersion(input, MarshalVersion);
                return new ConceptVersionDTO(componentUuids, StampDTO.Make(input));
            }
            catch (Exception ex)
            {
                throw new UncheckedIOException(ex);
            }
        }

        ///**
        // * Version marshaler for ConceptVersionDTO
        // * @param out
        // */
        //@Override
        //@Marshaler
        //public void marshal(TinkarOutput out) {
        //    try {
        //        out.writeInt(marshalVersion);
        //        // note that componentUuids are not written redundantly here,
        //        // they are written with the ConceptChronologyDTO...
        //        stampDTO.marshal(out);
        //    } catch (Exception ex) {
        //        throw new UncheckedIOException(ex);
        //    }
        //}
    }
}
