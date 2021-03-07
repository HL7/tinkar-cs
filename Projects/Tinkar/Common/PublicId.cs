using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    /// <summary>
    /// Public Id.
    /// This is one or more Guids that serve as a unique id's.
    /// Notes:
    /// a) if there are more than one id, that means that each id identifies this
    /// object. Any other PublicId that contains any of these ID's is considered equivelant.
    /// b) there are several modifications made to this class to minimize memory allocations. in
    /// particular to minimize memory references. That is why we have SingleIdLsb and SingleIdMsb, which 
    /// are set if there is only a single id to this. If there are multiple id's, then IdArray is 
    /// set to an array of items.
    /// It is expected that the bulk of PublicId's created will be a single id.
    /// </summary>
    public struct PublicId : IPublicId
    {
        GuidUnion SingleId;
        GuidUnion[] MultipleId;

        public GuidUnion this[Int32 index]
        { 
            get
            {
                if (MultipleId == null)
                {
                    if (index != 0)
                        throw new Exception("Invalid access indice");
                    return this.SingleId;
                }

                if (index >= this.MultipleId.Length)
                    throw new Exception("Invalid access indice");
                return this.MultipleId[index];
            }
        }

        public Guid[] AsUuidArray => AsUuidList.ToArray();

        public IEnumerable<Guid> AsUuidList
        {
            get
            {
                if (MultipleId == null)
                    yield return this.SingleId.Uuid;
                else
                    for (Int32 i = 0; i < this.MultipleId.Length; i++)
                        yield return this.MultipleId[i].Uuid;
            }
        }

        public Int32 UuidCount => (MultipleId == null) ? 1 : MultipleId.Length;

        public PublicId(params Guid[] guids)
        {
            if (guids.Length < 1)
                throw new Exception($"Can not create a private id with no parameters");
            else if (guids.Length == 1)
            {
                this.MultipleId = null;
                this.SingleId = new GuidUnion(guids[0]);
            }
            else
            {
                List<GuidUnion> guidUnionList = new List<GuidUnion>();

                // Insert into ordered position. Discard duplicates.
                void InsertOrdered(GuidUnion newItem)
                {
                    if (guidUnionList.Count == 0)
                        guidUnionList.Add(newItem);
                    Int32 i = guidUnionList.Count;
                    while (i > 0)
                    {
                        Int32 cmpVal = newItem.IsSame(guidUnionList[i - 1]);
                        // discard duplicates.
                        if (cmpVal == 0)
                            return;
                        if (cmpVal > 0)
                            break;
                        i -= 1;
                    }
                    guidUnionList.Insert(i, newItem);
                }

                for (Int32 i = 0; i < guids.Length; i++)
                    InsertOrdered(new GuidUnion(guids[i]));

                this.SingleId = new GuidUnion(Guid.Empty);
                this.MultipleId = guidUnionList.ToArray();
            }
        }

        public Int32 IsSame(IPublicId other)
        {
            Int32 cmpVal = this.UuidCount.CompareTo(other.UuidCount);
            if (cmpVal != 0)
                return cmpVal;
            for (Int32 i = 0; i < this.UuidCount; i++)
            {
                cmpVal = this[i].IsSame(other[i]);
                if (cmpVal != 0)
                    return cmpVal;
            }
            return 0;
        }

        public bool IsEquivalent(IPublicId other)
        {
            Int32 thisIdIndex = 0;
            Int32 otherIdIndex = 0;
            Int32 thisCount = this.UuidCount;
            Int32 otherCount = other.UuidCount;
            while (true)
            {
                Int32 cmpVal = this[thisIdIndex].IsSame(other[otherIdIndex]);
                if (cmpVal < 0)
                {
                    if (++thisIdIndex >= thisCount)
                        return false;
                }
                else if (cmpVal > 0)
                {
                    if (++otherIdIndex >= otherCount)
                        return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
