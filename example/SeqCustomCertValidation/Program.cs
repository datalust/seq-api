using DocoptNet;
using Seq.Api;
using Seq.Api.Model.Settings;
using System;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace SeqCustomCertValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            const string Usage = @"seq-query: run Seq SQL queries from your console.

Usage:
    seq-cust-ssl.exe <url> [--usecustssl=<c>] 
    seq-cust-ssl.exe (-h | --help)

Options:
    -h --help     Show this screen.
    --usecustssl=<c>  Use custom ssl true/false.
    ";

            var arguments = new Docopt().Apply(Usage, args, version: "Seq Custom Certificate Validation 0.1", exit: true);
            string url = arguments["<url>"].ToString();
            bool usecustssl = Convert.ToBoolean(arguments["--usecustssl"].Value);
        

            var connection = new SeqConnection(url, serverCertificateCustomValidationCallback: !usecustssl? ((Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool>)null): (httpRequestMessage, cert, cetChain, policyErrors) =>
            {
                return true;
            });
            var limit = connection?.Settings.FindNamedAsync(SettingName.RawEventMaximumContentLength).GetAwaiter().GetResult();
            Console.WriteLine($"RawEventMaximumContentLength:{limit.Value}");
        }
    }
}
