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
using System.Collections.Immutable;

namespace Tinkar
{
    /// <summary>
    /// Defines the fields and purpose of a Semantic Version.
    /// SemanticVersion instances all reference a ITypePatternVersion
    /// to define the expected fields of that Semantic Version.
    /// </summary>
    [JavaAttribute("TypePatternVersion")]
    public interface ITypePatternVersion<TFieldDefinition> : IVersion, ITypePattern
        where TFieldDefinition : IFieldDefinition
    {
        /// <summary>
        /// Gets the SemanticVersion field definitions.
        /// </summary>
        ImmutableArray<TFieldDefinition> FieldDefinitions { get; }

        /// <summary>
        /// Gets the purpose of referenced component.
        /// </summary>
        IConcept ReferencedComponentPurpose { get; }

        /// <summary>
        /// Gets the meaning of referenced component.
        /// </summary>
        IConcept ReferencedComponentMeaning { get; }
    }
}
