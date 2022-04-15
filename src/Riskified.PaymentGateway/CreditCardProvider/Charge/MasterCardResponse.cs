using System;
using Newtonsoft.Json;

namespace Riskified.PaymentGateway.CreditCardProvider.Charge
{
    [Serializable]
    public class MasterCardResponse
    {
        [JsonProperty("decline_reason")] 
        public string DeclineReason { get; set; } = null!;

        [JsonProperty("error")] 
        public string Error { get; set; } = null!;
    }
}