namespace AivenEcommerce.V1.Domain.Shared.Dtos.Products
{
    public record CreateProductInput(string Name,
        string Description,
        decimal Cost,
        int Price,
        string Category,
        string SubCategory);


}
