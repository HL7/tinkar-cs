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
    public class TinkarInput
    {
        private BinaryReader reader;

        public TinkarInput(Stream inStream)
        {
            this.reader = new BinaryReader(inStream);
        }

        public IEnumerable<Guid> ReadImmutableUuidList() => this.ReadUuidArray();

        /// <summary>
        /// Read little endian Int32 from input stream.
        /// </summary>
        /// <returns></returns>
        public Int32 ReadInt()
        {
            byte[] bytes = this.reader.ReadBytes(4);
            return bytes[0] |
                   bytes[1] << 8 |
                   bytes[2] << 16 |
                   bytes[3] << 24;
        }

        /// <summary>
        /// Read little endian Int64 from input stream.
        /// </summary>
        /// <returns></returns>
        public Int64 ReadLong()
        {
            UInt64 lsInt = (UInt64)(UInt32)this.ReadInt();
            UInt64 msInt = (UInt64)(UInt32)this.ReadInt();
            return (Int64)(lsInt | msInt << 32);
        }

        public Guid[] ReadUuidArray()
        {
            try
            {
                int length = ReadInt();
                Guid[] array = new Guid[length];
                for (int i = 0; i < length; i++)
                    array[i] = new Guid(this.reader.ReadBytes(16));
                return array;
            }
            catch (Exception ex)
            {
                throw new UncheckedIOException(ex);
            }
        }

        public DateTime ReadInstant()
        {
            try
            {
                return DateTimeExtensions.FromInstant(this.ReadLong(), this.ReadInt());
            }
            catch (Exception ex)
            {
                throw new UncheckedIOException(ex);
            }
        }

        public static TinkarInput Make(byte[] buf)
        {
            MemoryStream bais = new MemoryStream(buf);
            return new TinkarInput(bais);
        }

        public static TinkarInput Make(TinkarByteArrayOutput tinkarOut)
        {
            MemoryStream bais = new MemoryStream(tinkarOut.getBytes());
            return new TinkarInput(bais);
        }

        public IEnumerable<FieldDefinitionDTO> readFieldDefinitionList()
        {
            try
            {
                int length = ReadInt();
                FieldDefinitionDTO[] array = new FieldDefinitionDTO[length];
                for (int i = 0; i < length; i++)
                {
                    //            array[i] = FieldDefinitionDTO.make(this);
                }
                return array;
            }
            catch (Exception ex)
            {
                throw new UncheckedIOException(ex);
            }
        }


        public IEnumerable<ConceptVersionDTO> ReadConceptVersionList(IEnumerable<Guid> componentUuids)
        {
            try
            {
                int length = ReadInt();
                ConceptVersionDTO[] array = new ConceptVersionDTO[length];
                for (int i = 0; i < length; i++)
                    array[i] = ConceptVersionDTO.Make(this, componentUuids);
                return array;
            }
            catch (Exception ex)
            {
                throw new UncheckedIOException(ex);
            }
        }

        //public IEnumerable<DefinitionForSemanticVersionDTO> readDefinitionForSemanticVersionList(IEnumerable<Guid> componentUuids)
        //{
        //    try
        //    {
        //        int length = ReadInt();
        //        DefinitionForSemanticVersionDTO[] array = new DefinitionForSemanticVersionDTO[length];
        //        for (int i = 0; i < length; i++)
        //        {
        //            array[i] = DefinitionForSemanticVersionDTO.make(this, componentUuids);
        //        }
        //        return Lists.immutable.of(array);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new UncheckedIOException(ex);
        //    }
        //}

        //public IEnumerable<SemanticVersionDTO> readSemanticVersionList(IEnumerable<Guid> componentUuids,
        //                                                                 IEnumerable<Guid> definitionForSemanticUuids,
        //                                                                 IEnumerable<Guid> referencedComponentUuids)
        //{
        //    try
        //    {
        //        int length = ReadInt();
        //        SemanticVersionDTO[] array = new SemanticVersionDTO[length];
        //        for (int i = 0; i < length; i++)
        //        {
        //            array[i] = SemanticVersionDTO.make(this, componentUuids,
        //                    definitionForSemanticUuids, referencedComponentUuids);
        //        }
        //        return Lists.immutable.of(array);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new UncheckedIOException(ex);
        //    }
        //}

        //public IEnumerable<Object> readImmutableObjectList()
        //{
        //    return Lists.immutable.of(readObjectArray());
        //}

        //public Object[] readObjectArray()
        //{
        //    try
        //    {
        //        int fieldCount = ReadInt();
        //        Object[] array = new Object[fieldCount];
        //        for (int i = 0; i < array.length; i++)
        //        {
        //            readObject(array, i);
        //        }
        //        return array;
        //    }
        //    catch (IOException e)
        //    {
        //        throw new UncheckedIOException(e);
        //    }
        //}

        //        private void readObject(Object[] array, int i)
        //        {
        //            try
        //            {
        //                byte token = readByte();
        //                FieldDataType dataType = FieldDataType.fromToken(token);
        //                array[i] = switch (dataType)
        //                {
        //                    case STRING -> readUTF();
        //                    case FLOAT -> readFloat();
        //                    case BOOLEAN -> readBoolean();
        //                    case BYTE_ARRAY -> readByteArray();
        //                    case IDENTIFIED_THING -> new IdentifiedThingDTO(ReadImmutableUuidList());
        //                    case INTEGER -> ReadInt();
        //                    case OBJECT_ARRAY -> readEmbeddedObjectArray();
        //                    case DIGRAPH -> throw new UnsupportedEncodingException("Can't handle DIGRAPH.");
        //                    case INSTANT -> readInstant();
        //                    case CONCEPT_CHRONOLOGY, CONCEPT, DEFINITION_FOR_SEMANTIC_CHRONOLOGY,
        //                        DEFINITION_FOR_SEMANTIC, SEMANTIC_CHRONOLOGY, SEMANTIC->unmarshal(dataType);
        //                    default -> throw new UnsupportedEncodingException(dataType.toString());
        //                };
        //            }
        //            catch (IOException e)
        //            {
        //                throw new UncheckedIOException(e);
        //            }
        //        }

        //        private Object unmarshal(FieldDataType dataType)
        //        {
        //            return switch (dataType)
        //            {
        //                case CONCEPT_CHRONOLOGY -> ConceptChronologyDTO.make(this);
        //                case CONCEPT -> ConceptDTO.make(this);
        //                case DEFINITION_FOR_SEMANTIC_CHRONOLOGY -> DefinitionForSemanticChronologyDTO.make(this);
        //                case DEFINITION_FOR_SEMANTIC -> DefinitionForSemanticDTO.make(this);
        //                case SEMANTIC_CHRONOLOGY -> SemanticChronologyDTO.make(this);
        //                case SEMANTIC -> SemanticDTO.make(this);
        //                default -> throw new UnsupportedOperationException("TinkarInput does know how to unmarshal: " + dataType);
        //            };
        //        }

        //        private Object[] readEmbeddedObjectArray() throws IOException
        //        {
        //        int size = ReadInt();
        //        Object[]
        //        objects = new Object[size];
        //        for (int j = 0; j<size; j++) {
        //            readObject(objects, j);
        //    }
        //return objects;
        //    }

        byte[] ReadByteArray() => this.reader.ReadBytes(ReadInt());
    }
}
