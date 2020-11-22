using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Dtos.WishLists
{
    public record UpdateWishListInput(string CustomerEmail, IEnumerable<string> Products);
}
