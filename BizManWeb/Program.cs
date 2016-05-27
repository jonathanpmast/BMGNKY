using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;

namespace BizManWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.OpenExistingOnly);
            X509Certificate2 localhost = store.Certificates.Cast<X509Certificate2>().First(cert => cert.FriendlyName == @"IIS Express Development Certificate");
            var hostingConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", true)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .AddCommandLine(args)
                .Build();
            var host = new WebHostBuilder()
                .UseKestrel(options =>
                {
                    options.UseHttps(localhost);

                })
                .UseConfiguration(hostingConfig)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
