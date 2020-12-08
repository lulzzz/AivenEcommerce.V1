namespace AivenEcommerce.V1.Infrastructure.Options.PaymentProvider
{
    public class PaymentProviderOptions : IPaymentProviderOptions
    {
        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }
    }
}
