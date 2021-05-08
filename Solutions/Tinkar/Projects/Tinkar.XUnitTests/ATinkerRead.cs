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

namespace Tinkar.XUnitTests
{
    public class TinkarRead
    {
        [DoNotParallelize]
        [Fact]
        public void TinkarSolorUSRead()
        {
            using Stream s = File.OpenRead(@"C:\Development\Tinkar\tinkar-solor-us-export.zip");
            using ZipArchive archive = new ZipArchive(s);
            ZipArchiveEntry entry = archive.GetEntry("export.tink");
            Stream tinkarStream = entry.Open();
            using TinkarInput input = new TinkarInput(tinkarStream);
            Int32 counter = 0;
            List<IComponent> items = new List<IComponent>();
            while (true)
            {
                counter += 1;
                if ((counter % 10000) == 0)
                    Trace.WriteLine($"{counter}");
                var dtoRead = input.GetField();
                items.Add((IComponent)dtoRead);
            }
        }
    }
}
