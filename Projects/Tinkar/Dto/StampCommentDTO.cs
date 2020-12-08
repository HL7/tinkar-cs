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
        private const int MarshalVersion = 1;

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

        ///**
        //    * Marshal method for StampCommentDTO using JSON
        //    * @param writer
        //    */
        //@Override
        //public void jsonMarshal(Writer writer)
        //{
        //	final JSONObject json = new JSONObject();
        //	json.put(ComponentFieldForJson.CLASS, this.getClass().getCanonicalName());
        //	json.put(ComponentFieldForJson.STAMP, stampDTO);
        //	json.put(ComponentFieldForJson.COMMENT, comment);
        //	json.writeJSONString(writer);
        //}

        ///**
        //    * Unmarshal method for StampCommentDTO using JSON
        //    * @param jsonObject
        //    * @return
        //    */
        //@JsonChronologyUnmarshaler
        //public static StampCommentDTO Make(JSONObject jsonObject)
        //{
        //	return new StampCommentDTO(
        //			StampDTO.Make((JSONObject)jsonObject.get(ComponentFieldForJson.STAMP)),
        //			(String)jsonObject.get(ComponentFieldForJson.COMMENT));
        //}

        ///**
        //    * Unarshal method for StampCommentDTO
        //    * @param in
        //    * @return
        //    */

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static StampCommentDTO Make(TinkarInput input)
        {
            CheckMarshalVersion(input, MarshalVersion);
            return new StampCommentDTO(
                StampDTO.Make(input),
                input.ReadUTF());
        }

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream</param>
        public void Marshal(TinkarOutput output)
        {
            WriteMarshalVersion(output, MarshalVersion);
            this.StampDTO.Marshal(output);
            output.WriteUTF(this.Comment);
        }
    }
}