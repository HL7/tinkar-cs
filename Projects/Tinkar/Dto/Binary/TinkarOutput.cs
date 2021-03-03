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
using System.Linq;
using System.Net;

namespace Tinkar
{
    /// <summary>
    /// Tinkar binary output stream. Writes Tinkar objects to serialized
    /// binary byte stream.
    /// </summary>
    public class TinkarOutput : IDisposable
    {
        /// <summary>
        /// Binary writer that gets written to.
        /// </summary>
        private BinaryWriter writer;
        private Int32 marshalVersion;

        /// <summary>
        /// Initializes a new instance of the <see cref="TinkarOutput"/> class.
        /// </summary>
        /// <param name="outStream">Output stream.</param>
        /// <param name="marshalVersion">Marshal version number.</param>
        public TinkarOutput(Stream outStream, Int32 marshalVersion)
        {
            this.writer = new BinaryWriter(outStream);
            this.WriteInt32(marshalVersion);
            this.marshalVersion = marshalVersion;
        }

        /// <summary>
        /// Dispose. This flushes streams and releases underlying output stream.
        /// If dispose is not called, cached data is not flushed to output stream,
        /// rendering the underlying output stream truncated.
        /// </summary>
        public void Dispose()
        {
            if (this.writer != null)
            {
                this.writer.Flush();
                this.writer = null;
            }
        }

        /// <summary>
        /// Write boolean to output stream.
        /// </summary>
        /// <param name="value">value to write.</param>
        public void WriteBoolean(Boolean value) => this.writer.Write(value);

        /// <summary>
        /// Write byte array to output stream.
        /// </summary>
        /// <param name="value">value to write.</param>
        public void WriteByteArray(Byte[] value) => this.writer.Write(value);

        /// <summary>
        /// Write float to output stream.
        /// </summary>
        /// <param name="value">value to write.</param>
        public void WriteSingle(float value)
        {
            Int32 v = BitConverter.SingleToInt32Bits(value);
            this.writer.Write(IPAddress.HostToNetworkOrder(v));
        }

        /// <summary>
        /// Write little endian Int32 to output stream.
        /// </summary>
        /// <param name="value">value to write.</param>
        public void WriteInt32(Int32 value) =>
            this.writer.Write(IPAddress.HostToNetworkOrder(value));

        /// <summary>
        /// Write little endian Int64 to output stream.
        /// </summary>
        /// <param name="value">value to write.</param>
        public void WriteInt64(Int64 value) =>
            this.writer.Write(IPAddress.HostToNetworkOrder(value));

        /// <summary>
        /// Write out date time.
        /// </summary>
        /// <param name="value">value to write.</param>
        public void WriteInstant(DateTime value)
        {
            this.WriteInt64(value.EpochSecond());
            this.WriteInt32(value.Nano());
        }

        /// <summary>
        /// Write out a series of IMarshalable items.
        /// </summary>
        /// <param name="items">Items to write.</param>
        public void WriteMarshalableList(IEnumerable<IMarshalable> items)
        {
            this.WriteInt32(items.Count());
            foreach (IMarshalable version in items)
                version.Marshal(this);
        }

        /// <summary>
        /// Write sinple object field to output stream.
        /// </summary>
        /// <param name="field">Value to write.</param>
        public void WriteField(Object field)
        {
            switch (field)
            {
                case Boolean item:
                    this.WriteFieldType(FieldDataType.BooleanType);
                    this.WriteBoolean(item);
                    break;

                case byte[] item:
                    this.WriteFieldType(FieldDataType.ByteArrayType);
                    this.WriteInt32(item.Length);
                    this.WriteByteArray(item);
                    break;

                case Single item:
                    this.WriteFieldType(FieldDataType.FloatType);
                    this.WriteSingle(item);
                    break;

                case Double item:
                    this.WriteFieldType(FieldDataType.FloatType);
                    this.WriteSingle((Single)item);
                    break;

                case Int32 int32Item:
                    this.WriteFieldType(FieldDataType.IntegerType);
                    this.WriteInt32(int32Item);
                    break;

                case Int64 item:
                    this.WriteFieldType(FieldDataType.IntegerType);
                    this.WriteInt32((Int32)item);
                    break;

                case String item:
                    this.WriteFieldType(FieldDataType.StringType);
                    this.WriteUTF(item);
                    break;

                case DateTime item:
                    this.WriteFieldType(FieldDataType.InstantType);
                    this.WriteInstant(item);
                    break;

                case Object[] item:
                    this.WriteFieldType(FieldDataType.ObjectArrayType);
                    this.WriteObjects(item);
                    break;

                case ConceptDTO item:
                    this.WriteFieldType(FieldDataType.ConceptType);
                    item.Marshal(this);
                    break;

                case ConceptChronologyDTO item:
                    this.WriteFieldType(FieldDataType.ConceptChronologyType);
                    item.Marshal(this);
                    break;

                case SemanticDTO item:
                    this.WriteFieldType(FieldDataType.SemanticType);
                    item.Marshal(this);
                    break;

                case SemanticChronologyDTO item:
                    this.WriteFieldType(FieldDataType.SemanticChronologyType);
                    item.Marshal(this);
                    break;

                case PatternForSemanticChronologyDTO item:
                    this.WriteFieldType(FieldDataType.PatternForSemanticChronologyType);
                    item.Marshal(this);
                    break;

                case PatternForSemanticDTO item:
                    this.WriteFieldType(FieldDataType.PatternForSemanticType);
                    item.Marshal(this);
                    break;

                // case DigraphDTO item:
                //    this.WriteDigraph();
                //    break;
                default:
                    throw new NotSupportedException($"Can not serialize type {field.GetType().Name}");
            }
        }

        /// <summary>
        /// Write string.
        /// Note: BinaryWriter.WriteString is supposed to be identical to java WriteUTF().
        /// </summary>
        /// <param name="s">String to write.</param>
        public void WriteUTF(String s) => this.writer.Write(s);

        /// <summary>
        /// Write a stream of Uuids (guids) to output stream.
        /// </summary>
        /// <param name="statusUuids">values to write.</param>
        public void WriteUuids(IEnumerable<Guid> statusUuids)
        {
            this.WriteInt32(statusUuids.Count());
            foreach (Guid statusUuid in statusUuids)
                this.writer.Write(statusUuid.ToByteArray());
        }

        /// <summary>
        /// Write a stream of Uuids (guids) to output stream.
        /// </summary>
        /// <param name="publicId">publicId to write.</param>
        public void WritePublicId(IPublicId publicId) => WriteUuids(publicId.AsUuidArray);

        /// <summary>
        /// Write out a stream ob simple objects.
        /// </summary>
        /// <param name="fields">Values to write.</param>
        public void WriteObjects(IEnumerable<Object> fields)
        {
            this.WriteInt32(fields.Count());
            foreach (Object field in fields)
                this.WriteField(field);
        }

        /// <summary>
        /// Write marshal version to output stream.
        /// </summary>
        /// <param name="marshalVersion">value to write.</param>
        public void CheckMarshalVersion(Int32 marshalVersion)
        {
            if (this.marshalVersion != marshalVersion)
                throw new ArgumentException($"Unsupported version: {this.marshalVersion}. Require {marshalVersion}");
        }

        //private void WriteDigraph() => throw new NotImplementedException("WriteDigraph");

        /// <summary>
        /// Write field type to output stream.
        /// </summary>
        /// <param name="fieldType">value to write.</param>
        private void WriteFieldType(FieldDataType fieldType) => this.writer.Write((byte)fieldType);
    }
}
