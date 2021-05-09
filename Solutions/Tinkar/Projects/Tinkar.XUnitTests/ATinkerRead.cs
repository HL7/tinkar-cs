using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;
using Tinkar.Dto;
using System.IO.Compression;
using System.Diagnostics;
using Tinkar.ProtoBuf.CS;
using Google.Protobuf.WellKnownTypes;

namespace Tinkar.XUnitTests
{
    public class TinkarRead
    {
        //[DoNotParallelize]
        //[Fact]
        //public void ReadAllConcepts()
        //{
        //    DateTime start = DateTime.Now;
        //    Int32 counter = 0;
        //    foreach (IComponent component in this.ReadConcepts())
        //    {
        //        counter += 1;
        //        if ((counter % 10000) == 0)
        //        {
        //            Trace.WriteLine($"{counter}");
        //            GC.Collect(2, GCCollectionMode.Forced);
        //        }
        //    }
        //    TimeSpan elapsed = DateTime.Now - start;
        //    Trace.WriteLine($"Read time {elapsed}");
        //}

        [DoNotParallelize]
        [Fact]
        public void ConvertAllConcepts()
        {
            DateTime start = DateTime.Now;
            Int32 counter = 0;
            foreach (IComponent component in this.ReadConcepts())
            {
                counter += 1;
                if ((counter % 10000) == 0)
                {
                    Trace.WriteLine($"{counter}");
                    GC.Collect(2, GCCollectionMode.Forced);
                }
                var pb = PBConvert.Convert(component);
                if (pb != null)
                {
                    var dto = DTOConvert.Convert(pb);
                    Assert.True(component.CompareTo(dto) == 0);
                }
            }
            TimeSpan elapsed = DateTime.Now - start;
            Trace.WriteLine($"Read time {elapsed}");
        }


        IEnumerable<IComponent> ReadConcepts()
        {
            using Stream s = File.OpenRead(@"C:\Development\Tinkar\tinkar-solor-us-export.zip");
            using ZipArchive archive = new ZipArchive(s);
            ZipArchiveEntry entry = archive.GetEntry("export.tink");
            Stream tinkarStream = entry.Open();
            using TinkarInput input = new TinkarInput(tinkarStream);
            bool done = false;
            while (done == false)
            {
                IComponent c = (IComponent)input.GetField();
                if (c == null)
                    done = true;
                else
                    yield return c;
            }
        }

    }
}
