using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public abstract record BaseDTO
    {
        protected static void WriteMarshalVersion(TinkarOutput output,
            Int32 marshalVersion)
        {
            output.WriteInt(marshalVersion);
        }

        protected static void CheckMarshalVersion(TinkarInput input,
                Int32 MarshalVersion)
        {
            int objectMarshalVersion = input.ReadInt();
            if (objectMarshalVersion != MarshalVersion)
                throw new UnsupportedOperationException("Unsupported version: " + objectMarshalVersion);
        }
    }
}
