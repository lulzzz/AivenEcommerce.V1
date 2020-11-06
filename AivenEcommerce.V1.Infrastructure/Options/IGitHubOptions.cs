namespace AivenEcommerce.V1.Infrastructure.Options
{
    public interface IGitHubOptions
    {
        int BasketRepositoryId { get; set; }
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        int DeliveryRepositoryId { get; set; }
        int OrderRepositoryId { get; set; }
        int ProductRepositoryId { get; set; }
        string Token { get; set; }
    }
}