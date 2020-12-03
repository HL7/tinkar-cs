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
    public record StampCommentDTO : IChangeSetThing,
    IJsonMarshalable,
    IMarshalable,
    IStampComment
    {
        private const int marshalVersion = 1;

        public IStamp Stamp {get; init; }

        public String Comment {get; init; }

        //$@Override
        //public Stamp stamp()
        //{
        //	return stampDTO;
        //}

        //@Override
        //public String comment()
        //{
        //	return comment;
        //}

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
        //public static StampCommentDTO make(JSONObject jsonObject)
        //{
        //	return new StampCommentDTO(
        //			StampDTO.make((JSONObject)jsonObject.get(ComponentFieldForJson.STAMP)),
        //			(String)jsonObject.get(ComponentFieldForJson.COMMENT));
        //}

        ///**
        //    * Unarshal method for StampCommentDTO
        //    * @param in
        //    * @return
        //    */
        //@Unmarshaler
        //public static StampCommentDTO make(TinkarInput in)
        //{
        //	try
        //	{
        //		int objectMarshalVersion = in.readInt();
        //		if (objectMarshalVersion == marshalVersion)
        //		{
        //			return new StampCommentDTO(
        //					StampDTO.make(in),
        //					in.readUTF());
        //		}
        //		else
        //		{
        //			throw new UnsupportedOperationException("Unsupported version: " + objectMarshalVersion);
        //		}
        //	}
        //	catch (IOException ex)
        //	{
        //		throw new UncheckedIOException(ex);
        //	}
        //}

        ///**
        //    * Marshal method for StampCommentDTO
        //    * @param out
        //    */
        //@Override
        //@Marshaler

        //public void marshal(TinkarOutput out)
        //{
        //	try
        //	{
        //           out.writeInt(marshalVersion);
        //		stampDTO.marshal(out);
        //           out.writeUTF(comment);
        //	}
        //	catch (IOException ex)
        //	{
        //		throw new UncheckedIOException(ex);
        //	}
        //}
    }
}