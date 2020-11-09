namespace AivenEcommerce.V1.Infrastructure.Options
{
    public interface IGitHubOptions
    {
        int ProductImageRepositoryId { get; set; }
        int ProductCategoryRepositoryId { get; set; }
        string Token { get; set; }
    }
}