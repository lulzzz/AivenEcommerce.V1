using System.Collections.Generic;

using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IBasketItemRepository : IRepository<BasketItem, string>
    {
        IEnumerable<BasketItem> GetBasketItems(Basket basket);
        IEnumerable<BasketItem> GetBasketItems(Basket basket, Product product);
    }
}
