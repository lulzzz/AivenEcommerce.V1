
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order GetOrder(Basket basket);
        IEnumerable<Order> GetOrdersByUser(User user);
        Task<Order> UpdateLinkAsync(Order order, Uri uri);


    }
}
