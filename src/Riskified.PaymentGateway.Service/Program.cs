using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Riskified.PaymentGateway.Service.Configuration;
using Serilog;

namespace Riskified.PaymentGateway.Service
{
    public static class Program 
    {
        public static void Main(string[] args)
        {
            Log.Logger = LoggerInitializer.CreateMinimalLogger();
            try {
                Log.Information("Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex) {
                Log.Fatal(ex, "Application startup failed");
                throw;
            }
            finally {
                Log.CloseAndFlush();
            }
        }
        
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(LoggerInitializer.ConfigureSerilog)
                .ConfigureWebHostDefaults(static webBuilder => webBuilder.UseStartup<Startup>());
    }
}


