using AivenEcommerce.V1.Domain.Dtos.Products;

using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Dtos.Baskets
{
    public record UpdateBasketInput(IEnumerable<ProductDefinitive> Products, string CustomerEmail);
}
