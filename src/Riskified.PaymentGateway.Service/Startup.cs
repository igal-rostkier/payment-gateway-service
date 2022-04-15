using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Riskified.PaymentGateway.Service.Configuration;
using Riskified.PaymentGateway.Service.Options;
using Riskified.PaymentGateway.Service.Registry;
using Riskified.PaymentGateway.Service.Services;
using Serilog;

namespace Riskified.PaymentGateway.Service
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(SwaggerConfiguration.Configure);

            services.AddHealthChecks();

            services.Configure<AppConfigurationOptions>(
                _configuration.GetSection(AppConfigurationOptions.AppConfiguration));
            services.Configure<VisaServiceOptions>(_configuration.GetSection(VisaServiceOptions.VisaServiceConfig));
            services.Configure<MasterCardServiceOptions>(
                _configuration.GetSection(MasterCardServiceOptions.MasterCardServiceConfig));

            services.AddScoped<ICreditCardProviderFactory, CreditCardProviderFactory>();

            var identifier = _configuration.GetSection(AppConfigurationOptions.AppConfiguration)["Identifier"];
            Int32.TryParse(_configuration.GetSection(AppConfigurationOptions.AppConfiguration)["Retries"], out int retries);
            
            services.AddHttpClient<ICreditCardProvider, VisaService>(e =>
                    e.DefaultRequestHeaders.Add("identifier", identifier))
                .AddPolicyHandler((serviceProvider, request) => GetRetryPolicy(serviceProvider, retries));
            
            services.AddHttpClient<ICreditCardProvider, MasterCardService>();

            services.AddSingleton<IRegistryService, RegistryService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(static endpoints =>
            {
                endpoints.MapControllers();
                endpoints.ConfigureHealthCheck();
            });

            app.UseSwagger()
                .UseSwaggerUI(static c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentGateway API V1"));
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(IServiceProvider services, int retries)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => !msg.IsSuccessStatusCode && msg.StatusCode != HttpStatusCode.BadRequest)
                .WaitAndRetryAsync(retries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(retryAttempt, 2)),
                    onRetry: (result, span, retry, _) =>
                    {
                        services.GetService<ILogger<HttpClient>>()!
                            .LogInformation($"HttpClient retry - statusCode: {result.Result.StatusCode}, span: {span}, retryCount: {retry}");
                    });
        }
    }
}
