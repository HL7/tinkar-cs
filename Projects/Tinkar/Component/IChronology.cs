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
    /// <summary>
    /// Chronology interaface.
    /// </summary>
    /// <typeparam name="TVersion">Version type.</typeparam>
    /// <typeparam name="TIdentifiedThing">Thing type.</typeparam>
    [JavaAttribute("Chronology")]
    public interface IChronology<TVersion, TIdentifiedThing> : IComponent
        where TVersion : IVersion
        where TIdentifiedThing : IComponent
    {
        /// <summary>
        /// Gets ChronologySet.
        /// </summary>
        TIdentifiedThing ChronologySet { get; }

        /// <summary>
        /// Gets versions.
        /// </summary>
        IEnumerable<TVersion> Versions { get; }
    }
}