using AivenEcommerce.V1.Domain.Enums;

using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Dtos.Products
{
    public record UpdateProductBadgeInput(string ProductId, short PercentageOff, IEnumerable<ProductBadgeName> Badges);
}
