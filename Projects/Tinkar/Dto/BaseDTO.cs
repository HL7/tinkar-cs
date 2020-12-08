using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public abstract record BaseDTO<TDto> : IComparable<TDto>,
        IEquivalent<TDto>
    {
        /// <summary>
        /// Write marshall version to output stream
        /// </summary>
        protected static void WriteMarshalVersion(TinkarOutput output,
            Int32 marshalVersion)
        {
            output.WriteInt32(marshalVersion);
        }

        protected static void CheckMarshalVersion(TinkarInput input,
                        Int32 MarshalVersion)
        {
            int objectMarshalVersion = input.ReadInt32();
            if (objectMarshalVersion != MarshalVersion)
                throw new UnsupportedOperationException("Unsupported version: " + objectMarshalVersion);
        }

        /// <summary>
        /// Implementation of IEquivalent.IsEquivalent
        /// We manually create this rather than using the default
        /// record implementation because we want to compare to
        /// do a deep comparison, not just compare reference equality.
        /// </summary>
        /// <param name="other">Item to compare to for equivalence</param>
        /// <returns>true if equal</returns>
        public Boolean IsEquivalent(TDto other) => this.CompareTo(other) == 0;

        /// <summary>
        /// Compare two items of same DTO type.
        /// An exception is thrown if an attempt is made to compare obects
        /// of different types.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public abstract Int32 CompareTo(TDto obj);
    }
}
