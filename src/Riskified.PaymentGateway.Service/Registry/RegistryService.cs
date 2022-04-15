using System.Collections.Generic;
using Riskified.PaymentGateway.Registry;
using Riskified.PaymentGateway.Service.DTOs;

namespace Riskified.PaymentGateway.Service.Registry
{
    public class RegistryService : IRegistryService
    {
        private readonly Dictionary<string, MerchantRegistry> _registry;

        public RegistryService()
        {
            _registry = new Dictionary<string, MerchantRegistry>();
        }
        
        public void AddRegistry(string merchantId, string message)
        {
            _registry.TryGetValue(merchantId, out MerchantRegistry? registry);
            if (registry == null)
            {
                registry = new MerchantRegistry();
                _registry.Add(merchantId, registry);
            }
            registry.AddRegistry(message);
        }

        public List<MerchantRegistryDto> GetRegistry(string merchantId)
        {
            var merchantRegistry = new List<MerchantRegistryDto>();
            _registry.TryGetValue(merchantId, out MerchantRegistry? registry);
            if (registry == null) return merchantRegistry;
            foreach (var registryKey in registry.Registry.Keys)
            {
                merchantRegistry.Add(new MerchantRegistryDto
                {
                    Reason = registryKey,
                    Count = registry.Registry[registryKey]
                });
            }
            return merchantRegistry;
        }
    }
}