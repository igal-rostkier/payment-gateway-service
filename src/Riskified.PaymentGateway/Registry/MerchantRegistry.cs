using System;
using System.Collections.Generic;

namespace Riskified.PaymentGateway.Registry
{
    [Serializable]
    public class MerchantRegistry
    {
        public Dictionary<string, int> Registry { get; set; }

        public MerchantRegistry() => Registry = new Dictionary<string, int>();

        public void AddRegistry(string message)
        {
            Registry.TryGetValue(message, out var quantity);
            if (quantity == 0)
            {
                Registry.Add(message, 1);
            }
            else
            {
                Registry[message]++;
            }
        }
    }
}