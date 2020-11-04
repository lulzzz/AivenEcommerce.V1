using System.Collections.Generic;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IBasketItemRepository
    {
        Task<IEnumerable<BasketItem>> GetBasketItems(Basket basket);
        Task<BasketItem> GetBasketItem(int id);
        Task<IEnumerable<BasketItem>> GetBasketItems(Basket basket, Product product);
    }
}
