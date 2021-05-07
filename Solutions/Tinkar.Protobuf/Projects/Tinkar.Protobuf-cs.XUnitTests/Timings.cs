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

        [DoNotParallelize]
        [Fact]
        public void BulkWrite()
        {
            const Int32 count = 1 * 1000000;

            PBConcept[] concepts = new PBConcept[count];
            {
                using StopWatch s = new StopWatch("Creation");
                for (Int32 i = 0; i < count; i++)
                {
                    PBConcept concept = new PBConcept
                    {
                        PublicId = new PBPublicId()
                    };
                    TinkarId tid = new TinkarId(Guid.NewGuid());
                    concept.PublicId.Id.Add(tid.ToPBTinkarId());
                    concepts[i] = concept;
                }
            }
            {
                using StopWatch s = new StopWatch("Writing");
                using (var output = File.Create(@"c:\Temp\ProtobufTest.bin"))
                {
                    for (Int32 i = 0; i < count; i++)
                        concepts[i].WriteTo(output);
                }
            }
            {
                using StopWatch s = new StopWatch("Reading");
                using (var input = File.OpenRead(@"c:\Temp\ProtobufTest.bin"))
                {
                    var parser = PBConcept.Parser;
                    for (Int32 i = 0; i < count; i++)
                        concepts[i] = parser.ParseFrom(input);
                }
            }
        }
    }
}
