using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Dtos.ProductVariants
{
    public record UpdateProductVariantInput(string ProductId, IEnumerable<ProductVariantLiteDto> Variants);
}
