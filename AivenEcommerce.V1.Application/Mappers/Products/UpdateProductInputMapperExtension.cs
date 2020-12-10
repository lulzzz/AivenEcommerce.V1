using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Products;

namespace AivenEcommerce.V1.Application.Mappers.Products
{
    public static class UpdateProductInputMapperExtension
    {
        public static Product ConvertToEntity(this UpdateProductInput source)
        {
            return new()
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
    }
}
