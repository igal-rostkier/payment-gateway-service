using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Riskified.PaymentGateway.CreditCardProvider.Charge;
using Riskified.PaymentGateway.Service.DTOs;
using Riskified.PaymentGateway.Service.Options;

namespace Riskified.PaymentGateway.Service.Services
{
    public class VisaService : ICreditCardProvider
    {
        private readonly ILogger<VisaService> _logger;
        private readonly HttpClient _httpClient;
        private readonly VisaServiceOptions _options;
        
        public VisaService(ILogger<VisaService> logger, HttpClient httpClient, IOptions<VisaServiceOptions> options)
        {
            _logger = logger;
            _httpClient = httpClient;
            _options = options.Value;
        }
        
        public async Task<ChargeResponse> Charge(ChargeDto charge)
        {
            try
            {
                var requestBody = MapRequest(charge);
                var body = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_options.Url, body);
                if (response.IsSuccessStatusCode)
                {
                    string responseStr = await response.Content.ReadAsStringAsync();
                    return MapResponse(JsonConvert.DeserializeObject<VisaResponse>(responseStr)!);
                }
                return new ChargeResponse
                {
                    IsSuccess = false,
                    ResultReason = "Bad request"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while trying to charge with VisaService");
                throw;
            }
        }

        private VisaRequest MapRequest(ChargeDto charge)
            => new VisaRequest
            {
                FullName = charge.Fullname,
                Number = charge.CreditCardNumber,
                Expiration = charge.ExpirationDate,
                Cvv = charge.Cvv,
                TotalAmount = charge.Amount
            };

        private ChargeResponse MapResponse(VisaResponse response)
            => new ChargeResponse
            {
                IsSuccess = response.ChargeResult == "Success",
                ResultReason = response.ResultReason
            };
    }
}