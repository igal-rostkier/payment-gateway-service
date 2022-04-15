using System;
using Newtonsoft.Json;

namespace Riskified.PaymentGateway.CreditCardProvider.Charge
{
    [Serializable]
    public class VisaRequest
    {
        [JsonProperty("fullName")] 
        public string FullName { get; set; } = null!;
        
        [JsonProperty("number")] 
        public string Number { get; set; } = null!;
        
        [JsonProperty("expiration")] 
        public string Expiration { get; set; } = null!;
        
        [JsonProperty("cvv")] 
        public string Cvv { get; set; } = null!;
        
        [JsonProperty("totalAmount")] 
        public decimal TotalAmount { get; set; }
    }
}