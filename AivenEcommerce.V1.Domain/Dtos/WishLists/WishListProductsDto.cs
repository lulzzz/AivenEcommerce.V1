using AivenEcommerce.V1.Domain.Dtos.Products;

using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Dtos.WishLists
{
    public record WishListProductsDto(Guid Id, string CustomerEmail, IEnumerable<ProductDto> Products);
}
