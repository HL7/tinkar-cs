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
using System.IO;

namespace Tinkar
{
    /// <summary>
    /// Tinkar binary output stream that writes to a byte arrray.
    /// </summary>
    public class TinkarByteArrayOutput : TinkarOutput
    {
        /// <summary>
        /// Store output in this memory stream.
        /// </summary>
        private MemoryStream byteArrayOutputStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="TinkarByteArrayOutput"/> class.
        /// </summary>
        /// <param name="byteArrayOutputStream">Underlying memory stream.</param>
        private TinkarByteArrayOutput(MemoryStream byteArrayOutputStream)
            : base(byteArrayOutputStream)
        {
            this.byteArrayOutputStream = byteArrayOutputStream;
        }

        /// <summary>
        /// Create a TinkarByteArrayOutput item.
        /// </summary>
        /// <returns>TinkarByteArrayOutput item.</returns>
        public static TinkarByteArrayOutput Make() => new TinkarByteArrayOutput(new MemoryStream());

        /// <summary>
        /// Get output stream bytes.
        /// </summary>
        /// <returns>array or read bytes.</returns>
        public byte[] GetBytes() => this.byteArrayOutputStream.ToArray();

        /// <summary>
        /// Convert output stream to an input stream
        /// containing output streams bytes.
        /// </summary>
        /// <returns>Tinkar input stream.</returns>
        public TinkarInput ToInput()
        {
            MemoryStream bais = new MemoryStream(this.GetBytes());
            return new TinkarInput(bais);
        }
    }
}
