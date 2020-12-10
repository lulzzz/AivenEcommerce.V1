using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.WishLists;

namespace AivenEcommerce.V1.Application.Mappers.WishLists
{
    public static class WishListDtoMappingExtensions
    {
        public static WishListDto ConvertToDto(this WishList source)
        {
            return new(source.Id, source.CustomerEmail, source.Products);
        }
    }
}
