using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using ShopsRUs.Service.Core.Storage.Models;

namespace ShopsRUs.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();

            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(config)
            .CreateLogger();

            try
            {
                Log.Information("Application Is Starting");
                CreateWebHostBuilder(args)
                    .Build()
                    .MigrateDatabase()
                    .Run();
            }
            catch (Exception exception)
            {
                Log.Fatal(exception, "Application Startup Failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
             WebHost.CreateDefaultBuilder(args)
             .UseKestrel()
             .UseSerilog()
             .UseIISIntegration()
             .UseIIS()
             .UseEnvironment(LoadEnvironment())
             .UseStartup<Startup>();

        public static string LoadEnvironment()
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            return config.GetSection("Serilog").GetSection("Properties").GetSection("Environment").Value;
        }
    }
}
