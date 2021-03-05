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
    /// Tinkar Semantic Chronology record.
    /// </summary>
    public record SemanticChronologyDTO :
        ComponentDTO,
        ISemanticChronology<SemanticVersionDTO, PatternForSemanticDTO>,
        IDTO,
        IJsonMarshalable,
        IMarshalable
    {
        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public const String JSONCLASSNAME = "SemanticChronologyDTO";

        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public override String JsonClassName => JSONCLASSNAME;

        /// <summary>
        /// Gets PatternForSemantic PublicId.
        /// </summary>
        public IPublicId PatternForSemanticPublicId { get; init; }

        /// <summary>
        /// Gets ReferencedComponent PublicId.
        /// </summary>
        public IPublicId ReferencedComponentPublicId { get; init; }

        /// <summary>
        /// Gets SemanticVersion.
        /// </summary>
        public IEnumerable<SemanticVersionDTO> SemanticVersions { get; init; }

        /// <summary>
        /// Gets ReferencedComponent.
        /// </summary>
        public IComponent ReferencedComponent => new ComponentDTO(this.ReferencedComponentPublicId);

        /// <summary>
        /// Gets PatternForSemantic.
        /// </summary>
        public IPatternForSemantic PatternForSemantic => new PatternForSemanticDTO(this.PatternForSemanticPublicId);

        /// <summary>
        /// Gets Versions.
        /// </summary>
        public IEnumerable<SemanticVersionDTO> Versions =>
            this.SemanticVersions.Select((dto) => dto);

        /// <summary>
        /// Gets ChronologySet.
        /// </summary>
        public PatternForSemanticDTO ChronologySet =>
            new PatternForSemanticDTO(this.PatternForSemanticPublicId);

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticChronologyDTO"/> class.
        /// </summary>
        /// <param name = "publicId" > Public id(component ids).</param>
        /// <param name="patternForSemanticPublicId">definitionForSemanticPublicId.</param>
        /// <param name="referencedComponentPublicId">ReferencedComponentPublicId.</param>
        /// <param name="semanticVersions">SemanticVersions.</param>
        public SemanticChronologyDTO(
            IPublicId publicId,
            IPublicId patternForSemanticPublicId,
            IPublicId referencedComponentPublicId,
            IEnumerable<SemanticVersionDTO> semanticVersions) : base(publicId)
        {
            this.PatternForSemanticPublicId = patternForSemanticPublicId;
            this.ReferencedComponentPublicId = referencedComponentPublicId;
            this.SemanticVersions = semanticVersions;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticChronologyDTO"/> class
        /// from json stream.
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        public SemanticChronologyDTO(JObject jObj) : base(jObj)
        {
            this.PatternForSemanticPublicId = jObj.ReadPublicId(ComponentFieldForJson.PATTERN_FOR_SEMANTIC_PUBLIC_ID);
            this.ReferencedComponentPublicId = jObj.ReadPublicId(ComponentFieldForJson.REFERENCED_COMPONENT_PUBLIC_ID);
            this.SemanticVersions = jObj.ReadSemanticVersionList(
                this.PublicId,
                this.PatternForSemanticPublicId,
                this.ReferencedComponentPublicId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticChronologyDTO"/> class.
        /// </summary>
        /// <param name = "publicId" > Public id(component ids).</param>
        /// <param name="definitionForSemantic">definitionForSemanticPublicId.</param>
        /// <param name="referencedComponent">ReferencedComponentPublicId.</param>
        /// <param name="semanticVersions">SemanticVersions.</param>
        public SemanticChronologyDTO(
            IPublicId publicId,
            IPatternForSemantic definitionForSemantic,
            IComponent referencedComponent,
            IEnumerable<SemanticVersionDTO> semanticVersions)
            : this(
                    publicId,
                    definitionForSemantic.PublicId,
                    referencedComponent.PublicId,
                    semanticVersions)
        {
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="otherObject">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public override Int32 CompareTo(Object otherObject)
        {
            SemanticChronologyDTO other = otherObject as SemanticChronologyDTO;
            if (other == null)
                return -1;

            Int32 cmp = base.CompareTo(other);
            if (cmp != 0)
                return cmp;
            cmp = this.PatternForSemanticPublicId.CompareTo(other.PatternForSemanticPublicId);
            if (cmp != 0)
                return cmp;
            cmp = this.ReferencedComponentPublicId.CompareTo(other.ReferencedComponentPublicId);
            if (cmp != 0)
                return cmp;
            cmp = FieldCompare.CompareSequence(this.SemanticVersions, other.SemanticVersions);
            if (cmp != 0)
                return cmp;
            return 0;
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        /// <returns>new DTO item.</returns>
        public static SemanticChronologyDTO Make(TinkarInput input)
        {
            IPublicId publicId = input.GetPublicId();
            IPublicId patternForSemanticPublicId = input.GetPublicId();
            IPublicId referencedComponentPublicId = input.GetPublicId();
            
            return new SemanticChronologyDTO(
                publicId,
                patternForSemanticPublicId,
                referencedComponentPublicId,
                input.GetSemanticVersionList(publicId, patternForSemanticPublicId, referencedComponentPublicId)
                );
        }

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public override void MarshalFields(TinkarOutput output)
        {
            base.MarshalFields(output);
            output.WritePublicId(this.PatternForSemanticPublicId);
            output.WritePublicId(this.ReferencedComponentPublicId);
            output.WriteMarshalableList(this.SemanticVersions);
        }

        /// <summary>
        /// Static method to Create DTO item from json stream.
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        /// <returns>Deserialized SemanticChronology record.</returns>
        public static SemanticChronologyDTO Make(JObject jObj) =>
            new SemanticChronologyDTO(jObj);

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public override void MarshalFields(TinkarJsonOutput output)
        {
            base.MarshalFields(output);
            output.WritePublicId(
                ComponentFieldForJson.PATTERN_FOR_SEMANTIC_PUBLIC_ID,
                this.PatternForSemanticPublicId);
            output.WritePublicId(
                ComponentFieldForJson.REFERENCED_COMPONENT_PUBLIC_ID,
                this.ReferencedComponentPublicId);
            output.WriteMarshalableList(
                ComponentFieldForJson.VERSIONS,
                this.SemanticVersions);
            base.MarshalFields(output);
        }
    }
}