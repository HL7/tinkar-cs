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
using Newtonsoft.Json.Linq;

namespace Tinkar
{
    /// <summary>
    /// ConceptVersion record.
    /// </summary>
    public record ConceptVersionDTO : VersionDTO,
        IConceptVersion,
        IDTO,
        IJsonMarshalable,
        IMarshalable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptVersionDTO"/> class.
        /// </summary>
        /// <param name = "componentPublicId" > Public id(component ids).</param>
        /// <param name="stampDTO">Stamp.</param>
        public ConceptVersionDTO(IPublicId componentPublicId, StampDTO stampDTO) : base(componentPublicId, stampDTO)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptVersionDTO"/> class.
        /// </summary>
        /// <param name = "conceptVersion" > Concept Version DTO.</param>
        public ConceptVersionDTO(IConceptVersion conceptVersion) : base(conceptVersion.PublicId, StampDTO.Make(conceptVersion.Stamp))
        {
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="otherObject">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public override Int32 CompareTo(Object otherObject)
        {
            ConceptVersionDTO other = otherObject as ConceptVersionDTO;
            if (other == null)
                return -1;

            Int32 cmp = base.CompareTo(other);
            if (cmp != 0)
                return cmp;
            return 0;
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        /// <param name="publicId">Public id (component ids).</param>
        /// <returns>new DTO item.</returns>
        public static ConceptVersionDTO Make(TinkarInput input, IPublicId publicId) => 
            new ConceptVersionDTO(publicId, StampDTO.Make(input));

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="jsonObject">JSON parent container.</param>
        /// <param name="publicId">Public id (component ids).</param>
        /// <returns>new DTO item.</returns>
        public static ConceptVersionDTO Make(JObject jsonObject, IPublicId publicId) => 
            new ConceptVersionDTO(publicId,
                StampDTO.Make((JObject) jsonObject[ComponentFieldForJson.STAMP]));

        /// <summary>
        /// Marshal all fields to binary output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public virtual void Marshal(TinkarOutput output)
        {
            this.StampDTO.Marshal(output);
        }

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public virtual void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WritePropertyName(ComponentFieldForJson.STAMP);
            this.StampDTO.Marshal(output);
            output.WriteEndObject();
        }
    }
}
