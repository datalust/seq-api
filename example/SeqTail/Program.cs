using System;
using System.Linq;
using System.Threading.Tasks;
using DocoptNet;
using Seq.Api;
using Serilog;
using System.Reactive.Linq;
using Serilog.Formatting.Compact.Reader;
using System.IO;
using Serilog.Events;

namespace SeqTail
{
    class Program
    {
        const string Usage = @"seq-tail: watch a Seq query from your console.

Usage:
    seq-tail.exe <server> [--filter=<f>] [--apikey=<k>] [--window=<w>]
    seq-tail.exe (-h | --help)

Options:
    -h --help     Show this screen.
    --filter=<f>  Filter expression or free text to match.
    --apikey=<k>  Seq API key.
    --window=<w>  Window size [default: 100].

    ";

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.LiterateConsole()
                .CreateLogger();

            try
            {
                var arguments = new Docopt().Apply(Usage, args, version: "Seq Tail 0.2", exit: true);

                var server = arguments["<server>"].ToString();
                var apiKey = Normalize(arguments["--apikey"]);
                var filter = Normalize(arguments["--filter"]);

                Run(server, apiKey, filter).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Tailing aborted");
                Environment.Exit(-1);
            }
        }

        static string Normalize(ValueObject v)
        {
            if (v == null) return null;
            var s = v.ToString();
            return string.IsNullOrWhiteSpace(s) ? null : s;
        }

        static async Task Run(string server, string apiKey, string filter)
        {
            var connection = new SeqConnection(server, apiKey);

            string strict = null;
            if (filter != null)
            {
                var converted = await connection.Expressions.ToStrictAsync(filter);
                strict = converted.StrictExpression;
            }

            using (var stream = await connection.Events.StreamDocumentsAsync(filter: strict))
            {
                var subscription = stream.Subscribe(document =>
                {
                    var reader = new LogEventReader(new StringReader(document));
                    LogEvent evt;
                    if (!reader.TryRead(out evt))
                        throw new InvalidOperationException("Expected document to contain data.");
                    Log.Write(evt);
                });

                Console.ReadKey(true);
                subscription.Dispose();
            }
        }
    }
}
