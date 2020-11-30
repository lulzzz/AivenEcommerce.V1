using AivenEcommerce.V1.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IAddressRepository : IRepository<Address, Guid>
    {
        Task<IEnumerable<Address>> GetByCustomerAsync(string customer);
    }
}
