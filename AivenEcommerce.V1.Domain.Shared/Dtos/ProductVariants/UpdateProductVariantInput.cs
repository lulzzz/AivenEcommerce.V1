
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.ProductVariants
{
    public record UpdateProductVariantInput(string ProductId, IEnumerable<ProductVariantLiteDto> Variants);
}
