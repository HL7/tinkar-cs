using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public abstract record BaseDTO
    {
        /// <summary>
        /// Write marshall version to output stream
        /// </summary>
        protected static void WriteMarshalVersion(TinkarOutput output,
            Int32 marshalVersion)
        {
            output.WriteInt32(marshalVersion);
        }

        /// <summary>
        /// Compare two IEnumerable<Guid> instances and return true if each has the same
        /// Guid values.
        /// </summary>
        /// <param name="a">First item to compare</param>
        /// <param name="b">Second item to compare</param>
        /// <returns></returns>
        protected bool CompareSequence(IEnumerable<Guid> a, IEnumerable<Guid> b) =>
            a.SequenceEqual(b);

        /// <summary>
        /// Compare two IEnumerable<IEquatable> instances and return true if list contains
        /// imtesm that are equal.
        /// </summary>
        /// <param name="a">First item to compare</param>
        /// <param name="b">Second item to compare</param>
        /// <returns></returns>
        protected bool CompareSequence<T>(IEnumerable<IEquatable<T>> a, IEnumerable<IEquatable<T>> b) =>
            a.SequenceEqual(b);

        /// <summary>
        /// Compare two IEquatable instances and return true if each are equal.
        /// </summary>
        /// <param name="a">First item to compare</param>
        /// <param name="b">Second item to compare</param>
        /// <returns></returns>
        protected bool CompareItem<T>(IEquatable<T> a, IEquatable<T> b) => a.Equals(b);

        protected static void CheckMarshalVersion(TinkarInput input,
                        Int32 MarshalVersion)
        {
            int objectMarshalVersion = input.ReadInt32();
            if (objectMarshalVersion != MarshalVersion)
                throw new UnsupportedOperationException("Unsupported version: " + objectMarshalVersion);
        }
    }
}
