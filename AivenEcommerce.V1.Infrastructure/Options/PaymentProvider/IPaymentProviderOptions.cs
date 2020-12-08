namespace AivenEcommerce.V1.Infrastructure.Options.PaymentProvider
{
    public interface IPaymentProviderOptions
    {
        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }
    }
}
