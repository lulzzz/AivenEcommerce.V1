using AivenEcommerce.V1.Application.Mappers.Products;
using AivenEcommerce.V1.Domain.Dtos.Baskets;
using AivenEcommerce.V1.Domain.Entities;

using System.Collections.Generic;
using System.Linq;

namespace AivenEcommerce.V1.Application.Mappers.Baskets
{
    public static class BasketProductsDtoMappingExtenions
    {
        public static BasketProductsDto ConvertToDto(this Basket source, IEnumerable<Product> products)
        {
            return new(source.Id, source.Products, products.Select(x => x.ConvertToDto()), source.CustomerEmail);
        }
    }
}
