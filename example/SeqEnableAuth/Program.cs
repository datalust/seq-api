using System;
using System.Threading.Tasks;
using DocoptNet;
using Seq.Api;
using Seq.Api.Model.Settings;

namespace SeqEnableAuth
{
    class Program
    {
        const string Usage = @"seq-enable-auth: enable authentication on your Seq server.

Usage:
    seq-enable-auth.exe <server> [--apikey=<k>]
    seq-enable-auth.exe (-h | --help)

Options:
    -h --help     Show this screen.
    --apikey=<k>  Seq API key.

    ";
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                try
                {
                    var arguments = new Docopt().Apply(Usage, args, version: "Seq Enable Auth 0.1", exit: true);

                    var server = arguments["<server>"].ToString();
                    var apiKey = Normalize(arguments["--apikey"]);

                    await Run(server, apiKey);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("seq-enable-auth: {0}", ex);
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
        
        static async Task Run(string server, string apiKey)
        {
            var connection = new SeqConnection(server, apiKey);

            // check for valid license?
            
            var admin = await connection.Users.FindCurrentAsync();

            admin.NewPassword = "password";
            
            await connection.Users.UpdateAsync(admin);
            
            var iae = await connection.Settings.FindNamedAsync(SettingName.IsAuthenticationEnabled);
            iae.Value = true;
            await connection.Settings.UpdateAsync(iae);
            
            await connection.Users.LoginAsync(admin.Username, "password");
            
            var result = await connection.Users.FindAsync(admin.Id);
            Console.WriteLine(result);
        }
    }
}
