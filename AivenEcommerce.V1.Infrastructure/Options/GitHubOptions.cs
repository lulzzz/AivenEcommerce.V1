namespace AivenEcommerce.V1.Infrastructure.Options
{
    public class GitHubOptions : IGitHubOptions
    {
        public int ProductRepositoryId { get; set; }
        public int BasketRepositoryId { get; set; }
        public int OrderRepositoryId { get; set; }
        public int DeliveryRepositoryId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Token { get; set; }
    }
}
