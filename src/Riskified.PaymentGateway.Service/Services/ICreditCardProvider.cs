using System.Threading.Tasks;
using Riskified.PaymentGateway.Service.DTOs;

namespace Riskified.PaymentGateway.Service.Services
{
    public interface ICreditCardProvider
    {
        Task<ChargeResponse> Charge(ChargeDto charge);
    }
}