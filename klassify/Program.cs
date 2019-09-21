using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Utility.CommandLine;

namespace klassify
{
    partial class Program
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
            try
            {
                try
                {
                    DotNetEnv.Env.Load();
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("No .env file detected. Using specified arguments or defaults.");
                }
                Arguments.Populate();

                OutputDirectory = OutputDirectory
                    .Or(Environment.GetEnvironmentVariable("KLASSIFY_OUT")
                    .Or("."));
                UserId = UserId
                    .Or(Environment.GetEnvironmentVariable("KLASSIFY_USER"));
                Password = Password
                    .Or(Environment.GetEnvironmentVariable("KLASSIFY_PASSWORD"));
                Server = Server
                    .Or(Environment.GetEnvironmentVariable("KLASSIFY_SERVER")
                    .Or("localhost"));
                Database = Database
                    .Or(Environment.GetEnvironmentVariable("KLASSIFY_DATABASE"));
                Timeout = Timeout
                    .Or(Environment.GetEnvironmentVariable("KLASSIFY_TIMEOUT")
                    .Or("30"));

                if (String.IsNullOrWhiteSpace(Database) || String.IsNullOrWhiteSpace(UserId) || String.IsNullOrWhiteSpace(Password))
                {
                    throw new ArgumentException("One or more of these required parameters not set: Database, UserId, Password.");
                }

                using (SqlConnection connection = new SqlConnection($"Server={Server};Initial Catalog={Database};Persist Security Info=False;User ID={UserId};Password={Password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout={Timeout};"))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(
                        $"SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_CATALOG='{Database}'",
                        connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        string tableName = row["TABLE_NAME"].ToString();
                        string sql = $"declare @TableName sysname = '{tableName}'{queryText}";
                        SqlCommand command = new SqlCommand(sql, connection);
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }
                        string code = (string)command.ExecuteScalar();

                        if (String.IsNullOrWhiteSpace(code))
                        {
                            continue;
                        }

                        Directory.CreateDirectory(Path.GetFullPath(OutputDirectory));
                        string path = Path.Combine(OutputDirectory, $"{tableName}.cs");
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                        using (FileStream fs = File.Create(path))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes(code);
                            fs.Write(info, 0, info.Length);
                            Console.WriteLine($"Created {path}");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            Console.WriteLine("Press a key to exit...");
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
