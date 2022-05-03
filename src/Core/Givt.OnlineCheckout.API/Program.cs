using Givt.OnlineCheckout.API;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace Givt.OnlineCheckout.API
{
    public class Program
    {
        private const string ConfigKeyCommonPrefix = "Common:";
        private const string ConfigKeyAppPrefix = "GOC:";
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var settings = config.Build();
                        config
                            .AddAzureAppConfiguration(options =>
                            {
                                options.Connect(settings.GetConnectionString("AzureAppConfiguration"))
                                // order of .Select() is important, as the key/values selected will be layered. Last one takes precedence.
                                .Select(ConfigKeyCommonPrefix + KeyFilter.Any, null)
                                .Select(ConfigKeyCommonPrefix + KeyFilter.Any, hostingContext.HostingEnvironment.EnvironmentName)
                                .Select(ConfigKeyAppPrefix + KeyFilter.Any, null)
                                .Select(ConfigKeyAppPrefix + KeyFilter.Any, hostingContext.HostingEnvironment.EnvironmentName)
                                // order of .TrimKeyPrefix() is important too, as existing key names will survive collisions. So first one takes precedence.
                                .TrimKeyPrefix(ConfigKeyAppPrefix) 
                                .TrimKeyPrefix(ConfigKeyCommonPrefix)
                                ;
                            })
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) // I think we dont need this anymore right? Bcus AddAzureAppConfig() ?
                            .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables();
                    })
                    .UseDefaultServiceProvider(x =>
                    {
                        x.ValidateOnBuild = false;
                        x.ValidateScopes = false;
                    })
                    .UseStartup<Startup>()
                    .UseUrls("http://*:5000")
                    .Build();
    }
}