using AivenEcommerce.V1.Domain.Enums;

using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Dtos.ProductBadges
{
    public record ProductBadgeDto(Guid Id, string ProductId, IEnumerable<ProductBadgeName> Badges);
}
