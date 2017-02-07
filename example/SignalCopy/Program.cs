using System;
using System.Threading.Tasks;
using DocoptNet;
using Seq.Api;
using Seq.Api.Model.Signals;
using System.Collections.Generic;

namespace SeqQuery
{
    class Program
    {
        const string Usage = @"signal-copy: copy Seq signals from one server to another.

Usage:
    signal-copy.exe <src> <dst> [--srckey=<sk>] [--dstkey=<dk>]
    signal-copy.exe (-h | --help)

Options:
    -h --help     Show this screen.
    --srckey=<sk> Source server API key.
    --dstkey=<dk> Destination server API key.

    ";

        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                try
                {
                    var arguments = new Docopt().Apply(Usage, args, version: "Seq Signal Copy 0.1", exit: true);

                    var src = arguments["<src>"].ToString();
                    var dst = arguments["<dst>"].ToString();
                    var srcKey = Normalize(arguments["--srckey"]);
                    var dstKey = Normalize(arguments["--dstkey"]);

                    await Run(src, srcKey, dst, dstKey);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("signal-copy: {0}", ex);
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

        static async Task Run(string src, string srcKey, string dst, string dstKey)
        {
            var srcConnection = new SeqConnection(src, srcKey);
            var dstConnection = new SeqConnection(dst, dstKey);

            var dstSignals = new Dictionary<string, SignalEntity>();
            foreach (var dstSignal in await dstConnection.Signals.ListAsync())
            {
                if (dstSignals.ContainsKey(dstSignal.Title))
                {
                    Console.WriteLine($"The destination server has more than one signal named '{dstSignal.Title}'; only one copy of the signal will be updated.");
                    continue;
                }

                dstSignals.Add(dstSignal.Title, dstSignal);
            }

            var count = 0;

            foreach (var signal in await srcConnection.Signals.ListAsync())
            {
                SignalEntity target;
                if (dstSignals.TryGetValue(signal.Title, out target))
                {
                    if (target.IsRestricted)
                    {
                        Console.WriteLine($"Skipping restricted signal '{signal.Title}' ({target.Id})");
                        continue;
                    }

                    Console.WriteLine($"Updating existing signal '{signal.Title}' ({target.Id})");
                }
                else
                {
                    Console.WriteLine($"Creating new signal '{signal.Title}'");
                    target = await dstConnection.Signals.TemplateAsync();
                }

                target.Title = signal.Title;
                target.Filters = signal.Filters;
                target.TaggedProperties = signal.TaggedProperties;
                target.Description = signal.Description + " (copy)";

                await (target.Id != null ? dstConnection.Signals.UpdateAsync(target) : dstConnection.Signals.AddAsync(target));
                ++count;
            }

            Console.WriteLine($"Done, {count} signals updated.");
        }
    }
}
