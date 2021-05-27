using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tinkar.Protobuf.CS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Tinkar.ProtoBuf.CS;
using Xunit;
using Assert = Xunit.Assert;

namespace Tinkar.Protobuf.CS.XUnitTests
{
    public class Timings
    {
        class StopWatch : IDisposable
        {
            string msg;
            DateTime start;

            public StopWatch(String msg)
            {
                this.msg = msg;
                this.start = DateTime.Now;
            }

            public void Dispose()
            {
                TimeSpan s = DateTime.Now - start;
                Trace.WriteLine($"{this.msg} {s.ToString()}");
            }
        }
    }
}
