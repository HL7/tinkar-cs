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
        ComponentDTO,
        IPatternForSemanticChronology<IConcept>,
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
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public override String JsonClassName => JSONCLASSNAME;

        /// <summary>
        /// Gets public id.
        /// </summary>
        IPublicId chronologySetPublicId { get; init; }

        /// <summary>
        /// Gets Versions record.
        /// </summary>
        IEnumerable<PatternForSemanticVersionDTO> definitionVersions { get; init; }

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
            this.chronologySetPublicId = componentPublicId;
            this.definitionVersions = definitionVersions;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatternForSemanticChronologyDTO"/> class
        /// from binary stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        protected PatternForSemanticChronologyDTO(TinkarInput input) : base(input)
        {
            this.chronologySetPublicId = input.ReadPublicId();
            this.definitionVersions =
                input.ReadPatternForSemanticVersionList(this.chronologySetPublicId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatternForSemanticChronologyDTO"/> class
        /// from json stream.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        public PatternForSemanticChronologyDTO(JObject jObj) : base(jObj)
        {
            this.chronologySetPublicId  = jObj.ReadPublicId(ComponentFieldForJson.COMPONENT_PUBLIC_ID);
            this.definitionVersions =
                jObj.ReadPatternForSemanticVersionList(this.chronologySetPublicId);
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="otherObject">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public override Int32 CompareTo(Object otherObject)
        {
            PatternForSemanticChronologyDTO other = otherObject as PatternForSemanticChronologyDTO;
            if (other == null)
                return -1;

            Int32 cmp = base.CompareTo(other);
            if (cmp != 0)
                return cmp;

            cmp = FieldCompare.ComparePublicIds(this.chronologySetPublicId, other.chronologySetPublicId);
            if (cmp != 0)
                return cmp;

            cmp = FieldCompare.CompareSequence(this.definitionVersions, other.definitionVersions);
            if (cmp != 0)
                return cmp;
            return 0;
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        /// <returns>new DTO item.</returns>
        public static PatternForSemanticChronologyDTO Make(TinkarInput input) =>
            new PatternForSemanticChronologyDTO(input);

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public override void MarshalFields(TinkarOutput output)
        {
            base.MarshalFields(output);
            output.WritePublicId(this.chronologySetPublicId);
            output.WritePublicId(this.chronologySetPublicId);
            output.WriteMarshalableList(this.definitionVersions);
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        /// <returns>new DTO item.</returns>
        public static PatternForSemanticChronologyDTO Make(JObject jObj) =>
            new PatternForSemanticChronologyDTO(jObj);

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public override void MarshalFields(TinkarJsonOutput output)
        {
            base.MarshalFields(output);
            output.WritePublicId(ComponentFieldForJson.COMPONENT_PUBLIC_ID, this.chronologySetPublicId);
            output.WritePublicId(ComponentFieldForJson.CHRONOLOGY_SET_PUBLIC_ID, this.chronologySetPublicId);
            output.WriteMarshalableList(
                ComponentFieldForJson.DEFINITION_VERSIONS,
                this.definitionVersions);
        }
    }
}
