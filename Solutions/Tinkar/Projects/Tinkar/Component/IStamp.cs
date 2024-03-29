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
    /// Tinkar Stamp interface.
    /// </summary>
    [JavaAttribute("Stamp")]
    public interface IStamp : IComponent, IComparable, IEquivalent
    {
        /// <summary>
        /// Gets stamp status.
        /// </summary>
        IConcept Status { get; }

        /// <summary>
        /// Gets stamp time.
        /// </summary>
        DateTime Time { get; }

        /// <summary>
        /// Gets stamp author.
        /// </summary>
        IConcept Author { get; }

        /// <summary>
        /// Gets stamp module.
        /// </summary>
        IConcept Module { get; }

        /// <summary>
        /// Gets stamp path.
        /// </summary>
        IConcept Path { get; }
    }
}