using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Baskets;

namespace AivenEcommerce.V1.Application.Mappers.Baskets
{
    public static class BasketDtoMappingExtensions
    {
        public static BasketDto ConvertToDto(this Basket source)
        {
            return new(source.Id, source.Products, source.CustomerEmail);
        }
    }
}
