using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Tinkar.Dto
{
    /// <summary>
    /// Class that implements static methods to support field comparison.
    /// </summary>
    public static class FieldCompare
    {
        /// <summary>
        /// Compare two IComparable instances.
        /// </summary>
        /// <typeparam name="TItem">Items types to compare.</typeparam>
        /// <param name="a">First item to compare.</param>
        /// <param name="b">Second item to compare.</param>
        /// <returns>&lt; if a &lt; b, 0 if a == b, &gt; if a &gt; b.</returns>
        public static Int32 CompareItem<TItem>(TItem a, TItem b)
            where TItem : IComparable
        {
            return a.CompareTo(b);
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
        /// Compare two IEnumerable&lt;IComparable&gt; instances and return true if list contains
        /// items that are equal.
        /// </summary>
        /// <typeparam name="TSeq">Sequence type to compare.</typeparam>
        /// <param name="a">First item to compare.</param>
        /// <param name="b">Second item to compare.</param>
        /// <returns>&lt; if a &lt; b, 0 if a == b, &gt; if a &gt; b.</returns>
        public static Boolean IsEquivalentSequence<TSeq>(IEnumerable<TSeq> a, IEnumerable<TSeq> b)
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
        /// Compare two IEnumerable&lt;IComparable&gt; instances and return true if list contains
        /// items that are equal.
        /// </summary>
        /// <typeparam name="TSeq">Sequence type to compare.</typeparam>
        /// <param name="a">First item to compare.</param>
        /// <param name="b">Second item to compare.</param>
        /// <returns>&lt; if a &lt; b, 0 if a == b, &gt; if a &gt; b.</returns>
        public static Int32 CompareSequence<TSeq>(IEnumerable<TSeq> a, IEnumerable<TSeq> b)
            where TSeq : IComparable
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
                cmp = aItem.CompareTo(bItem);
                if (cmp != 0)
                    return cmp;
            }

            return 0;
        }

        /// <summary>
        /// Compare two IEnumerable&lt;IComparable&gt; instances and return true if list contains
        /// items that are equal.
        /// </summary>
        /// <param name="a">First item to compare.</param>
        /// <param name="b">Second item to compare.</param>
        /// <returns>&lt; if a &lt; b, 0 if a == b, &gt; if a &gt; b.</returns>
        public static Int32 CompareSequence(IEnumerable<Object> a, IEnumerable<Object> b)
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
            IEnumerator<Object> aIterator = a.GetEnumerator();
            IEnumerator<Object> bIterator = b.GetEnumerator();
            for (Int32 i = 0; i < a.Count(); i++)
            {
                aIterator.MoveNext();
                bIterator.MoveNext();
                Object aItem = aIterator.Current;
                Object bItem = bIterator.Current;
                cmp = FieldCompare.Compare(aItem, bItem);
                if (cmp != 0)
                    return cmp;
            }

            return 0;
        }

        public static Int32 CompareMap<TKey, TValue>(
            ImmutableDictionary<TKey, TValue> a,
            ImmutableDictionary<TKey, TValue> b)
            where TKey : IComparable
            where TValue : IComparable
        {
            return CompareMap(a, b, (x, y) => x.CompareTo(y));
        }


        public static Int32 CompareMap<TKey, TValue>(
        ImmutableDictionary<TKey, TValue> a,
        ImmutableDictionary<TKey, TValue> b,
        Comparison<TValue> comparer)
        where TKey : IComparable
        {
            Int32 CompareMapTuples(KeyValuePair<TKey, TValue> cva, KeyValuePair<TKey, TValue> cvb)
                => cva.Key.CompareTo(cvb.Key);

            List<KeyValuePair<TKey, TValue>> aItems = a.ToList();
            aItems.Sort(CompareMapTuples);

            List<KeyValuePair<TKey, TValue>> bItems = b.ToList();
            bItems.Sort(CompareMapTuples);

            Int32 cmpVal = aItems.Count.CompareTo(bItems.Count);
            if (cmpVal != 0)
                return cmpVal;

            for (Int32 i = 0; i < aItems.Count; i++)
            {
                cmpVal = aItems[i].Key.CompareTo(bItems[i].Key);
                if (cmpVal != 0)
                    return cmpVal;
                cmpVal = comparer(aItems[i].Value, bItems[i].Value);
                if (cmpVal != 0)
                    return cmpVal;
            }
            return 0;
        }

        /// <summary>
        /// Compare two Object arrays.
        /// </summary>
        /// <param name="a">First item to compare.</param>
        /// <param name="b">Second item to compare.</param>
        /// <returns>&lt; if a &lt; b, 0 if a == b, &gt; if a &gt; b.</returns>
        public static Int32 CompareObjArray(Object[] a, Object[] b)
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
        /// Compare two Object Lists.
        /// </summary>
        /// <returns>&lt; if a &lt; b, 0 if a == b, &gt; if a &gt; b.</returns>
        public static Int32 CompareObjList(IList a, IList b)
        {
            Int32 cmp = a.Count.CompareTo(b.Count);
            if (cmp != 0)
                return cmp;
            for (Int32 i = 0; i < a.Count; i++)
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
                case IComparable a:
                    return a.CompareTo(bObj);

                case Byte[] a:
                    return CompareByteArray(a, (Byte[])bObj);

                case Object[] aArr:
                    return CompareObjArray(aArr, (Object[])bObj);

                case IList aList:
                    return CompareObjList(aList, (IList)bObj);

                case HashSet<IPublicId> aSet:
                    {
                        HashSet<IPublicId> bSet = (HashSet<IPublicId>)bObj;
                        cmp = aSet.Count.CompareTo(bSet.Count);
                        if (cmp != 0)
                            return cmp;
                        List<IPublicId> aList = aSet.ToList();
                        aList.Sort();

                        List<IPublicId> bList = bSet.ToList();
                        bList.Sort();
                        for (Int32 i = 0; i < aList.Count; i++)
                        {
                            cmp = aList[i].CompareTo(bList[i]);
                            if (cmp != 0)
                                return cmp;
                        }
                        return 0;
                    }
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
        /// Compare two Object arrays.
        /// </summary>
        /// <param name="a">First item to compare.</param>
        /// <param name="b">Second item to compare.</param>
        /// <returns>&lt; if a &lt; b, 0 if a == b, &gt; if a &gt; b.</returns>
        public static Boolean Equivalent(ImmutableArray<Object> a, ImmutableArray<Object> b)
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
            switch (aObj)
            {
                case IEquivalent a:
                    return a.IsEquivalent(bObj);

                case IComparable a:
                    return a.CompareTo(bObj) == 0;

                case Byte[] a:
                    return CompareByteArray(a, (Byte[])bObj) == 0;

                case Object[] aArr:
                    return Equivalent(aArr, (Object[])bObj);

                case ImmutableArray<Object> aArr:
                    return Equivalent(aArr, (ImmutableArray<Object>)bObj);

                default:
                    throw new NotImplementedException($"Can not handle type {aObj.GetType().Name}");
            }
        }



    }
}
