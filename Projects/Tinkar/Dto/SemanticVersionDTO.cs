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
    public record SemanticVersionDTO : VersionDTO,
        ISemanticVersion,
        IJsonMarshalable,
        IMarshalable
    {
        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public const String JSONCLASSNAME = "SemanticVersionDTO";

        /// <summary>
        /// Unique ID for binary marshal of this item.
        /// </summary>
        public FieldDataType FieldDataType => FieldDataType.SemanticVersionType;

        /// <summary>
        /// Gets PatternForSemantic UUID's.
        /// </summary>
        public IPublicId DefinitionForSemanticPublicId { get; init; }

        /// <summary>
        /// Gets ReferencedComponent Uuids.
        /// </summary>
        public IPublicId ReferencedComponentPublicId { get; init; }

        /// <summary>
        /// Gets ReferencedComponent.
        /// </summary>
        public IComponent ReferencedComponent => new ComponentDTO(this.ReferencedComponentPublicId);

        /// <summary>
        /// Gets PatternForSemantic.
        /// </summary>
        public IPatternForSemantic PatternForSemantic => new PatternForSemanticDTO(this.DefinitionForSemanticPublicId);

        /// <summary>
        /// Gets Fields array.
        /// </summary>
        public IEnumerable<Object> Fields { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticVersionDTO"/> class.
        /// </summary>
        /// <param name = "componentPublicId" > Public id(component ids).</param>
        /// <param name="definitionForSemanticUuids">PatternForSemanticUuids.</param>
        /// <param name="referencedComponentUuids">ReferencedComponentUuids.</param>
        /// <param name="stampDTO">StampDTO.</param>
        /// <param name="fields">Fields.</param>
        public SemanticVersionDTO(
            IPublicId componentPublicId,
            IPublicId definitionForSemanticUuids,
            IPublicId referencedComponentUuids,
            StampDTO stampDTO,
            IEnumerable<Object> fields) : base(componentPublicId, stampDTO)
        {
            this.DefinitionForSemanticPublicId = definitionForSemanticUuids;
            this.ReferencedComponentPublicId = referencedComponentUuids;
            this.Fields = fields;
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

            SemanticVersionDTO other = otherObject as SemanticVersionDTO;
            if (other == null)
                return false;

            if (this == other)
                return true;

            if (this.DefinitionForSemanticPublicId.IsEquivalent(other.DefinitionForSemanticPublicId) == false)
                return false;
            if (this.ReferencedComponentPublicId.IsEquivalent(other.ReferencedComponentPublicId) == false)
                return false;
            if (FieldCompare.Equivalent(this.Fields, other.Fields) == false)
                return false;
            return true;
        }

        /// <summary>
        /// Compare this with another item of same type.
        /// </summary>
        /// <param name="otherObject">Item to compare to for equality.</param>
        /// <returns> -1, 0, or 1.</returns>
        public override Int32 CompareTo(Object otherObject)
        {
            SemanticVersionDTO other = otherObject as SemanticVersionDTO;
            if (other == null)
                return -1;

            Int32 cmp = base.CompareTo(other);
            if (cmp != 0)
                return cmp;
            cmp = this.DefinitionForSemanticPublicId.CompareTo(other.DefinitionForSemanticPublicId);
            if (cmp != 0)
                return cmp;
            cmp = this.ReferencedComponentPublicId.CompareTo(other.ReferencedComponentPublicId);
            if (cmp != 0)
                return cmp;

            cmp = this.Fields.Count().CompareTo(other.Fields.Count());
            if (cmp != 0)
                return cmp;

            return 0;
        }

#warning TODO
        //public static SemanticVersionDTO make(SemanticVersion semanticVersion)
        //{
        //    MutableList<Object> convertedFields = Lists.mutable.empty();
        //    semanticVersion.fields().forEach(objectToConvert-> {
        //        if (objectToConvert instanceof Concept) {
        //            Concept concept = (Concept)objectToConvert;
        //            convertedFields.add(new ConceptDTO(concept.publicId()));
        //        } else if (objectToConvert instanceof PatternForSemantic) {
        //            PatternForSemantic patternForSemantic = (PatternForSemantic)objectToConvert;
        //            convertedFields.add(new PatternForSemanticDTO(patternForSemantic.publicId()));
        //        } else if (objectToConvert instanceof Semantic) {
        //            Semantic semantic = (Semantic)objectToConvert;
        //            convertedFields.add(new SemanticDTO(semantic.publicId(), semantic.patternForSemantic(),
        //                    semantic.referencedComponent()));
        //        } else if (objectToConvert instanceof Component) {
        //            Component component = (Component)objectToConvert;
        //            convertedFields.add(new ComponentDTO(component.publicId()));
        //        } else if (objectToConvert instanceof Number) {
        //            Number number = (Number)objectToConvert;
        //            if (number instanceof Long) {
        //                convertedFields.add(number.intValue());
        //            } else if (number instanceof Double) {
        //                convertedFields.add(number.floatValue());
        //            } else
        //            {
        //                convertedFields.add(number);
        //            }
        //        } else if (objectToConvert instanceof String) {
        //            convertedFields.add(objectToConvert);
        //        } else if (objectToConvert instanceof Instant) {
        //            convertedFields.add(objectToConvert);
        //        } else
        //        {
        //            throw new UnsupportedOperationException("Can't convert:\n  " + objectToConvert + "\nin\n  " + semanticVersion);
        //        }
        //    });
        //    return new SemanticVersionDTO(semanticVersion.publicId(),
        //            semanticVersion.patternForSemantic(),
        //            semanticVersion.referencedComponent(),
        //            StampDTO.make(semanticVersion.stamp()), convertedFields.toImmutable());
        //}

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        /// <param name="componentPublicId">Public id (component ids).</param>
        /// <param name="definitionForSemanticUuids">PatternForSemantic UUIDs.</param>
        /// <param name="referencedComponentUuids">ReferencedComponent UUIDs.</param>
        /// <returns>new DTO item.</returns>
        public static SemanticVersionDTO Make(
            TinkarInput input,
            IPublicId componentPublicId,
            IPublicId definitionForSemanticUuids,
            IPublicId referencedComponentUuids) =>
            new SemanticVersionDTO(
                componentPublicId,
                definitionForSemanticUuids,
                referencedComponentUuids,
                StampDTO.Make(input),
                input.GetObjects());

        /// <summary>
        /// Static method to Create DTO item from json .
        /// </summary>
        /// <param name="jsonObject">JSON parent container.</param>
        /// <param name="componentPublicId">Public id (component ids).</param>
        /// <param name="definitionForSemanticPublicId">PatternForSemantic UUIDs.</param>
        /// <param name="referencedComponentPublicId">ReferencedComponent UUIDs.</param>
        /// <returns>Deserialized SemanticVersion record.</returns>
        public static SemanticVersionDTO Make(
            JObject jsonObject,
            IPublicId componentPublicId,
            IPublicId definitionForSemanticPublicId,
            IPublicId referencedComponentPublicId) =>
            new SemanticVersionDTO(componentPublicId,
                definitionForSemanticPublicId,
                referencedComponentPublicId,
                StampDTO.Make((JObject)jsonObject[ComponentFieldForJson.STAMP]),
                jsonObject.AsObjects(ComponentFieldForJson.FIELDS));

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public virtual void Marshal(TinkarOutput output)
        {
            this.StampDTO.Marshal(output);
            output.WriteObjects(this.Fields);
        }

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public virtual void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WritePropertyName(ComponentFieldForJson.STAMP);
            this.StampDTO.Marshal(output);
            output.Put(ComponentFieldForJson.FIELDS, this.Fields);
            output.WriteEndObject();
        }
    }
}
