namespace AivenEcommerce.V1.Infrastructure.Options
{
    public interface IGitHubOptions
    {
        long ProductImageRepositoryId { get; set; }
        long ProductCategoryRepositoryId { get; set; }
        long ProductOverviewRepositoryId { get; set; }
        long ProductBadgeRepositoryId { get; set; }
        string Token { get; set; }
    }
}