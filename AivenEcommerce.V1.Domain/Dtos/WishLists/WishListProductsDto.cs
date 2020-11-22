using System;
using System.Collections.Generic;

using AivenEcommerce.V1.Domain.Dtos.Products;

namespace AivenEcommerce.V1.Domain.Dtos.WishLists
{
    public record WishListProductsDto(Guid Id, string CustomerEmail, IEnumerable<ProductDto> Products);
}
