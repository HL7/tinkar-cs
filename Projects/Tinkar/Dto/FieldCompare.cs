using System;
using System.Collections.Generic;
using System.Linq;

namespace Tinkar
{
    /// <summary>
    /// Class that implements static methods to support field comparison.
    /// </summary>
    public static class FieldCompare
    {
        /// <summary>
        /// Compare two ISame instances.
        /// </summary>
        /// <typeparam name="TItem">Items types to compare.</typeparam>
        /// <param name="a">First item to compare.</param>
        /// <param name="b">Second item to compare.</param>
        /// <returns>&lt; if a &lt; b, 0 if a == b, &gt; if a &gt; b.</returns>
        public static Int32 CompareItem<TItem>(TItem a, TItem b)
            where TItem : ISame<TItem>
        {
            return a.IsSame(b);
        }

        /// <summary>
        /// Compare two IEnumerable&lt;Guid&gt; instances and return true if each has the same
        /// Guid values.
        /// </summary>
        /// <param name="a">First item to compare.</param>
        /// <param name="b">Second item to compare.</param>
        /// <returns>&lt; if a &lt; b, 0 if a == b, &gt; if a &gt; b.</returns>
        public static Int32 CompareByteArray(Byte[] a, Byte[] b)
        {
            Int32 cmp = a.Length.CompareTo(b.Length);
            if (cmp != 0)
                return cmp;
            for (Int32 i = 0; i < a.Count(); i++)
            {
                cmp = a[i].CompareTo(b[i]);
                if (cmp != 0)
                    return cmp;
            }
            return 0;
        }

        /// <summary>
        /// Compare two IEnumerable&lt;Guid&gt; instances and return true if each has the same
        /// Guid values.
        /// </summary>
        /// <param name="a">First item to compare.</param>
        /// <param name="b">Second item to compare.</param>
        /// <returns>&lt; if a &lt; b, 0 if a == b, &gt; if a &gt; b.</returns>
        public static Int32 CompareGuids(IEnumerable<Guid> a, IEnumerable<Guid> b)
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
        /// Compare two PublicId instances and return true if each has the same
        /// Guid values.
        /// </summary>
        /// <param name="a">First item to compare.</param>
        /// <param name="b">Second item to compare.</param>
        /// <returns>&lt; if a &lt; b, 0 if a == b, &gt; if a &gt; b.</returns>
        public static Int32 ComparePublicIds(IPublicId a, IPublicId b) => CompareGuids(a.AsUuidArray, b.AsUuidArray);

        /// <summary>
        /// Compare two IEnumerable&lt;ISame&gt; instances and return true if list contains
        /// items that are equal.
        /// </summary>
        /// <typeparam name="TSeq">Sequence type to compare.</typeparam>
        /// <param name="a">First item to compare.</param>
        /// <param name="b">Second item to compare.</param>
        /// <returns>&lt; if a &lt; b, 0 if a == b, &gt; if a &gt; b.</returns>
        public static Boolean EquivelateSequence<TSeq>(IEnumerable<TSeq> a, IEnumerable<TSeq> b)
            where TSeq : IEquivalent
        {
            if ((a == null) && (b == null))
                return true;
            if (a == null)
                return false;
            if (b == null)
                return false;
            Int32 cmp = a.Count().CompareTo(b.Count());
            if (cmp != 0)
                return false;
            IEnumerator<TSeq> aIterator = a.GetEnumerator();
            IEnumerator<TSeq> bIterator = b.GetEnumerator();
            for (Int32 i = 0; i < a.Count(); i++)
            {
                aIterator.MoveNext();
                bIterator.MoveNext();
                TSeq aItem = aIterator.Current;
                TSeq bItem = bIterator.Current;
                if (aItem.IsEquivalent(bItem) == false)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Compare two IEnumerable&lt;ISame&gt; instances and return true if list contains
        /// items that are equal.
        /// </summary>
        /// <typeparam name="TSeq">Sequence type to compare.</typeparam>
        /// <param name="a">First item to compare.</param>
        /// <param name="b">Second item to compare.</param>
        /// <returns>&lt; if a &lt; b, 0 if a == b, &gt; if a &gt; b.</returns>
        public static Int32 CompareSequence<TSeq>(IEnumerable<TSeq> a, IEnumerable<TSeq> b)
            where TSeq : ISame
        {
            if ((a == null) && (b == null))
                return 0;
            if (a == null)
                return -1;
            if (b == null)
                return 1;
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
                cmp = aItem.IsSame(bItem);
                if (cmp != 0)
                    return cmp;
            }

            return 0;
        }

        /// <summary>
        /// Compare two Object arrays.
        /// </summary>
        /// <param name="a">First item to compare.</param>
        /// <param name="b">Second item to compare.</param>
        /// <returns>&lt; if a &lt; b, 0 if a == b, &gt; if a &gt; b.</returns>
        public static Int32 Compare(Object[] a, Object[] b)
        {
            Int32 cmp = a.Length.CompareTo(b.Length);
            if (cmp != 0)
                return cmp;
            for (Int32 i = 0; i < a.Count(); i++)
            {
                cmp = Compare(a[i], b[i]);
                if (cmp != 0)
                    return cmp;
            }

            return 0;
        }

        /// <summary>
        /// Returns true if two bojects are equivalent, or the same with
        /// a deep comparison.
        /// </summary>
        /// <param name="aObj">First item to compare.</param>
        /// <param name="bObj">Second item to compare.</param>
        /// <returns>true if equivalent.</returns>
        public static Boolean Same(Object aObj, Object bObj)
        {
            return Compare(aObj, bObj) == 0;
        }

        /// <summary>
        /// Compares two objects for 'sameness'. Does deep compare.
        /// </summary>
        /// <param name="aObj">First objec to compare.</param>
        /// <param name="bObj">Second object to compare.</param>
        /// <returns>&lt; if a &lt; b, 0 if a == b, &gt; if a &gt; b.</returns>
        public static Int32 Compare(Object aObj, Object bObj)
        {
            Int32 cmp = aObj.GetType().Name.CompareTo(bObj.GetType().Name);
            if (cmp != 0)
                return cmp;

            switch (aObj)
            {
                case ISame a:
                    return a.IsSame(bObj);

                case IComparable a:
                    return a.CompareTo(bObj);

                case Byte[] a:
                    return CompareByteArray(a, (Byte[])bObj);

                // DiGraphType = 6,
                case Object[] aArr:
                    return Compare(aArr, (Object[])bObj);

                default:
                    throw new NotImplementedException($"Can not handle type {aObj.GetType().Name}");
            }
        }




        /// <summary>
        /// Compare two Object arrays.
        /// </summary>
        /// <param name="a">First item to compare.</param>
        /// <param name="b">Second item to compare.</param>
        /// <returns>&lt; if a &lt; b, 0 if a == b, &gt; if a &gt; b.</returns>
        public static Boolean Equivalent(Object[] a, Object[] b)
        {
            Int32 cmp = a.Length.CompareTo(b.Length);
            if (cmp != 0)
                return false;
            for (Int32 i = 0; i < a.Count(); i++)
            {
                if (Equivalent(a[i], b[i]) == false)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Compares two objects for 'sameness'. Does deep compare.
        /// </summary>
        /// <param name="aObj">First objec to compare.</param>
        /// <param name="bObj">Second object to compare.</param>
        /// <returns>&lt; if a &lt; b, 0 if a == b, &gt; if a &gt; b.</returns>
        public static Boolean Equivalent(Object aObj, Object bObj)
        {
            Int32 cmp = aObj.GetType().Name.CompareTo(bObj.GetType().Name);
            if (cmp != 0)
                return false;

            switch (aObj)
            {
                case IEquivalent a:
                    return a.IsEquivalent(bObj);

                case IComparable a:
                    return a.CompareTo(bObj) == 0;

                case Byte[] a:
                    return CompareByteArray(a, (Byte[])bObj) == 0;

                // DiGraphType = 6,
                case Object[] aArr:
                    return Equivalent(aArr, (Object[])bObj);

                default:
                    throw new NotImplementedException($"Can not handle type {aObj.GetType().Name}");
            }
        }



    }
}
