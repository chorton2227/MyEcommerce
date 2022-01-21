namespace MyEcommerce.Services.PaymentService.API.Configurations
{
    public class StripeOptions
    {
        public const string Stripe = "Stripe";
        public string PublicKey { get; set; }
        public string SecretKey { get; set; }
    }
}