using AivenEcommerce.V1.Domain.Entities;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, Guid>
    {
        Task<Customer> GetCustomer(string email);
    }
}
