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
    public class TinkarOutput
    {
        private BinaryWriter writer;

        public TinkarOutput(Stream outStream)
        {
            this.writer = new BinaryWriter(outStream);
        }

        /// <summary>
        /// Write boolean to output stream.
        /// </summary>
        /// <returns></returns>
        public void WriteBoolean(Boolean value) => this.writer.Write(value);

        /// <summary>
        /// Write byte array to output stream.
        /// </summary>
        /// <returns></returns>
        public void WriteByteArray(Byte[] value) => this.writer.Write(value);

        /// <summary>
        /// Write float to output stream.
        /// </summary>
        /// <returns></returns>
        public void WriteSingle(float value)
        {
            Int32 v = BitConverter.SingleToInt32Bits(value);
            this.writer.Write(IPAddress.HostToNetworkOrder(v));
        }

        /// <summary>
        /// Write little endian Int32 to output stream.
        /// </summary>
        /// <returns></returns>
        public void WriteInt32(Int32 value) =>
            this.writer.Write(IPAddress.HostToNetworkOrder(value));

        /// <summary>
        /// Write little endian Int64 to output stream.
        /// </summary>
        /// <returns></returns>
        public void WriteInt64(Int64 value) =>
            this.writer.Write(IPAddress.HostToNetworkOrder(value));

        /**
         * Always convert to UTC...
         * @param instant 
         */
        public void WriteInstant(DateTime instant)
        {
            this.WriteInt64(instant.EpochSecond());
            this.WriteInt32(instant.Nano());
        }

        public void WriteMarshalableList(IEnumerable<IMarshalable> items)
        {
            this.WriteInt32(items.Count());
            foreach (IMarshalable version in items)
                version.Marshal(this);
        }

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
                    this.WriteSingle((Single) item);
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
                    this.WriteObjectList(item);
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

                case DefinitionForSemanticDTO item:
                    this.WriteFieldType(FieldDataType.DefinitionForSymanticType);
                    item.Marshal(this);
                    break;

                case DefinitionForSemanticChronologyDTO item:
                    this.WriteFieldType(FieldDataType.DefinitionForSymanticType);
                    item.Marshal(this);
                    break;

                case IIdentifiedThing item:
                    this.WriteFieldType(FieldDataType.IdentifiedThingType);
                    this.WriteIdentifiedThing(item);
                    break;

                case DigraphDTO item:
                    this.WriteDigraph();
                    break;

                default:
                    throw new NotSupportedException($"Can not serialize type {field.GetType().Name}");
            }
        }


        private void WriteIdentifiedThing(IIdentifiedThing thing) => this.WriteUuidList(thing.ComponentUuids);

        /// <summary>
        /// Read string.
        /// Note: BinaryWriter.WriteString is supposed to be identical to java WriteUTF().
        /// </summary>
        /// <returns></returns>
        public void WriteUTF(String s) => this.writer.Write(s);

        private void WriteDigraph() => throw new UnsupportedOperationException("WriteDigraph");
        private void WriteFieldType(FieldDataType fieldType) => this.writer.Write((byte)fieldType);
        private void WriteByte(byte value) => this.writer.Write(value);

        public void WriteUuidList(IEnumerable<Guid> statusUuids)
        {
            this.WriteInt32(statusUuids.Count());
            foreach (Guid statusUuid in statusUuids)
                this.writer.Write(statusUuid.ToByteArray());
        }

        public void WriteObjectList(IEnumerable<Object> fields)
        {
            this.WriteInt32(fields.Count());
            foreach (Object field in fields)
                this.WriteField(field);
        }
    }
}
