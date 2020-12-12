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
using System.Collections.Generic;

namespace Tinkar
{
    /// <summary>
    /// Tinkar IdentifiedThing record.
    /// </summary>
    public record IdentifiedThingDTO :
        BaseDTO<IdentifiedThingDTO>,
        IIdentifiedThing,
        IChangeSetThing
    {
        /// <summary>
        /// Gets Component UUIDs.
        /// </summary>
        public IEnumerable<Guid> ComponentUuids { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifiedThingDTO"/> class.
        /// </summary>
        /// <param name="componentUuids">ComponentUuids.</param>
        public IdentifiedThingDTO(IEnumerable<Guid> componentUuids)
        {
            this.ComponentUuids = componentUuids;
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="other">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public override Int32 CompareTo(IdentifiedThingDTO other) =>
            FieldCompare.CompareGuids(this.ComponentUuids, other.ComponentUuids);
    }
}