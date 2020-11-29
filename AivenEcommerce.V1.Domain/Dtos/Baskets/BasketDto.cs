using AivenEcommerce.V1.Domain.Dtos.Products;

using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Dtos.Baskets
{
    public record BasketDto(Guid Id, IEnumerable<ProductDefinitive> ProductDefinitives, string CustomerEmail);
}
