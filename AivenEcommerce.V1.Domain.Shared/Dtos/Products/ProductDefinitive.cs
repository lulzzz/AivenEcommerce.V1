using AivenEcommerce.V1.Domain.Shared.Dtos.ProductVariants;

using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.Products
{
    public record ProductDefinitive(string ProductId, IEnumerable<ProductVariantPair> Variants, int Quantity);
}
