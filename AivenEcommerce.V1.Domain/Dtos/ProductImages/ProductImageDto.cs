using System;

namespace AivenEcommerce.V1.Domain.Dtos.ProductImages
{
    public record ProductImageDto(Guid Id, string ProductId, Uri Image);
}
