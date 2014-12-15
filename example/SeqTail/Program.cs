using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DocoptNet;
using Seq.Api;
using Seq.Api.Model.Events;

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
            var cts = new CancellationTokenSource();

            // ReSharper disable once MethodSupportsCancellation
            var tail = Task.Run(async () =>
            {
                try
                {
                    var arguments = new Docopt().Apply(Usage, args, version: "Seq Tail 0.1", exit: true);

                    var server = arguments["<server>"].ToString();
                    var apiKey = Normalize(arguments["--apikey"]);
                    var filter = Normalize(arguments["--filter"]);
                    var window = arguments["--window"].AsInt;

                    await Run(server, apiKey, filter, window, cts.Token);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("seq-tail: {0}", ex);
                    Console.ResetColor();
                    Environment.Exit(-1);
                }
            });

            Console.ReadKey(true);
            cts.Cancel();
            // ReSharper disable once MethodSupportsCancellation
            tail.Wait();
        }

        static string Normalize(ValueObject v)
        {
            if (v == null) return null;
            var s = v.ToString();
            return string.IsNullOrWhiteSpace(s) ? null : s;
        }

        static async Task Run(string server, string apiKey, string filter, int window, CancellationToken cancel)
        {
            var startedAt = DateTime.UtcNow;

            var connection = new SeqConnection(server, apiKey);

            string strict = null;
            if (filter != null)
            {
                var converted = await connection.Expressions.ToStrictAsync(filter);
                strict = converted.StrictExpression;
            }

            var result = await connection.Events.ListAsync(count: window, render: true, fromDateUtc: startedAt, filter: strict);

            // Since results may come late, we request an overlapping window and exclude
            // events that have already been printed. If the last seen ID wasn't returned
            // we assume the count was too small to cover the window.
            var lastPrintedBatch = new HashSet<string>();
            string lastReturnedId = null;
            
            while (!cancel.IsCancellationRequested)
            {
                if (result.Count == 0)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
                else
                {
                    var noOverlap = result.All(e => e.Id != lastReturnedId);

                    if (noOverlap && lastReturnedId != null)
                        Console.WriteLine("<window exceeded>");

                    foreach (var eventEntity in ((IEnumerable<EventEntity>)result).Reverse())
                    {
                        if (lastPrintedBatch.Contains(eventEntity.Id))
                        {
                            continue;
                        }

                        lastReturnedId = eventEntity.Id;

                        var exception = "";
                        if (eventEntity.Exception != null)
                            exception = Environment.NewLine + eventEntity.Exception;

                        var ts = DateTimeOffset.Parse(eventEntity.Timestamp).ToLocalTime();

                        var color = ConsoleColor.White;
                        switch (eventEntity.Level)
                        {
                            case "Verbose":
                            case "Debug":
                                color = ConsoleColor.Gray;
                                break;
                            case "Warning":
                                color = ConsoleColor.Yellow;
                                break;
                            case "Error":
                            case "Fatal":
                                color = ConsoleColor.Red;
                                break;
                        }

                        Console.ForegroundColor = color;
                        Console.WriteLine("{0:G} [{1}] {2}{3}", ts, eventEntity.Level, eventEntity.RenderedMessage, exception);
                        Console.ResetColor();
                    }

                    lastPrintedBatch = new HashSet<string>(result.Select(e => e.Id));
                }

                var fromDateUtc = lastReturnedId == null ? startedAt : DateTime.UtcNow.AddMinutes(-3);
                result = await connection.Events.ListAsync(count: window, render: true, fromDateUtc: fromDateUtc, filter: strict);
            }
        }
    }
}
