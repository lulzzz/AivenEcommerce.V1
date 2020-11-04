
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrder(int id);
        Task<Order> GetOrder(Basket basket);
        Task<IEnumerable<Order>> GetOrders();
        Task<IEnumerable<Order>> GetOrdersByUser(User user);
        Task<Order> UpdateLink(Order order, Uri uri);


    }
}
