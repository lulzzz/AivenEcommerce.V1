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
        long SaleDetailRepositoryId { get; set; }
        long BasketRepositoryId { get; set; }
        long WishListRepositoryId { get; set; }
        long CouponCodeRepositoryId { get; set; }
        long OrderDetailRepositoryId { get; set; }
        long AddressRepositoryId { get; set; }
        long InvoiceRepositoryId { get; set; }
        long DeliveryRepositoryId { get; set; }
        string Token { get; set; }
    }
}