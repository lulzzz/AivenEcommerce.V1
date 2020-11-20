namespace AivenEcommerce.V1.Modules.GitHub.Options
{
    public interface IGitHubOptions
    {
        long ProductImageRepositoryId { get; set; }
        long ProductCategoryRepositoryId { get; set; }
        long ProductOverviewRepositoryId { get; set; }
        long ProductBadgeRepositoryId { get; set; }
        long ProductVariantRepositoryId { get; set; }
        long CustomerRepositoryId { get; set; }
        string Token { get; set; }
    }
}