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
    public record SemanticChronologyDTO :
        BaseDTO<SemanticChronologyDTO>,
        ISemanticChronology<DefinitionForSemanticDTO>,
        IChangeSetThing,
        IJsonMarshalable,
        IMarshalable
    {

        private const int MarshalVersion = 1;

        /// <summary>
        /// Uuids for DefinitionForSemantic
        /// </summary>
        public IEnumerable<Guid> DefinitionForSemanticUuids { get; init; }

        /// <summary>
        /// Uuids for ReferencedComponent
        /// </summary>
        public IEnumerable<Guid> ReferencedComponentUuids { get; init; }

        /// <summary>
        /// ???
        /// </summary>
        public IEnumerable<SemanticVersionDTO> SemanticVersions { get; init; }

        /// <summary>
        /// Implements IIdentifiedThing.ComponendUuids
        /// </summary>
        public IEnumerable<Guid> ComponentUuids { get; init; }

        /// <summary>
        /// Implements ISemantic.ReferencedComponent
        /// </summary>
        public IIdentifiedThing ReferencedComponent => new IdentifiedThingDTO(this.ReferencedComponentUuids);

        /// <summary>
        /// Implements ISemantic.DefinitionForSemantic
        /// </summary>
        public IDefinitionForSemantic DefinitionForSemantic => new DefinitionForSemanticDTO(this.DefinitionForSemanticUuids);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="componentUuids">ComponentUuids</param>
        /// <param name="definitionForSemanticUuids">definitionForSemanticUuids</param>
        /// <param name="referencedComponentUuids">ReferencedComponentUuids</param>
        /// <param name="semanticVersions">SemanticVersions</param>
        public SemanticChronologyDTO(
            IEnumerable<Guid> componentUuids,
            IEnumerable<Guid> definitionForSemanticUuids,
            IEnumerable<Guid> referencedComponentUuids,
            IEnumerable<SemanticVersionDTO> semanticVersions)
        {
            this.ComponentUuids = componentUuids;
            this.DefinitionForSemanticUuids = definitionForSemanticUuids;
            this.ReferencedComponentUuids = referencedComponentUuids;
            this.SemanticVersions = semanticVersions;
        }

        public SemanticChronologyDTO(IEnumerable<Guid> componentUuids,
                                  IDefinitionForSemantic definitionForSemantic,
                                  IIdentifiedThing referencedComponent,
                                  IEnumerable<SemanticVersionDTO> semanticVersions) :
                this(componentUuids,
                     definitionForSemantic.ComponentUuids,
                     referencedComponent.ComponentUuids,
                     semanticVersions)
        {
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="other">Item to compare to</param>
        /// <returns>-1, 0, or 1</returns>
        public override Int32 CompareTo(SemanticChronologyDTO other)
        {
            Int32 cmp = this.CompareGuids(this.ComponentUuids, other.ComponentUuids);
            if (cmp != 0)
                return cmp;
            cmp = this.CompareGuids(this.DefinitionForSemanticUuids, other.DefinitionForSemanticUuids);
            if (cmp != 0)
                return cmp;
            cmp = this.CompareGuids(this.ReferencedComponentUuids, other.ReferencedComponentUuids);
            if (cmp != 0)
                return cmp;
            cmp = this.CompareSequence(this.SemanticVersions, other.SemanticVersions);
            if (cmp != 0)
                return cmp;
            return 0;
        }

        //@Override
        //public void jsonMarshal(Writer writer) {
        //    final JSONObject json = new JSONObject();
        //    json.put(ComponentFieldForJson.CLASS, this.getClass().getCanonicalName());
        //    json.put(ComponentFieldForJson.COMPONENT_UUIDS, componentUuids);
        //    json.put(ComponentFieldForJson.DEFINITION_FOR_SEMANTIC_UUIDS, definitionForSemanticUuids);
        //    json.put(ComponentFieldForJson.REFERENCED_COMPONENT_UUIDS, referencedComponentUuids);
        //    json.put(ComponentFieldForJson.VERSIONS, semanticVersions);
        //    json.writeJSONString(writer);
        //}

        //@JsonChronologyUnmarshaler
        //public static SemanticChronologyDTO Make(JSONObject jsonObject) {
        //    IEnumerable<Guid> componentUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.COMPONENT_UUIDS);
        //    IEnumerable<Guid> definitionForSemanticUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.DEFINITION_FOR_SEMANTIC_UUIDS);
        //    IEnumerable<Guid> referencedComponentUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.REFERENCED_COMPONENT_UUIDS);
        //    return new SemanticChronologyDTO(componentUuids,
        //            definitionForSemanticUuids,
        //            referencedComponentUuids,
        //                    jsonObject.asSemanticVersionList(ComponentFieldForJson.VERSIONS,
        //                            componentUuids,
        //                            definitionForSemanticUuids,
        //                            referencedComponentUuids)
        //            );
        //}

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static SemanticChronologyDTO Make(TinkarInput input)
        {
            //$NotTested
            CheckMarshalVersion(input, MarshalVersion);
            IEnumerable<Guid> componentUuids = input.ReadImmutableUuidList();
            IEnumerable<Guid> definitionForSemanticUuids = input.ReadImmutableUuidList();
            IEnumerable<Guid> referencedComponentUuids = input.ReadImmutableUuidList();
            return new SemanticChronologyDTO(
                    componentUuids, definitionForSemanticUuids, referencedComponentUuids,
                    input.ReadSemanticVersionList(componentUuids, definitionForSemanticUuids, referencedComponentUuids)
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
            output.WriteUuidList(this.DefinitionForSemanticUuids);
            output.WriteUuidList(this.ReferencedComponentUuids);
            output.WriteMarshalableList(this.SemanticVersions);
        }

        public IEnumerable<ISemanticVersion> Versions =>
            this.SemanticVersions.Select((dto) => (ISemanticVersion)dto);

        public DefinitionForSemanticDTO ChronologySet =>
            new DefinitionForSemanticDTO(this.DefinitionForSemanticUuids);
    }
}