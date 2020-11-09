namespace AivenEcommerce.V1.Infrastructure.Options
{
    public class GitHubOptions : IGitHubOptions
    {
        public int ProductCategoryRepositoryId { get; set; }
        public int ProductImageRepositoryId { get; set; }
        public string Token { get; set; }
        
    }
}
