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
    /// Stamp record.
    /// </summary>
    public record StampDTO : ComponentDTO,
        IDTO,
        IJsonMarshalable,
        IMarshalable,
        IStamp
    {
        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public const String JSONCLASSNAME = "StampDTO";

        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public override String JsonClassName => JSONCLASSNAME;

        /// <summary>
        /// Gets Status UUIDs.
        /// </summary>
        public IPublicId StatusPublicId { get; init; }

        /// <summary>
        /// Gets Author UUIDs.
        /// </summary>
        public IPublicId AuthorPublicId { get; init; }

        /// <summary>
        /// Gets Module UUIDs.
        /// </summary>
        public IPublicId ModulePublicId { get; init; }

        /// <summary>
        /// Gets Path UUIDs.
        /// </summary>
        public IPublicId PathPublicId { get; init; }

        /// <summary>
        /// Gets Time.
        /// </summary>
        public DateTime Time { get; init; }

        /// <summary>
        /// Gets Status.
        /// </summary>
        public IConcept Status => new ConceptDTO(this.StatusPublicId);

        /// <summary>
        /// Gets Author.
        /// </summary>
        public IConcept Author => new ConceptDTO(this.AuthorPublicId);

        /// <summary>
        /// Gets Module.
        /// </summary>
        public IConcept Module => new ConceptDTO(this.ModulePublicId);

        /// <summary>
        /// Gets Path.
        /// </summary>
        public IConcept Path => new ConceptDTO(this.PathPublicId);

        /// <summary>
        /// Initializes a new instance of the <see cref="StampDTO"/> class.
        /// </summary>
        /// <param name = "publicId" > Public id(component ids).</param>
        /// <param name="statusPublicId">StatusPublicId.</param>
        /// <param name="time">Time.</param>
        /// <param name="authorPublicId">AuthorPublicId.</param>
        /// <param name="modulePublicId">ModulePublicId.</param>
        /// <param name="pathPublicId">PathPublicId.</param>
        public StampDTO(
            IPublicId publicId,
            IPublicId statusPublicId,
            DateTime time,
            IPublicId authorPublicId,
            IPublicId modulePublicId,
            IPublicId pathPublicId) : base(publicId)
        {
            this.StatusPublicId = statusPublicId;
            this.Time = time;
            this.AuthorPublicId = authorPublicId;
            this.ModulePublicId = modulePublicId;
            this.PathPublicId = pathPublicId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StampDTO"/> class
        /// from json stream.
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        public StampDTO(JObject jObj) : base(jObj)
        {
            this.StatusPublicId = jObj.ReadPublicId(ComponentFieldForJson.STATUS_PUBLIC_ID);
            this.Time = jObj.ReadInstant(ComponentFieldForJson.TIME);
            this.AuthorPublicId = jObj.ReadPublicId(ComponentFieldForJson.AUTHOR_PUBLIC_ID);
            this.ModulePublicId = jObj.ReadPublicId(ComponentFieldForJson.MODULE_PUBLIC_ID);
            this.PathPublicId = jObj.ReadPublicId(ComponentFieldForJson.PATH_PUBLIC_ID);
        }

        /// <summary>
        /// Compare this with another item of same type.
        /// </summary>
        /// <param name="otherObject">Item to compare to for equality.</param>
        /// <returns> -1, 0, or 1.</returns>
        public override Int32 CompareTo(Object otherObject)
        {
            StampDTO other = otherObject as StampDTO;
            if (other == null)
                return -1;

            Int32 cmp = base.CompareTo(other);
            if (cmp != 0)
                return cmp;

            cmp = this.StatusPublicId.CompareTo(other.StatusPublicId);
            if (cmp != 0)
                return cmp;
            cmp = this.Time.CompareTo(other.Time);
            if (cmp != 0)
                return cmp;
            cmp = this.AuthorPublicId.CompareTo(other.AuthorPublicId);
            if (cmp != 0)
                return cmp;
            cmp = this.ModulePublicId.CompareTo(other.ModulePublicId);
            if (cmp != 0)
                return cmp;
            cmp = this.PathPublicId.CompareTo(other.PathPublicId);
            if (cmp != 0)
                return cmp;
            return 0;
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        /// <returns>new DTO item.</returns>
        public static StampDTO Make(TinkarInput input) =>
            new StampDTO(
                input.GetPublicId(),
                input.GetPublicId(),
                input.GetInstant(),
                input.GetPublicId(),
                input.GetPublicId(),
                input.GetPublicId());

        /// <summary>
        /// Static method to Create DTO item from json stream.
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        /// <returns>Deserialized Stamp record.</returns>
        public static StampDTO Make(JObject jObj) =>
            new StampDTO(jObj);

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public override void MarshalFields(TinkarOutput output)
        {
            base.MarshalFields(output);
            output.WritePublicId(this.StatusPublicId);
            output.WriteInstant(this.Time);
            output.WritePublicId(this.AuthorPublicId);
            output.WritePublicId(this.ModulePublicId);
            output.WritePublicId(this.PathPublicId);
        }

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public override void MarshalFields(TinkarJsonOutput output)
        {
            base.MarshalFields(output);
            output.WritePublicId(ComponentFieldForJson.STATUS_PUBLIC_ID, this.StatusPublicId);
            output.WriteInstant(ComponentFieldForJson.TIME, this.Time);
            output.WritePublicId(ComponentFieldForJson.AUTHOR_PUBLIC_ID, this.AuthorPublicId);
            output.WritePublicId(ComponentFieldForJson.MODULE_PUBLIC_ID, this.ModulePublicId);
            output.WritePublicId(ComponentFieldForJson.PATH_PUBLIC_ID, this.PathPublicId);
        }
    }
}