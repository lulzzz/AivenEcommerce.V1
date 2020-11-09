namespace AivenEcommerce.V1.Domain.Dtos.Products
{
    public record UpdateProductCostPriceInput(string ProductId, decimal Cost, decimal Price);
}
