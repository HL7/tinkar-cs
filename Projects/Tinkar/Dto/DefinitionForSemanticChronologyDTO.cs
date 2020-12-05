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
        private const int MarshalVersion = 1;

        /// <summary>
        /// Implementation of IIdentifiedThing.ComponentUuids.
        /// </summary>
        public IEnumerable<Guid> ComponentUuids { get; init; }

        /// <summary>
        /// ???.
        /// </summary>
        public IEnumerable<Guid> ChronologySetUuids { get; init; }

        /// <summary>
        /// DTO instance for Versions.
        /// </summary>
        public IEnumerable<DefinitionForSemanticVersionDTO> DefinitionVersions { get; init; }

        public IEnumerable<IDefinitionForSemanticVersion> Versions =>
                this.DefinitionVersions.Select((dto) => (IDefinitionForSemanticVersion)dto);

        public IConcept ChronologySet => new ConceptDTO(this.ChronologySetUuids);

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
        /// Compares this to another item.
        /// </summary>
        /// <param name="other">Item to compare to</param>
        /// <returns>-1, 0, or 1</returns>
        public override Int32 CompareTo(DefinitionForSemanticChronologyDTO other)
        {
            Int32 cmp = this.CompareGuids(this.ComponentUuids, other.ComponentUuids);
            if (cmp != 0)
                return cmp;
            cmp = this.CompareGuids(this.ChronologySetUuids, other.ChronologySetUuids);
            if (cmp != 0)
                return cmp;
            cmp = this.CompareSequence<DefinitionForSemanticVersionDTO>(this.DefinitionVersions, other.DefinitionVersions);
            if (cmp != 0)
                return cmp;
            return 0;
        }

        /// <summary>
        /// Override of default hashcode. Must provide if Equals overridden.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => this.ComponentUuids.GetHashCode() ^
                                             this.ChronologySetUuids.GetHashCode() ^
                                             this.DefinitionVersions.GetHashCode();


        //@Override
        //public void jsonMarshal(Writer writer) {
        //    final JSONObject json = new JSONObject();
        //    json.put(ComponentFieldForJson.CLASS, this.getClass().getCanonicalName());
        //    json.put(ComponentFieldForJson.COMPONENT_UUIDS, componentUuids);
        //    json.put(ComponentFieldForJson.CHRONOLOGY_SET_UUIDS, chronologySetUuids);
        //    json.put(ComponentFieldForJson.DEFINITION_VERSIONS, definitionVersions);
        //    json.writeJSONString(writer);
        //}

        //@JsonChronologyUnmarshaler
        //public static DefinitionForSemanticChronologyDTO Make(JSONObject jsonObject) {
        //    IEnumerable<Guid> componentUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.COMPONENT_UUIDS);
        //    return new DefinitionForSemanticChronologyDTO(componentUuids,
        //                    jsonObject.asImmutableUuidList(ComponentFieldForJson.CHRONOLOGY_SET_UUIDS),
        //                    jsonObject.asDefinitionForSemanticVersionList(ComponentFieldForJson.DEFINITION_VERSIONS, componentUuids));
        //}

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static DefinitionForSemanticChronologyDTO Make(TinkarInput input)
        {
            //$NotTested
            CheckMarshalVersion(input, MarshalVersion);
            IEnumerable<Guid> componentUuids = input.ReadImmutableUuidList();
            return new DefinitionForSemanticChronologyDTO(
                    componentUuids,
                    input.ReadImmutableUuidList(),
                    input.ReadDefinitionForSemanticVersionList(componentUuids)
                    );
        }

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream</param>
        public void Marshal(TinkarOutput output)
        {
            //$NotTested
            WriteMarshalVersion(output, MarshalVersion);
            output.WriteUuidList(this.ComponentUuids);
            output.WriteUuidList(this.ChronologySetUuids);
            output.WriteMarshalableList(this.DefinitionVersions);
        }
    }
}
