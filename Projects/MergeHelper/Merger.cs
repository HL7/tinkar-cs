using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MergeHelper
{
    public class Merger
    {
        /// <summary>
        /// Base dir of currrent java code that we are merging into out cs code.
        /// </summary>
        public String JavaBaseDir_Current { get; set; }

        /// <summary>
        /// Base dir of old java code that we are merging into from. This is the jave version that
        /// was used tha last time the cs code was updated./
        /// </summary>
        public String JavaBaseDir_Old { get; set; }

        /// <summary>
        /// CSharp base directory
        /// </summary>
        public String CSBaseDir { get; set; }

        public void Process()
        {
            ProcessRecursively(this.JavaBaseDir_Current, this.JavaBaseDir_Old, this.CSBaseDir);
        }

        void Msg(String msg)
        {
            Trace.WriteLine(msg);
            Console.WriteLine(msg);
        }

        public void ProcessRecursively(String javaDirCurrent,
            String javaDirOld,
            String csDir)
        {
            if (Directory.Exists(javaDirCurrent) == false)
            {
                Msg($"Current java dir {javaDirCurrent} missing.");
                return;
            }

            if (Directory.Exists(javaDirOld) == false)
            {
                Msg($"Old java dir {javaDirOld} missing.");
                return;
            }

            if (Directory.Exists(csDir) == false)
            {
                Msg($"Old java dir {csDir} missing.");
                return;
            }

            ProcessSubDirs(javaDirCurrent, javaDirOld, csDir);
            ProcessFiles(javaDirCurrent, javaDirOld, csDir);
        }

        String SubDir(String basePath, String alternatePath) => Path.GetFullPath(Path.Combine(basePath, Path.GetFileName(alternatePath)));

        void ProcessFiles(String javaDirCurrent,
            String javaDirOld,
            String csDir)
        {
            List<String> javaDirCurrentFiles = Directory.GetFiles(javaDirCurrent, "*.java").ToList();
            List<String> javaDirOldFiles = Directory.GetFiles(javaDirOld, "*.java").ToList();

            javaDirCurrentFiles.Sort();
            javaDirOldFiles.Sort();

            if (Directory.Exists(csDir) == false)
            {
                Msg($"CS dir {Path.GetFullPath(csDir)} does not exist. Ignoring java dirs");
                return;
            }

            Int32 javaDirCurrentFileIndex = 0;
            Int32 javaDirOldFileIndex = 0;
            Int32 maxIndex = javaDirCurrentFiles.Count;
            if (maxIndex < javaDirOldFiles.Count)
                maxIndex = javaDirOldFiles.Count;

            while ((javaDirCurrentFileIndex < maxIndex) &&
                   (javaDirOldFileIndex < maxIndex))
            {
                String javaDirCurrentFile = javaDirCurrentFiles[javaDirCurrentFileIndex];
                String javaDirOldFile = javaDirOldFiles[javaDirOldFileIndex];

                Int32 compareVal = String.Compare(Path.GetFileName(javaDirCurrentFile), Path.GetFileName(javaDirOldFile), true);
                if (compareVal < 0)
                {
                    Msg($"Found   {Path.GetFullPath(javaDirOldFile)}");
                    Msg($"Missing {SubDir(javaDirCurrent, javaDirCurrentFile)}");
                    javaDirOldFile += 1;
                }
                else if (compareVal > 0)
                {
                    Msg($"Found   {Path.GetFullPath(javaDirCurrentFile)}");
                    Msg($"Missing {SubDir(javaDirOld, javaDirCurrentFile)}");
                    javaDirCurrentFileIndex += 1;
                }
                else
                {
                    //Msg($"Found   {Path.GetFullPath(javaDirCurrentFile)}");
                    //Msg($"Found {Path.GetFullPath(javaDirOldFile)}");

                    String csSubDir = SubDir(csDir, javaDirOldFile);
                    javaDirCurrentFileIndex += 1;
                    javaDirOldFileIndex += 1;

                    String currentJavaCode = File.ReadAllText(javaDirCurrentFile).Trim();
                    String oldJavaCode = File.ReadAllText(javaDirOldFile).Trim();
                    if (String.Compare(oldJavaCode, currentJavaCode) != 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine($"winmerge {javaDirCurrentFile} {javaDirOldFile}");
                        String csBatPath = Path.Combine(
                            Path.GetFullPath(csDir),
                            $"{Path.GetFileNameWithoutExtension(javaDirCurrentFile)}.bat"
                            );
                        File.WriteAllText(csBatPath, sb.ToString());
                    }
                }
            }

            while (javaDirCurrentFileIndex < maxIndex)
            {
                String javaDirCurrentSubDir = javaDirCurrentFiles[javaDirCurrentFileIndex];
                Msg($"Found {Path.GetFullPath(javaDirCurrentSubDir)}");
                javaDirCurrentFileIndex += 1;
            }

            while (javaDirOldFileIndex < maxIndex)
            {
                String javaDirOldSubDir = javaDirOldFiles[javaDirOldFileIndex];
                Msg($"Found {Path.GetFullPath(javaDirOldSubDir)}");
                javaDirOldFileIndex += 1;
            }
        }

        void ProcessSubDirs(String javaDirCurrent,
            String javaDirOld,
            String csDir)
        {
            List<String> javaDirCurrentSubDirs = Directory.GetDirectories(javaDirCurrent).ToList();
            List<String> javaDirOldSubDirs = Directory.GetDirectories(javaDirOld).ToList();

            javaDirCurrentSubDirs.Sort();
            javaDirOldSubDirs.Sort();

            Int32 javaDirCurrentSubDirIndex = 0;
            Int32 javaDirOldSubDirIndex = 0;

            while ((javaDirCurrentSubDirIndex < javaDirCurrentSubDirs.Count()) &&
                   (javaDirOldSubDirIndex < javaDirOldSubDirs.Count()))
            {
                String javaDirCurrentSubDir = javaDirCurrentSubDirs[javaDirCurrentSubDirIndex];
                String javaDirOldSubDir = javaDirOldSubDirs[javaDirOldSubDirIndex];

                Int32 compareVal = String.Compare(Path.GetFileName(javaDirCurrentSubDir), Path.GetFileName(javaDirOldSubDir), true);
                if (compareVal < 0)
                {
                    Msg($"Found   {Path.GetFullPath(javaDirOldSubDir)}");
                    Msg($"Missing {SubDir(javaDirCurrent, javaDirCurrentSubDir)}");
                    javaDirOldSubDir += 1;
                }
                else if (compareVal > 0)
                {
                    Msg($"Found   {Path.GetFullPath(javaDirCurrentSubDir)}");
                    Msg($"Missing {SubDir(javaDirOld, javaDirCurrentSubDir)}");
                    javaDirCurrentSubDirIndex += 1;
                }
                else
                {
                    javaDirCurrentSubDirIndex += 1;
                    javaDirOldSubDirIndex += 1;
                    String csSubDir = SubDir(csDir, javaDirOldSubDir);
                    ProcessRecursively(javaDirCurrentSubDir, javaDirOldSubDir, csSubDir);
                }
            }

            while (javaDirCurrentSubDirIndex < javaDirCurrentSubDirs.Count())
            {
                String javaDirCurrentSubDir = javaDirCurrentSubDirs[javaDirCurrentSubDirIndex];
                Msg($"Found {Path.GetFullPath(javaDirCurrentSubDir)}");
                javaDirCurrentSubDirIndex += 1;
            }

            while (javaDirOldSubDirIndex < javaDirOldSubDirs.Count())
            {
                String javaDirOldSubDir = javaDirOldSubDirs[javaDirOldSubDirIndex];
                Msg($"Found {Path.GetFullPath(javaDirOldSubDir)}");
                javaDirOldSubDirIndex += 1;
            }
        }
    }
}
