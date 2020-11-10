using System;

namespace AivenEcommerce.V1.Domain.Dtos.Products
{
    public record ProductDto(string? Id, string Name, decimal Cost, decimal Price, short PercentageOff, string? Category, string? SubCategory, int Stock, Uri? Thumbnail, bool IsActive);
}
