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
	/**
		 *
		 * @author kec
		 */
	public class TinkarOutput
    {
        private BinaryWriter writer;
		public TinkarOutput(Stream outStream)
		{
            writer = new BinaryWriter(outStream);
		}

        /// <summary>
        /// Write little endian Int32 to output stream.
        /// </summary>
        /// <returns></returns>
        public void WriteInt(Int32 value)
        {
            this.writer.Write((byte) value);
            this.writer.Write((byte) (value >> 8));
            this.writer.Write((byte)(value >> 16));
            this.writer.Write((byte)(value >> 24));
		}

        /// <summary>
        /// Write little endian Int64 to output stream.
        /// </summary>
        /// <returns></returns>
        public void WriteLong(Int64 value)
        {
            this.writer.Write((byte)value);
            this.writer.Write((byte)(value >> 8));
            this.writer.Write((byte)(value >> 16));
            this.writer.Write((byte)(value >> 24));
			this.writer.Write((byte)(value >> 32));
			this.writer.Write((byte)(value >> 40));
			this.writer.Write((byte)(value >> 48));
            this.writer.Write((byte)(value >> 56));
        }

        public void WriteUuidArray(Guid[] array)
        {
            try
            {
                this.WriteInt(array.Length);
                foreach (Guid uuid in array)
                    this.writer.Write(uuid.ToByteArray());
            }
            catch (Exception ex)
            {
                throw new UncheckedIOException(ex);
            }
        }

        /**
         * Always convert to UTC...
         * @param instant 
         */
        public void WriteInstant(DateTime instant)
        {
            try
            {
                WriteLong(instant.EpochSecond());
                WriteInt(instant.Nano());
            }
            catch (Exception ex)
            {
                throw new UncheckedIOException(ex);
            }
        }

        //public void writeFieldDefinitionList(IEnumerable<FieldDefinitionDTO> fieldDefinitions) {
        //    try {
        //        writeInt(fieldDefinitions.size());
        //        for (FieldDefinitionDTO fieldDefinition: fieldDefinitions) {
        //            fieldDefinition.marshal(this);
        //        }
        //    } catch (Exception ex) {
        //        throw new UncheckedIOException(ex);
        //    }
        //}

        //public void writeConceptVersionList(IEnumerable<ConceptVersionDTO> versions) {
        //    try {
        //        writeInt(versions.size());
        //        for (ConceptVersionDTO version: versions) {
        //            version.marshal(this);
        //        }
        //    } catch (Exception ex) {
        //        throw new UncheckedIOException(ex);
        //    }
        //}

        //public void writeDefinitionForSemanticVersionList(IEnumerable<DefinitionForSemanticVersionDTO> versions) {
        //    try {
        //        writeInt(versions.size());
        //        for (DefinitionForSemanticVersionDTO version: versions) {
        //            version.marshal(this);
        //        }
        //    } catch (Exception ex) {
        //        throw new UncheckedIOException(ex);
        //    }
        //}

        //public void writeSemanticVersionList(IEnumerable<SemanticVersionDTO> versions) {
        //    try {
        //        writeInt(versions.size());
        //        for (SemanticVersionDTO version: versions) {
        //            version.marshal(this);
        //        }
        //    } catch (Exception ex) {
        //        throw new UncheckedIOException(ex);
        //    }
        //}

        //private void writeField(Object object) {
        //    FieldDataType fieldDataType = FieldDataType.getFieldDataType(object);
        //    try {
        //        switch (fieldDataType) {
        //            case BOOLEAN -> writeBoolean((boolean) object, fieldDataType);
        //            case BYTE_ARRAY -> writeByteArray((byte[]) object, fieldDataType);
        //            case DIGRAPH -> writeDigraph();
        //            case FLOAT -> writeFloat((Number) object, fieldDataType);
        //            case INTEGER -> writeInteger((Number) object, fieldDataType);
        //            case OBJECT_ARRAY -> writeObjectArray((Object[]) object, fieldDataType);
        //            case STRING -> writeString((String) object, fieldDataType);
        //            case INSTANT -> writeInstant((Instant) object, fieldDataType);
        //            case IDENTIFIED_THING -> writeIdentifiedThing((IdentifiedThing) object, fieldDataType);
        //            case CONCEPT,
        //                    CONCEPT_CHRONOLOGY,
        //                    DEFINITION_FOR_SEMANTIC,
        //                    DEFINITION_FOR_SEMANTIC_CHRONOLOGY,
        //                    SEMANTIC,
        //                    SEMANTIC_CHRONOLOGY -> writeMarshalableObject((Marshalable) object);

        //            default -> throw new UnsupportedOperationException("writeField can't handle: " + object + " " + fieldDataType);
        //        }
        //    } catch (IOException e) {
        //        throw new UncheckedIOException(e);
        //    }
        //}


        //private void writeInstant(Instant instant, FieldDataType fieldDataType) throws IOException {
        //    writeByte(fieldDataType.token);
        //    writeInstant(instant);
        //}

        //private void writeIdentifiedThing(IdentifiedThing object, FieldDataType fieldDataType) throws IOException {
        //    writeByte(fieldDataType.token);
        //    writeUuidList(object.componentUuids());
        //}
        //private void writeMarshalableObject(Marshalable object) throws IOException {
        //    FieldDataType dataType = FieldDataType.getFieldDataType(object);
        //    this.writeByte(dataType.token);
        //    object.marshal(this);
        //}

        //private void writeString(String object, FieldDataType fieldDataType) throws IOException {
        //    writeByte(fieldDataType.token);
        //    writeUTF(object);
        //}

        //private void writeObjectArray(Object[] object, FieldDataType fieldDataType) throws IOException {
        //    writeByte(fieldDataType.token);
        //    Object[] objects = object;
        //    writeInt(objects.length);
        //    for (int i = 0; i < objects.length; i++) {
        //        writeField(objects[i]);
        //    }
        //}

        //private void writeInteger(Number object, FieldDataType fieldDataType) throws IOException {
        //    writeByte(fieldDataType.token);
        //    writeInt(object.intValue());
        //}

        //private void writeFloat(Number object, FieldDataType fieldDataType) throws IOException {
        //    writeByte(fieldDataType.token);
        //    writeFloat(object.floatValue());
        //}

        //private void writeDigraph() {
        //    throw new UnsupportedOperationException();
        //}

        //private void writeByteArray(byte[] object, FieldDataType fieldDataType) throws IOException {
        //    writeByte(fieldDataType.token);
        //    writeInt(object.length);
        //    write(object);
        //}

        //private void writeBoolean(boolean object, FieldDataType fieldDataType) throws IOException {
        //    writeByte(fieldDataType.token);
        //    writeBoolean(object);
        //}

        //public void writeUuidList(IEnumerable<Guid> statusUuids) {
        //    writeUuidArray(statusUuids.toArray(new Guid[statusUuids.size()]));
        //}

        //public void writeObjectArray(Object[] fields) {
        //    try {
        //        writeInt(fields.length);
        //        for (Object object : fields) {
        //            writeField(object);
        //        }
        //    } catch (Exception ex) {
        //        throw new UncheckedIOException(ex);
        //    }
        //}

        //public void writeObjectList(IEnumerable<Object> fields) {
        //    writeObjectList(fields.castToList());
        //}

        //public void writeObjectList(List<Object> fields) {
        //    writeObjectArray(fields.toArray());
        //}
    }
}
