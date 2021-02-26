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
    /// Tinkar Semantic Chronology interface.
    /// </summary>
    /// <typeparam name="TIdentifiedThing">Generic param for IChnronology interface.</typeparam>
    [JavaAttribute("SemanticChronology")]
    public interface ISemanticChronology<TIdentifiedThing> :
        IChronology<ISemanticVersion, TIdentifiedThing>,
        ISemantic
        where TIdentifiedThing : IComponent
    {
        //$default SemanticChronologyDTO toChangeSetThing()
        //{
        //    MutableList<SemanticVersionDTO> changeSetVersions = Lists.mutable.ofInitialCapacity(versions().size());
        //    for (SemanticVersion semanticVersion :
        //    versions()) {
        //        changeSetVersions.add(semanticVersion.toChangeSetThing());
        //    }
        //    return new SemanticChronologyDTO(componentUuids(),
        //        definitionForSemantic(),
        //        referencedComponent(),
        //        changeSetVersions.toImmutable());
        //}
    }
}
