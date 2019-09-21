using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Utility.CommandLine;

namespace klassify
{
    class Program
    {
        [Argument('o', "out", "output directory")]
        private static string OutputDirectory { get; set; }

        static void Main(string[] args)
        {
            Arguments.Populate();

            if (String.IsNullOrEmpty(OutputDirectory))
            {
                OutputDirectory = ".";
            }

            Console.WriteLine("Output Dir: " + OutputDirectory);
            Console.ReadKey();
        }
    }

    internal static class Extensions
    {
        public static string Or(this string value, string alternative)
        {
            return string.IsNullOrWhiteSpace(value) ? alternative : value;
        }
    }
}
