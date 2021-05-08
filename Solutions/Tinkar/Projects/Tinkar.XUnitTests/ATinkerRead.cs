using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;
using Tinkar.Dto;
using System.IO.Compression;

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
            while (true)
            {
                var dtoRead = input.GetField();
            }
        }
    }
}
