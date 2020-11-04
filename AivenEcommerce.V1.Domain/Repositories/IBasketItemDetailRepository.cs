using System.Collections.Generic;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IBasketItemDetailRepository
    {
        Task<IEnumerable<BasketItemDetail>> GetBasketItems(BasketItem basket);
        Task<BasketItemDetail> GetBasketItem(int id);
    }
}
