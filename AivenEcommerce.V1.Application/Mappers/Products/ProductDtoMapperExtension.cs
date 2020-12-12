using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Products;

namespace AivenEcommerce.V1.Application.Mappers.Products
{
    public static class ProductDtoMapperExtension
    {

        public static ProductDto ConvertToDto(this Product source)
        {
            return new(source.Id,
                source.Name,
                source.Cost,
                (int)source.Price,
                source.PercentageOff,
                source.Category,
                source.SubCategory,
                source.Stock,
                source.Thumbnail,
                source.IsActive);
        }

        public static Product ConvertToEntity(this ProductDto source)
        {
            return new()
            {
                Id = source.Id,
                IsActive = source.IsActive,
                PercentageOff = source.PercentageOff,
                Stock = source.Stock,
                Category = source.Category,
                Cost = source.Cost,
                Name = source.Name,
                Price = source.Price,
                SubCategory = source.SubCategory,
                Thumbnail = source.Thumbnail
            };
        }
    }
}
