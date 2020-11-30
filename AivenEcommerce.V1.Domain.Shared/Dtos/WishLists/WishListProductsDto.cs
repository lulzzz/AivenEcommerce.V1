using AivenEcommerce.V1.Domain.Shared.Dtos.Products;

using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.WishLists
{
    public record WishListProductsDto(Guid Id, string CustomerEmail, IEnumerable<ProductDto> Products);
}
