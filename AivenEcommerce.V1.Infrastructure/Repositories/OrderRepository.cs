using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Options.Mongo;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class OrderRepository : MongoRepository<Order>, IOrderRepository
    {
        public OrderRepository(IMongoOrderOptions options) : base(options)
        {
        }

        public IEnumerable<Order> GetOrdersByUser(User user)
        {
            //TODO: fieohfoi
            return base.GetQueryable().Where(x => true).ToList();
        }

        public async Task<Order> UpdateLinkAsync(Order order, Uri uri)
        {
            order = await base.GetAsync(order.Id);

            order.Link = uri;

            await base.UpdateAsync(order);

            return order;
        }
    }
}
