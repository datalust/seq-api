﻿using System;
using System.Threading.Tasks;
using DocoptNet;
using Seq.Api;
using Serilog;
using System.Reactive.Linq;
using Serilog.Formatting.Compact.Reader;
using System.Threading;
using Newtonsoft.Json.Linq;

const string usage = @"seq-tail: watch a Seq query from your console.

Usage:
    seq-tail.exe <server> [--filter=<f>] [--apikey=<k>]
    seq-tail.exe (-h | --help)

Options:
    -h --help     Show this screen.
    --filter=<f>  Filter expression or free text to match.
    --apikey=<k>  Seq API key.
";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .CreateLogger();

TaskScheduler.UnobservedTaskException += (_,e) => Log.Fatal(e.Exception, "Unobserved task exception");

try
{
    var arguments = new Docopt().Apply(usage, args, version: "Seq Tail 0.2", exit: true)!;

    var server = arguments["<server>"].ToString();
    var apiKey = Normalize(arguments["--apikey"]);
    var filter = Normalize(arguments["--filter"]);

    var cancel = new CancellationTokenSource();
    Console.WriteLine("Tailing, press Ctrl+C to exit.");
    Console.CancelKeyPress += (_,_) => cancel.Cancel();

    var run = Task.Run(() => Run(server, apiKey, filter, cancel), cancel.Token);

    run.GetAwaiter().GetResult();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Tailing aborted");
    Environment.Exit(-1);
}

static string? Normalize(ValueObject? v)
{
    if (v == null) return null;
    var s = v.ToString();
    return string.IsNullOrWhiteSpace(s) ? null : s;
}

static async Task Run(string server, string? apiKey, string? filter, CancellationTokenSource cancel)
{
    var connection = new SeqConnection(server, apiKey);

    string? strict = null;
    if (filter != null)
    {
        var converted = await connection.Expressions.ToStrictAsync(filter);
        strict = converted.StrictExpression;
    }

    using var stream = await connection.Events.StreamAsync<JObject>(filter: strict);
    var subscription = stream
        .Select(LogEventReader.ReadFromJObject)
        .Subscribe(Log.Write, cancel.Cancel);

    cancel.Token.WaitHandle.WaitOne();
    subscription.Dispose();
}
