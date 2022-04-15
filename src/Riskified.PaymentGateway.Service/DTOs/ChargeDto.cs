using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Riskified.PaymentGateway.Service.DTOs
{
    [Serializable]
    public class ChargeDto
    {
        [Required]
        [JsonProperty("fullName")] 
        public string Fullname { get; set; } = null!;
        
        [Required]
        [JsonProperty("creditCardNumber")]
        public string CreditCardNumber { get; set; } = null!;
        
        [Required]
        [CreditCardCompanyValidator]
        [JsonProperty("creditCardCompany")] 
        public string CreditCardCompany { get; set; } = null!;
        
        [Required]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$")] // MM/YY
        [JsonProperty("expirationDate")] 
        public string ExpirationDate { get; set; } = null!;
        
        [Required]
        [JsonProperty("cvv")] 
        public string Cvv { get; set; } = null!;
        
        [Required]
        [JsonProperty("amount")] 
        public decimal Amount { get; set; }

        public Enums.CreditCardProvider GetCreditCardProvider()
            => Enum.Parse<Enums.CreditCardProvider>(CreditCardCompany, ignoreCase: true);
    }

    public class CreditCardCompanyValidator : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return Enum.TryParse(value!.ToString(), true, out Enums.CreditCardProvider enumValue);
        }
    }
}