
using AivenEcommerce.V1.Domain.Dtos.ProductCategories;
using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Application.Mappers.ProductCategories
{
    public static class UpdateProductCategoryInputMappingExtensions
    {
        public static ProductCategory ConvertToEntity(this UpdateProductCategoryInput source)
        {
            return new ProductCategory
            {
                Name = source.Name,
                SubCategories = source.SubCategories,
                Id = source.Id
            };
        }
    }
}
