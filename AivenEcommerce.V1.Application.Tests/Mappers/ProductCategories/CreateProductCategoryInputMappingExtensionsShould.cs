using AivenEcommerce.V1.Application.Mappers.ProductCategories;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductCategories;

using System;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Mappers.ProductCategories
{
    public class CreateProductCategoryInputMappingExtensionsShould
    {
        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnEmplyId()
        {
            CreateProductCategoryInput input = MockCreateProductCategoryInput();

            ProductCategory category = input.ConvertToEntity();

            Assert.Equal(Guid.Empty, category.Id);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameName()
        {
            CreateProductCategoryInput input = MockCreateProductCategoryInput();

            ProductCategory category = input.ConvertToEntity();

            Assert.Equal(input.Name, category.Name);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameSubCategories()
        {
            CreateProductCategoryInput input = MockCreateProductCategoryInput();

            ProductCategory category = input.ConvertToEntity();

            Assert.Equal(input.SubCategories, category.SubCategories);
        }

        private CreateProductCategoryInput MockCreateProductCategoryInput() =>
        new(nameof(CreateProductCategoryInput.Name), new[] { "a" });


    }
}
