using System;

namespace Riskified.PaymentGateway.Service.Options
{
    [Serializable]
    public class VisaServiceOptions
    {
        public const string VisaServiceConfig = "VisaServiceConfig";
        
        public string Url { get; set; } = null!;
    }
}