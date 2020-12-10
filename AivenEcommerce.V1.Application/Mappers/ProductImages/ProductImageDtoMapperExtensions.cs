using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductImages;

namespace AivenEcommerce.V1.Application.Mappers.ProductImages
{
    public static class ProductImageDtoMapperExtensions
    {
        public static ProductImageDto ConvertToDto(this ProductImage source)
        {
            return new(source.Id, source.ProductId, source.Image);
        }
    }
}
