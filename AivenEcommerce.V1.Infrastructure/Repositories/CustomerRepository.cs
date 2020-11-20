using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Extensions;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;
using AivenEcommerce.V1.Modules.GitHub.Options;
using AivenEcommerce.V1.Modules.GitHub.Services;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class CustomerRepository : GitHubRepository<Customer, Guid>, ICustomerRepository
    {
        const string PATH = "customers";

        private readonly IGitHubOptions _options;
        public CustomerRepository(IGitHubOptions options, IGitHubService githubService) : base(githubService)
        {
            _options = options;
        }

        public async Task<Customer> GetCustomer(string email)
        {
            var fileContent = await base._githubService.GetFileContentAsync(_options.CustomerRepositoryId, PATH, email);

            if (fileContent is null)
            {
                return null;
            }

            return fileContent.Content.Deserialize<Customer>();
        }

        public override async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var files = await base._githubService.GetAllFilesWithContentAsync(_options.CustomerRepositoryId, PATH);

            if (files is null)
            {
                return Enumerable.Empty<Customer>();
            }

            return files.Select(x => x.Content.Deserialize<Customer>());
        }

        public override async Task<Customer> CreateAsync(Customer entity)
        {
            entity.Id = Guid.NewGuid();
            await base._githubService.CreateFileAsync(_options.CustomerRepositoryId, PATH, entity.Email, entity.Serialize());

            return entity;
        }

        public override Task UpdateAsync(Customer entityIn)
        {
            return base._githubService.UpdateFileAsync(_options.CustomerRepositoryId, PATH, entityIn.Email, entityIn.Serialize());
        }

        public override Task RemoveAsync(Customer entityIn)
        {
            return base._githubService.DeleteFileAsync(_options.CustomerRepositoryId, PATH, entityIn.Email);
        }
    }
}
