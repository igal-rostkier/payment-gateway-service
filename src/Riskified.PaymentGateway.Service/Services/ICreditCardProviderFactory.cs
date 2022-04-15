namespace Riskified.PaymentGateway.Service.Services
{
    public interface ICreditCardProviderFactory
    {
        ICreditCardProvider? GetCreditCardProvider(Enums.CreditCardProvider creditCardProvider);
    }
}