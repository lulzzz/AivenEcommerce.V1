using AivenEcommerce.V1.Application.Mappers.ProductBadges;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductBadges;
using AivenEcommerce.V1.Domain.Shared.Enums;

using System;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Mappers.ProductBadges
{
    public class ProductBadgeDtoMappingExtensionsShould
    {
        [Fact]
        public void ConvertToDto_ProductBadgeNotNull_ReturnSameId()
        {
            ProductBadge ProductBadge = MockProductBadge();

            ProductBadgeDto ProductBadgeDto = ProductBadge.ConvertToDto();

            Assert.Equal(ProductBadge.Id, ProductBadgeDto.Id);
        }

        [Fact]
        public void ConvertToDto_ProductBadgeNotNull_ReturnSameProductId()
        {
            ProductBadge ProductBadge = MockProductBadge();

            ProductBadgeDto ProductBadgeDto = ProductBadge.ConvertToDto();

            Assert.Equal(ProductBadge.ProductId, ProductBadgeDto.ProductId);
        }

        [Fact]
        public void ConvertToDto_ProductBadgeNotNull_ReturnSameBadges()
        {
            ProductBadge ProductBadge = MockProductBadge();

            ProductBadgeDto ProductBadgeDto = ProductBadge.ConvertToDto();

            Assert.Equal(ProductBadge.Badges, ProductBadgeDto.Badges);
        }



        private ProductBadge MockProductBadge() =>
            new()
            {
                Id = Guid.NewGuid(),
                ProductId = nameof(ProductBadge.ProductId),
                Badges = new[] { ProductBadgeName.LastUnits }
            };
    }
}
