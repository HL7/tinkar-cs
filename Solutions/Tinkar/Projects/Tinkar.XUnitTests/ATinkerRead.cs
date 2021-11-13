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
using Google.Protobuf;

namespace Tinkar.XUnitTests
{
    public class ATinkarRead
    {
        const Int32 BlockSize = 100000;
        String ProtobufFile => Path.Combine(FindParentDir("tinkar-cs"),
            "..",
            "DataFiles",
            "tinkar-solor-us-export.pb.zip");

        String TinkarZipFile => Path.Combine(FindParentDir("tinkar-cs"),
            "..",
            "DataFiles",
            "tinkar-solor-us-export.zip");

        String TinkFile => Path.Combine(FindParentDir("tinkar-cs"),
            "..",
            "DataFiles",
            "tinkar-solor-us-export.tink");

        public String FindParentDir(String dirName)
        {
            String servicePath = Path.GetFullPath(".");
            while (true)
            {
                servicePath = Path.GetFullPath(servicePath);
                String serviceDir = Path.Combine(servicePath, dirName);
                if (Directory.Exists(serviceDir))
                    return serviceDir;
                String newPath = Path.Combine(servicePath, "..");
                newPath = Path.GetFullPath(newPath);
                if (String.Compare(newPath, servicePath, StringComparison.InvariantCulture) == 0)
                    throw new Exception($"Parent directory {dirName} not found");
                servicePath = newPath;
            }
        }

        [DoNotParallelize]
        [Fact]
        public void PBToDTOVertex()
        {
            VertexDTO dtoStart = Misc.CreateVertexDTO();
            PBVertex pb = dtoStart.ToPBVertex();
            VertexDTO dtoEnd = pb.ToVertex();
            Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
        }

        [DoNotParallelize]
        [Fact]
        public void PBToDTODiTree()
        {
            DiTreeDTO dtoStart = Misc.CreateDiTreeDTO();
            PBDiTree pb = dtoStart.ToPBDiTree();
            DiTreeDTO dtoEnd = pb.ToDiTree();
            Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
        }

        [DoNotParallelize]
        [Fact]
        public void PBToDTODiGraph()
        {
            DiGraphDTO dtoStart = Misc.CreateDiGraphDTO();
            PBDiGraph pb = dtoStart.ToPBDiGraph();
            DiGraphDTO dtoEnd = pb.ToDiGraph();
            Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
        }

        [DoNotParallelize]
        [Fact]
        public void PBToDTOGraph()
        {
            GraphDTO dtoStart = Misc.CreateGraphDTO();
            PBGraph pb = dtoStart.ToPBGraph();
            GraphDTO dtoEnd = pb.ToGraph();
            Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
        }

        [DoNotParallelize]
        [Fact]
        public void PBToDTOConcept()
        {
            ConceptDTO dtoStart = Misc.CreateConceptDTO;
            PBConcept pb = dtoStart.ToPBConcept();
            ConceptDTO dtoEnd = pb.ToConcept();
            Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
        }

        [DoNotParallelize]
        [Fact]
        public void PBToDTOConceptVersion()
        {
            ConceptVersionDTO dtoStart = Misc.CreateConceptVersionDTO;
            PBConceptVersion pb = dtoStart.ToPBConceptVersion();
            ConceptVersionDTO dtoEnd = pb.ToConceptVersion();
            Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
        }

        [DoNotParallelize]
        [Fact]
        public void PBToDTOConceptChronology()
        {
            ConceptChronologyDTO dtoStart = Misc.CreateConceptChronologyDTO;
            PBConceptChronology pb = dtoStart.ToPBConceptChronology();
            ConceptChronologyDTO dtoEnd = pb.ToConceptChronology();
            Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
        }




        [DoNotParallelize]
        [Fact]
        public void PBToDTOPattern()
        {
            PatternDTO dtoStart = Misc.CreatePatternDTO;
            PBPattern pb = dtoStart.ToPBPattern();
            PatternDTO dtoEnd = pb.ToPattern();
            Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
        }

        [DoNotParallelize]
        [Fact]
        public void PBToDTOPatternVersion()
        {
            PatternVersionDTO dtoStart = Misc.CreatePatternVersionDTO;
            PBPatternVersion pb = dtoStart.ToPBPatternVersion();
            PatternVersionDTO dtoEnd = pb.ToPatternVersion();
            Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
        }

        [DoNotParallelize]
        [Fact]
        public void PBToDTOPatternChronology()
        {
            PatternChronologyDTO dtoStart = Misc.CreatePatternChronologyDTO;
            PBPatternChronology pb = dtoStart.ToPBPatternChronology();
            PatternChronologyDTO dtoEnd = pb.ToPatternChronology();
            Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
        }



        [DoNotParallelize]
        [Fact]
        public void PBToDTOSemantic()
        {
            SemanticDTO dtoStart = Misc.CreateSemanticDTO;
            PBSemantic pb = dtoStart.ToPBSemantic();
            SemanticDTO dtoEnd = pb.ToSemantic();
            Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
        }

        [DoNotParallelize]
        [Fact]
        public void PBToDTOSemanticVersion()
        {
            SemanticVersionDTO dtoStart = Misc.CreateSemanticVersionDTO;
            PBSemanticVersion pb = dtoStart.ToPBSemanticVersion();
            SemanticVersionDTO dtoEnd = pb.ToSemanticVersion();
            Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
        }

        [DoNotParallelize]
        [Fact]
        public void PBToDTOSemanticChronology()
        {
            SemanticChronologyDTO dtoStart = Misc.CreateSemanticChronologyDTO;
            PBSemanticChronology pb = dtoStart.ToPBSemanticChronology();
            SemanticChronologyDTO dtoEnd = pb.ToSemanticChronology();
            Assert.True(dtoStart.CompareTo(dtoEnd) == 0);
        }



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

        /// <summary>
        /// Read in each element in protobuf file
        /// </summary>
        [DoNotParallelize]
        [Fact]
        public void AReadPB()
        {
            DateTime start = DateTime.Now;
            DateTime current = start;
            TimeSpan elapsed;
            using FileStream input = File.OpenRead(ProtobufFile);
            PBReader pbReader = new PBReader(input);
            NativeMethods.PreventSleep();

            Int32 i = 0;
            while (pbReader.Read() != null)
            { 
                if ((i % BlockSize) == 0)
                {
                    elapsed = DateTime.Now - current;
                    current = DateTime.Now;
                    float itemsPerSec = (float)BlockSize / (float)elapsed.TotalSeconds;
                    Trace.WriteLine($"{i} - Read time {elapsed.TotalSeconds} / {itemsPerSec.ToString("F2")}");
                    GC.Collect(2, GCCollectionMode.Forced);
                }
                i += 1;
            }

            elapsed = DateTime.Now - start;
            NativeMethods.AllowSleep();
            Trace.WriteLine($"AProtobufReadConcepts time {elapsed}");
        }

        /// <summary>
        /// Read in each element in protobuf file and dto file, and compare
        /// them. Should be the same as the protobuf was created from dto file.
        /// </summary>
        [DoNotParallelize]
        [Fact]
        public void AReadPBAndDTOAndValidate()
        {
            DateTime start = DateTime.Now;
            DateTime current = start;
            TimeSpan elapsed;
            using FileStream input = File.OpenRead(ProtobufFile);
            Int32 i = 0;
            PBReader pbReader = new PBReader(input);
            NativeMethods.PreventSleep();
            foreach (IComponent component in this.ReadConcepts())
            {
                PBTinkarMsg pbMsg = pbReader.Read();
                IComponent dto = ConvertPBToDTO.Convert(pbMsg);
                Assert.True(component.CompareTo(dto) == 0);
                if ((i % BlockSize) == 0)
                {
                    elapsed = DateTime.Now - current;
                    current = DateTime.Now;
                    float itemsPerSec = (float)BlockSize / (float)elapsed.TotalSeconds;
                    Trace.WriteLine($"{i} - Read time {elapsed.TotalSeconds} / {itemsPerSec.ToString("F2")}");
                    GC.Collect(2, GCCollectionMode.Forced);
                }
                i += 1;
            }
            Assert.True(pbReader.Read() == null);

            elapsed = DateTime.Now - start;
            NativeMethods.AllowSleep();
            Trace.WriteLine($"AProtobufReadConcepts time {elapsed}");
        }

        [DoNotParallelize]
        [Fact]
        public void AReadDTOAndWritePBAllConcepts()
        {
            DateTime start = DateTime.Now;
            FileStream output = File.Create(ProtobufFile);
            using PBWriter pbWriter = new PBWriter(output);
            NativeMethods.PreventSleep();
            Int32 i = 0;
            foreach (IComponent component in this.ReadConcepts())
            {
                PBTinkarMsg pb = ConvertDTOToPB.Convert(component);
                pbWriter.Write(pb);
                i += 1;
            }
            NativeMethods.AllowSleep();
            TimeSpan elapsed = DateTime.Now - start;
            Trace.WriteLine($"AConvertAndWriteAllConcepts time {elapsed}");
        }

        [DoNotParallelize]
        [Fact]
        public void AConvertAndVerifyAllConcepts()
        {

            NativeMethods.PreventSleep();
            DateTime start = DateTime.Now;
            foreach (IComponent component in this.ReadConcepts())
            {
                PBTinkarMsg pb = ConvertDTOToPB.Convert(component);
                if (pb != null)
                {
                    IComponent dto = ConvertPBToDTO.Convert(pb);
                    Assert.True(component.CompareTo(dto) == 0);
                }
            }
            NativeMethods.AllowSleep();
            TimeSpan elapsed = DateTime.Now - start;
            Trace.WriteLine($"AConvertAndVerifyAllConcepts time {elapsed}");
        }

        [DoNotParallelize]
        [Fact]
        public void ADTOReadAllConcepts()
        {
            IComponent[] msgs = new IComponent[8000000];
            DateTime start = DateTime.Now;
            Int32 i = 0;
            NativeMethods.PreventSleep();
            foreach (IComponent component in this.ReadConcepts())
            {
                msgs[i] = component;
                //msgs[i] = null;
                i += 1;
            }
            NativeMethods.AllowSleep();
            TimeSpan elapsed = DateTime.Now - start;
            Trace.WriteLine($"ADTOReadAllConcepts time {elapsed}");
        }

        IEnumerable<IComponent> ReadConcepts()
        {
            String ExportPath = TinkFile;

            using Stream zipFile = File.OpenRead(TinkarZipFile);
            using ZipArchive archive = new ZipArchive(zipFile);
            ZipArchiveEntry entry = archive.GetEntry("export.tink");
            Stream zipStream = entry.Open();

            {
                using TinkarInput input = new TinkarInput(zipStream);

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
                        if ((counter % BlockSize) == 0)
                            GC.Collect(2, GCCollectionMode.Forced);
                    }
                }
            }
        }
    }
}
