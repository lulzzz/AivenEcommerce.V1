
using System;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.Products
{
    public record ProductDto(string Id,
        string Name,
        decimal Cost,
        int Price,
        short PercentageOff,
        string Category,
        string SubCategory,
        int Stock,
        Uri Thumbnail,
        bool IsActive);
}
