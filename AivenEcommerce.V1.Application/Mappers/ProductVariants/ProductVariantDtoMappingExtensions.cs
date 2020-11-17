
using AivenEcommerce.V1.Domain.Dtos.ProductVariants;
using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Application.Mappers.ProductVariants
{
    public static class ProductVariantDtoMappingExtensions
    {
        public static ProductVariantDto ConvertToDto(this ProductVariant source)
        {
            return new(source.Id, source.ProductId, source.Name, source.Values);
        }

        public static ProductVariant ConvertToEntity(this ProductVariantDto source)
        {
            return new()
            {
                Id = source.Id,
                ProductId = source.ProductId,
                Name = source.Name,
                Values = source.Values
            };
        }
    }
}
