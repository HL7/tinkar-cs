/*
 * Copyright 2020 HL7.
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
using System.Linq;

namespace Tinkar
{
    /**
	 *
	 * @author kec
	 */
    public record ConceptChronologyDTO(
            IEnumerable<Guid> ComponentUuids,
            IEnumerable<Guid> ChronologySetUuids,
            IEnumerable<ConceptVersionDTO> ConceptVersions
        ) : BaseDTO,
        IChangeSetThing,
        IJsonMarshalable,
        IMarshalable,
        IConceptChronology<IConcept>
    {
        private const int MarshalVersion = 1;
        public IConcept ChronologySet => new ConceptDTO(this.ChronologySetUuids);

        public IEnumerable<IConceptVersion> Versions =>
            this.ConceptVersions.Select((dto) => (IConceptVersion)dto);

        //@Override
        //public void jsonMarshal(Writer writer) {
        //    final JSONObject json = new JSONObject();
        //    json.put(ComponentFieldForJson.CLASS, this.getClass().getCanonicalName());
        //    json.put(ComponentFieldForJson.COMPONENT_UUIDS, componentUuids);
        //    json.put(ComponentFieldForJson.CHRONOLOGY_SET_UUIDS, chronologySetUuids);
        //    json.put(ComponentFieldForJson.CONCEPT_VERSIONS, conceptVersions);
        //    json.writeJSONString(writer);
        //}

        //@JsonChronologyUnmarshaler
        //public static ConceptChronologyDTO Make(JSONObject jsonObject) {
        //    IEnumerable<Guid> componentUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.COMPONENT_UUIDS);
        //    return new ConceptChronologyDTO(componentUuids,
        //                    jsonObject.asImmutableUuidList(ComponentFieldForJson.CHRONOLOGY_SET_UUIDS),
        //                    jsonObject.asConceptVersionList(ComponentFieldForJson.CONCEPT_VERSIONS, componentUuids));
        //}

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// $NotTested
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static ConceptChronologyDTO Make(TinkarInput input)
        {
            CheckMarshalVersion(input, MarshalVersion);
            IEnumerable<Guid> componentUuids = input.ReadImmutableUuidList();
            return new ConceptChronologyDTO(componentUuids,
                input.ReadImmutableUuidList(),
                input.ReadConceptVersionList(componentUuids)
            );
        }

        /// <summary>
        /// Marshal DTO item to output stream.
        /// $NotTested
        /// </summary>
        /// <param name="output">output data stream</param>
        public void Marshal(TinkarOutput output)
        {
            WriteMarshalVersion(output, MarshalVersion);
            output.WriteUuidList(this.ComponentUuids);
            output.WriteUuidList(this.ChronologySetUuids);
            // Note that the componentIds are not written redundantly
            // in writeConceptVersionList...
            output.WriteMarshalableList(this.ConceptVersions);
        }
    }
}
