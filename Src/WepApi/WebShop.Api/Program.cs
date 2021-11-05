using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using WebShop.Infrastructure;

namespace WebShop.Api
{
    public class Program
    {
        private static String env = Environment.GetEnvironmentVariable("APSNETCORE_ENVIRONMENT");

        private static IConfiguration Configuration
        {
            get
            {
                return new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile($"Configurations/appsettings.json", optional: false)
                    .AddJsonFile($"Configurations/appsettings.{env}.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();  
            }
        }
        
        private static IConfiguration SerilogConfiguration
        {
            get
            {                
                return new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile($"Configurations/serilog.json", optional: false)
                    .AddJsonFile($"Configurations/serilog.{env}.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();
            }
        }

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(SerilogConfiguration)
                .CreateLogger();

            host.SeedData()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseConfiguration(Configuration)
                        .UseStartup<Startup>()
                        .ConfigureLogging(e => e.ClearProviders())
                        .UseSerilog();
                });
            
        }
            
    }
}