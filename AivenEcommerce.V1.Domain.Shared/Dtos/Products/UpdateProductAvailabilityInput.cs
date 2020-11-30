namespace AivenEcommerce.V1.Domain.Shared.Dtos.Products
{
    public record UpdateProductAvailabilityInput(string ProductId,
        int Stock,
        bool IsActive);

}
