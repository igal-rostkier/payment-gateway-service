using System;

namespace Riskified.PaymentGateway
{
    [Serializable]
    public class ChargeResponse
    {
        public bool IsSuccess { get; set; }

        public string ResultReason { get; set; } = null!;
    }
}