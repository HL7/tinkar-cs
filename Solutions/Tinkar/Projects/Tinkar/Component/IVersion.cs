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
    /// Tinkar Version Interface. This is the base interface
    /// for many specialized Tinkar Version interfaces.
    /// </summary>
    [JavaAttribute("Version")]
    public interface IVersion : IComponent
    {
        /// <summary>
        /// Gets stamp for version.
        /// </summary>
        IStamp Stamp { get; }
    }
}
