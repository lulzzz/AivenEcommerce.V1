using AivenEcommerce.V1.Application.Mappers.ProductVariants;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductVariants;

using System;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Mappers.ProductVariants
{
    public class CreateProductVariantInputMappingExtensionsShould
    {

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameName()
        {
            CreateProductVariantInput input = MockCreateProductVariantInput();

            ProductVariant variant = input.ConvertToEntity();

            Assert.Equal(input.Name, variant.Name);
        }


        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameProductId()
        {
            CreateProductVariantInput input = MockCreateProductVariantInput();

            ProductVariant variant = input.ConvertToEntity();

            Assert.Equal(input.ProductId, variant.ProductId);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameValues()
        {
            CreateProductVariantInput input = MockCreateProductVariantInput();

            ProductVariant variant = input.ConvertToEntity();

            Assert.Equal(input.Values, variant.Values);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnEmplyId()
        {
            CreateProductVariantInput input = MockCreateProductVariantInput();

            ProductVariant variant = input.ConvertToEntity();

            Assert.Equal(Guid.Empty, variant.Id);
        }

        private CreateProductVariantInput MockCreateProductVariantInput() =>
        new(nameof(CreateProductVariantInput.ProductId), nameof(CreateProductVariantInput.Name), new[] { "a" });
    }
}
