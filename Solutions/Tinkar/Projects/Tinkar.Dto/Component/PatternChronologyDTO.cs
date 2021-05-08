/*
 * Copyright 2020 kec.
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
    /// Tinkar PatternForSemanticChronology record.
    /// </summary>
    public record PatternChronologyDTO :
        ComponentDTO,
        IPatternChronology<PatternVersionDTO, FieldDefinitionDTO, IConcept>,
        IDTO,
        IJsonMarshalable,
        IMarshalable
    {
        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public const String JSONCLASSNAME = "PatternForSemanticChronologyDTO";

        /// <summary>
        /// Unique ID for binary marshal of this item.
        /// </summary>
        public virtual FieldDataType FieldDataType => FieldDataType.PatternChronologyType;

        /// <summary>
        /// Gets Versions list.
        /// </summary>
        public ImmutableArray<PatternVersionDTO> Versions { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatternChronologyDTO"/> class.
        /// </summary>
        /// <param name = "componentPublicId" > Public id(component ids).</param>
        /// <param name="definitionVersions">DefinitionVersions.</param>
        public PatternChronologyDTO(
            IPublicId componentPublicId,
            ImmutableArray<PatternVersionDTO> definitionVersions) : base(componentPublicId)
        {
            this.Versions = definitionVersions;
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

            PatternChronologyDTO other = otherObject as PatternChronologyDTO;
            if (other == null)
                return false;

            if (this == other)
                return true;

            if (FieldCompare.IsEquivalentSequence(this.Versions, other.Versions) == false)
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
            PatternChronologyDTO other = otherObject as PatternChronologyDTO;
            if (other == null)
                return -1;

            Int32 cmp = base.CompareTo(other);
            if (cmp != 0)
                return cmp;

            cmp = FieldCompare.CompareSequence(this.Versions, other.Versions);
            if (cmp != 0)
                return cmp;
            return 0;
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        /// <returns>new DTO item.</returns>
        public static PatternChronologyDTO Make(TinkarInput input)
        {
            IPublicId componentPublicId = input.GetPublicId();
            return new PatternChronologyDTO(
                componentPublicId,
                input.GetPatternVersionList(componentPublicId).ToImmutableArray());
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        /// <returns>new DTO item.</returns>
        public static PatternChronologyDTO Make(JObject jObj)
        {
            PublicId componentPublicId = jObj.AsPublicId(ComponentFieldForJson.COMPONENT_PUBLIC_ID);
            return new PatternChronologyDTO(componentPublicId, 
                jObj.ReadPatternVersionList(componentPublicId).ToImmutableArray());
        }


        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public virtual void Marshal(TinkarOutput output)
        {
            output.PutPublicId(this.PublicId);
            output.WriteMarshalableList(this.Versions);
        }

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public virtual void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(JSONCLASSNAME);
            output.Put(ComponentFieldForJson.COMPONENT_PUBLIC_ID, this.PublicId);
            output.WriteMarshalableList(ComponentFieldForJson.DEFINITION_VERSIONS, this.Versions);
            output.WriteEndObject();
        }
    }
}
