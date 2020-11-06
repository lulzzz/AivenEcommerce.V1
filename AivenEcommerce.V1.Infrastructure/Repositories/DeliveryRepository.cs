using System.Linq;

using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Options.Mongo;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class DeliveryRepository : MongoRepository<Delivery>, IDeliveryRepository
    {
        public DeliveryRepository(IMongoDeliveryOptions options) : base(options)
        {
        }

        public Delivery GetDelivery(Order order)
        {
            return base.GetQueryable().Where(x => x.OrderId == order.Id).Single();
        }
    }
}
