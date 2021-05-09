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
using System.Collections.Immutable;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Tinkar.Dto
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
        /// Unique ID for binary marshal of this item.
        /// </summary>
        public virtual FieldDataType FieldDataType => FieldDataType.ConceptChronologyType;

        /// <summary>
        /// Gets Versions.
        /// </summary>
        ImmutableArray<ConceptVersionDTO> conceptVersions { get; init; }

        /// <summary>
        /// Gets Versions.
        /// </summary>
        public ImmutableArray<ConceptVersionDTO> Versions => this.conceptVersions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptChronologyDTO"/> class.
        /// </summary>
        /// <param name = "componentPublicId" > Public id(component ids).</param>
        /// <param name="conceptVersions">ConceptVersions.</param>
        public ConceptChronologyDTO(
            IPublicId componentPublicId,
            ImmutableArray<ConceptVersionDTO> conceptVersions) : base(componentPublicId)
        {
            this.conceptVersions = conceptVersions;
        }

        /// <summary>
        /// Returns zero of the two items are equivalent. This is not necessarily the
        /// same is same.
        /// </summary>
        /// <param name="otherObject"></param>
        /// <returns></returns>
        public override Boolean IsEquivalent(Object otherObject)
        {
            if (base.IsEquivalent(otherObject) == false)
                return false;

            ConceptChronologyDTO other = otherObject as ConceptChronologyDTO;
            if (other == null)
                return false;

            if (this == other)
                return true;

            if (FieldCompare.IsEquivalentSequence<ConceptVersionDTO>(this.conceptVersions, other.conceptVersions) == false)
                return false;
            return true;
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

            cmp = FieldCompare.CompareSequence<ConceptVersionDTO>(this.conceptVersions, other.conceptVersions);
            if (cmp != 0)
                return cmp;
            return 0;
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <returns>new DTO item.</returns>
        public static ConceptChronologyDTO Make(TinkarInput input)
        {
            IPublicId publicId = input.GetPublicId();
            return new ConceptChronologyDTO(
                publicId,
                input.GetConceptVersionList(publicId).ToImmutableArray());
        }

        /// <summary>
        /// Static method to Create DTO item from json stream.
        /// </summary>
        /// <param name="jsonObject">JSON parent container.</param>
        /// <returns>new DTO item.</returns>
        public static ConceptChronologyDTO Make(JObject jsonObject)
        {
            PublicId publicId = jsonObject.AsPublicId(ComponentFieldForJson.COMPONENT_PUBLIC_ID);
            return new ConceptChronologyDTO(publicId,
                            jsonObject.AsConceptVersionList(ComponentFieldForJson.CONCEPT_VERSIONS, publicId).ToImmutableArray());
        }


        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public virtual void Marshal(TinkarOutput output)
        {
            output.PutPublicId(this.PublicId);

            // Note that the componentIds are not written redundantly
            // in writeConceptVersionList...
            output.WriteMarshalableList(this.conceptVersions);
        }

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public virtual void Marshal(TinkarJsonOutput output)
        {
            // Note that the componentIds are not written redundantly
            // in writeConceptVersionList...
            output.WriteStartObject();
            output.WriteClass(JSONCLASSNAME);
            output.Put(ComponentFieldForJson.COMPONENT_PUBLIC_ID, this.PublicId);
            output.WriteMarshalableList(ComponentFieldForJson.CONCEPT_VERSIONS, this.conceptVersions);
            output.WriteEndObject();
        }
    }
}
