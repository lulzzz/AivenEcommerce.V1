
using System;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.Products
{
    public record UpdateProductMainImageInput(string ProductId, Uri Image);
}
