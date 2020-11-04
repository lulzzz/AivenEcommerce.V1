using System.Collections.Generic;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IDeliveryRepository
    {
        Task<Delivery> GetDelivery(Order order);
        Task<Delivery> GetDelivery(Basket basket);
        Task<Delivery> GetDelivery(int id);
        Task<IEnumerable<Delivery>> GetDeliveries(User user);
    }
}
