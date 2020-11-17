using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Dtos.ProductVariants
{
    public record UpdateProductVariantInput(string ProductId, string Name, IEnumerable<string> Values);
}
