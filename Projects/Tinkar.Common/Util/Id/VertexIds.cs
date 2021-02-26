using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public class VertexIds
    {
        public static IVertexId NewRandom()
        {
            return new PublicId1(Guid.NewGuid());
        }

        public static  IVertexId of(long msb, long lsb)
        {
            return new PublicId1(msb, lsb);
        }

        public static  IVertexId of(UUID uuid)
        {
            return new PublicId1(uuid);
        }

    }
}
