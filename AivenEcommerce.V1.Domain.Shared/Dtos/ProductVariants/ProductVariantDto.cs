
using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.ProductVariants
{
    public record ProductVariantDto(Guid Id, string ProductId, string Name, IEnumerable<string> Values);
}
