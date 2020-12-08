namespace AivenEcommerce.V1.Infrastructure.Options.ClientConfig
{
    public class ClientConfigOptions : IClientConfigOptions
    {
        public string WebhookReturnUrl { get; set; }
        public string WebhookCancelUrl { get; set; }
        public string BuildVersion { get; set; }
    }
}
