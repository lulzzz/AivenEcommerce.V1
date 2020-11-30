using AivenEcommerce.V1.Domain.Shared.Enums;

using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.Products
{
    public record UpdateProductBadgeInput(string ProductId, short PercentageOff, IEnumerable<ProductBadgeName> Badges);
}
