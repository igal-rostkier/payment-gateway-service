using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Riskified.PaymentGateway.Service.Configuration
{
    public static class HealthCheckConfiguration
    {
        public static void ConfigureHealthCheck(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/api/charge/health", new HealthCheckOptions
            {
                ResponseWriter = WriteHealthCheckResponse,
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status406NotAcceptable,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
                },
            });
        }

        private static Task WriteHealthCheckResponse(HttpContext context, HealthReport result)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            var obj =
                new
                {
                    Status = result.Status.ToString(),
                    Results = result.Entries.Select(static entry => new
                    {
                        entry.Key,
                        Status = entry.Value.Status.ToString(),
                        Exception = entry.Value.Exception?.ToString(),
                    }),
                };
            string json = JsonSerializer.Serialize(obj);

            return context.Response.WriteAsync(json);
        }
    }   
}
