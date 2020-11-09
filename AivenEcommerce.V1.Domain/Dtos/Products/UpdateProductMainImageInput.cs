using System;

namespace AivenEcommerce.V1.Domain.Dtos.Products
{
    public record UpdateProductMainImageInput(string ProductId, Uri Image);
}
