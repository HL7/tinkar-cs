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
using Newtonsoft.Json;

namespace Tinkar
{
    /**
     *
     * Template for JsonMarshalable implementations classes
     *
     *
     * // Using a static method rather than a constructor eliminates the need for //
     * a readResolve method, but allows the implementation to decide how // to
     * handle special cases.
     *
     * @JsonUnmarshaler public static Object Make(JSONObject jsonObject) {
     *
     * }
     *
     * @Override
     * @JsonMarshaler public void marshal(Writer out) { final JSONObject json = new
     * JSONObject(); json.put("class", this.getClass().getCanonicalName());
     *
     * json.writeJSONString(writer); throw new UnsupportedOperationException(); }
     *
     *
     *
     *
     *
     */
    public interface IJsonMarshalable
    {
        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream</param>
        void Marshal(TinkarJsonOutput output);
    }
}
