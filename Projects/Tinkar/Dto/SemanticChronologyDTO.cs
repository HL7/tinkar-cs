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
        ComponentDTO<SemanticChronologyDTO>,
        ISemanticChronology<PatternForSemanticDTO>,
        IChangeSetThing,
        IJsonMarshalable,
        IMarshalable
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
        public const String JsonClassName = "SemanticChronologyDTO";

        /// <summary>
        /// Gets PatternForSemantic UUIDs.
        /// </summary>
        public IEnumerable<Guid> PatternForSemanticUuids { get; init; }

        /// <summary>
        /// Gets ReferencedComponent UUIDs.
        /// </summary>
        public IEnumerable<Guid> ReferencedComponentUuids { get; init; }

        /// <summary>
        /// Gets SemanticVersion.
        /// </summary>
        public IEnumerable<SemanticVersionDTO> SemanticVersions { get; init; }

        /// <summary>
        /// Gets public id.
        /// </summary>
        public IPublicId PublicId { get; init; }

        /// <summary>
        /// Gets ReferencedComponent.
        /// </summary>
        public IComponent ReferencedComponent => new ComponentDTO(this.ReferencedComponentUuids);

        /// <summary>
        /// Gets PatternForSemantic.
        /// </summary>
        public IPatternForSemantic PatternForSemantic => new PatternForSemanticDTO(this.PatternForSemanticUuids);

        /// <summary>
        /// Gets Versions.
        /// </summary>
        public IEnumerable<ISemanticVersion> Versions =>
            this.SemanticVersions.Select((dto) => (ISemanticVersion)dto);

        /// <summary>
        /// Gets ChronologySet.
        /// </summary>
        public PatternForSemanticDTO ChronologySet =>
            new PatternForSemanticDTO(this.PatternForSemanticUuids);

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticChronologyDTO"/> class.
        /// </summary>
        /// <param name = "publicId" > Public id(component ids).</param>
        /// <param name="definitionForSemanticUuids">definitionForSemanticUuids.</param>
        /// <param name="referencedComponentUuids">ReferencedComponentUuids.</param>
        /// <param name="semanticVersions">SemanticVersions.</param>
        public SemanticChronologyDTO(
            IPublicId publicId,
            IEnumerable<Guid> definitionForSemanticUuids,
            IEnumerable<Guid> referencedComponentUuids,
            IEnumerable<SemanticVersionDTO> semanticVersions)
        {
            this.PublicId = publicId;
            this.PatternForSemanticUuids = definitionForSemanticUuids;
            this.ReferencedComponentUuids = referencedComponentUuids;
            this.SemanticVersions = semanticVersions;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticChronologyDTO"/> class
        /// from binary stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        public SemanticChronologyDTO(TinkarInput input)
        {
            input.CheckMarshalVersion(LocalMarshalVersion);
            this.PublicId = input.ReadPublicId();
            this.PatternForSemanticUuids = input.ReadUuids();
            this.ReferencedComponentUuids = input.ReadUuids();
            this.SemanticVersions = input.ReadSemanticVersionList(
                this.PublicId,
                this.PatternForSemanticUuids,
                this.ReferencedComponentUuids);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticChronologyDTO"/> class
        /// from json stream.
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        public SemanticChronologyDTO(JObject jObj)
        {
            this.PublicId  = jObj.ReadPublicId(ComponentFieldForJson.COMPONENT_PUBLIC_ID);
            this.PatternForSemanticUuids = jObj.ReadUuids(ComponentFieldForJson.DEFINITION_FOR_SEMANTIC_UUIDS);
            this.ReferencedComponentUuids = jObj.ReadUuids(ComponentFieldForJson.REFERENCED_COMPONENT_UUIDS);
            this.SemanticVersions = jObj.ReadSemanticVersionList(
                this.PublicId,
                this.PatternForSemanticUuids,
                this.ReferencedComponentUuids);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticChronologyDTO"/> class.
        /// </summary>
        /// <param name = "publicId" > Public id(component ids).</param>
        /// <param name="definitionForSemantic">definitionForSemanticUuids.</param>
        /// <param name="referencedComponent">ReferencedComponentUuids.</param>
        /// <param name="semanticVersions">SemanticVersions.</param>
        public SemanticChronologyDTO(
            IPublicId publicId,
            IPatternForSemantic definitionForSemantic,
            IComponent referencedComponent,
            IEnumerable<SemanticVersionDTO> semanticVersions)
            : this(
                    publicId,
                    definitionForSemantic.PublicId.AsUuidArray,
                    referencedComponent.PublicId.AsUuidArray,
                    semanticVersions)
        {
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="other">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public override Int32 CompareTo(SemanticChronologyDTO other)
        {
            Int32 cmp = base.CompareTo(other);
            if (cmp != 0)
                return cmp;
            cmp = FieldCompare.CompareGuids(this.PatternForSemanticUuids, other.PatternForSemanticUuids);
            if (cmp != 0)
                return cmp;
            cmp = FieldCompare.CompareGuids(this.ReferencedComponentUuids, other.ReferencedComponentUuids);
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
        public static SemanticChronologyDTO Make(TinkarInput input) =>
            new SemanticChronologyDTO(input);

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public void Marshal(TinkarOutput output)
        {
            output.CheckMarshalVersion(LocalMarshalVersion);;
            output.WriteUuids(this.PublicId);
            output.WriteUuids(this.PatternForSemanticUuids);
            output.WriteUuids(this.ReferencedComponentUuids);
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
        public void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(JsonClassName);
            output.WriteUuids(
                ComponentFieldForJson.COMPONENT_PUBLIC_ID,
                this.PublicId);
            output.WriteUuids(
                ComponentFieldForJson.DEFINITION_FOR_SEMANTIC_UUIDS,
                this.PatternForSemanticUuids);
            output.WriteUuids(
                ComponentFieldForJson.REFERENCED_COMPONENT_UUIDS,
                this.ReferencedComponentUuids);
            output.WriteMarshalableList(
                ComponentFieldForJson.VERSIONS,
                this.SemanticVersions);
            output.WriteEndObject();
        }
    }
}