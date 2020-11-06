using System;

using AivenEcommerce.V1.Domain.Enums;

namespace AivenEcommerce.V1.Domain.Dtos.Products
{
    public record ProductDto(string? Id, string Name, string Description, decimal Cost, decimal Price, short PercentageOff, ProductCategory Category, ProductSubCategory SubCategory, int Stock, Uri? Thumbnail, bool IsActive);
}
