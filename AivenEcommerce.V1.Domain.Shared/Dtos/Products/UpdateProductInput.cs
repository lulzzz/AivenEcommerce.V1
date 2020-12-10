﻿
using System;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.Products
{
    public record UpdateProductInput(string Id,
        string Name,
        decimal Cost,
        decimal Price,
        short PercentageOff,
        string Category,
        string SubCategory,
        Uri Thumbnail);
}