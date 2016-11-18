using System;
using System.Linq;
using System.Threading.Tasks;
using DocoptNet;
using Seq.Api;
using Serilog;
using System.Reactive.Linq;
using Serilog.Formatting.Compact.Reader;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace SeqTail
{
    class Program
    {
        const string Usage = @"seq-tail: watch a Seq query from your console.

Usage:
    seq-tail.exe <server> [--filter=<f>] [--apikey=<k>]
    seq-tail.exe (-h | --help)

Options:
    -h --help     Show this screen.
    --filter=<f>  Filter expression or free text to match.
    --apikey=<k>  Seq API key.

    ";

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.LiterateConsole()
                .CreateLogger();

            TaskScheduler.UnobservedTaskException += (s,e) => Log.Fatal(e.Exception, "Unobserved task exception");

            try
            {
                var arguments = new Docopt().Apply(Usage, args, version: "Seq Tail 0.2", exit: true);

                var server = arguments["<server>"].ToString();
                var apiKey = Normalize(arguments["--apikey"]);
                var filter = Normalize(arguments["--filter"]);

                var cancel = new CancellationTokenSource();
                Console.WriteLine("Tailing, press Ctrl+C to exit.");
                Console.CancelKeyPress += (s,a) => cancel.Cancel();

                var run = Task.Run(() => Run(server, apiKey, filter, cancel));

                run.GetAwaiter().GetResult();
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

        static async Task Run(string server, string apiKey, string filter, CancellationTokenSource cancel)
        {
            var connection = new SeqConnection(server, apiKey);

            string strict = null;
            if (filter != null)
            {
                var converted = await connection.Expressions.ToStrictAsync(filter);
                strict = converted.StrictExpression;
            }

            using (var stream = await connection.Events.StreamAsync<JObject>(filter: strict))
            {
                var subscription = stream
                    .Select(jObject => LogEventReader.ReadFromJObject(jObject))
                    .Subscribe(evt => Log.Write(evt), () => cancel.Cancel());

                cancel.Token.WaitHandle.WaitOne();
                subscription.Dispose();
            }
        }
    }
}
