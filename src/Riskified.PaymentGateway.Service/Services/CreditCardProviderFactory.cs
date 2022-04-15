using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Riskified.PaymentGateway.Service.Services
{
    public class CreditCardProviderFactory : ICreditCardProviderFactory
    {
        private ICreditCardProvider _visaService;
        private ICreditCardProvider _masterCardService;
        
        public CreditCardProviderFactory(IServiceProvider serviceProvider)
        {
            var services = serviceProvider.GetServices<ICreditCardProvider>();
            _visaService = services.First(e => e.GetType() == typeof(VisaService));
            _masterCardService = services.First(e => e.GetType() == typeof(MasterCardService));
        }

        public ICreditCardProvider? GetCreditCardProvider(Enums.CreditCardProvider creditCardProvider)
        {
            switch (creditCardProvider)
            {
                case Enums.CreditCardProvider.Visa:
                    return _visaService;
                case Enums.CreditCardProvider.MasterCard:
                    return _masterCardService;
                default:
                    return null;
            }
        }
    }
}