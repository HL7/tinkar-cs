using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public abstract record BaseDTO
    {
        /// <summary>
        /// Version of marshalling code. Update when marshalling changes.
        /// </summary>
        protected abstract Int32 MarshalVersion { get; }

        protected void CheckMarshallVersion(TinkarInput input)
        {
            int objectMarshalVersion = input.ReadInt();
            if (objectMarshalVersion != MarshalVersion)
                throw new UnsupportedOperationException("Unsupported version: " + objectMarshalVersion);
        }
    }
}
}
