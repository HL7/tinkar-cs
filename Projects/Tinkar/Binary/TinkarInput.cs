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

        /// <summary>
        /// Read string.
        /// Note: BinaryReader.ReadString is supposed to be identical to java ReadUTF().
        /// </summary>
        /// <returns></returns>
        public String ReadUTF() => this.reader.ReadString();

        /// <summary>
        /// Read network ordered  Int32 from input stream.
        /// </summary>
        /// <returns></returns>
        public Int32 ReadInt32() =>
            IPAddress.NetworkToHostOrder(this.reader.ReadInt32());

        /// <summary>
        /// Read network ordered float from input stream.
        /// </summary>
        public Single ReadSingle()
        {
            Int32 v = IPAddress.NetworkToHostOrder(this.reader.ReadInt32());
            return BitConverter.Int32BitsToSingle(v);
        }


        /// <summary>
        /// Read network ordered boolean from input stream.
        /// </summary>
        public Boolean ReadBoolean() => this.reader.ReadBoolean();

        /// <summary>
        /// Read network ordered Int64 from input stream.
        /// </summary>
        /// <returns></returns>
        public Int64 ReadLong() =>
                IPAddress.NetworkToHostOrder(this.reader.ReadInt64());

        public byte[] ReadByteArray() => this.reader.ReadBytes(this.ReadInt32());

        public Guid[] ReadUuidArray()
        {
            int length = this.ReadInt32();
            Guid[] array = new Guid[length];
            for (int i = 0; i < length; i++)
                array[i] = new Guid(this.reader.ReadBytes(16));
            return array;
        }

        public DateTime ReadInstant() =>
            DateTimeExtensions.FromInstant(this.ReadLong(), this.ReadInt32());

        public FieldDefinitionDTO[] ReadFieldDefinitionList()
        {
            int length = this.ReadInt32();
            FieldDefinitionDTO[] retVal = new FieldDefinitionDTO[length];
            for (int i = 0; i < length; i++)
                retVal[i] = FieldDefinitionDTO.Make(this);
            return retVal;
        }

        public ConceptVersionDTO[] ReadConceptVersionList(IEnumerable<Guid> componentUuids)
        {
            int length = this.ReadInt32();
            ConceptVersionDTO[] retVal = new ConceptVersionDTO[length];
            for (int i = 0; i < length; i++)
                retVal[i] = ConceptVersionDTO.Make(this, componentUuids);
            return retVal;
        }

        public DefinitionForSemanticVersionDTO[] ReadDefinitionForSemanticVersionList(IEnumerable<Guid> componentUuids)
        {
            Int32 length = this.ReadInt32();
            DefinitionForSemanticVersionDTO[] retVal = new DefinitionForSemanticVersionDTO[length];
            // Generate array to avoid multiple enumerations of componentUuids.
            Guid[] componentUuidArr = componentUuids.ToArray();
            for (int i = 0; i < length; i++)
                retVal[i] = DefinitionForSemanticVersionDTO.Make(this, componentUuidArr);
            return retVal;
        }

        public SemanticVersionDTO[] ReadSemanticVersionList(
            IEnumerable<Guid> componentUuids,
            IEnumerable<Guid> definitionForSemanticUuids,
            IEnumerable<Guid> referencedComponentUuids)
        {
            int length = this.ReadInt32();
            SemanticVersionDTO[] retVal = new SemanticVersionDTO[length];
            for (int i = 0; i < length; i++)
            {
                retVal[i] = SemanticVersionDTO.Make(this,
                    componentUuids,
                    definitionForSemanticUuids,
                    referencedComponentUuids);
            }

            return retVal;
        }

        public Object[] ReadObjects()
        {
            int fieldCount = this.ReadInt32();
            Object[] retVal = new Object[fieldCount];
            for (int i = 0; i < fieldCount; i++)
                retVal[i] = this.ReadField();
            return retVal;
        }

        public Object ReadField()
        {
            FieldDataType token = (FieldDataType)this.reader.ReadByte();
            switch (token)
            {
                case FieldDataType.StringType:
                    return this.ReadUTF();
                case FieldDataType.IntegerType:
                    return this.ReadInt32();
                case FieldDataType.FloatType:
                    return this.ReadSingle();
                case FieldDataType.BooleanType:
                    return this.ReadBoolean();
                case FieldDataType.ByteArrayType:
                    return this.ReadByteArray();
                case FieldDataType.ObjectArrayType:
                    return this.ReadObjects().ToArray();
                case FieldDataType.DiGraphType:
                    throw new NotImplementedException();
                case FieldDataType.InstantType:
                    return this.ReadInstant();
                case FieldDataType.ConceptChronologyType:
                    return ConceptChronologyDTO.Make(this);
                case FieldDataType.ConceptType:
                    return ConceptDTO.Make(this);
                case FieldDataType.DefinitionForSemanticChronologyType:
                    return DefinitionForSemanticChronologyDTO.Make(this);
                case FieldDataType.DefinitionForSymanticType:
                    return DefinitionForSemanticDTO.Make(this);
                case FieldDataType.SemanticChronologyType:
                    return SemanticChronologyDTO.Make(this);
                case FieldDataType.SemanticType:
                    return SemanticDTO.Make(this);
                default:
                    throw new NotImplementedException($"FieldDataType {token} not known");
            };
        }
    }
}
