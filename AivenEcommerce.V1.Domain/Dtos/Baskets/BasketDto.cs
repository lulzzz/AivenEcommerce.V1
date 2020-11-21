using System;
using System.Collections.Generic;

using AivenEcommerce.V1.Domain.Dtos.Products;

namespace AivenEcommerce.V1.Domain.Dtos.Baskets
{
    public record BasketDto(Guid Id, IEnumerable<ProductDefinitive> Products, string CustomerEmail);
}
