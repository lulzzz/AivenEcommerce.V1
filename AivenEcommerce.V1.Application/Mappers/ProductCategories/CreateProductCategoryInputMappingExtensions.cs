using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductCategories;

namespace AivenEcommerce.V1.Application.Mappers.ProductCategories
{
    public static class CreateProductCategoryInputMappingExtensions
    {
        public static ProductCategory ConvertToEntity(this CreateProductCategoryInput source)
        {
            return new()
            {
                Name = source.Name,
                SubCategories = source.SubCategories
            };
        }
    }
}
