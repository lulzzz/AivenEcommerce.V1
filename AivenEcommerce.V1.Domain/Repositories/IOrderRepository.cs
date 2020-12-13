
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Orders;
using AivenEcommerce.V1.Domain.Shared.Paginations;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order, string>
    {
        IEnumerable<Order> GetOrdersByUser(User user);
        Task<PagedData<Order>> GetAllAsync(OrderParameters parameters);
    }
}
