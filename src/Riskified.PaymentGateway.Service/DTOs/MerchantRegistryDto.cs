using System;
using Newtonsoft.Json;

namespace Riskified.PaymentGateway.Service.DTOs
{
    [Serializable]
    public class MerchantRegistryDto
    {
        [JsonProperty("reason")] 
        public string Reason { get; set; } = null!;
        
        [JsonProperty("count")] 
        public int Count { get; set; }
    }
}