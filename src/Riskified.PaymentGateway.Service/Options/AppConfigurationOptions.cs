using System;

namespace Riskified.PaymentGateway.Service.Options
{
    [Serializable]
    public class AppConfigurationOptions
    {
        public const string AppConfiguration = "AppConfiguration";

        public string Identifier { get; set; } = null!;
    }
}