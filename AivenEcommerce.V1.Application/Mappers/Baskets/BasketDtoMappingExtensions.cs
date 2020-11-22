
using AivenEcommerce.V1.Domain.Dtos.Baskets;
using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Application.Mappers.Baskets
{
    public static class BasketDtoMappingExtensions
    {
        public static BasketDto ConvertToDto(this Basket source)
        {
            return new(source.Id, source.Products, source.CustomerEmail);
        }

        public static Basket ConvertToEntity(this BasketDto source)
        {
            return new()
            {
                Id = source.Id,
                Products = source.ProductDefinitives,
                CustomerEmail = source.CustomerEmail
            };
        }
    }
}
