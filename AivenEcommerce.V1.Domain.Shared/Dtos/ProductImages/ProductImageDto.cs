
using System;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.ProductImages
{
    public record ProductImageDto(Guid Id, string ProductId, Uri Image);
}
