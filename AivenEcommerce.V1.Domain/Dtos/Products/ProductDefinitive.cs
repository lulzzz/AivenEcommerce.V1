using System.Collections.Generic;

using AivenEcommerce.V1.Domain.Dtos.ProductVariants;

namespace AivenEcommerce.V1.Domain.Dtos.Products
{
    public class ProductDefinitive
    {
        public int ProductId { get; set; }
        public IEnumerable<ProductVariantPair> Variants { get; set; }
    }
}
