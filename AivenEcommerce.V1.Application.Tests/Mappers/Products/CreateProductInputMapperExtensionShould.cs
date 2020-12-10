using AivenEcommerce.V1.Application.Mappers.Products;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Products;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Mappers.Products
{
    public class CreateProductInputMapperExtensionShould
    {
        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameName()
        {
            CreateProductInput input = MockCreateProductInput();

            Product product = input.ConvertToEntity();

            Assert.Equal(product.Name, input.Name);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameCost()
        {
            CreateProductInput input = MockCreateProductInput();

            Product product = input.ConvertToEntity();

            Assert.Equal(product.Cost, input.Cost);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSamePrice()
        {
            CreateProductInput input = MockCreateProductInput();

            Product product = input.ConvertToEntity();

            Assert.Equal(product.Price, input.Price);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameCategory()
        {
            CreateProductInput input = MockCreateProductInput();

            Product product = input.ConvertToEntity();

            Assert.Equal(product.Category, input.Category);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameSubCategory()
        {
            CreateProductInput input = MockCreateProductInput();

            Product product = input.ConvertToEntity();

            Assert.Equal(product.SubCategory, input.SubCategory);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnIdNull()
        {
            CreateProductInput input = MockCreateProductInput();

            Product product = input.ConvertToEntity();

            Assert.Null(product.Id);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnThumbnailNull()
        {
            CreateProductInput input = MockCreateProductInput();

            Product product = input.ConvertToEntity();

            Assert.Null(product.Thumbnail);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnIsActiveFalse()
        {
            CreateProductInput input = MockCreateProductInput();

            Product product = input.ConvertToEntity();

            Assert.False(product.IsActive);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnPercentageOffIsZero()
        {
            CreateProductInput input = MockCreateProductInput();

            Product product = input.ConvertToEntity();

            Assert.Equal(0, product.PercentageOff);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnStockIsZero()
        {
            CreateProductInput input = MockCreateProductInput();

            Product product = input.ConvertToEntity();

            Assert.Equal(0, product.Stock);
        }

        private CreateProductInput MockCreateProductInput() =>
            new("Name", "Description", 1, 2, "Category", "SubCategory");
    }
}
