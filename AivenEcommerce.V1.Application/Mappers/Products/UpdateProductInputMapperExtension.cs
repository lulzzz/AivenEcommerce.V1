
using AivenEcommerce.V1.Domain.Dtos.Products;
using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Application.Mappers.Products
{
    public static class UpdateProductInputMapperExtension
    {
        public static Product ConvertToEntity(this UpdateProductInput source)
        {
            return new Product
            {
                Id = source.Id,
                Category = source.Category,
                Cost = source.Cost,
                Name = source.Name,
                Price = source.Price,
                SubCategory = source.SubCategory,
                PercentageOff = source.PercentageOff,
                Thumbnail = source.Thumbnail,
            };
        }

        public static Product ConvertToEntity(this UpdateProductInput source, Product destination)
        {
            destination.Id = source.Id;
            destination.Category = source.Category;
            destination.Cost = source.Cost;
            destination.Name = source.Name;
            destination.PercentageOff = source.PercentageOff;
            destination.Price = source.Price;
            destination.SubCategory = source.SubCategory;
            destination.Thumbnail = source.Thumbnail;

            return destination;

        }
    }
}
