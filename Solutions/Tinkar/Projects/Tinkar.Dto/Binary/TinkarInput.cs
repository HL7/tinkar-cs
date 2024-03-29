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

namespace Tinkar.Dto
{
    /// <summary>
    /// Read data from input stream into Tinkar objects.
    /// The serialization format read must be compatible the Java Tinkar
    /// serializer.
    /// </summary>
    public class TinkarInput : IDisposable
    {
        private BinaryReader reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="TinkarInput"/> class.
        /// </summary>
        /// <param name="inStream">Binary input stream.</param>
        /// <param name="marshalVersion">Marshal version number.</param>
        public TinkarInput(Stream inStream, Int32 marshalVersion = MarshalVersion.LocalMarshalVersion)
        {
            this.reader = new BinaryReader(inStream);
            Int32 readMarshalVersion = this.GetInt32();
            if (readMarshalVersion != marshalVersion)
                throw new Exception($"Invalid Marshal Version in file. Found {readMarshalVersion}, expected {marshalVersion}");
        }

        /// <summary>
        /// Dispose function.
        /// </summary>
        public void Dispose()
        {
        }

        List<IPublicId> GetPublicIdList() => new List<IPublicId>(this.GetPublicIds());
        HashSet<IPublicId> GetPublicIdSet() => new HashSet<IPublicId>(this.GetPublicIds());

        /// <summary>
        /// Read PublicIds from input stream.
        /// </summary>
        /// <returns>PublicId.</returns>
        public IEnumerable<IPublicId> GetPublicIds()
        {
            Int32 length = this.GetInt32();
            for (Int32 i = 0; i < length; i++)
                yield return this.GetPublicId();
        }

        /// <summary>
        /// Read string.
        /// Note: BinaryReader.ReadString is supposed to be identical to java ReadUTF().
        /// </summary>
        /// <returns>String.</returns>
        public String GetUTF()
        {
            Int16 len = this.GetInt16();
            Char[] c = new char[len];
            Byte[] bytes = this.reader.ReadBytes(len);
            for (Int32 i = 0; i < len; i++)
                c[i] = (Char)bytes[i];
            return new string(c);
        }

        /// <summary>
        /// Read network ordered Int15 from input stream.
        /// </summary>
        /// <returns>Int32.</returns>
        public Int16 GetInt16() =>
            IPAddress.NetworkToHostOrder(this.reader.ReadInt16());

        /// <summary>
        /// Read network ordered  Int32 from input stream.
        /// </summary>
        /// <returns>Int32.</returns>
        public Int32 GetInt32() =>
            IPAddress.NetworkToHostOrder(this.reader.ReadInt32());

        /// <summary>
        /// Read network ordered float from input stream.
        /// </summary>
        /// <returns>read single value.</returns>
        public Single GetSingle()
        {
            Int32 v = IPAddress.NetworkToHostOrder(this.reader.ReadInt32());
            return BitConverter.Int32BitsToSingle(v);
        }

        /// <summary>
        /// Read network ordered boolean from input stream.
        /// </summary>
        /// <returns>read boolean value.</returns>
        public Boolean GetBoolean() => this.reader.ReadBoolean();

        /// <summary>
        /// Read network ordered Int64 from input stream.
        /// </summary>
        /// <returns>Int64.</returns>
        public Int64 GetLong() =>
                IPAddress.NetworkToHostOrder(this.reader.ReadInt64());

        /// <summary>
        /// Read a byte array from input stream.
        /// </summary>
        /// <returns>byte[].</returns>
        public Byte[] GetByteArray() => this.reader.ReadBytes(this.GetInt32());

        /// <summary>
        /// Read array of guids from input stream.
        /// </summary>
        /// <returns>Guid[].</returns>
        public Guid[] GetUuids()
        {
            Int32 length = this.GetInt32();
            Guid[] array = new Guid[length];
            for (Int32 i = 0; i < length; i++)
                array[i] = this.GetUuid();
            return array;
        }

        /// <summary>
        /// Read array of guids from input stream.
        /// </summary>
        /// <returns>Guid[].</returns>
        public Guid GetUuid() => new Guid(this.reader.ReadBytes(16));

        /// <summary>
        /// Read PublicId from input stream.
        /// </summary>
        /// <returns>PublicId.</returns>
        public IPublicId GetPublicId() => new PublicId(this.GetUuids());

        /// <summary>
        /// Read data tome from input stream.
        /// </summary>
        /// <returns>DateTime.</returns>
        public DateTime GetInstant()
        {
            Int64 epoch = this.GetLong();
            Int64 seconds = epoch / 1000;
            Int64 ms = epoch - (seconds * 1000);
            return DateTimeExtensions.FromInstant(seconds, (Int32)ms);
        }
        /// <summary>
        /// Read an array or FieldDefinitionDTO items.
        /// </summary>
        /// <returns>FieldDefinitionDTO[].</returns>
        public FieldDefinitionDTO[] GetFieldDefinitionList()
        {
            Int32 length = this.GetInt32();
            FieldDefinitionDTO[] retVal = new FieldDefinitionDTO[length];
            for (Int32 i = 0; i < length; i++)
                retVal[i] = FieldDefinitionDTO.Make(this);
            return retVal;
        }

        /// <summary>
        /// Read an array or ConceptVersionDTO items.
        /// </summary>
        /// <param name="publicId">Public id (component ids).</param>
        /// <returns>ConceptVersionDTO[].</returns>
        public ConceptVersionDTO[] GetConceptVersionList(IPublicId publicId)
        {
            Int32 length = this.GetInt32();
            ConceptVersionDTO[] retVal = new ConceptVersionDTO[length];
            for (Int32 i = 0; i < length; i++)
                retVal[i] = ConceptVersionDTO.Make(this, publicId);
            return retVal;
        }

        /// <summary>
        /// Read an array or TypePatternVersionDTO items.
        /// </summary>
        /// <param name="publicId">Public id (component ids).</param>
        /// <returns>TypePatternVersionDTO[].</returns>
        public PatternVersionDTO[] GetPatternVersionList(IPublicId publicId)
        {
            Int32 length = this.GetInt32();
            PatternVersionDTO[] retVal = new PatternVersionDTO[length];

            // Generate array to avoid multiple enumerations of componentUuids.
            Guid[] componentUuidArr = publicId.AsUuidArray;
            for (Int32 i = 0; i < length; i++)
                retVal[i] = PatternVersionDTO.Make(this, publicId);
            return retVal;
        }

        /// <summary>
        /// Read an array or SemanticVersionDTO items.
        /// </summary>
        /// <param name="publicId">Public id (component ids).</param>
        /// <returns>SemanticVersionDTO[].</returns>
        public SemanticVersionDTO[] ReadSemanticVersionList(
            IPublicId publicId)
        {
            Int32 length = this.GetInt32();
            SemanticVersionDTO[] retVal = new SemanticVersionDTO[length];
            for (Int32 i = 0; i < length; i++)
            {
                retVal[i] = SemanticVersionDTO.Make(
                    this,
                    publicId);
            }

            return retVal;
        }

        /// <summary>
        /// Read an array or Object items.
        /// </summary>
        /// <returns>Object[].</returns>
        public Object[] GetObjects()
        {
            Int32 fieldCount = this.GetInt32();
            Object[] retVal = new Object[fieldCount];
            for (Int32 i = 0; i < fieldCount; i++)
                retVal[i] = this.GetField();
            return retVal;
        }

        /// <summary>
        /// Read Tinkar items from file.
        /// </summary>
        /// <returns>Object[].</returns>
        public IEnumerable<ComponentDTO> GetComponents()
        {
            while (this.reader.BaseStream.Position < this.reader.BaseStream.Length)
                yield return (ComponentDTO)this.GetField();
        }

        /// <summary>
        /// Read an array or Object fields.
        /// </summary>
        /// <returns>Object[].</returns>
        public Object GetField()
        {
            FieldDataType token;
            try
            {
                token = (FieldDataType)this.reader.ReadByte();
            }
            catch
            {
                return null;
            }

            switch (token)
            {
                case FieldDataType.ConceptChronologyType:
                    return ConceptChronologyDTO.Make(this);
                case FieldDataType.PatternChronologyType:
                    return PatternChronologyDTO.Make(this);
                case FieldDataType.SemanticChronologyType:
                    return SemanticChronologyDTO.Make(this);
                case FieldDataType.ConceptVersionType:
                    throw new NotImplementedException();
                case FieldDataType.PatternVersionType:
                    throw new NotImplementedException();
                case FieldDataType.SemanticVersionType:
                    throw new NotImplementedException();
                case FieldDataType.StampType:
                    throw new NotImplementedException();

                case FieldDataType.ConceptType:
                    return ConceptDTO.Make(this);
                case FieldDataType.PatternType:
                    return PatternDTO.Make(this);
                case FieldDataType.SemanticType:
                    return SemanticDTO.Make(this);
                case FieldDataType.DiTreeType:
                    return DiTreeDTO.Make(this);
                case FieldDataType.DiGraphType:
                    return DiGraphDTO.Make(this);
                case FieldDataType.VertexType:
                    throw new NotImplementedException();
                case FieldDataType.ComponentIdList:
                    return this.GetPublicIdList();
                case FieldDataType.ComponentIdSet:
                    return this.GetPublicIdSet();
                case FieldDataType.PlanarPoint:
                    return new PlanarPointDTO(this.GetInt32(), this.GetInt32());
                case FieldDataType.SpatialPoint:
                    return new SpatialPointDTO(this.GetInt32(), this.GetInt32(), this.GetInt32());

                case FieldDataType.StringType:
                    return this.GetUTF();
                case FieldDataType.IntegerType:
                    return this.GetInt32();
                case FieldDataType.FloatType:
                    return this.GetSingle();
                case FieldDataType.BooleanType:
                    return this.GetBoolean();
                case FieldDataType.ByteArrayType:
                    return this.GetByteArray();
                case FieldDataType.ObjectArrayType:
                    return this.GetObjects().ToArray();
                case FieldDataType.InstantType:
                    return this.GetInstant();
                default:
                    throw new NotImplementedException($"FieldDataType {token} not known");
            }
        }
    }
}
