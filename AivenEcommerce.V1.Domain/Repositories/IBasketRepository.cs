using System.Collections.Generic;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IBasketRepository
    {
        Task<Basket> GetBasket(int id);
        Task<IEnumerable<Basket>> GetBaskets();
        Task<IEnumerable<Basket>> GetBasketsByUser(User user);
        Task<Basket> GetBasketOpenByUser(User user);
    }
}
