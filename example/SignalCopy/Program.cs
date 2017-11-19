using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DocoptNet;
using Seq.Api;
using Seq.Api.Model.Signals;

namespace SignalCopy
{
    class Program
    {
        const string Usage = @"signal-copy: copy/print/delete Seq signals from one server to another.

Usage:
    signal-copy.exe [--action=<a>] <src> <dst> [--srckey=<sk>] [--dstkey=<dk>]
    signal-copy.exe [--action=<a>] <src> [--srckey=<sk>] [--signal=<sg>]
    signal-copy.exe [--action=<a>] <src> [--srckey=<sk>]
    signal-copy.exe (-h | --help)

Options:
    -h --help     Show this screen.
    --action=<a> Action, can be 'copy', 'print' or 'delete'.
    --srckey=<sk> Source server API key.
    --dstkey=<dk> Destination server API key.
    --signal=<sg> Signal title.

    ";

        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                try
                {
                    var arguments = new Docopt().Apply(Usage, args, version: "Seq Signal Copy 0.1", exit: true);

                    var action = Normalize(arguments["--action"]);
                    var src = arguments["<src>"].Value.ToString();
                    var dst = arguments["<dst>"]?.Value?.ToString();
                    var srcKey = Normalize(arguments["--srckey"]);
                    var dstKey = Normalize(arguments["--dstkey"]);
                    var signal = Normalize(arguments["--signal"]);

                    switch (action)
                    {
                        case "copy":
                            await RunCopy(src, srcKey, dst, dstKey);
                            break;
                        case "delete":
                            await RunDelete(src, srcKey, signal);
                            break;
                        case "print":
                            await RunPrint(src, srcKey);
                            break;
                        default:
                            Console.WriteLine($"Unknown action: '{action}'");
                            break;
                    }
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

        static async Task RunPrint(string src, string srcKey)
        {
            var srcConnection = new SeqConnection(src, srcKey);
            var count = 0;
            var countAll = 0;

            foreach (var signal in await srcConnection.Signals.ListAsync())
            {
                Console.WriteLine();
                Console.WriteLine($"---- Signal Title: '{signal.Title}', Id: '{signal.Id}' ----");
                Console.WriteLine($"Restricted: '{signal.IsRestricted}', Watched: '{signal.IsWatched}:'");
                Console.WriteLine($"Description: '{signal.Description}'");
                foreach (var property in signal.TaggedProperties)
                {
                    Console.Write($"Property {count}: {property.PropertyName}; ");
                    count++;
                }
                Console.WriteLine();
                count = 0;
                foreach (var filter in signal.Filters)
                {
                    Console.Write($"Filter {count}: {filter.Filter}, {filter.Description}; ");
                    count++;
                }
                Console.WriteLine();
                countAll++;
            }
            Console.WriteLine($"Done, {countAll} signals printed.");
        }

        static async Task RunDelete(string src, string srcKey, string signalName)
        {
            var srcConnection = new SeqConnection(src, srcKey);
            var count = 0;

            foreach (var signal in await srcConnection.Signals.ListAsync())
            {
                if (signalName != null && signal.Title == signalName)
                {
                    Console.WriteLine($"Deleting signal '{signal.Title}' with id='{signal.Id}'");
                    await srcConnection.Signals.RemoveAsync(signal);
                    ++count;
                }
                else if (signalName == null)
                {
                    Console.WriteLine($"Deleting signal '{signal.Title}' with id='{signal.Id}'");
                    await srcConnection.Signals.RemoveAsync(signal);
                    ++count;
                }
            }

            Console.WriteLine($"Done, {count} signals deleted.");
        }

        static async Task RunCopy(string src, string srcKey, string dst, string dstKey)
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
