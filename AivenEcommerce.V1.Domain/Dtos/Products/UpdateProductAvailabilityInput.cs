namespace AivenEcommerce.V1.Domain.Dtos.Products
{
    public record UpdateProductAvailabilityInput(string ProductId, 
        int Stock, 
        bool IsActive);
    
}
