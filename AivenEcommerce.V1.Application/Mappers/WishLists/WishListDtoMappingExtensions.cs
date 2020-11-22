
using AivenEcommerce.V1.Domain.Dtos.WishLists;
using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Application.Mappers.WishLists
{
    public static class WishListDtoMappingExtensions
    {
        public static WishListDto ConvertToDto(this WishList source)
        {
            return new(source.Id, source.CustomerEmail, source.Products);
        }

        public static WishList ConvertToEntity(this WishListDto source)
        {
            return new()
            {
                Id = source.Id,
                Products = source.Products,
                CustomerEmail = source.CustomerEmail
            };
        }
    }
}
