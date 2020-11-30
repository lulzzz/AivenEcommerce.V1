using AivenEcommerce.V1.Modules.PayPal.Enum;

namespace AivenEcommerce.V1.Modules.PayPal.Options
{
    public class PayPalOptions : IPayPalOptions
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public PayPalEnvironment Environment { get; set; }
        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }
        public string WebhookUrl { get; set; }
    }
}
