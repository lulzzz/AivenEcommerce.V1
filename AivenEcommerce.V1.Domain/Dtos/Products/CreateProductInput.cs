namespace AivenEcommerce.V1.Domain.Dtos.Products
{
    public record CreateProductInput(string Name, string Description, decimal Cost, decimal Price, string Category, string SubCategory);


}
