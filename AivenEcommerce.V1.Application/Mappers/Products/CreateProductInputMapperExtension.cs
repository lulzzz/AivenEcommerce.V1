
using AivenEcommerce.V1.Domain.Dtos.Products;
using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Application.Mappers.Products
{
    public static class CreateProductInputMapperExtension
    {
        public static Product ConvertToEntity(this CreateProductInput source)
        {
            return new Product
            {
                Description = source.Description,
                Category = source.Category,
                Cost = source.Cost,
                Name = source.Name,
                Price = source.Price,
                PercentageOff = source.PercentageOff,
                SubCategory = source.SubCategory,
                Thumbnail = source.Thumbnail
            };
        }

        public static Product ConvertToEntity(this CreateProductInput source, Product destination)
        {

            destination.Description = source.Description;
            destination.Category = source.Category;
            destination.Cost = source.Cost;
            destination.Name = source.Name;
            destination.Price = source.Price;
            destination.PercentageOff = source.PercentageOff;
            destination.SubCategory = source.SubCategory;
            destination.Thumbnail = source.Thumbnail;

            return destination;

        }
    }
}
