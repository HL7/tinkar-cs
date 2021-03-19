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
    /// Chronology interaface.
    /// </summary>
    /// <typeparam name="TVersion">Version type.</typeparam>
    /// <typeparam name="TComponent">Component type.</typeparam>
    [JavaAttribute("Chronology")]
    public interface IChronology<TVersion, TComponent> : IComponent
        where TVersion : IVersion
        where TComponent : IComponent
    {
        /// <summary>
        /// Gets ChronologySet.
        /// </summary>
        TComponent ChronologySet { get; }

        /// <summary>
        /// Gets versions.
        /// </summary>
        ImmutableArray<TVersion> Versions { get; }
    }
}