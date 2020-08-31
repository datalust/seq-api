using System;
using System.Threading.Tasks;
using DocoptNet;
using Seq.Api;
using Seq.Api.Model.Settings;

namespace SeqEnableAAD
{
    class Program
    {
        const string Usage = @"seq-enable-aad: enable authentication on your Seq server (for initial setup of a new Seq server only).

Usage:
    seq-enable-aad.exe <server> --uname=<un> --tenantid=<tid> --clientid=<cid> --clientkey=<ckey> [--authority=<a>]
    seq-enable-aad.exe (-h | --help)

Options:
    -h --help             Show this screen.
    --uname=<un>          Username. Azure Active Directory usernames must take the form of an email address.
    --tenantid=<tid>      Tenant ID.
    --clientid=<cid>      Client ID.
    --clientkey=<ckey>    Client key.
    --authority=<a>       Authority (optional, defaults to 'login.windows.net').
    ";
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                try
                {
                    var arguments = new Docopt().Apply(Usage, args, version: "Seq Enable AAD 0.1", exit: true);

                    var server = arguments["<server>"].ToString();
                    var username = Normalize(arguments["--uname"]);
                    var tenantId = Normalize(arguments["--tenantid"]);
                    var clientId = Normalize(arguments["--clientid"]);
                    var clientKey = Normalize(arguments["--clientkey"]);
                    var authority = Normalize(arguments["--authority"]);

                    await Run(server, username, tenantId, clientId, clientKey, authority);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("seq-enable-aad: {0}", ex);
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
        
        static async Task Run(string server, string username, string tenantId, string clientId, string clientKey, string authority="login.windows.net")
        {
            var connection = new SeqConnection(server);

            var user = await connection.Users.FindCurrentAsync();
            var provider = await connection.Settings.FindNamedAsync(SettingName.AuthenticationProvider);
            var cid = await connection.Settings.FindNamedAsync(SettingName.AzureADClientId);
            var ckey = await connection.Settings.FindNamedAsync(SettingName.AzureADClientKey);
            var aut = await connection.Settings.FindNamedAsync(SettingName.AzureADAuthority);
            var tid = await connection.Settings.FindNamedAsync(SettingName.AzureADTenantId);

            user.Username = username;
            provider.Value = "Azure Active Directory";
            cid.Value = clientId;
            ckey.Value = clientKey;
            tid.Value = tenantId;
            aut.Value = authority;

            await connection.Users.UpdateAsync(user);
            await connection.Settings.UpdateAsync(cid);
            await connection.Settings.UpdateAsync(ckey);
            await connection.Settings.UpdateAsync(tid);
            await connection.Settings.UpdateAsync(aut);

            await connection.Settings.UpdateAsync(provider); // needs to go before IsAuthenticationEnabled but after the other settings
            
            var iae = await connection.Settings.FindNamedAsync(SettingName.IsAuthenticationEnabled);
            iae.Value = true;
            await connection.Settings.UpdateAsync(iae); // this update needs to happen last, as enabling auth will lock this connection out
        }
    }
}
