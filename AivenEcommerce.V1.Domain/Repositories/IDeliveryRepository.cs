
using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IDeliveryRepository : IRepository<Delivery>
    {
        Delivery GetDelivery(Order order);
    }
}
