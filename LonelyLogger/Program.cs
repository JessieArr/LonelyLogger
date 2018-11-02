using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LonelyLogger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = BuildWebHost(args);
            
            webHost.Start();

            string address = webHost.ServerFeatures
                .Get<IServerAddressesFeature>()
                .Addresses
                .First();
            Console.WriteLine("Listening on: " + address);
            Console.WriteLine("Browse logs at: " + address + "/web/index.html");
            Console.WriteLine("Ctrl + C to terminate.");
            Console.CancelKeyPress += async (caller, eventArgs) =>
            {
                Console.WriteLine("Preparing to stop.");
                await webHost.StopAsync();
                Console.WriteLine("Webhost stopped.");
                return;
            };
            webHost.WaitForShutdown();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://localhost:5050")
                .Build();
    }
}
