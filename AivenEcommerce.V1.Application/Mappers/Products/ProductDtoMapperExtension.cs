using AivenEcommerce.V1.Domain.Dtos.Products;
using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Application.Mappers.Products
{
    public static class ProductDtoMapperExtension
    {

        public static ProductDto ConvertToDto(this Product source)
        {
            return new ProductDto(source.Id,
                source.Name,
                source.Description,
                source.Cost,
                source.Price,
                source.PercentageOff,
                source.Category,
                source.SubCategory,
                source.Stock,
                source.Thumbnail,
                source.IsActive);
        }

        public static ProductDto ConvertToDto(this Product source, ProductDto destination)
        {
            return destination with
            {
                Id = source.Id,
                IsActive = source.IsActive,
                PercentageOff = source.PercentageOff,
                Stock = source.Stock,
                Description = source.Description,
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
