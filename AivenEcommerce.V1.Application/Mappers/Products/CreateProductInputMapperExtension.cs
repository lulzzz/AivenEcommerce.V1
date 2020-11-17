
using AivenEcommerce.V1.Domain.Dtos.Products;
using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Application.Mappers.Products
{
    public static class CreateProductInputMapperExtension
    {
        public static Product ConvertToEntity(this CreateProductInput source)
        {
            return new()
            {
                Category = source.Category,
                Cost = source.Cost,
                Name = source.Name,
                Price = source.Price,
                SubCategory = source.SubCategory,
            };
        }
    }
}
