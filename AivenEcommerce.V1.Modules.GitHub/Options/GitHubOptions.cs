namespace AivenEcommerce.V1.Modules.GitHub.Options
{
    public class GitHubOptions : IGitHubOptions
    {
        public long ProductCategoryRepositoryId { get; set; }
        public long ProductImageRepositoryId { get; set; }
        public long ProductOverviewRepositoryId { get; set; }
        public long ProductBadgeRepositoryId { get; set; }
        public long ProductVariantRepositoryId { get; set; }
        public long CustomerRepositoryId { get; set; }
        public string Token { get; set; }
    }
}
