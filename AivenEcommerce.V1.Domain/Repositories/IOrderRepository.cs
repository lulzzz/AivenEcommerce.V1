
using AivenEcommerce.V1.Domain.Entities;

using System.Collections.Generic;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order, string>
    {
        IEnumerable<Order> GetOrdersByUser(User user);
    }
}
