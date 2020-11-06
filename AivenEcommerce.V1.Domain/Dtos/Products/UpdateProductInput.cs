using System;

using AivenEcommerce.V1.Domain.Enums;

namespace AivenEcommerce.V1.Domain.Dtos.Products
{
    public record UpdateProductInput(string Id, string Name, string Description, decimal Cost, decimal Price, short PercentageOff, ProductCategory Category, ProductSubCategory SubCategory, Uri Thumbnail);
}
