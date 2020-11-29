using AivenEcommerce.V1.Domain.Dtos.Products;

using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Dtos.Baskets
{
    public record BasketProductsDto(Guid Id, IEnumerable<ProductDefinitive> ProductDefinitives, IEnumerable<ProductDto> Products, string CustomerEmail);
}
