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
    /**
	 *
	 * @author kec
	 */
    public record DefinitionForSemanticChronologyDTO :
        BaseDTO<DefinitionForSemanticChronologyDTO>,
        IDefinitionForSemanticChronology<IConcept>,
        IChangeSetThing,
        IJsonMarshalable,
        IMarshalable
    {
        /// <summary>
        /// Version of marshalling code.
        /// If code is modified in a way that renders old serialized data
        /// non-conformant, then this number should be incremented.
        /// </summary>
        private const int MarshalVersion = 1;

        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        private const String JsonClassName = "DefinitionForSemanticChronologyDTO";

        /// <summary>
        /// Implementation of IIdentifiedThing.ComponentUuids.
        /// </summary>
        public IEnumerable<Guid> ComponentUuids { get; init; }

        /// <summary>
        /// DTO instance for Versions.
        /// </summary>
        public IEnumerable<DefinitionForSemanticVersionDTO> DefinitionVersions { get; init; }

        /// <summary>
        /// Implementation of IChronology.Versions
        /// </summary>
        public IEnumerable<IDefinitionForSemanticVersion> Versions =>
            this.DefinitionVersions.Select((dto) => (IDefinitionForSemanticVersion)dto);

        /// <summary>
        /// Implementation of IChronology.ChronologySet
        /// </summary>
        public IConcept ChronologySet => new ConceptDTO(this.ChronologySetUuids);

        /// <summary>
        /// ChronologySet uuids
        /// </summary>
        public IEnumerable<Guid> ChronologySetUuids { get; init; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="componentUuids">ComponentUuids</param>
        /// <param name="chronologySetUuids">ChronologySetUuids</param>
        /// <param name="definitionVersions">DefinitionVersions</param>
        public DefinitionForSemanticChronologyDTO(
            IEnumerable<Guid> componentUuids,
            IEnumerable<Guid> chronologySetUuids,
            IEnumerable<DefinitionForSemanticVersionDTO> definitionVersions)
        {
            this.ComponentUuids = componentUuids;
            this.ChronologySetUuids = chronologySetUuids;
            this.DefinitionVersions = definitionVersions;
        }

        /// <summary>
        /// Create item from binary stream
        /// </summary>
        /// <param name="input">input data stream</param>
        public DefinitionForSemanticChronologyDTO(TinkarInput input)
        {
            input.CheckMarshalVersion(MarshalVersion);
            this.ComponentUuids = input.ReadUuids();
            this.ChronologySetUuids = input.ReadUuids();
            this.DefinitionVersions =
                input.ReadDefinitionForSemanticVersionList(this.ComponentUuids);
        }

        /// <summary>
        /// Create item from json stream
        /// </summary>
        /// <param name="input">input data stream</param>
        public DefinitionForSemanticChronologyDTO(JObject jObj)
        {
            jObj.GetClass(JsonClassName);
            this.ComponentUuids = jObj.ReadUuids(ComponentFieldForJson.COMPONENT_UUIDS);
            this.ChronologySetUuids = jObj.ReadUuids(ComponentFieldForJson.CHRONOLOGY_SET_UUIDS);
            this.DefinitionVersions =
                jObj.ReadDefinitionForSemanticVersionList(this.ComponentUuids);
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="other">Item to compare to</param>
        /// <returns>-1, 0, or 1</returns>
        public override Int32 CompareTo(DefinitionForSemanticChronologyDTO other)
        {
            Int32 cmp = FieldCompare.CompareGuids(this.ComponentUuids, other.ComponentUuids);
            if (cmp != 0)
                return cmp;
            cmp = FieldCompare.CompareGuids(this.ChronologySetUuids, other.ChronologySetUuids);
            if (cmp != 0)
                return cmp;
            cmp = FieldCompare.CompareSequence<DefinitionForSemanticVersionDTO>(this.DefinitionVersions, other.DefinitionVersions);
            if (cmp != 0)
                return cmp;
            return 0;
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static DefinitionForSemanticChronologyDTO Make(TinkarInput input) =>
            new DefinitionForSemanticChronologyDTO(input);

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream</param>
        public void Marshal(TinkarOutput output)
        {
            output.WriteMarshalVersion(MarshalVersion);
            output.WriteUuids(this.ComponentUuids);
            output.WriteUuids(this.ChronologySetUuids);
            output.WriteMarshalableList(this.DefinitionVersions);
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static DefinitionForSemanticChronologyDTO Make(JObject jObj) =>
            new DefinitionForSemanticChronologyDTO(jObj);

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream</param>
        public void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(JsonClassName);
            output.WriteUuids(ComponentFieldForJson.COMPONENT_UUIDS, this.ComponentUuids);
            output.WriteUuids(ComponentFieldForJson.CHRONOLOGY_SET_UUIDS, this.ChronologySetUuids);
            output.WriteMarshalableList(ComponentFieldForJson.DEFINITION_VERSIONS,
                this.DefinitionVersions);
            output.WriteEndObject();
        }
    }
}
