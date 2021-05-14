using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinkar.Dto;
using Tinkar.ProtoBuf.CS;

namespace Tinkar.XUnitTests
{
    public class PBReader
    {
        Stream inputStream;
        public PBReader(Stream inputStream)
        {
            this.inputStream = inputStream;
        }

        public PBTinkarMsg Read() =>
            PBTinkarMsg.Parser.ParseDelimitedFrom(inputStream);
    }

    public class PBWriter
    {
        Stream outputStream;
        public PBWriter(Stream outputStream)
        {
            this.outputStream = outputStream;
        }

        public void Write(PBTinkarMsg msg) =>
            msg.WriteDelimitedTo(outputStream);
    }
}
