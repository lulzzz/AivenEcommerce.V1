
using AivenEcommerce.V1.Domain.Dtos.ProductCategories;
using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Application.Mappers.ProductCategories
{
    public static class ProductCategoryDtoMappingExtensions
    {
        public static ProductCategoryDto ConvertToDto(this ProductCategory source, int productCount)
        {
            return new(source.Id, source.Name, productCount, source.SubCategories);
        }

        public static ProductCategory ConvertToEntity(this ProductCategoryDto source)
        {
            return new()
            {
                Id = source.Id,
                Name = source.Name,
                SubCategories = source.SubCategories
            };
        }
    }
}
