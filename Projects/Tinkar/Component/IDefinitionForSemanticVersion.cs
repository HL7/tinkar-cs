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
     * @author KWA
     */
    public interface IDefinitionForSemanticVersion : IVersion, IDefinitionForSemantic
    {
        IEnumerable<IFieldDefinition> FieldDefinitions { get; }
        IConcept referencedComponentPurpose { get; }

        //$default DefinitionForSemanticVersionDTO toChangeSetThing() {
        //    MutableList<FieldDefinitionDTO> fields = Lists.mutable.ofInitialCapacity(fieldDefinitions().size());
        //    for (FieldDefinition fieldDefinition : fieldDefinitions()) {
        //        fields.add(fieldDefinition.toChangeSetThing());
        //    }

        //    return new DefinitionForSemanticVersionDTO(
        //            componentUuids(),
        //            stamp().toChangeSetThing(),
        //            referencedComponentPurpose().componentUuids(),
        //            fields.toImmutable());
        //}
    }
}
