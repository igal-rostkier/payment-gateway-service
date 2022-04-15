using System;
using Newtonsoft.Json;

namespace Riskified.PaymentGateway.Service.DTOs
{
    [Serializable]
    public class ChargeResponseDto
    {
        [JsonProperty("error")] 
        public string Error { get; set; } = null!;
    }
}