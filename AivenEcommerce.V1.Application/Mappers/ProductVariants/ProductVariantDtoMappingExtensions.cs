using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductVariants;

namespace AivenEcommerce.V1.Application.Mappers.ProductVariants
{
    public static class ProductVariantDtoMappingExtensions
    {
        public static ProductVariantDto ConvertToDto(this ProductVariant source)
        {
            return new(source.Id, source.ProductId, source.Name, source.Values);
        }
    }
}
