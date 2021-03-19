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

namespace Tinkar
{
    /// <summary>
    /// TypePatternChronology interace.
    /// </summary>
    /// <typeparam name="TTypePatternVersion">Generic type pattern  for semantic version. </typeparam>
    /// <typeparam name="TFieldDefinition">Generic type for Field definition. </typeparam>
    /// <typeparam name="TComponent">Generic type for component definition. </typeparam>
    [JavaAttribute("TypePatternChronology")]
    public interface ITypePatternChronology<TTypePatternVersion, TFieldDefinition, TComponent> :
        IChronology<TTypePatternVersion, TComponent>,
        ITypePattern
        where TTypePatternVersion : ITypePatternVersion<TFieldDefinition>
        where TFieldDefinition : IFieldDefinition
        where TComponent : IComponent
    {
    }
}