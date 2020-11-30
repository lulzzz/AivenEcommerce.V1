
using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.WishLists
{
    public record WishListDto(Guid Id, string CustomerEmail, IEnumerable<string> Products);
}