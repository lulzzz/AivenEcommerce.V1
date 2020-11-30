using AivenEcommerce.V1.Domain.Shared.Dtos.Products;

using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.Baskets
{
    public record BasketDto(Guid Id, IEnumerable<ProductDefinitive> ProductDefinitives, string CustomerEmail);
}
