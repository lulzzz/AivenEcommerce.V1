using System;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, Guid>
    {
        Task<Customer> GetCustomer(string email);
    }
}
