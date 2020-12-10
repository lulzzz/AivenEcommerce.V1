using AivenEcommerce.V1.Application.Mappers.ProductCategories;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductCategories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Mappers.ProductCategories
{
    public class ProductCategoryDtoMappingExtensionsShould
    {
        [Fact]
        public void ConvertToDto_ProductCategoryNotNull_ReturnSameId()
        {
            ProductCategory category = MockProductCategory();

            ProductCategoryDto categoryDto = category.ConvertToDto(5);

            Assert.Equal(category.Id, categoryDto.Id);
        }

        [Fact]
        public void ConvertToDto_ProductCategoryNotNull_ReturnSameProductCount()
        {
            ProductCategory category = MockProductCategory();

            ProductCategoryDto categoryDto = category.ConvertToDto(5);

            Assert.Equal(5, categoryDto.ProductCount);
        }

        [Fact]
        public void ConvertToDto_ProductCategoryNotNull_ReturnSameName()
        {
            ProductCategory category = MockProductCategory();

            ProductCategoryDto categoryDto = category.ConvertToDto(5);

            Assert.Equal(category.Name, categoryDto.Name);
        }

        [Fact]
        public void ConvertToDto_ProductCategoryNotNull_ReturnSameSubCategories()
        {
            ProductCategory category = MockProductCategory();

            ProductCategoryDto categoryDto = category.ConvertToDto(5);

            Assert.Equal(category.SubCategories, categoryDto.SubCategories);
        }

        private ProductCategory MockProductCategory() =>
            new()
            {
                Id = Guid.NewGuid(),
                Name = nameof(ProductCategory.Name),
                SubCategories = new[] { "a" }
            };
    }
}
