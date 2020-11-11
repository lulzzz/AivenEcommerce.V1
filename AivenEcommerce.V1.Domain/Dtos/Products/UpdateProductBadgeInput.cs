using System.Collections.Generic;

using AivenEcommerce.V1.Domain.Enums;

namespace AivenEcommerce.V1.Domain.Dtos.Products
{
    public record UpdateProductBadgeInput(string ProductId, short PercentageOff, IEnumerable<ProductBadgeName> Badges);
}
