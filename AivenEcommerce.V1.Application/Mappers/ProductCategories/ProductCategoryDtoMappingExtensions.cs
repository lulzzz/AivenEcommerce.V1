using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductCategories;

namespace AivenEcommerce.V1.Application.Mappers.ProductCategories
{
    public static class ProductCategoryDtoMappingExtensions
    {
        public static ProductCategoryDto ConvertToDto(this ProductCategory source, int productCount)
        {
            return new(source.Id, source.Name, productCount, source.SubCategories);
        }
    }
}
