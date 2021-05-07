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

namespace Tinkar.Dto
{
    /// <summary>
    /// Definitions used in JSON serialization.
    /// </summary>
    public static class ComponentFieldForJson
    {
        /// <summary>
        /// Name of JSON property that stores author PublicId.
        /// </summary>
        public const String AUTHOR_PUBLIC_ID = "authorPublicId";

        /// <summary>
        /// Name of JSON property that stores chronology set public id.
        /// </summary>
        public const String CHRONOLOGY_SET_PUBLIC_ID = "chronologySetPublicID";

        /// <summary>
        /// Name of JSON property that stores class name.
        /// </summary>
        public const String CLASS = "class";

        /// <summary>
        /// Name of JSON property that stores comment string.
        /// </summary>
        public const String COMMENT = "comment";

        /// <summary>
        /// Name of JSON property that stores component public id.
        /// </summary>
        public const String COMPONENT_PUBLIC_ID = "publicId";

        /// <summary>
        /// Name of JSON property that stores concept versions.
        /// </summary>
        public const String CONCEPT_VERSIONS = "conceptVersions";

        /// <summary>
        /// Name of JSON property that stores data type public id.
        /// </summary>
        public const String DATATYPE_PUBLIC_ID = "dataTypePublicId";

        /// <summary>
        /// Name of JSON property that stores type pattern public id.
        /// </summary>
        public const String TYPE_PATTERN_PUBLIC_ID = "typePatternPublicId";

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
        /// Name of JSON property that stores meaning public id.
        /// </summary>
        public const String MEANING_PUBLIC_ID = "meaningPublicId";

        /// <summary>
        /// Name of JSON property that stores module public id.
        /// </summary>
        public const String MODULE_PUBLIC_ID = "modulePublicId";

        /// <summary>
        /// Name of JSON property that stores path public id.
        /// </summary>
        public const String PATH_PUBLIC_ID = "pathPublicId";

        /// <summary>
        /// Name of JSON property that stores purpose UUIDs.
        /// </summary>
        public const String PURPOSE_PUBLIC_ID = "purposePublicId";

        /// <summary>
        /// Name of JSON property that stores referenced component meaning public id.
        /// </summary>
        public const String REFERENCED_COMPONENT_MEANING_PUBLIC_ID = "referencedComponentMeaningPublicId";

        /// <summary>
        /// Name of JSON property that stores referenced component purpose public id.
        /// </summary>
        public const String REFERENCED_COMPONENT_PURPOSE_PUBLIC_ID = "referencedComponentPurposePublicId";

        /// <summary>
        /// Name of JSON property that stores referenced component public id.
        /// </summary>
        public const String REFERENCED_COMPONENT_PUBLIC_ID = "referencedComponentPublicId";

        /// <summary>
        /// Name of JSON property that stores stamp record.
        /// </summary>
        public const String STAMP = "stamp";

        /// <summary>
        /// Name of JSON property that stores status public id.
        /// </summary>
        public const String STATUS_PUBLIC_ID = "statusPublicId";

        /// <summary>
        /// Name of JSON property that stores time field.
        /// </summary>
        public const String TIME = "time";

        /// <summary>
        /// Name of JSON property that stores versions record.
        /// </summary>
        public const String VERSIONS = "versions";

        /// <summary>
        /// Name of JSON property that stores vertex id.
        /// </summary>
        public const String VERTEX_ID = "vertexId";

        /// <summary>
        /// Name of JSON property that stores vertex meaning.
        /// </summary>
        public const String VERTEX_MEANING = "vertexMeaning";

        /// <summary>
        /// Name of JSON property that stores vertex index.
        /// </summary>
        public const String VERTEX_INDEX = "vertexIndex";

        /// <summary>
        /// Name of JSON property that stores vertex properties.
        /// </summary>
        public const String VERTEX_PROPERTIES = "vertexProperties";
    }
}
