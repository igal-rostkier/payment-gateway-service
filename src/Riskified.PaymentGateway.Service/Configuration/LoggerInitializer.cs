using System;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace Riskified.PaymentGateway.Service.Configuration
{
    internal static class LoggerInitializer
    {
        /// <summary>
        /// Used for logging errors in application start
        /// </summary>
        public static Logger CreateMinimalLogger() =>
            new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

        /// <summary>
        /// Configure Serilog for general ILogger usage
        /// </summary>
        public static void ConfigureSerilog(HostBuilderContext builderContext, IServiceProvider serviceProvider,
            LoggerConfiguration config)
        {
            config.ReadFrom.Configuration(builderContext.Configuration);
        }
    }   
}