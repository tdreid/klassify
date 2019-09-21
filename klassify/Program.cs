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

        [Argument('u', "user", "sql server user id")]
        private static string UserId { get; set; }

        [Argument('p', "password", "sql server password")]
        private static string Password { get; set; }

        [Argument('s', "server", "sql server")]
        private static string Server { get; set; }

        [Argument('d', "database", "sql database")]
        private static string Database { get; set; }

        [Argument('t', "timeout", "connection timeout")]
        private static string Timeout { get; set; }

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
