using System;
using Newtonsoft.Json;

namespace Riskified.PaymentGateway.CreditCardProvider.Charge
{
    [Serializable]
    public class MasterCardRequest
    {
        [JsonProperty("first_name")] 
        public string FirstName { get; set; } = null!;
        
        [JsonProperty("last_name")] 
        public string LastName { get; set; } = null!;
        
        [JsonProperty("card_number")] 
        public string CardNumber { get; set; } = null!;
        
        [JsonProperty("expiration")] 
        public string Expiration { get; set; } = null!;
        
        [JsonProperty("cvv")] 
        public string Cvv { get; set; } = null!;
        
        [JsonProperty("charge_amount")] 
        public decimal ChargeAmount { get; set; }
    }
}