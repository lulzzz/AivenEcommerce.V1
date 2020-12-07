using AivenEcommerce.V1.Domain.Entities;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IInvoiceRepository : IRepository<Invoice, Guid>
    {
        Task<Invoice> GetInvoiceByOrderAsync(Order order);
    }
}
