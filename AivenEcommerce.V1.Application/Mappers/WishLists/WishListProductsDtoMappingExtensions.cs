using AivenEcommerce.V1.Application.Mappers.Products;
using AivenEcommerce.V1.Domain.Dtos.WishLists;
using AivenEcommerce.V1.Domain.Entities;

using System.Collections.Generic;
using System.Linq;

namespace AivenEcommerce.V1.Application.Mappers.WishLists
{
    public static class WishListProductsDtoMappingExtensions
    {
        public static WishListProductsDto ConvertToDto(this WishList source, IEnumerable<Product> products)
        {
            return new(source.Id, source.CustomerEmail, products.Select(x => x.ConvertToDto()));
        }
    }
}
