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

namespace Tinkar
{
    /**
	 *
	 * @author kec
	 */
    public record SemanticVersionDTO : BaseDTO<SemanticVersionDTO>,
        ISemanticVersion,
        IChangeSetThing,
        IJsonMarshalable,
        IMarshalable
    {
        private const int MarshalVersion = 1;

        /// <summary>
        /// Implements IVersion.Stamp
        /// </summary>

        public IStamp Stamp => this.StampDTO;

        /// <summary>
        /// Implements ISemantic.ReferencedComponent
        /// </summary>
        public IIdentifiedThing ReferencedComponent => new IdentifiedThingDTO(this.ReferencedComponentUuids);

        /// <summary>
        /// Implements ISemantic.DefinitionForSemantic
        /// </summary>
        public IDefinitionForSemantic DefinitionForSemantic => new DefinitionForSemanticDTO(this.DefinitionForSemanticUuids);


        /// <summary>
        /// Implementation of IIdentifiedThing.ComponentUuids
        /// </summary>
        public IEnumerable<Guid> ComponentUuids { get; init; }

        /// <summary>
        /// Implementation of ISemanticVersion.Fields
        /// </summary>
        public IEnumerable<Object> Fields { get; init; }

        /// <summary>
        /// Backing for DefinitionForSemantic UUID's
        /// </summary>
        public IEnumerable<Guid> DefinitionForSemanticUuids { get; init; }

        /// <summary>
        /// Backing for ReferencedComponent Uuids
        /// </summary>
        public IEnumerable<Guid> ReferencedComponentUuids { get; init; }

        /// <summary>
        /// Backing for Stamp.
        /// </summary>
        public StampDTO StampDTO { get; init; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="componentUuids">ComponentUuids</param>
        /// <param name="definitionForSemanticUuids">DefinitionForSemanticUuids</param>
        /// <param name="referencedComponentUuids">ReferencedComponentUuids</param>
        /// <param name="stampDTO">StampDTO</param>
        /// <param name="fields">Fields</param>
        public SemanticVersionDTO(IEnumerable<Guid> componentUuids,
                                IEnumerable<Guid> definitionForSemanticUuids,
                                IEnumerable<Guid> referencedComponentUuids,
                                StampDTO stampDTO,
                                IEnumerable<Object> fields)
        {
            this.ComponentUuids = componentUuids;
            this.DefinitionForSemanticUuids = definitionForSemanticUuids;
            this.ReferencedComponentUuids = referencedComponentUuids;
            this.StampDTO = stampDTO;
            this.Fields = fields;
        }

        public SemanticVersionDTO(IEnumerable<Guid> componentUuids,
                IDefinitionForSemantic definitionForSemantic,
                IIdentifiedThing referencedComponent,
                IStamp stamp,
                IEnumerable<Object> fields) :
            this(componentUuids,
                definitionForSemantic.ComponentUuids,
                referencedComponent.ComponentUuids,
                stamp.ToChangeSetThing(),
                fields)
        {
        }

        /// <summary>
        /// Compare this with another item of same type.
        /// </summary>
        /// <param name="other">Item to compare to for equality</param>
        /// <returns> -1, 0, or 1</returns>
        public override Int32 CompareTo(SemanticVersionDTO other)
        {
            Int32 cmp = FieldCompare.CompareGuids(this.ComponentUuids, other.ComponentUuids);
            if (cmp != 0)
                return cmp;
            cmp = FieldCompare.CompareGuids(this.DefinitionForSemanticUuids, other.DefinitionForSemanticUuids);
            if (cmp != 0)
                return cmp;
            cmp = FieldCompare.CompareGuids(this.ReferencedComponentUuids, other.ReferencedComponentUuids);
            if (cmp != 0)
                return cmp;
            cmp = this.CompareItem<StampDTO>(this.StampDTO, other.StampDTO);
            if (cmp != 0)
                return cmp;

            cmp = this.Fields.Count().CompareTo(other.Fields.Count());
            if (cmp != 0)
                return cmp;

            return 0;
        }

        //@Override
        //public void jsonMarshal(Writer writer) {
        //    final JSONObject json = new JSONObject();
        //    json.put(ComponentFieldForJson.STAMP, stampDTO);
        //    json.put(FIELDS, fields);
        //    json.writeJSONString(writer);
        //}

        //@JsonSemanticVersionUnmarshaler
        //public static SemanticVersionDTO Make(JSONObject jsonObject,
        //                                      IEnumerable<Guid> componentUuids,
        //                                      IEnumerable<Guid> definitionForSemanticUuids,
        //                                      IEnumerable<Guid> referencedComponentUuids) {
        //    JSONObject jsonStampObject = (JSONObject) jsonObject.get(ComponentFieldForJson.STAMP);
        //    return new SemanticVersionDTO(componentUuids,
        //            definitionForSemanticUuids,
        //            referencedComponentUuids,
        //            StampDTO.Make(jsonStampObject),
        //            jsonObject.asImmutableObjectList(FIELDS));
        //}

        //@SemanticVersionUnmarshaler
        public static SemanticVersionDTO Make(TinkarInput input,
                                              IEnumerable<Guid> componentUuids,
                                              IEnumerable<Guid> definitionForSemanticUuids,
                                              IEnumerable<Guid> referencedComponentUuids)
        {
            CheckMarshalVersion(input, MarshalVersion);
            return new SemanticVersionDTO(componentUuids,
                    definitionForSemanticUuids,
                    referencedComponentUuids,
                    StampDTO.Make(input),
                    input.ReadObjects());
        }

        public void Marshal(TinkarOutput output)
        {
            WriteMarshalVersion(output, MarshalVersion);
            this.StampDTO.Marshal(output);
            output.WriteObjectList(this.Fields);
        }
    }
}