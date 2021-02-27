using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public abstract class PublicIdA : IPublicId
    {
        public abstract Guid[] AsUuidArray();

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            Guid[] uuids = this.AsUuidArray();

            String Str(Int32 i) => $"\"{uuids[i++].ToString()}\"";

            Int32 i = 0;
            while (i < uuids.Length - 1)
                sb.Append($"{Str(i++)}, ");
            sb.Append($"{Str(i++)}]");
            return sb.ToString();
        }

        public abstract Int32 UuidCount { get; }
    }
}
