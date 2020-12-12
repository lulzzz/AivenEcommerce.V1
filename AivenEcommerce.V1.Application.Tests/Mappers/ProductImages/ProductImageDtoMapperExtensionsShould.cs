using AivenEcommerce.V1.Application.Mappers.ProductImages;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductImages;

using System;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Mappers.ProductImages
{
    public class ProductImageDtoMapperExtensionsShould
    {
        [Fact]
        public void ConvertToDto_ProductImageNotNull_ReturnSameId()
        {
            ProductImage image = MockProductImage();

            ProductImageDto imageDto = image.ConvertToDto();

            Assert.Equal(image.Id, imageDto.Id);
        }

        [Fact]
        public void ConvertToDto_ProductImageNotNull_ReturnSameImage()
        {
            ProductImage image = MockProductImage();

            ProductImageDto imageDto = image.ConvertToDto();

            Assert.Equal(image.Image, imageDto.Image);
        }

        [Fact]
        public void ConvertToDto_ProductImageNotNull_ReturnSameProductId()
        {
            ProductImage image = MockProductImage();

            ProductImageDto imageDto = image.ConvertToDto();

            Assert.Equal(image.ProductId, imageDto.ProductId);
        }

        private ProductImage MockProductImage() =>
            new()
            {
                Id = Guid.NewGuid(),
                Image = new("http://contoso.com"),
                ProductId = nameof(ProductImage.ProductId)
            };
    }
}
