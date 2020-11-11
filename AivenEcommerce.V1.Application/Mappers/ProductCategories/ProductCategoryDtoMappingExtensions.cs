
using AivenEcommerce.V1.Domain.Dtos.ProductCategories;
using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Application.Mappers.ProductCategories
{
    public static class ProductCategoryDtoMappingExtensions
    {
        public static ProductCategoryDto ConvertToDto(this ProductCategory source)
        {
            return new ProductCategoryDto(source.Id, source.Name, source.SubCategories);
        }

        public static ProductCategory ConvertToEntity(this ProductCategoryDto source)
        {
            return new ProductCategory
            {
                Id = source.Id,
                Name = source.Name,
                SubCategories = source.SubCategories
            };
        }
    }
}
