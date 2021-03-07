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
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Tinkar
{
    /// <summary>
    /// Tinkar PatternForSemanticChronology record.
    /// </summary>
    public record PatternForSemanticChronologyDTO :
        PatternForSemanticDTO,
        IPatternForSemanticChronology<PatternForSemanticVersionDTO, FieldDefinitionDTO, IConcept>,
        IDTO,
        IJsonMarshalable,
        IMarshalable
    {
        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public new const String JSONCLASSNAME = "PatternForSemanticChronologyDTO";

        /// <summary>
        /// Gets public id.
        /// </summary>
        public IPublicId ChronologySetPublicId { get; init; }

        /// <summary>
        /// Gets Versions list.
        /// </summary>
        public IEnumerable<PatternForSemanticVersionDTO> Versions { get; init; }

        public IConcept ChronologySet => new ConceptDTO(ChronologySetPublicId);

        /// <summary>
        /// Initializes a new instance of the <see cref="PatternForSemanticChronologyDTO"/> class.
        /// </summary>
        /// <param name = "componentPublicId" > Public id(component ids).</param>
        /// <param name="chronologySetPublicId">ChronologySetPublicId.</param>
        /// <param name="definitionVersions">DefinitionVersions.</param>
        public PatternForSemanticChronologyDTO(
            IPublicId componentPublicId,
            IPublicId chronologySetPublicId,
            IEnumerable<PatternForSemanticVersionDTO> definitionVersions) : base(componentPublicId)
        {
            this.ChronologySetPublicId = chronologySetPublicId;
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

            PatternForSemanticChronologyDTO other = otherObject as PatternForSemanticChronologyDTO;
            if (other == null)
                return false;

            if (this == other)
                return true;

            if (this.ChronologySetPublicId.IsEquivalent(other.ChronologySetPublicId) == false)
                return false;

            if (FieldCompare.EquivelateSequence(this.Versions, other.Versions) == false)
                return false;
            return true;
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="otherObject">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public override Int32 IsSame(Object otherObject)
        {
            PatternForSemanticChronologyDTO other = otherObject as PatternForSemanticChronologyDTO;
            if (other == null)
                return -1;

            Int32 cmp = base.IsSame(other);
            if (cmp != 0)
                return cmp;

            cmp = FieldCompare.ComparePublicIds(this.ChronologySetPublicId, other.ChronologySetPublicId);
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
        public static new PatternForSemanticChronologyDTO Make(TinkarInput input)
        {
            IPublicId componentPublicId = input.GetPublicId();
            IPublicId chronologySetPublicId = input.GetPublicId();
            return new PatternForSemanticChronologyDTO(
                componentPublicId,
                chronologySetPublicId,
                input.GetPatternForSemanticVersionList(componentPublicId));
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        /// <returns>new DTO item.</returns>
        public static new PatternForSemanticChronologyDTO Make(JObject jObj)
        {
            PublicId componentPublicId = jObj.AsPublicId(ComponentFieldForJson.COMPONENT_PUBLIC_ID);
            PublicId chronologySetPublicId = jObj.AsPublicId(ComponentFieldForJson.CHRONOLOGY_SET_PUBLIC_ID);
            return new PatternForSemanticChronologyDTO(componentPublicId, 
                chronologySetPublicId, 
                jObj.ReadPatternForSemanticVersionList(componentPublicId));
        }


        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public override void Marshal(TinkarOutput output)
        {
            output.PutPublicId(this.PublicId);
            output.PutPublicId(this.ChronologySetPublicId);
            output.WriteMarshalableList(this.Versions);
        }

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public override void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(JSONCLASSNAME);
            output.Put(ComponentFieldForJson.COMPONENT_PUBLIC_ID, this.PublicId);
            output.Put(ComponentFieldForJson.CHRONOLOGY_SET_PUBLIC_ID, this.ChronologySetPublicId);
            output.WriteMarshalableList(ComponentFieldForJson.DEFINITION_VERSIONS, this.Versions);
            output.WriteEndObject();
        }
    }
}
