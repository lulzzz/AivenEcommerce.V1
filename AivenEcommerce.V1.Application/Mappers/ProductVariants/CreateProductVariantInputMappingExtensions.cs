using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductVariants;

namespace AivenEcommerce.V1.Application.Mappers.ProductVariants
{
    public static class CreateProductVariantInputMappingExtensions
    {
        public static ProductVariant ConvertToEntity(this CreateProductVariantInput source)
        {
            return new()
            {
                Name = source.Name,
                Values = source.Values,
                ProductId = source.ProductId
            };
        }
    }
}
