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
                Category = source.Category,
                Cost = source.Cost,
                Name = source.Name,
                Price = source.Price,
                SubCategory = source.SubCategory,
                Thumbnail = source.Thumbnail
            };
        }

        public static Product ConvertToEntity(this ProductDto source, Product destination)
        {
            destination.Id = source.Id;
            destination.IsActive = source.IsActive;
            destination.PercentageOff = source.PercentageOff;
            destination.Stock = source.Stock;
            destination.Category = source.Category;
            destination.Cost = source.Cost;
            destination.Name = source.Name;
            destination.Price = source.Price;
            destination.SubCategory = source.SubCategory;
            destination.Thumbnail = source.Thumbnail;

            return destination;
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
