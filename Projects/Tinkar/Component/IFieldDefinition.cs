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
    public interface IFieldDefinition
    {
        /**
         * Underlying object type such as String or Integer.
         * @return Concept designating the data type of the defined field.
         */
        IConcept DataType { get; }

        /**
         * What the object represents: a String might be a URI,
         * a component identifier might represent a mapping, or an
         * integer might represent a coordinate.
         * @return Concept designating the purpose of the defined field.
         */
        IConcept Purpose { get; }

        /**
         * The context in which this specific field is used. Maybe it is the
         * "SNOMED code" in a mapping, or the location of an image if a URI.
         * @return Concept designating the use of the defined field.
         */
        IConcept Use { get; }

        //$default FieldDefinitionDTO toChangeSetThing()
        //{
        //    return new FieldDefinitionDTO(getDataType().componentUuids(),
        //        getPurpose().componentUuids(), getUse().componentUuids());
        //}
    }
}
