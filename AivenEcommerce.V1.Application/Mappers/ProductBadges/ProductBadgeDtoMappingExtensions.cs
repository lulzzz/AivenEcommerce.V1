using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductBadges;

namespace AivenEcommerce.V1.Application.Mappers.ProductBadges
{
    public static class ProductBadgeDtoMappingExtensions
    {
        public static ProductBadgeDto ConvertToDto(this ProductBadge badge)
        {
            return new(badge.Id, badge.ProductId, badge.Badges);
        }
    }
}
