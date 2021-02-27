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
    /// <summary>
    /// Tinkar ConceptChronology record.
    /// </summary>
    public record ConceptChronologyDTO : ComponentDTO<ConceptChronologyDTO>,
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
        private const int LocalMarshalVersion = 3;

        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public const String JsonClassName = "ConceptChronologyDTO";

        /// <summary>
        /// Gets ChronologySet.
        /// </summary>
        public IConcept ChronologySet => new ConceptDTO(this.ChronologySetPublicId);

        /// <summary>
        /// Gets public id.
        /// </summary>
        public IPublicId PublicId { get; init; }

        /// <summary>
        /// Gets ChronologySet PublicId.
        /// </summary>
        public IPublicId ChronologySetPublicId { get; init; }

        /// <summary>
        /// Gets ConceptVersions.
        /// </summary>
        public IEnumerable<ConceptVersionDTO> ConceptVersions { get; init; }

        /// <summary>
        /// Gets Versions.
        /// </summary>
        public IEnumerable<IConceptVersion> Versions =>
            this.ConceptVersions.Select((dto) => (IConceptVersion)dto);

        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptChronologyDTO"/> class.
        /// </summary>
        /// <param name = "publicId" > Public id(component ids).</param>
        /// <param name="chronologySetPublicId">ChronologySetPublicId.</param>
        /// <param name="conceptVersions">ConceptVersions.</param>
        public ConceptChronologyDTO(
            IPublicId publicId,
            IPublicId chronologySetPublicId,
            IEnumerable<ConceptVersionDTO> conceptVersions)
        {
            this.PublicId = publicId;
            this.ChronologySetPublicId = chronologySetPublicId;
            this.ConceptVersions = conceptVersions;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptChronologyDTO"/> class
        /// from a TinkarInput Stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        public ConceptChronologyDTO(TinkarInput input)
        {
            input.CheckMarshalVersion(LocalMarshalVersion);
            this.PublicId = input.ReadPublicId();
            this.ChronologySetPublicId = input.ReadPublicId();
            this.ConceptVersions = input.ReadConceptVersionList(this.PublicId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptChronologyDTO"/> class
        /// from json stream.
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        public ConceptChronologyDTO(JObject jObj)
        {
            this.PublicId  = jObj.ReadPublicId(ComponentFieldForJson.COMPONENT_PUBLIC_ID);
            this.ChronologySetPublicId = jObj.ReadPublicId(ComponentFieldForJson.CHRONOLOGY_PUBLIC_ID);
            this.ConceptVersions = jObj.ReadConceptVersionList(this.PublicId);
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="other">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public override Int32 CompareTo(ConceptChronologyDTO other)
        {
            Int32 cmp = base.CompareTo(other);
            if (cmp != 0)
                return cmp;
            cmp = FieldCompare.ComparePublicIds(this.ChronologySetPublicId, other.ChronologySetPublicId);
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
        /// <param name="input">input data stream.</param>
        /// <returns>new DTO item.</returns>
        public static ConceptChronologyDTO Make(TinkarInput input) =>
            new ConceptChronologyDTO(input);

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public void Marshal(TinkarOutput output)
        {
            output.CheckMarshalVersion(LocalMarshalVersion);;
            output.WritePublicId(this.PublicId);
            output.WritePublicId(this.ChronologySetPublicId);

            // Note that the componentIds are not written redundantly
            // in writeConceptVersionList...
            output.WriteMarshalableList(this.ConceptVersions);
        }

        /// <summary>
        /// Static method to Create DTO item from json stream.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        /// <returns>new DTO item.</returns>
        public static ConceptChronologyDTO Make(JObject jObj) =>
            new ConceptChronologyDTO(jObj);

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public void Marshal(TinkarJsonOutput output)
        {
            // Note that the componentIds are not written redundantly
            // in writeConceptVersionList...
            output.WriteStartObject();
            output.WriteClass(JsonClassName);
            output.WritePublicId(
                ComponentFieldForJson.COMPONENT_PUBLIC_ID,
                this.PublicId);
            output.WritePublicId(
                ComponentFieldForJson.CHRONOLOGY_PUBLIC_ID,
                this.ChronologySetPublicId);
            output.WriteMarshalableList(
                ComponentFieldForJson.CONCEPT_VERSIONS,
                this.ConceptVersions);
            output.WriteEndObject();
        }
    }
}
