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
    }
}
