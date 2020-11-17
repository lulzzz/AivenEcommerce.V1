
using AivenEcommerce.V1.Domain.Dtos.ProductImages;
using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Application.Mappers.ProductImages
{
    public static class ProductImageDtoMapperExtensions
    {
        public static ProductImageDto ConvertToDto(this ProductImage source)
        {
            return new(source.Id, source.ProductId, source.Image);
        }

        public static ProductImage ConvertToEntity(this ProductImageDto source)
        {
            return new()
            {
                Id = source.Id,
                Image = source.Image,
                ProductId = source.ProductId
            };
        }
    }
}
