using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Options.Mongo;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;

using System.Collections.Generic;
using System.Linq;

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
    }
}
