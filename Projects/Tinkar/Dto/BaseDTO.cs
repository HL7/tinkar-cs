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

        /// <summary>
        /// Compare two IEnumerable<Object> instances and return true if list contains
        /// items that are equal.
        /// </summary>
        /// <param name="a">First item to compare</param>
        /// <param name="b">Second item to compare</param>
        /// <returns></returns>
        //$protected Int32 CompareObjects(IEnumerable<Object> a, IEnumerable<Object> b)
        //{
        //    Int32 cmp = a.Count().CompareTo(b.Count());
        //    if (cmp != 0)
        //        return cmp;
        //    IEnumerator<Object> aIterator = a.GetEnumerator();
        //    IEnumerator<Object> bIterator = b.GetEnumerator();
        //    for (Int32 i = 0; i < a.Count(); i++)
        //    {
        //        aIterator.MoveNext();
        //        bIterator.MoveNext();
        //        Object aItem = aIterator.Current;
        //        Object bItem = bIterator.Current;
        //        cmp = aItem.CompareTo(bItem);
        //        if (cmp != 0)
        //            return cmp;
        //    }

        //    return 0;
        //}

        /// <summary>
        /// Compare two IComparable instances.
        /// </summary>
        /// <param name="a">First item to compare</param>
        /// <param name="b">Second item to compare</param>
        /// <returns></returns>
        protected Int32 CompareItem<TItem>(TItem a, TItem b)
            where TItem : IComparable<TItem>
        {
            return a.CompareTo(b);
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
