using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public record PublicId : IPublicId
    {
        Guid[] guids;
        public Guid[] AsUuidArray => guids;

        public IEnumerable<Guid> AsUuidList => guids;

        public Int32 UuidCount => guids.Length;

        public PublicId(Guid[] guids)
        {
            this.guids = guids;
        }

        #warning 'Is this the same as Keiths comparison algorithymn, is array always sorted? no duplicates?'
        public Int32 CompareTo(IPublicId other)
        {
            Guid[] myGuids = this.AsUuidArray;
            Guid[] otherGuids = other.AsUuidArray;
            Int32 cmp = myGuids.Length - otherGuids.Length;
            if (cmp != 0)
                return cmp;
            for (Int32 i = 0; i < myGuids.Length; i++)
            {
                cmp = myGuids[i].CompareTo(otherGuids[i]);
                if (cmp != 0)
                    return cmp;
            }

            return 0;
        }
    }
}
