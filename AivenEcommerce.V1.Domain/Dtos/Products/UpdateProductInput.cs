using System;

namespace AivenEcommerce.V1.Domain.Dtos.Products
{
    public record UpdateProductInput(string Id, string Name, string Description, decimal Cost, decimal Price, short PercentageOff, string? Category, string? SubCategory, Uri Thumbnail);
}
