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
using Newtonsoft.Json.Linq;

namespace Tinkar
{
    /**
	 *
	 * @author kec
	 */
    public record ConceptChronologyDTO : BaseDTO<ConceptChronologyDTO>,
        IChangeSetThing,
        IJsonMarshalable,
        IMarshalable,
        IConceptChronology<IConcept>
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
        private const String JsonClassName = "ConceptChronologyDTO";

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
        /// Construct item from TinkarInput Stream
        /// </summary>
        public ConceptChronologyDTO(TinkarInput input)
        {
            input.CheckMarshalVersion(MarshalVersion);
            this.ComponentUuids = input.ReadUuids();
            this.ChronologySetUuids = input.ReadUuids();
            this.ConceptVersions = input.ReadConceptVersionList(this.ComponentUuids);
        }

        /// <summary>
        /// Create item from json stream
        /// </summary>
        public ConceptChronologyDTO(JObject jObj)
        {
            this.ComponentUuids = jObj.ReadUuids(ComponentFieldForJson.COMPONENT_UUIDS);
            this.ChronologySetUuids = jObj.ReadUuids(ComponentFieldForJson.CHRONOLOGY_SET_UUIDS);
            this.ConceptVersions = jObj.ReadConceptVersionList(this.ComponentUuids);
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="other">Item to compare to</param>
        /// <returns>-1, 0, or 1</returns>
        public override Int32 CompareTo(ConceptChronologyDTO other)
        {
            Int32 cmp = FieldCompare.CompareGuids(this.ComponentUuids, other.ComponentUuids);
            if (cmp != 0)
                return cmp;
            cmp = FieldCompare.CompareGuids(this.ChronologySetUuids, other.ChronologySetUuids);
            if (cmp != 0)
                return cmp;
            cmp = FieldCompare.CompareSequence<ConceptVersionDTO>(this.ConceptVersions, other.ConceptVersions);
            if (cmp != 0)
                return cmp;
            return 0;
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static ConceptChronologyDTO Make(TinkarInput input) =>
            new ConceptChronologyDTO(input);

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream</param>
        public void Marshal(TinkarOutput output)
        {
            output.WriteMarshalVersion(MarshalVersion);
            output.WriteUuids(this.ComponentUuids);
            output.WriteUuids(this.ChronologySetUuids);
            // Note that the componentIds are not written redundantly
            // in writeConceptVersionList...
            output.WriteMarshalableList(this.ConceptVersions);
        }

        /// <summary>
        /// Static method to Create DTO item from json stream.
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static ConceptChronologyDTO Make(JObject jObj) =>
            new ConceptChronologyDTO(jObj);

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream</param>
        public void Marshal(TinkarJsonOutput output)
        {
            // Note that the componentIds are not written redundantly
            // in writeConceptVersionList...

            output.WriteStartObject();
            output.WriteClass(JsonClassName);
            output.WriteUuids(ComponentFieldForJson.COMPONENT_UUIDS, 
                this.ComponentUuids);
            output.WriteUuids(ComponentFieldForJson.CHRONOLOGY_SET_UUIDS,
                this.ChronologySetUuids);
            output.WriteMarshalableList(ComponentFieldForJson.CONCEPT_VERSIONS,
                this.ConceptVersions);
            output.WriteEndObject();
        }

    }
}
