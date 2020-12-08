namespace AivenEcommerce.V1.Infrastructure.Options.ClientConfig
{
    public interface IClientConfigOptions
    {
        public string WebhookReturnUrl { get; set; }
        public string WebhookCancelUrl { get; set; }
        public string BuildVersion { get; set; }
    }
}
