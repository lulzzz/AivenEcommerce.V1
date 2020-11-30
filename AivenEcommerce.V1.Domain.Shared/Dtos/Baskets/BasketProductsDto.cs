using AivenEcommerce.V1.Domain.Shared.Dtos.Products;

using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.Baskets
{
    public record BasketProductsDto(Guid Id, IEnumerable<ProductDefinitive> ProductDefinitives, IEnumerable<ProductDto> Products, string CustomerEmail);
}
