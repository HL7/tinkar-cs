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
            // start posi is for debugging. Can restart at problem point for faster turnaround.
            // should always be 0 for full test of all elements.
            Int64 startPos = 895401585;

            DateTime start = DateTime.Now;
            foreach (IComponent component in this.ReadConcepts(startPos))
            {
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


        IEnumerable<IComponent> ReadConcepts(Int64 position = 0)
        {
            const String ExportPath = @"C:\Development\Tinkar\export.tink";

            if (File.Exists(ExportPath) == false)
            {
                using Stream zipFile = File.OpenRead(@"C:\Development\Tinkar\tinkar-solor-us-export.zip");
                using ZipArchive archive = new ZipArchive(zipFile);
                ZipArchiveEntry entry = archive.GetEntry("export.tink");
                Stream zipStream = entry.Open();
                using Stream outFile = File.OpenWrite(ExportPath);
                zipStream.CopyTo(outFile);
                outFile.Close();
            }

            using FileStream tinkarStream = File.OpenRead(ExportPath);
            using TinkarInput input = new TinkarInput(tinkarStream);

            if (position > 0)
                tinkarStream.Seek(position, SeekOrigin.Begin);

            Int32 counter = 0;
            bool done = false;
            while (done == false)
            {
                IComponent c = (IComponent)input.GetField();
                if (c == null)
                    done = true;
                else
                {
                    yield return c;
                    counter += 1;
                    if ((counter % 10000) == 0)
                    {
                        Trace.WriteLine($"{counter} {tinkarStream.Position}");
                        GC.Collect(2, GCCollectionMode.Forced);
                    }
                }
            }
        }

    }
}
