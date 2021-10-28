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
    /// Tinkar Semantic Chronology record.
    /// </summary>
    public record SemanticChronologyDTO :
        ComponentDTO,
        ISemanticChronology<SemanticVersionDTO, PatternDTO>,
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
        /// Unique ID for binary marshal of this item.
        /// </summary>
        public virtual FieldDataType FieldDataType => FieldDataType.SemanticChronologyType;

        /// <summary>
        /// Gets ReferencedComponent UUIDs.
        /// </summary>
        public IPublicId ReferencedComponentPublicId { get; init; }

        /// <summary>
        /// Gets PatternForSemantic UUIDs.
        /// </summary>
        public IPublicId PatternForSemantic { get; init; }

        /// <summary>
        /// Gets ReferencedComponent.
        /// </summary>
        public IComponent ReferencedComponent => new ComponentDTO(this.ReferencedComponentPublicId);

        /// <summary>
        /// Gets PatternForSemantic.
        /// </summary>
        public IPattern Pattern => new PatternDTO(this.PatternForSemantic);

        /// <summary>
        /// Gets SemanticVersion.
        /// </summary>
        public ImmutableArray<SemanticVersionDTO> SemanticVersions { get; init; }

        /// <summary>
        /// Gets Versions.
        /// </summary>
        public ImmutableArray<SemanticVersionDTO> Versions => this.SemanticVersions;

        public PatternDTO ChronologySet => new PatternDTO(this.PatternForSemantic);

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticChronologyDTO"/> class.
        /// </summary>
        /// <param name = "componentPublicId" > Public id(component ids).</param>
        /// <param name="patternPublicId">patternPublicId.</param>
        /// <param name="referencedComponentPublicId">ReferencedComponentPublicId.</param>
        /// <param name="semanticVersions">SemanticVersions.</param>
        public SemanticChronologyDTO(
            IPublicId componentPublicId,
            IPublicId patternPublicId,
            IPublicId referencedComponentPublicId,
            ImmutableArray<SemanticVersionDTO> semanticVersions) : base(componentPublicId)
        {
            this.ReferencedComponentPublicId = referencedComponentPublicId;
            this.PatternForSemantic = patternPublicId;
            this.SemanticVersions = semanticVersions;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticChronologyDTO"/> class.
        /// </summary>
        /// <param name = "componentPublicId" > Public id(component ids).</param>
        /// <param name="patternPublicId">Pattern for semantic.</param>
        /// <param name="referencedComponentPublicId">ReferencedComponentPublicId.</param>
        /// <param name="semanticVersions">SemanticVersions.</param>
        public SemanticChronologyDTO(
                IPublicId componentPublicId,
                IPattern patternPublicId,
                IPublicId referencedComponentPublicId,
                ImmutableArray<SemanticVersionDTO> semanticVersions) : base(componentPublicId)
        {
            this.ReferencedComponentPublicId = referencedComponentPublicId;
            this.PatternForSemantic = patternPublicId.PublicId;
            this.SemanticVersions = semanticVersions;
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

            SemanticChronologyDTO other = otherObject as SemanticChronologyDTO;
            if (other == null)
                return false;

            if (this == other)
                return true;

            if (this.PatternForSemantic.IsEquivalent(other.PatternForSemantic) == false)
                return false;
            if (this.ReferencedComponentPublicId.IsEquivalent(other.ReferencedComponentPublicId) == false)
                return false;

            if (FieldCompare.IsEquivalentSequence(this.SemanticVersions, other.SemanticVersions) == false)
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
            SemanticChronologyDTO other = otherObject as SemanticChronologyDTO;
            if (other == null)
                return -1;

            Int32 cmp = base.CompareTo(other);
            if (cmp != 0)
                return cmp;
            cmp = this.PatternForSemantic.CompareTo(other.PatternForSemantic);
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
        /// Static method to Create DTO item from  another SemanticChronology.
        /// </summary>
        /// <param name="semanticChronology">Semantic Chronology DTO.</param>
        /// <returns>new DTO item.</returns>
        public static SemanticChronologyDTO Make(SemanticChronologyDTO semanticChronology) =>
            new SemanticChronologyDTO(semanticChronology.PublicId,
                    semanticChronology.Pattern,
                    semanticChronology.ReferencedComponentPublicId,
                    semanticChronology.SemanticVersions.ToImmutableArray());


        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        /// <returns>new DTO item.</returns>
        public static SemanticChronologyDTO Make(TinkarInput input)
        {
            IPublicId componentPublicId = input.GetPublicId();
            IPublicId patternForSemanticPublicId = input.GetPublicId();
            IPublicId referencedComponentPublicId = input.GetPublicId();

            return new SemanticChronologyDTO(
                componentPublicId,
                patternForSemanticPublicId,
                referencedComponentPublicId,
                input.ReadSemanticVersionList(componentPublicId).ToImmutableArray()
                );
        }

        /// <summary>
        /// Static method to Create DTO item from json stream.
        /// </summary>
        /// <param name="jsonObject">JSON parent container to read from.</param>
        /// <returns>Deserialized SemanticChronology record.</returns>
        public static SemanticChronologyDTO Make(JObject jsonObject)
        {
            PublicId componentPublicId = jsonObject.AsPublicId(ComponentFieldForJson.COMPONENT_PUBLIC_ID);
            PublicId patternPublicId = jsonObject.AsPublicId(ComponentFieldForJson.PATTERN_PUBLIC_ID);
            PublicId referencedComponentPublicId = jsonObject.AsPublicId(ComponentFieldForJson.REFERENCED_COMPONENT_PUBLIC_ID);
            return new SemanticChronologyDTO(componentPublicId,
                    patternPublicId,
                    referencedComponentPublicId,
                    jsonObject.ReadSemanticVersionList(ComponentFieldForJson.VERSIONS,
                            componentPublicId,
                            patternPublicId,
                            referencedComponentPublicId).ToImmutableArray()
                    );
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
            output.Put(ComponentFieldForJson.PATTERN_PUBLIC_ID, this.PatternForSemantic);
            output.Put(ComponentFieldForJson.REFERENCED_COMPONENT_PUBLIC_ID, this.ReferencedComponentPublicId);
            output.WriteMarshalableList(
                ComponentFieldForJson.VERSIONS,
                this.SemanticVersions);
            output.WriteEndObject();
        }

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public virtual void Marshal(TinkarOutput output)
        {
            output.PutPublicId(this.PublicId);
            output.PutPublicId(this.PatternForSemantic);
            output.PutPublicId(this.ReferencedComponentPublicId);
            output.WriteMarshalableList(this.SemanticVersions);
        }
    }
}