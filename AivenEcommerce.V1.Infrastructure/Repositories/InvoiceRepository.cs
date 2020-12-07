using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;
using AivenEcommerce.V1.Modules.GitHub.Options;
using AivenEcommerce.V1.Modules.GitHub.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class InvoiceRepository : GitHubRepository<Invoice, Guid>, IInvoiceRepository
    {
        public InvoiceRepository(IGitHubService githubService, IGitHubOptions options) : base(githubService, options.InvoiceRepositoryId, "invoices")
        {
        }

        public Task<Invoice> GetInvoiceByOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
