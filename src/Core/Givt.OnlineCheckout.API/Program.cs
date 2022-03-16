using Givt.OnlineCheckout.API;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Givt.OnlineCheckout.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();

            
        }
        public static IWebHost BuildWebHost(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration((hostingContext, config) => {
                        config
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
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
