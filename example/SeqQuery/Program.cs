using System;
using System.Threading.Tasks;
using DocoptNet;
using Seq.Api;

namespace SeqQuery
{
    class Program
    {
        const string Usage = @"seq-query: run Seq SQL queries from your console.

Usage:
    seq-query.exe <server> <query> [--apikey=<k>] [--from=<f>] [--to=<t>]
    seq-query.exe (-h | --help)

Options:
    -h --help     Show this screen.
    --apikey=<k>  Seq API key.
    --from=<f>    ISO 8601 date/time to query from (default: now - 24h)
    --to=<t>      Date/time to query to (default: now)

    ";

        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                try
                {
                    var arguments = new Docopt().Apply(Usage, args, version: "Seq Query 0.1", exit: true);

                    var server = arguments["<server>"].ToString();
                    var query = Normalize(arguments["<query>"]);
                    var apiKey = Normalize(arguments["--apikey"]);
                    var @from = Normalize(arguments["--from"]);
                    var to = Normalize(arguments["--to"]);

                    await Run(server, apiKey, query, from, to);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("seq-query: {0}", ex);
                    Console.ResetColor();
                    Environment.Exit(-1);
                }
            }).Wait();
        }

        static string Normalize(ValueObject v)
        {
            if (v == null) return null;
            var s = v.ToString();
            return string.IsNullOrWhiteSpace(s) ? null : s;
        }

        static async Task Run(string server, string apiKey, string query, string from, string to)
        {
            var connection = new SeqConnection(server, apiKey);

            var now = DateTime.UtcNow;
            var rangeStartUtc = from != null ? DateTime.Parse(from) : now - TimeSpan.FromDays(1);
            DateTime? rangeEndUtc = to != null ? DateTime.Parse(to) : now;

            var result = await connection.Data.QueryCsvAsync(query, rangeStartUtc, rangeEndUtc);
            Console.WriteLine(result);
        }
    }
}
