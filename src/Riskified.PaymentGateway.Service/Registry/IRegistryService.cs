using System.Collections.Generic;
using Riskified.PaymentGateway.Service.DTOs;

namespace Riskified.PaymentGateway.Service.Registry
{
    public interface IRegistryService
    {
        void AddRegistry(string merchantId, string message);

        List<MerchantRegistryDto> GetRegistry(string merchantId);
    }
}