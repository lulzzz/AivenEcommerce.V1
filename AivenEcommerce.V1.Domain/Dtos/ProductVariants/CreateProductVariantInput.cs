using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Dtos.ProductVariants
{
    public record CreateProductVariantInput(string ProductId, string Name, IEnumerable<string> Values);
}
