using AivenEcommerce.V1.Domain.Entities;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IDeliveryRepository : IRepository<Delivery, Guid>
    {
        Task<Delivery> GetDeliveryAsync(Order order);
    }
}
