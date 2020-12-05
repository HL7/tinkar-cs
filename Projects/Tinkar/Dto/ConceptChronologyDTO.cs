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
    public record ConceptChronologyDTO : BaseDTO,
        IEquatable<ConceptChronologyDTO>,
        IChangeSetThing,
        IJsonMarshalable,
        IMarshalable,
        IConceptChronology<IConcept>
    {
        private const int MarshalVersion = 1;

        /// <summary>
        /// Implementation of IChronology.ChronologySet.
        /// </summary>
        public IConcept ChronologySet => new ConceptDTO(this.ChronologySetUuids);

        /// <summary>
        /// Implementation of IIdentifiedThing.ComponentUuids.
        /// </summary>
        public IEnumerable<Guid> ComponentUuids { get; init; }

        /// <summary>
        /// ???.
        /// </summary>
        public IEnumerable<Guid> ChronologySetUuids { get; init; }

        /// <summary>
        /// DTO instance for Versions.
        /// </summary>
        public IEnumerable<ConceptVersionDTO> ConceptVersions { get; init; }

        /// <summary>
        /// IConceptVersion versions
        /// </summary>
        public IEnumerable<IConceptVersion> Versions =>
            this.ConceptVersions.Select((dto) => (IConceptVersion)dto);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="componentUuids">ComponentUuids</param>
        /// <param name="chronologySetUuids">ChronologySetUuids</param>
        /// <param name="conceptVersions">ConceptVersions</param>
        public ConceptChronologyDTO(IEnumerable<Guid> componentUuids,
            IEnumerable<Guid> chronologySetUuids,
            IEnumerable<ConceptVersionDTO> conceptVersions)
        {
            this.ComponentUuids = componentUuids;
            this.ChronologySetUuids = chronologySetUuids;
            this.ConceptVersions = conceptVersions;
        }

        /// <summary>
        /// Implementation of Equals.
        /// We manually create this rather than using the default
        /// record implementation because we want to compare to
        /// do a deep comparison, not just compare reference equality.
        /// </summary>
        /// <param name="other">Item to compare to for equality</param>
        /// <returns>true if equal</returns>
        public virtual bool Equals(ConceptChronologyDTO other) =>
                    this.CompareSequence(this.ComponentUuids, other.ComponentUuids) &&
                    this.CompareSequence(this.ChronologySetUuids, other.ChronologySetUuids) &&
                    this.CompareSequence<ConceptVersionDTO>(this.ConceptVersions, other.ConceptVersions)
                    ;

        /// <summary>
        /// Override of default hashcode. Must provide if Equals overridden.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => this.ComponentUuids.GetHashCode() ^
                                             this.ChronologySetUuids.GetHashCode() ^
                                             this.ConceptVersions.GetHashCode();

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
