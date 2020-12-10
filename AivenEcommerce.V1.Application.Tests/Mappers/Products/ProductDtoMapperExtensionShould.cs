using AivenEcommerce.V1.Application.Mappers.Products;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Products;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Mappers.Products
{
    public class ProductDtoMapperExtensionShould
    {
        [Fact]
        public void ConvertToDto_ProductNotNull_ReturnSameName()
        {
            Product product = MockProduct();

            ProductDto productDto = product.ConvertToDto();

            Assert.Equal(productDto.Name, product.Name);
        }

        [Fact]
        public void ConvertToDto_ProductNotNull_ReturnSameId()
        {
            Product product = MockProduct();

            ProductDto productDto = product.ConvertToDto();

            Assert.Equal(productDto.Id, product.Id);
        }

        [Fact]
        public void ConvertToDto_ProductNotNull_ReturnSameCategory()
        {
            Product product = MockProduct();

            ProductDto productDto = product.ConvertToDto();

            Assert.Equal(productDto.Category, product.Category);
        }

        [Fact]
        public void ConvertToDto_ProductNotNull_ReturnSameSubCategory()
        {
            Product product = MockProduct();

            ProductDto productDto = product.ConvertToDto();

            Assert.Equal(productDto.SubCategory, product.SubCategory);
        }

        [Fact]
        public void ConvertToDto_ProductNotNull_ReturnSameCost()
        {
            Product product = MockProduct();

            ProductDto productDto = product.ConvertToDto();

            Assert.Equal(productDto.Cost, product.Cost);
        }

        [Fact]
        public void ConvertToDto_ProductNotNull_ReturnSamePrice()
        {
            Product product = MockProduct();

            ProductDto productDto = product.ConvertToDto();

            Assert.Equal(productDto.Price, product.Price);
        }

        [Fact]
        public void ConvertToDto_ProductNotNull_ReturnSameIsActive()
        {
            Product product = MockProduct();

            ProductDto productDto = product.ConvertToDto();

            Assert.Equal(productDto.IsActive, product.IsActive);
        }

        [Fact]
        public void ConvertToDto_ProductNotNull_ReturnSamePercentageOff()
        {
            Product product = MockProduct();

            ProductDto productDto = product.ConvertToDto();

            Assert.Equal(productDto.PercentageOff, product.PercentageOff);
        }

        [Fact]
        public void ConvertToDto_ProductNotNull_ReturnSameStock()
        {
            Product product = MockProduct();

            ProductDto productDto = product.ConvertToDto();

            Assert.Equal(productDto.Stock, product.Stock);
        }

        [Fact]
        public void ConvertToDto_ProductNotNull_ReturnSameThumbnail()
        {
            Product product = MockProduct();

            ProductDto productDto = product.ConvertToDto();

            Assert.Equal(productDto.Thumbnail, product.Thumbnail);
        }





        [Fact]
        public void ConvertToEntity_ProductDtoNotNull_ReturnSameName()
        {
            ProductDto productDto = MockProductDto();

            Product product = productDto.ConvertToEntity();

            Assert.Equal(product.Name, productDto.Name);
        }

        [Fact]
        public void ConvertToEntity_ProductDtoNotNull_ReturnSameId()
        {
            ProductDto productDto = MockProductDto();

            Product product = productDto.ConvertToEntity();

            Assert.Equal(product.Id, productDto.Id);
        }

        [Fact]
        public void ConvertToEntity_ProductDtoNotNull_ReturnSameCategory()
        {
            ProductDto productDto = MockProductDto();

            Product product = productDto.ConvertToEntity();

            Assert.Equal(product.Category, productDto.Category);
        }

        [Fact]
        public void ConvertToEntity_ProductDtoNotNull_ReturnSameSubCategory()
        {
            ProductDto productDto = MockProductDto();

            Product product = productDto.ConvertToEntity();

            Assert.Equal(product.SubCategory, productDto.SubCategory);
        }

        [Fact]
        public void ConvertToEntity_ProductDtoNotNull_ReturnSameCost()
        {
            ProductDto productDto = MockProductDto();

            Product product = productDto.ConvertToEntity();

            Assert.Equal(product.Cost, productDto.Cost);
        }

        [Fact]
        public void ConvertToEntity_ProductDtoNotNull_ReturnSamePrice()
        {
            ProductDto productDto = MockProductDto();

            Product product = productDto.ConvertToEntity();

            Assert.Equal(product.Price, productDto.Price);
        }

        [Fact]
        public void ConvertToEntity_ProductDtoNotNull_ReturnSameIsActive()
        {
            ProductDto productDto = MockProductDto();

            Product product = productDto.ConvertToEntity();

            Assert.Equal(product.IsActive, productDto.IsActive);
        }

        [Fact]
        public void ConvertToEntity_ProductDtoNotNull_ReturnSamePercentageOff()
        {
            ProductDto productDto = MockProductDto();

            Product product = productDto.ConvertToEntity();

            Assert.Equal(product.PercentageOff, productDto.PercentageOff);
        }

        [Fact]
        public void ConvertToEntity_ProductDtoNotNull_ReturnSameStock()
        {
            ProductDto productDto = MockProductDto();

            Product product = productDto.ConvertToEntity();

            Assert.Equal(product.Stock, productDto.Stock);
        }

        [Fact]
        public void ConvertToEntity_ProductDtoNotNull_ReturnSameThumbnail()
        {
            ProductDto productDto = MockProductDto();

            Product product = productDto.ConvertToEntity();

            Assert.Equal(product.Thumbnail, productDto.Thumbnail);
        }





        private Product MockProduct() =>
            new()
            {
                Id = "1",
                Category = "Category",
                Cost = 1,
                IsActive = true,
                Name = "Name",
                PercentageOff = 1,
                Price = 2,
                Stock = 3,
                SubCategory = "SubCategory",
                Thumbnail = new("http://contoso.com")
            };

        private ProductDto MockProductDto() =>
            new("1", "Name", 1, 2, 3, "Category", "SubCategory", 4, new("http://contoso.com"), true);
    }
}
