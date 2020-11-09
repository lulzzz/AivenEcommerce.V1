using System.Collections.Generic;

using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IBasketRepository : IRepository<Basket, string>
    {
        IEnumerable<Basket> GetBasketsByUser(User user);
        Basket GetBasketOpenByUser(User user);
    }
}
