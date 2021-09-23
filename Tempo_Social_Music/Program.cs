using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;

namespace Tempo_Social_Music
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
.ConfigureAppConfiguration((context, config) =>
{
    //var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
    var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("Vault"));
    config.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

    //var root = config.Build();
    //config.AddAzureKeyVault($"https://{root["KeyVault:Vault"]}.vault.azure.net/", root["KeyVault:ClientId"], root["KeyVault:ClientSecret"]);
})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
