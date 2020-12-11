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
using Newtonsoft.Json.Linq;
using System;

namespace Tinkar
{
    /**
	 *
	 * @author kec
	 */
    public record StampCommentDTO : BaseDTO<StampCommentDTO>,
        IChangeSetThing,
        IJsonMarshalable,
        IMarshalable,
        IStampComment
    {
        /// <summary>
        /// Version of marshalling code.
        /// If code is modified in a way that renders old serialized data
        /// non-conformant, then this number should be incremented.
        /// </summary>
        private const int MarshalVersion = 1;

        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        private const String JsonClassName = "StampCommentDTO";

        /// <summary>
        /// DTO for Stamp;
        /// </summary>
        public StampDTO StampDTO { get; init; }

        /// <summary>
        /// Implementation of IStampComment.Stamp
        /// </summary>
        public IStamp Stamp => this.StampDTO;

        /// <summary>
        /// Implementation of IStampComment.Comment
        /// </summary>
        public String Comment { get; init; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="stampDTO">StampDTO</param>
        /// <param name="comment">Comment</param>
        public StampCommentDTO(StampDTO stampDTO, String comment)
        {
            this.StampDTO = stampDTO;
            this.Comment = comment;
        }

        /// <summary>
        /// Create item from binary stream
        /// </summary>
        public StampCommentDTO(TinkarInput input)
        {
            input.CheckMarshalVersion(MarshalVersion);
            this.StampDTO = new StampDTO(input);
            this.Comment = input.ReadUTF();
        }

        /// <summary>
        /// Create item from json stream
        /// </summary>
        public StampCommentDTO(JObject jObj)
        {
            this.StampDTO = new StampDTO(jObj.ReadToken<JObject>(ComponentFieldForJson.STAMP));
            this.Comment = jObj.ReadString(ComponentFieldForJson.COMMENT);
        }

        /// <summary>
        /// Compare this with another item of same type.
        /// </summary>
        /// <param name="other">Item to compare to for equality</param>
        /// <returns> -1, 0, or 1</returns>
        public override Int32 CompareTo(StampCommentDTO other)
        {
            Int32 cmp = FieldCompare.CompareItem<StampDTO>(this.StampDTO, other.StampDTO);
            if (cmp != 0)
                return cmp;
            cmp = this.Comment.CompareTo(other.Comment);
            if (cmp != 0)
                return cmp;
            return 0;
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static StampCommentDTO Make(TinkarInput input) =>
            new StampCommentDTO(input);

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream</param>
        public void Marshal(TinkarOutput output)
        {
            output.WriteMarshalVersion(MarshalVersion);
            this.StampDTO.Marshal(output);
            output.WriteUTF(this.Comment);
        }

        /// <summary>
        /// Static method to Create DTO item from json stream.
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static StampCommentDTO Make(JObject jObj) =>
            new StampCommentDTO(jObj);

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream</param>
        public void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(JsonClassName);
            output.WritePropertyName(ComponentFieldForJson.STAMP);
            this.StampDTO.Marshal(output);
            output.WriteUTF(ComponentFieldForJson.COMMENT, this.Comment);
            output.WriteEndObject();
        }
    }
}