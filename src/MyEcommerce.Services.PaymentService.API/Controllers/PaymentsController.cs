namespace MyEcommerce.Services.PaymentService.API.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using MyEcommerce.Services.PaymentService.API.Configurations;
    using MyEcommerce.Services.PaymentService.API.Models;
    using Stripe;

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IOptionsMonitor<StripeOptions> _stripeOptions;

        public PaymentsController(IOptionsMonitor<StripeOptions> stripeOptions)
        {
            _stripeOptions = stripeOptions;
            StripeConfiguration.ApiKey = _stripeOptions.CurrentValue.SecretKey;
        }

        [HttpPost(Name=nameof(CreateAsync))]
        public async Task<ActionResult<StripePaymentResponse>> CreateAsync(
            [FromBody] StripePaymentRequest request
        ) {
            var options = new PaymentIntentCreateOptions
            {
                Amount = request.Amount,
                Currency = "usd",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true
                }
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            return new StripePaymentResponse
            {
                ClientSecret = paymentIntent.ClientSecret
            };
        }
    }
}