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
    /// Read data from input stream into Tinkar objects.
    /// The serialization format read must be compatible the Java Tinkar
    /// serializer.
    /// </summary>
    public class TinkarInput : IDisposable
    {
        private BinaryReader reader;
        private Int32 marshalVersion;

        /// <summary>
        /// Initializes a new instance of the <see cref="TinkarInput"/> class.
        /// </summary>
        /// <param name="inStream">Binary input stream.</param>
        public TinkarInput(Stream inStream)
        {
            this.reader = new BinaryReader(inStream);
            this.marshalVersion = this.ReadInt32();
        }

        /// <summary>
        /// Dispose function.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Read string.
        /// Note: BinaryReader.ReadString is supposed to be identical to java ReadUTF().
        /// </summary>
        /// <returns>String.</returns>
        public String ReadUTF() => this.reader.ReadString();

        /// <summary>
        /// Read network ordered  Int32 from input stream.
        /// </summary>
        /// <returns>Int32.</returns>
        public Int32 ReadInt32() =>
            IPAddress.NetworkToHostOrder(this.reader.ReadInt32());

        /// <summary>
        /// Read network ordered float from input stream.
        /// </summary>
        /// <returns>read single value.</returns>
        public Single ReadSingle()
        {
            Int32 v = IPAddress.NetworkToHostOrder(this.reader.ReadInt32());
            return BitConverter.Int32BitsToSingle(v);
        }

        /// <summary>
        /// Read network ordered boolean from input stream.
        /// </summary>
        /// <returns>read boolean value.</returns>
        public Boolean ReadBoolean() => this.reader.ReadBoolean();

        /// <summary>
        /// Read network ordered Int64 from input stream.
        /// </summary>
        /// <returns>Int64.</returns>
        public Int64 ReadLong() =>
                IPAddress.NetworkToHostOrder(this.reader.ReadInt64());

        /// <summary>
        /// Read a byte array from input stream.
        /// </summary>
        /// <returns>byte[].</returns>
        public byte[] ReadByteArray() => this.reader.ReadBytes(this.ReadInt32());

        /// <summary>
        /// Read array of guids from input stream.
        /// </summary>
        /// <returns>Guid[].</returns>
        public Guid[] ReadUuids()
        {
            int length = this.ReadInt32();
            Guid[] array = new Guid[length];
            for (int i = 0; i < length; i++)
                array[i] = new Guid(this.reader.ReadBytes(16));
            return array;
        }

        /// <summary>
        /// Read PublicId from input stream.
        /// </summary>
        /// <returns>PublicId.</returns>
        public IPublicId ReadPublicId() => new PublicId(ReadUuids());

        /// <summary>
        /// Read data tome from input stream.
        /// </summary>
        /// <returns>DateTime.</returns>
        public DateTime ReadInstant() =>
            DateTimeExtensions.FromInstant(this.ReadLong(), this.ReadInt32());

        /// <summary>
        /// Read an array or FieldDefinitionDTO items.
        /// </summary>
        /// <returns>FieldDefinitionDTO[].</returns>
        public FieldDefinitionDTO[] ReadFieldDefinitionList()
        {
            int length = this.ReadInt32();
            FieldDefinitionDTO[] retVal = new FieldDefinitionDTO[length];
            for (int i = 0; i < length; i++)
                retVal[i] = FieldDefinitionDTO.Make(this);
            return retVal;
        }

        /// <summary>
        /// Read an array or ConceptVersionDTO items.
        /// </summary>
        /// <param name="publicId">Public id (component ids).</param>
        /// <returns>ConceptVersionDTO[].</returns>
        public ConceptVersionDTO[] ReadConceptVersionList(IPublicId publicId)
        {
            int length = this.ReadInt32();
            ConceptVersionDTO[] retVal = new ConceptVersionDTO[length];
            for (int i = 0; i < length; i++)
                retVal[i] = ConceptVersionDTO.Make(this, publicId);
            return retVal;
        }

        /// <summary>
        /// Read an array or PatternForSemanticVersionDTO items.
        /// </summary>
        /// <param name="publicId">Public id (component ids).</param>
        /// <returns>PatternForSemanticVersionDTO[].</returns>
        public PatternForSemanticVersionDTO[] ReadPatternForSemanticVersionList(IPublicId publicId)
        {
            Int32 length = this.ReadInt32();
            PatternForSemanticVersionDTO[] retVal = new PatternForSemanticVersionDTO[length];

            // Generate array to avoid multiple enumerations of componentUuids.
            Guid[] componentUuidArr = publicId.ToArray();
            for (int i = 0; i < length; i++)
                retVal[i] = PatternForSemanticVersionDTO.Make(this, publicId);
            return retVal;
        }

        /// <summary>
        /// Read an array or SemanticVersionDTO items.
        /// </summary>
        /// <param name="publicId">Public id (component ids).</param>
        /// <param name="definitionForSemanticUuids">PatternForSemantic UUIDs.</param>
        /// <param name="referencedComponentUuids">ReferencedComponent UUIDs.</param>
        /// <returns>SemanticVersionDTO[].</returns>
        public SemanticVersionDTO[] ReadSemanticVersionList(
            IPublicId publicId,
            IEnumerable<Guid> definitionForSemanticUuids,
            IEnumerable<Guid> referencedComponentUuids)
        {
            int length = this.ReadInt32();
            SemanticVersionDTO[] retVal = new SemanticVersionDTO[length];
            for (int i = 0; i < length; i++)
            {
                retVal[i] = SemanticVersionDTO.Make(
                    this,
                    publicId,
                    definitionForSemanticUuids,
                    referencedComponentUuids);
            }

            return retVal;
        }

        /// <summary>
        /// Read an array or Object items.
        /// </summary>
        /// <returns>Object[].</returns>
        public Object[] ReadObjects()
        {
            int fieldCount = this.ReadInt32();
            Object[] retVal = new Object[fieldCount];
            for (int i = 0; i < fieldCount; i++)
                retVal[i] = this.ReadField();
            return retVal;
        }

        /// <summary>
        /// Read an array or Object fields.
        /// </summary>
        /// <returns>Object[].</returns>
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
                case FieldDataType.PatternForSemanticChronologyType:
                    return PatternForSemanticChronologyDTO.Make(this);
                case FieldDataType.PatternForSemanticType:
                    return PatternForSemanticDTO.Make(this);
                case FieldDataType.SemanticChronologyType:
                    return SemanticChronologyDTO.Make(this);
                case FieldDataType.SemanticType:
                    return SemanticDTO.Make(this);
                default:
                    throw new NotImplementedException($"FieldDataType {token} not known");
            }
        }

        /// <summary>
        /// Read version number from input stream and compare it to the
        /// passed expected value. Throw exception if doesnt match.
        /// </summary>
        /// <param name="marshalVersion">Expected version.</param>
        public void CheckMarshalVersion(Int32 marshalVersion)
        {
            if (this.marshalVersion != marshalVersion)
                throw new ArgumentException($"Unsupported version: {objectMarshalVersion}");
        }
    }
}
