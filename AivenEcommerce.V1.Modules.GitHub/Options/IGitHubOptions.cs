namespace AivenEcommerce.V1.Modules.GitHub.Options
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