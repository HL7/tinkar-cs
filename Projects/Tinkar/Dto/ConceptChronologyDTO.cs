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
    public record ConceptChronologyDTO : ComponentDTO,
        IDTO,
        IJsonMarshalable,
        IMarshalable,
        IConceptChronology<ConceptVersionDTO, IConcept>
    {
        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public const String JSONCLASSNAME = "ConceptChronologyDTO";

        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public override String JsonClassName => JSONCLASSNAME;

        /// <summary>
        /// Gets ChronologySet.
        /// </summary>
        public IConcept ChronologySet => new ConceptDTO(this.chronologySetPublicId);

        /// <summary>
        /// Gets ChronologySet PublicId.
        /// </summary>
        IPublicId chronologySetPublicId { get; init; }

        /// <summary>
        /// Gets ConceptVersions.
        /// </summary>
        IEnumerable<ConceptVersionDTO> conceptVersions { get; init; }

        /// <summary>
        /// Gets Versions.
        /// </summary>
        public IEnumerable<ConceptVersionDTO> Versions => conceptVersions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptChronologyDTO"/> class.
        /// </summary>
        /// <param name = "publicId" > Public id(component ids).</param>
        /// <param name="chronologySetPublicId">ChronologySetPublicId.</param>
        /// <param name="conceptVersions">ConceptVersions.</param>
        public ConceptChronologyDTO(
            IPublicId publicId,
            IPublicId chronologySetPublicId,
            IEnumerable<ConceptVersionDTO> conceptVersions) : base(publicId)
        {
            this.chronologySetPublicId = chronologySetPublicId;
            this.conceptVersions = conceptVersions;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptChronologyDTO"/> class
        /// from a TinkarInput Stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        protected ConceptChronologyDTO(TinkarInput input) : base(input)
        {
            this.chronologySetPublicId = input.ReadPublicId();
            this.conceptVersions = input.ReadConceptVersionList(this.PublicId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptChronologyDTO"/> class
        /// from json stream.
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        public ConceptChronologyDTO(JObject jObj) : base(jObj)
        {
            this.chronologySetPublicId = jObj.ReadPublicId(ComponentFieldForJson.CHRONOLOGY_SET_PUBLIC_ID);
            this.conceptVersions = jObj.ReadConceptVersionList(this.PublicId);
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="otherObject">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public override Int32 CompareTo(Object otherObject)
        {
            ConceptChronologyDTO other = otherObject as ConceptChronologyDTO;
            if (other == null)
                return -1;

            Int32 cmp = base.CompareTo(other);
            if (cmp != 0)
                return cmp;
            cmp = FieldCompare.ComparePublicIds(this.chronologySetPublicId, other.chronologySetPublicId);
            if (cmp != 0)
                return cmp;
            cmp = FieldCompare.CompareSequence<ConceptVersionDTO>(this.conceptVersions, other.conceptVersions);
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
        public override void MarshalFields(TinkarOutput output)
        {
            base.MarshalFields(output);
            output.WritePublicId(this.chronologySetPublicId);

            // Note that the componentIds are not written redundantly
            // in writeConceptVersionList...
            output.WriteMarshalableList(this.conceptVersions);
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
        public override void MarshalFields(TinkarJsonOutput output)
        {
            // Note that the componentIds are not written redundantly
            // in writeConceptVersionList...
            base.MarshalFields(output);
            output.WritePublicId(
                ComponentFieldForJson.CHRONOLOGY_SET_PUBLIC_ID,
                this.chronologySetPublicId);
            output.WriteMarshalableList(
                ComponentFieldForJson.CONCEPT_VERSIONS,
                this.conceptVersions);
        }
    }
}
