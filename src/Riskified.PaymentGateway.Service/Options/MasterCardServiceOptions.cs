using System;

namespace Riskified.PaymentGateway.Service.Options
{
    [Serializable]
    public class MasterCardServiceOptions
    {
        public const string MasterCardServiceConfig = "MasterCardServiceConfig";

        public string Url { get; set; } = null!;
    }
}