using AivenEcommerce.V1.Application.Mappers.Products;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Products;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Mappers.Products
{
    public class UpdateProductInputMapperExtensionShould
    {
        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameName()
        {
            UpdateProductInput input = MockUpdateProductInput();

            Product product = input.ConvertToEntity();

            Assert.Equal(product.Name, input.Name);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameCost()
        {
            UpdateProductInput input = MockUpdateProductInput();

            Product product = input.ConvertToEntity();

            Assert.Equal(product.Cost, input.Cost);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSamePrice()
        {
            UpdateProductInput input = MockUpdateProductInput();

            Product product = input.ConvertToEntity();

            Assert.Equal(product.Price, input.Price);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameCategory()
        {
            UpdateProductInput input = MockUpdateProductInput();

            Product product = input.ConvertToEntity();

            Assert.Equal(product.Category, input.Category);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameSubCategory()
        {
            UpdateProductInput input = MockUpdateProductInput();

            Product product = input.ConvertToEntity();

            Assert.Equal(product.SubCategory, input.SubCategory);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameId()
        {
            UpdateProductInput input = MockUpdateProductInput();

            Product product = input.ConvertToEntity();

            Assert.Equal(product.Id, input.Id);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameThumbnail()
        {
            UpdateProductInput input = MockUpdateProductInput();

            Product product = input.ConvertToEntity();

            Assert.Equal(product.Thumbnail, input.Thumbnail);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnIsActiveFalse()
        {
            UpdateProductInput input = MockUpdateProductInput();

            Product product = input.ConvertToEntity();

            Assert.False(product.IsActive);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnPercentageOffIsZero()
        {
            UpdateProductInput input = MockUpdateProductInput();

            Product product = input.ConvertToEntity();

            Assert.Equal(product.PercentageOff, input.PercentageOff);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnStockIsZero()
        {
            UpdateProductInput input = MockUpdateProductInput();

            Product product = input.ConvertToEntity();

            Assert.Equal(0, product.Stock);
        }

        private UpdateProductInput MockUpdateProductInput() =>
            new("1", "Name", 1, 2, 3, "Category", "SubCategory", new("http://contoso.com"));
    }
}
