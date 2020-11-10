namespace AivenEcommerce.V1.Infrastructure.Options
{
    public interface IGitHubOptions
    {
        long ProductImageRepositoryId { get; set; }
        long ProductCategoryRepositoryId { get; set; }
        long ProductOverviewRepositoryId { get; set; }
        string Token { get; set; }
    }
}