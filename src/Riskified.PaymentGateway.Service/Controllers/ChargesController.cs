using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Riskified.PaymentGateway.Service.DTOs;
using Riskified.PaymentGateway.Service.Registry;
using Riskified.PaymentGateway.Service.Services;

namespace Riskified.PaymentGateway.Service.Controllers
{
    [Route("api")]
    public class ChargesController : ControllerBase
    {
        private readonly ILogger<ChargesController> _logger;
        private readonly ICreditCardProviderFactory _creditCardProviderFactory;
        private readonly IRegistryService _registryService;

        public ChargesController(ILogger<ChargesController> logger, ICreditCardProviderFactory creditCardProviderFactory,
            IRegistryService registryService)
        {
            _logger = logger;
            _creditCardProviderFactory = creditCardProviderFactory;
            _registryService = registryService;
        }

        [HttpPost]
        [Route("charge")]
        public async Task<IActionResult> ChargeAsync([FromHeader(Name = "merchant-identifier")] string merchantId, 
            [FromBody] ChargeDto charge)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                var creditCardProvider = _creditCardProviderFactory.GetCreditCardProvider(charge.GetCreditCardProvider());
                ChargeResponse chargeResponse = await creditCardProvider!.Charge(charge);
                if (chargeResponse.IsSuccess) return Ok();
                _registryService.AddRegistry(merchantId, chargeResponse.ResultReason);
                return Ok(new ChargeResponseDto
                {
                    Error = "Card declined"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ChargesController ChargeAsync Error");
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("chargeStatuses")]
        public IActionResult ChargeStatusAsync([FromHeader(Name = "merchant-identifier")] string merchantId)
        {
            try
            {
                return Ok(_registryService.GetRegistry(merchantId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ChargesController ChargeStatusAsync Error");
                return StatusCode(500);
            }
        }
    }
}