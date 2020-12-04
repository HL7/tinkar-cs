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
    public record SemanticVersionDTO : BaseDTO, 
        ISemanticVersion,
        IChangeSetThing,
        IJsonMarshalable,
        IMarshalable
    {
        private const int MarshalVersion = 1;

        public IEnumerable<Object> Fields { get; init; }

        public IIdentifiedThing ReferencedComponent { get; init; }

        public IDefinitionForSemantic DefinitionForSemantic { get; init; }

        public IEnumerable<Guid> ComponentUuids { get; init; }

        public IStamp Stamp { get; init; }

        //$public SemanticVersionDTO(IEnumerable<Guid> componentUuids, DefinitionForSemantic definitionForSemantic,
        //                          IdentifiedThing referencedComponent, Stamp stamp, IEnumerable<Object> fields) {
        //    this(componentUuids,
        //            definitionForSemantic.componentUuids(),
        //            referencedComponent.componentUuids(),
        //            stamp.toChangeSetThing(), fields);
        //}

        //@Override
        //public IdentifiedThing referencedComponent() {
        //    return new IdentifiedThingDTO(referencedComponentUuids);
        //}

        //@Override
        //public DefinitionForSemantic definitionForSemantic() {
        //    return new DefinitionForSemanticDTO(definitionForSemanticUuids);
        //}

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
        //public static SemanticVersionDTO make(TinkarInput input,
        //                                      IEnumerable<Guid> componentUuids,
        //                                      IEnumerable<Guid> definitionForSemanticUuids,
        //                                      IEnumerable<Guid> referencedComponentUuids) {
        //    try {
        //        int objectMarshalVersion = input.ReadInt();
        //        if (objectMarshalVersion == marshalVersion) {
        //            return new SemanticVersionDTO(componentUuids,
        //                    definitionForSemanticUuids,
        //                    referencedComponentUuids,
        //                    StampDTO.make(in),
        //                    input.ReadImmutableObjectList());
        //        } else {
        //            throw new UnsupportedOperationException("Unsupported version: " + objectMarshalVersion);
        //        }
        //    } catch (Exception ex) {
        //        throw new UncheckedIOException(ex);
        //    }
        //}

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