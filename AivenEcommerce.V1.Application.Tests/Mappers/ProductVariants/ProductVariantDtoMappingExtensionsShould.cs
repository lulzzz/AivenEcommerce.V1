using AivenEcommerce.V1.Application.Mappers.ProductVariants;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductVariants;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Mappers.ProductVariants
{
    public class ProductVariantDtoMappingExtensionsShould
    {
        [Fact]
        public void ConvertToDto_ProductVariantNotNull_ReturnSameProductId()
        {
            ProductVariant variant = MockProductVariant();

            ProductVariantDto variantDto = variant.ConvertToDto();

            Assert.Equal(variant.ProductId, variantDto.ProductId);
        }

        [Fact]
        public void ConvertToDto_ProductVariantNotNull_ReturnSameId()
        {
            ProductVariant variant = MockProductVariant();

            ProductVariantDto variantDto = variant.ConvertToDto();

            Assert.Equal(variant.Id, variantDto.Id);
        }

        [Fact]
        public void ConvertToDto_ProductVariantNotNull_ReturnSameName()
        {
            ProductVariant variant = MockProductVariant();

            ProductVariantDto variantDto = variant.ConvertToDto();

            Assert.Equal(variant.Name, variantDto.Name);
        }

        [Fact]
        public void ConvertToDto_ProductVariantNotNull_ReturnSameValues()
        {
            ProductVariant variant = MockProductVariant();

            ProductVariantDto variantDto = variant.ConvertToDto();

            Assert.Equal(variant.Values, variantDto.Values);
        }

        private ProductVariant MockProductVariant() =>
            new()
            {
                Id = Guid.NewGuid(),
                Name = nameof(ProductVariant.Name),
                ProductId = nameof(ProductVariant.ProductId),
                Values = new[] { "a" }
            };
    }
}
