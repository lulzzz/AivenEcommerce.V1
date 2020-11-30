using AivenEcommerce.V1.Domain.Shared.Dtos.Products;

using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.Baskets
{
    public record UpdateBasketInput(IEnumerable<ProductDefinitive> Products, string CustomerEmail);
}
