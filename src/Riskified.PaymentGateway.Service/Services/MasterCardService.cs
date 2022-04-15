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
    public class MasterCardService : ICreditCardProvider
    {
        private readonly ILogger<MasterCardService> _logger;
        private readonly HttpClient _httpClient;
        private readonly MasterCardServiceOptions _options;
        
        public MasterCardService(ILogger<MasterCardService> logger, HttpClient httpClient, 
            IOptions<MasterCardServiceOptions> options)
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
                    return new ChargeResponse
                    {
                        IsSuccess = true
                    };
                }
                string responseStr = await response.Content.ReadAsStringAsync();
                MasterCardResponse responseObj = JsonConvert.DeserializeObject<MasterCardResponse>(responseStr)!;
                return new ChargeResponse
                {
                    IsSuccess = false,
                    ResultReason = !string.IsNullOrEmpty(responseObj.DeclineReason) ? 
                        responseObj.DeclineReason : responseObj.Error
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while trying to charge with MasterCardService");
                throw;
            }
        }
        
        private MasterCardRequest MapRequest(ChargeDto charge)
            => new MasterCardRequest
            {
                FirstName = charge.Fullname.Split(" ")[0],
                LastName = charge.Fullname.Split(" ")[1],
                CardNumber = charge.CreditCardNumber,
                Expiration = charge.ExpirationDate.Replace("/", "-"),
                Cvv = charge.Cvv,
                ChargeAmount = charge.Amount
            };
    }
}