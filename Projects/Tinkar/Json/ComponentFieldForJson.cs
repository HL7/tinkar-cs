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

namespace Tinkar
{
    /// <summary>
    /// Definitions used in JSON serialization.
    /// </summary>
    public static class ComponentFieldForJson
    {
        /// <summary>
        /// Name of JSON property that stores author UUIDs.
        /// </summary>
        public const String AUTHOR_UUIDS = "authorUuids";

        /// <summary>
        /// Name of JSON property that stores chronology set UUIDs.
        /// </summary>
        public const String CHRONOLOGY_SET_UUIDS = "chronologySetUuids";

        /// <summary>
        /// Name of JSON property that stores class name.
        /// </summary>
        public const String CLASS = "class";

        /// <summary>
        /// Name of JSON property that stores comment string.
        /// </summary>
        public const String COMMENT = "comment";

        /// <summary>
        /// Name of JSON property that stores component UUIDs.
        /// </summary>
        public const String COMPONENT_UUIDS = "componentUuids";

        /// <summary>
        /// Name of JSON property that stores concept version UUIDs.
        /// </summary>
        public const String CONCEPT_VERSIONS = "conceptVersions";

        /// <summary>
        /// Name of JSON property that stores data type UUIDs.
        /// </summary>
        public const String DATATYPE_UUIDS = "dataTypeUuids";

        /// <summary>
        /// Name of JSON property that stores definition for semantic UUIDs.
        /// </summary>
        public const String DEFINITION_FOR_SEMANTIC_UUIDS = "definitionForSemanticUuids";

        /// <summary>
        /// Name of JSON property that stores definition version records.
        /// </summary>
        public const String DEFINITION_VERSIONS = "definitionVersions";

        /// <summary>
        /// Name of JSON property that stores fiekds array.
        /// </summary>
        public const String FIELDS = "fields";

        /// <summary>
        /// Name of JSON property that stores field definition records UUIDs.
        /// </summary>
        public const String FIELD_DEFINITIONS = "fieldDefinitions";

        /// <summary>
        /// Name of JSON property that stores module UUIDs.
        /// </summary>
        public const String MODULE_UUIDS = "moduleUuids";

        /// <summary>
        /// Name of JSON property that stores path UUIDs.
        /// </summary>
        public const String PATH_UUIDS = "pathUuids";

        /// <summary>
        /// Name of JSON property that stores purpose UUIDs.
        /// </summary>
        public const String PURPOSE_UUIDS = "purposeUuids";

        /// <summary>
        /// Name of JSON property that stores referenced component purpose UUIDs.
        /// </summary>
        public const String REFERENCED_COMPONENT_PURPOSE_UUIDS = "referencedComponentPurposeUuids";

        /// <summary>
        /// Name of JSON property that stores referenced component UUIDs.
        /// </summary>
        public const String REFERENCED_COMPONENT_UUIDS = "referencedComponentUuids";

        /// <summary>
        /// Name of JSON property that stores stamp record.
        /// </summary>
        public const String STAMP = "stamp";

        /// <summary>
        /// Name of JSON property that stores status UUIDs.
        /// </summary>
        public const String STATUS_UUIDS = "statusUuids";

        /// <summary>
        /// Name of JSON property that stores time field.
        /// </summary>
        public const String TIME = "time";

        /// <summary>
        /// Name of JSON property that stores use UUIDs.
        /// </summary>
        public const String USE_UUIDS = "useUuids";

        /// <summary>
        /// Name of JSON property that stores versions record.
        /// </summary>
        public const String VERSIONS = "versions";
    }
}
