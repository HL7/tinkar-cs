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
        protected static void CheckMarshallVersion(TinkarInput input, 
            Int32 marshalVersion)
        {
            int objectMarshalVersion = input.ReadInt();
            if (objectMarshalVersion != marshalVersion)
                throw new UnsupportedOperationException("Unsupported version: " + objectMarshalVersion);
        }
    }
}
