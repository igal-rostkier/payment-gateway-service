using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Riskified.PaymentGateway.Service.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void Configure(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "PaymentGateway API", Version = "v1" });
            options.EnableAnnotations();
        }
    }   
}
