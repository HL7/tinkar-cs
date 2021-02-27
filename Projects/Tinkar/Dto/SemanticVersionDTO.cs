/*
 * Copyright 2020-2021 HL7.
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
    /// Tinkar SemanticVersion record.
    /// </summary>
    public record SemanticVersionDTO : ComponentDTO<SemanticVersionDTO>,
        ISemanticVersion,
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
        public const String JsonClassName = "SemanticVersionDTO";

        /// <summary>
        /// Gets Stamp.
        /// </summary>
        public IStamp Stamp => this.StampDTO;

        /// <summary>
        /// Gets ReferencedComponent.
        /// </summary>
        public IComponent ReferencedComponent => new ComponentDTO(this.ReferencedComponentUuids);

        /// <summary>
        /// Gets PatternForSemantic.
        /// </summary>
        public IPatternForSemantic PatternForSemantic =>
            new PatternForSemanticDTO(this.PatternForSemanticUuids);

        /// <summary>
        /// Gets public id.
        /// </summary>
        public IPublicId PublicId { get; init; }

        /// <summary>
        /// Gets Fields array.
        /// </summary>
        public IEnumerable<Object> Fields { get; init; }

        /// <summary>
        /// Gets PatternForSemantic UUID's.
        /// </summary>
        public IEnumerable<Guid> PatternForSemanticUuids { get; init; }

        /// <summary>
        /// Gets ReferencedComponent Uuids.
        /// </summary>
        public IEnumerable<Guid> ReferencedComponentUuids { get; init; }

        /// <summary>
        /// Gets Stamp.
        /// </summary>
        public StampDTO StampDTO { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticVersionDTO"/> class.
        /// </summary>
        /// <param name = "publicId" > Public id(component ids).</param>
        /// <param name="definitionForSemanticUuids">PatternForSemanticUuids.</param>
        /// <param name="referencedComponentUuids">ReferencedComponentUuids.</param>
        /// <param name="stampDTO">StampDTO.</param>
        /// <param name="fields">Fields.</param>
        public SemanticVersionDTO(
            IPublicId publicId,
            IEnumerable<Guid> definitionForSemanticUuids,
            IEnumerable<Guid> referencedComponentUuids,
            StampDTO stampDTO,
            IEnumerable<Object> fields)
        {
            this.PublicId = publicId;
            this.PatternForSemanticUuids = definitionForSemanticUuids;
            this.ReferencedComponentUuids = referencedComponentUuids;
            this.StampDTO = stampDTO;
            this.Fields = fields;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticVersionDTO"/> class
        /// from json stream.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        /// <param name="publicId">Public id (component ids).</param>
        /// <param name="definitionForSemanticUuids">PatternForSemantic UUIDs.</param>
        /// <param name="referencedComponentUuids">ReferencedComponent UUIDs.</param>
        public SemanticVersionDTO(
            JObject jObj,
            IPublicId publicId,
            IEnumerable<Guid> definitionForSemanticUuids,
            IEnumerable<Guid> referencedComponentUuids)
        {
            jObj.GetClass(JsonClassName);
            this.PublicId = publicId;
            this.PatternForSemanticUuids = definitionForSemanticUuids;
            this.ReferencedComponentUuids = referencedComponentUuids;
            this.StampDTO = new StampDTO(jObj.ReadToken<JObject>(ComponentFieldForJson.STAMP));
            this.Fields = jObj.ReadObjects(ComponentFieldForJson.FIELDS);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticVersionDTO"/> class
        /// from binary stream.
        /// </summary>
        /// <param name="input">Binary input stream to read from.</param>
        /// <param name="publicId">Public id (component ids).</param>
        /// <param name="definitionForSemanticUuids">PatternForSemantic UUIDs.</param>
        /// <param name="referencedComponentUuids">ReferencedComponent UUIDs.</param>
        public SemanticVersionDTO(
            TinkarInput input,
            IPublicId publicId,
            IEnumerable<Guid> definitionForSemanticUuids,
            IEnumerable<Guid> referencedComponentUuids)
        {
            input.CheckMarshalVersion(LocalMarshalVersion);
            this.PublicId = publicId;
            this.PatternForSemanticUuids = definitionForSemanticUuids;
            this.ReferencedComponentUuids = referencedComponentUuids;
            this.StampDTO = new StampDTO(input);
            this.Fields = input.ReadObjects();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticVersionDTO"/> class.
        /// </summary>
        /// <param name="publicId">Public id (component ids).</param>
        /// <param name="definitionForSemantic">PatternForSemantic UUIDs.</param>
        /// <param name="referencedComponent">ReferencedComponent UUIDs.</param>
        /// <param name="stamp">Stamp.</param>
        /// <param name="fields">SemanticVersion fields.</param>
        public SemanticVersionDTO(
            IPublicId publicId,
            IPatternForSemantic definitionForSemantic,
            IComponent referencedComponent,
            IStamp stamp,
            IEnumerable<Object> fields)
            : this(
                publicId,
                definitionForSemantic.PublicId.AsUuidArray,
                referencedComponent.PublicId.AsUuidArray,
                stamp.ToChangeSetThing(),
                fields)
        {
        }

        /// <summary>
        /// Compare this with another item of same type.
        /// </summary>
        /// <param name="other">Item to compare to for equality.</param>
        /// <returns> -1, 0, or 1.</returns>
        public override Int32 CompareTo(SemanticVersionDTO other)
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
            cmp = FieldCompare.CompareItem<StampDTO>(this.StampDTO, other.StampDTO);
            if (cmp != 0)
                return cmp;

            cmp = this.Fields.Count().CompareTo(other.Fields.Count());
            if (cmp != 0)
                return cmp;

            return 0;
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        /// <param name="publicId">Public id (component ids).</param>
        /// <param name="definitionForSemanticUuids">PatternForSemantic UUIDs.</param>
        /// <param name="referencedComponentUuids">ReferencedComponent UUIDs.</param>
        /// <returns>new DTO item.</returns>
        public static SemanticVersionDTO Make(
            TinkarInput input,
            IPublicId publicId,
            IEnumerable<Guid> definitionForSemanticUuids,
            IEnumerable<Guid> referencedComponentUuids) =>
            new SemanticVersionDTO(
                input,
                publicId,
                definitionForSemanticUuids,
                referencedComponentUuids);

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public void Marshal(TinkarOutput output)
        {
            output.CheckMarshalVersion(LocalMarshalVersion);;
            this.StampDTO.Marshal(output);
            output.WriteObjects(this.Fields);
        }

        /// <summary>
        /// Static method to Create DTO item from json .
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        /// <param name="publicId">Public id (component ids).</param>
        /// <param name="definitionForSemanticUuids">PatternForSemantic UUIDs.</param>
        /// <param name="referencedComponentUuids">ReferencedComponent UUIDs.</param>
        /// <returns>Deserialized SemanticVersion record.</returns>
        public static SemanticVersionDTO Make(
            JObject jObj,
            IPublicId publicId,
            IEnumerable<Guid> definitionForSemanticUuids,
            IEnumerable<Guid> referencedComponentUuids) =>
            new SemanticVersionDTO(
                jObj,
                publicId,
                definitionForSemanticUuids,
                referencedComponentUuids);

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(JsonClassName);
            output.WritePropertyName(ComponentFieldForJson.STAMP);
            this.StampDTO.Marshal(output);
            output.WriteObjects(ComponentFieldForJson.FIELDS, this.Fields);
            output.WriteEndObject();
        }
    }
}