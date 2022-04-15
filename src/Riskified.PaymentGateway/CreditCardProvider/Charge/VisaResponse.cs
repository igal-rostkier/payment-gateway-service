using System;
using Newtonsoft.Json;

namespace Riskified.PaymentGateway.CreditCardProvider.Charge
{
    [Serializable]
    public class VisaResponse
    {
        [JsonProperty("chargeResult")] 
        public string ChargeResult { get; set; } = null!;
        
        [JsonProperty("resultReason")] 
        public string ResultReason { get; set; } = null!;
    }
}