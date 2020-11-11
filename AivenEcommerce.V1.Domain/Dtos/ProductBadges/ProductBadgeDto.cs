using System;
using System.Collections.Generic;

using AivenEcommerce.V1.Domain.Enums;

namespace AivenEcommerce.V1.Domain.Dtos.ProductBadges
{
    public record ProductBadgeDto(Guid Id, string ProductId, IEnumerable<ProductBadgeName> Badges);
}
