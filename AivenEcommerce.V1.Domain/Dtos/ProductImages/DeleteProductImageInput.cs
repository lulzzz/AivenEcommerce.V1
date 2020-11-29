using System;

namespace AivenEcommerce.V1.Domain.Dtos.ProductImages
{
    public record DeleteProductImageInput(string ProductId, Guid ProductImageId);

}
