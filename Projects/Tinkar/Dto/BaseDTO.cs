using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public abstract record BaseDTO<TDto> : IComparable<TDto>, IEquatable<TDto>
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
        protected Int32 CompareGuids(IEnumerable<Guid> a, IEnumerable<Guid> b)
        {
            Int32 cmp = a.Count().CompareTo(b.Count());
            if (cmp != 0)
                return cmp;
            IEnumerator<Guid> aIterator = a.GetEnumerator();
            IEnumerator<Guid> bIterator = b.GetEnumerator();
            for (Int32 i = 0; i < a.Count(); i++)
            {
                aIterator.MoveNext();
                bIterator.MoveNext();
                cmp = aIterator.Current.CompareTo(bIterator.Current);
                if (cmp != 0)
                    return cmp;
            }
            return 0;
        }

        /// <summary>
        /// Compare two IEnumerable<IEquatable> instances and return true if list contains
        /// items that are equal.
        /// </summary>
        /// <param name="a">First item to compare</param>
        /// <param name="b">Second item to compare</param>
        /// <returns></returns>
        protected Int32 CompareSequence<TSeq>(IEnumerable<TSeq> a, IEnumerable<TSeq> b)
            where TSeq : IComparable<TSeq>
        {
            Int32 cmp = a.Count().CompareTo(b.Count());
            if (cmp != 0)
                return cmp;
            IEnumerator<TSeq> aIterator = a.GetEnumerator();
            IEnumerator<TSeq> bIterator = b.GetEnumerator();
            for (Int32 i = 0; i < a.Count(); i++)
            {
                aIterator.MoveNext();
                bIterator.MoveNext();
                TSeq aItem = aIterator.Current;
                TSeq bItem = bIterator.Current;
                cmp = aItem.CompareTo(bItem);
                if (cmp != 0)
                    return cmp;
            }

            return 0;
        }

        /// <summary>
        /// Compare two IComparable instances.
        /// </summary>
        /// <param name="a">First item to compare</param>
        /// <param name="b">Second item to compare</param>
        /// <returns></returns>
#nullable enable
        protected Int32 CompareItem<TItem>(TItem a, TItem b)
            where TItem : IComparable<TItem>
        {
            return a.CompareTo(b);
        }
#nullable disable

        protected static void CheckMarshalVersion(TinkarInput input,
                        Int32 MarshalVersion)
        {
            int objectMarshalVersion = input.ReadInt32();
            if (objectMarshalVersion != MarshalVersion)
                throw new UnsupportedOperationException("Unsupported version: " + objectMarshalVersion);
        }

        /// <summary>
        /// Implementation of Equals.
        /// We manually create this rather than using the default
        /// record implementation because we want to compare to
        /// do a deep comparison, not just compare reference equality.
        /// </summary>
        /// <param name="other">Item to compare to for equality</param>
        /// <returns>true if equal</returns>
        public Boolean Equals(TDto other) => this.CompareTo(other) == 0;

        /// <summary>
        /// Compare two items of same DTO type.
        /// An exception is thrown if an attempt is made to compare obects
        /// of different types.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual Int32 CompareTo(TDto obj) => throw new NotImplementedException();
    }
}
