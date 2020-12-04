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

namespace Tinkar
{
    /**
	 *
	 * @author kec
	 */
    public record SemanticVersionDTO(
                IEnumerable<Guid> ComponentUuids,
                IEnumerable<Guid> DefinitionForSemanticUuids,
                IEnumerable<Guid> ReferencedComponentUuids,
                StampDTO StampDTO,
                IEnumerable<Object> Fields
            ) : BaseDTO,
        ISemanticVersion,
        IChangeSetThing,
        IJsonMarshalable,
        IMarshalable
    {
        private const int MarshalVersion = 1;

        //$public SemanticVersionDTO(IEnumerable<Guid> componentUuids, DefinitionForSemantic definitionForSemantic,
        //                          IdentifiedThing referencedComponent, Stamp stamp, IEnumerable<Object> fields) {
        //    this(componentUuids,
        //            definitionForSemantic.componentUuids(),
        //            referencedComponent.componentUuids(),
        //            stamp.toChangeSetThing(), fields);
        //}

        public IStamp Stamp => this.StampDTO;
        public IIdentifiedThing ReferencedComponent => new IdentifiedThingDTO(this.ReferencedComponentUuids);
        public IDefinitionForSemantic DefinitionForSemantic => new DefinitionForSemanticDTO(this.DefinitionForSemanticUuids);

        //@Override
        //public void jsonMarshal(Writer writer) {
        //    final JSONObject json = new JSONObject();
        //    json.put(ComponentFieldForJson.STAMP, stampDTO);
        //    json.put(FIELDS, fields);
        //    json.writeJSONString(writer);
        //}

        //@JsonSemanticVersionUnmarshaler
        //public static SemanticVersionDTO make(JSONObject jsonObject,
        //                                      IEnumerable<Guid> componentUuids,
        //                                      IEnumerable<Guid> definitionForSemanticUuids,
        //                                      IEnumerable<Guid> referencedComponentUuids) {
        //    JSONObject jsonStampObject = (JSONObject) jsonObject.get(ComponentFieldForJson.STAMP);
        //    return new SemanticVersionDTO(componentUuids,
        //            definitionForSemanticUuids,
        //            referencedComponentUuids,
        //            StampDTO.make(jsonStampObject),
        //            jsonObject.asImmutableObjectList(FIELDS));
        //}

        //@SemanticVersionUnmarshaler
        public static SemanticVersionDTO Make(TinkarInput input,
                                              IEnumerable<Guid> componentUuids,
                                              IEnumerable<Guid> definitionForSemanticUuids,
                                              IEnumerable<Guid> referencedComponentUuids)
        {
            CheckMarshallVersion(input, MarshalVersion);
            return new SemanticVersionDTO(componentUuids,
                    definitionForSemanticUuids,
                    referencedComponentUuids,
                    StampDTO.Make(input),
                    input.ReadImmutableObjectList());
        }

        //@Override
        //@Marshaler
        //public void marshal(TinkarOutput out) {
        //    try {
        //        out.writeInt(marshalVersion);
        //        stampDTO.marshal(out);
        //        out.writeObjectList(fields);
        //    } catch (Exception ex) {
        //        throw new UncheckedIOException(ex);
        //    }
        //}

        //@Override
        //public Stamp stamp() {
        //    return stampDTO;
        //}
    }
}