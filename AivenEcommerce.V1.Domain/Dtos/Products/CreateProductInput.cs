using System;

namespace AivenEcommerce.V1.Domain.Dtos.Products
{
    public record CreateProductInput(string Name, string Description, decimal Cost, decimal Price, short PercentageOff, string Category, string SubCategory, Uri Thumbnail);


}
