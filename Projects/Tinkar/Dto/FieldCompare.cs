using System;
using System.Collections.Generic;
using System.Linq;

namespace Tinkar
{
    public static class FieldCompare
    {
        /// <summary>
        /// Compare two IEnumerable<Guid> instances and return true if each has the same
        /// Guid values.
        /// </summary>
        /// <param name="a">First item to compare</param>
        /// <param name="b">Second item to compare</param>
        /// <returns></returns>
        public static Int32 CompareByteArray(byte[] a, byte[] b)
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
        /// Compare two IEnumerable<Guid> instances and return true if each has the same
        /// Guid values.
        /// </summary>
        /// <param name="a">First item to compare</param>
        /// <param name="b">Second item to compare</param>
        /// <returns></returns>
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
        /// Compare two IEnumerable<IComparable> instances and return true if list contains
        /// items that are equal.
        /// </summary>
        /// <param name="a">First item to compare</param>
        /// <param name="b">Second item to compare</param>
        /// <returns></returns>
        public static Int32 CompareSequence<TSeq>(IEnumerable<TSeq> a, IEnumerable<TSeq> b)
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

        public static Boolean Equivalent(Object aObj, Object bObj)
        {
            return Compare(aObj, bObj) == 0;
        }


        public static Int32 Compare(Object aObj, Object bObj)
        {
            Int32 cmp = aObj.GetType().Name.CompareTo(bObj.GetType().Name);
            if (cmp != 0)
                return cmp;

            switch (aObj)
            {
                case String a: 
                    return a.CompareTo((String) bObj);

                case Int32 a: 
                    return a.CompareTo((Int32)bObj);
                
                case Single a: 
                    return a.CompareTo((Single) bObj);
                
                case Boolean a: 
                    return a.CompareTo((Boolean)bObj);
                
                case DateTime a: 
                    return a.CompareTo((DateTime)bObj);
                
                case byte[] a: 
                    return CompareByteArray(a, (byte[])bObj);
                
                case ConceptChronologyDTO a:
                    return a.CompareTo((ConceptChronologyDTO) bObj);

                case ConceptDTO a:
                    return a.CompareTo((ConceptDTO)bObj);

                case DefinitionForSemanticChronologyDTO a:
                    return a.CompareTo((DefinitionForSemanticChronologyDTO)bObj);

                case DefinitionForSemanticDTO a:
                    return a.CompareTo((DefinitionForSemanticDTO)bObj);

                case SemanticChronologyDTO a:
                    return a.CompareTo((SemanticChronologyDTO)bObj);

                case SemanticDTO a:
                    return a.CompareTo((SemanticDTO)bObj);

                // DiGraphType = 6,
                case Object[] aArr:
                    return Compare(aArr, (Object[]) bObj);

                default:
                    throw new NotImplementedException($"Can not handle type {aObj.GetType().Name}");
            }
        }
    }
}
