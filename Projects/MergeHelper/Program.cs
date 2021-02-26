using System;

namespace MergeHelper
{
    /// <summary>
    /// This is an ad-hoc program written to help merge modifications made to java code
    /// into CSharp code. 
    /// It is meant as a development aid only, and is not written for use in the field.
    /// </summary>
    class Program
    {
        static Merger merger = new Merger();

        /*-d tinkar-java\component\src\main\java\org\hl7\tinkar tinkar-java.cs\component\src\main\java\org\hl7\tinkar tinkar-cs\Projects\Tinkar 
         * 
         */
        static void ParseArgs(string[] args)
        {
            Int32 i = 0;
            while (i < args.Length)
            {
                String arg = args[i++];
                switch (arg.Trim().ToLower())
                {
                    case "-d":
                        merger.JavaBaseDir_Current = args[i++];
                        merger.JavaBaseDir_Old = args[i++];
                        merger.CSBaseDir = args[i++];
                        break;

                    default:
                        throw new Exception($"Unknown command line argument {arg}");
                }
            }
        }

        static void Main(string[] args)
        {
            ParseArgs(args);
            merger.Process();
        }
    }
}
