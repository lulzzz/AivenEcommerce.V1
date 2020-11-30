using AivenEcommerce.V1.Domain.Shared.Enums;

using System;
using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.ProductBadges
{
    public record ProductBadgeDto(Guid Id, string ProductId, IEnumerable<ProductBadgeName> Badges);
}
